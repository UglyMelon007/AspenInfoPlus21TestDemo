using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace RTDB.IP21
{
    public class Connection
    {
        public static string serverIp;
        private int serverIndex;
        public static string serverGroup = "200";
        private static Dictionary<string, int> serverPool = new Dictionary<string, int>();
        public static string currentServerIp;

        public int ServerIndex
        {
            get { return this.serverIndex; }
            set { this.serverIndex = value; }
        }

        public Connection(string serverIp)
        {
            Connection.serverIp = serverIp;
        }

        public static Connection OpenConnection(string serverIp)
        {
            Connection conn = new Connection(serverIp);

            conn.Init();

            return conn;
        }

        public void Close()
        {
            serverPool.Remove(serverIp);
            if (serverPool.Count == 0)
                infoplus21_api.DaShutdown();
        }

        private void Init()
        {
            infoplus21_api.ERRBLOCK errMsg; // = new infoplus21_api.ERRBLOCK();
            if (!serverPool.ContainsKey(serverIp))
            {
                //TDaBoolean DaInitialize(TDaBoolean ReadConfigFile);
                int res = infoplus21_api.DaInitialize(infoplus21_api.C_FALSE);
                if (res != infoplus21_api.C_TRUE)
                {
                    throw new IP21Exception("failed to initialize da without reading config file");
                }

                string connectString = serverIp + " " + serverGroup + " " + "/FATAL";
                Util.writestr(connectString);
                byte[] bpara = Encoding.Default.GetBytes(connectString);
                this.serverIndex = infoplus21_api.DaAddServer(bpara, out errMsg);

                Util.CheckResult(errMsg);
                serverPool.Add(serverIp, serverIndex);
            }
            else
            {
                infoplus21_api.DaSelectServer((int)serverPool[serverIp], out errMsg);
                Util.CheckResult(errMsg);
            }
            currentServerIp = serverIp;
        }

        public static bool SwitchToServerIp()
        {
            if (!currentServerIp.Equals(serverIp))
            {
                Connection conn = new Connection(serverIp);
                conn.Init();
            }
            return true;
        }

        // 从历史数据库取实际值
        public double GetActuals(string tag, DateTime time, int span)
        {
            return HistoryReader.ReadActuals(tag, time, span);
        }

        // 从历史数据库取插值
        public double GetInterpolated(string tag, DateTime time)
        {
            return HistoryReader.ReadInterpolated(tag, time);
        }

        // 从历史数据库取时间和实际值
        public IList<TimeValuePair> ListActuals(string tag, DateTime time, int span)
        {
            return HistoryReader.ListAllHistory(infoplus21_api.H21_GET_ACTUALS, tag, time, span);
        }

        // 从历史数据库取时间和实际值
        public IList<TimeValuePair> ListInterpolated(string tag, DateTime time, int span)
        {
            return HistoryReader.ListAllHistory(infoplus21_api.H21_GET_TIMES, tag, time, span);
        }




        // 从实时数据库取数
        public string GetStringValue(string tag, string property)
        {
            infoplus21_api.ERRBLOCK errMsg;

            byte[] des = new byte[200];

            infoplus21_api.DB2CHBF(Util.GetRecordId(tag), Util.GetFieldTag(property), out des, (short)des.Length, out errMsg);
            Util.CheckResult(errMsg);

            return des.ToString();
        }

        // 从实时数据库取数
        public float GetNumValue(string tag, string property)
        {
            infoplus21_api.ERRBLOCK errMsg;
            int recid = Util.GetRecordId(tag);

            float value = 0;
            infoplus21_api.DB2REAL(recid, Util.GetFieldTag(property), out value, out errMsg);
            Util.CheckResult(errMsg);

            return value;
        }

        // 直接读取实时值
        public Single GetValue(string tag)
        {
            return GetNumValue(tag, "IP_VALUE");
        }
        /// <summary>
        /// 返回输入点位号列表当前值的键值对
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetCurrentData(IList tags)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (string tag in tags)
            {
                if (!result.ContainsKey(tag))
                {
                    double value = GetNumValue(tag, "IP_VALUE");
                    result.Add(tag, value.ToString());
                }
            }
            return result;
        }

        public double GetAveragedData(string tag, DateTime timeOld, DateTime timeNew)
        {
            serverIp = Util.getAspenIPfromPointReg(tag);
            SwitchToServerIp();
            return AggreationReader.GetAggreagedData(tag, timeOld, timeNew, infoplus21_api.AG_DBL_AVG);
        }
        public double GetAveragedDataSTD(string tag, DateTime timeOld, DateTime timeNew)
        {
            return AggreationReader.GetAggreagedData(tag, timeOld, timeNew, infoplus21_api.AG_DBL_AVG);
        }

        public double GetAveragedDataFor(string tag, DateTime timeOld, DateTime timeNew)
        {
            return AggreationReader.GetAggreagedData(tag, timeOld, timeNew, infoplus21_api.AG_DBL_AVG);
        }

        public Dictionary<string, string> GetAveragedData(IList tags, DateTime timeOld, DateTime timeNew)
        {
            return AggreationReader.GetAggreagedData(tags, timeOld, timeNew, infoplus21_api.AG_DBL_AVG);
        }
        public Dictionary<string, string> GetAveragedSTD(IList tags, DateTime timeOld, DateTime timeNew)
        {
            return AggreationReader.GetAggreagedData(tags, timeOld, timeNew, infoplus21_api.AG_DBL_STD);
        }

        public IList<TimeAggreationPair> GetMaxMinAvgAtOnce(string tag, DateTime timeOld, DateTime timeNew, int interval)
        {
            return AggreationReader.GetMaxMinAvgAtOnce(tag, timeOld, timeNew, interval);
        }

        public IList<TimeValuePair> GetAveragedData(string tag, DateTime timeOld, DateTime timeNew, int interval)
        {
            return AggreationReader.GetAggreagedData(tag, timeOld, timeNew, interval, infoplus21_api.AG_DBL_AVG);
        }

        public IList<TimeValuePair> GetCurrentData(string tag, DateTime timeOld, DateTime timeNew, int interval)
        {
            return HistoryReader.ListHistoryDataByInterval(tag, timeOld, timeNew, interval);
        }
        public IList<TimeValuePair> GetCurrentData(string tag, DateTime timeOld, DateTime timeNew, string interval)
        {
            return HistoryReader.ListHistoryDataByInterval(tag, timeOld, timeNew, interval);
        }

        public double GetMaxData(string tag, DateTime timeOld, DateTime timeNew)
        {
            return AggreationReader.GetAggreagedData(tag, timeOld, timeNew, infoplus21_api.AG_DBL_MAX);
        }

        public double GetMinData(string tag, DateTime timeOld, DateTime timeNew)
        {
            return AggreationReader.GetAggreagedData(tag, timeOld, timeNew, infoplus21_api.AG_DBL_MIN);
        }

        public double GetDailyAverage(string tag, DateTime date)
        {
            return AggreationReader.GetDailyAverage(tag, date);
        }

        public double GetDailyMax(string tag, DateTime date)
        {
            return AggreationReader.GetDailyMax(tag, date);
        }

        public double GetDailyMin(string tag, DateTime date)
        {
            return AggreationReader.GetDailyMin(tag, date);
        }


        public double ASCIIDB2I(string tagName)
        {
            int recid;
            int indata;
            string fieldId = "IP_VALUE";
            byte[] tagbyte = Encoding.Default.GetBytes(tagName.Trim());
            infoplus21_api.ERRBLOCK errMsg;
            infoplus21_api.DECODNAM(tagbyte, (short)tagName.Length, out recid, out errMsg);
            Util.CheckResult(errMsg);
            tagbyte = Encoding.Default.GetBytes(fieldId.Trim());
            infoplus21_api.ASCIIDB2I(recid, Util.GetFieldTag(fieldId), tagbyte, (short)tagbyte.Length, out indata, out errMsg);
            Util.CheckResult(errMsg);
            return indata;
        }

        public void CHBF2DB(string data)
        {
            int recid = 11111111;
            byte[] tagbyte = Encoding.Default.GetBytes(data.Trim());
            infoplus21_api.ERRBLOCK errMsg;
            infoplus21_api.CHBF2DB(recid, 3, tagbyte, (short)tagbyte.Length, out errMsg);
            Util.CheckResult(errMsg);
        }

        public void CHKFREE(string tagName)
        {
            infoplus21_api.ERRBLOCK errMsg;
            infoplus21_api.CHKFREE(Util.GetRecordId(tagName), out errMsg);
            Util.CheckResult(errMsg);
        }

        public void CHKFTREC(string tagName)
        {
            infoplus21_api.ERRBLOCK errMsg;
            infoplus21_api.CHKFTREC(Util.GetFieldTag("Value"), Util.GetRecordId(tagName), out errMsg);
            Util.CheckResult(errMsg);
        }

        public void COPYREC(string tagName)
        {
            infoplus21_api.ERRBLOCK errMsg;
            int newId = 123456;
            string newTagName = "helloapitest";
            byte[] ptname = Encoding.Default.GetBytes(newTagName.Trim());
            Util.GetRecordId(newTagName);
            infoplus21_api.COPYREC(Util.GetRecordId(tagName), newId, 0, ptname, (short)ptname.Length, out errMsg);
            Util.CheckResult(errMsg);
        }

        public void CREATEREC(string recordName)
        {
            infoplus21_api.ERRBLOCK errMsg;
            int recordId = 123456;
            int definitionId = 123456;
            byte[] ptname = Encoding.Default.GetBytes(recordName.Trim());
            infoplus21_api.CREATEREC(recordId, definitionId, ptname, (short)ptname.Length, out errMsg);
            Util.CheckResult(errMsg);
        }

        public void D2ASCIIDB(int recid, int ft, double realData)
        {
            infoplus21_api.ERRBLOCK errMsg;
            byte[] ptbuff;
            short numchars;
            infoplus21_api.D2ASCIIDB(recid, ft, ref realData, out ptbuff, 1000, out numchars, out errMsg);
            Util.CheckResult(errMsg);
        }

        public void DATA2ASCII(string ptdata)
        {
            infoplus21_api.ERRBLOCK errMsg;
            byte[] ptbuff;
            short numchars;
            infoplus21_api.DATA2ASCII(ptdata, 1000, 2, 0, out ptbuff, 2000, out numchars, out errMsg);
            Util.CheckResult(errMsg);
        }

        public double DB2DUBL(string recordName, string filedName)
        {
            infoplus21_api.ERRBLOCK errMsg;
            double dubldata;
            infoplus21_api.DB2DUBL(Util.GetRecordId(recordName), Util.GetFieldTag(filedName), out dubldata, out errMsg);
            Util.CheckResult(errMsg);
            return dubldata;
        }

        public void DB2IDFT(string recordName, string fieldName)
        {
            infoplus21_api.ERRBLOCK errMsg;
            infoplus21_api.IDANDFT idandft;
            int recid = Util.GetRecordId(recordName);
            int ft = Util.GetFieldTag(fieldName);
            infoplus21_api.DB2IDFT(recid, ft, out idandft, out errMsg);
            Util.CheckResult(errMsg);
        }

        public void DB2LONG(string recordName, string fieldName)
        {
            infoplus21_api.ERRBLOCK errMsg;
            int intdata;
            int recid = Util.GetRecordId(recordName);
            int ft = Util.GetFieldTag(fieldName);
            infoplus21_api.DB2LONG(recid, ft, out intdata, out errMsg);
        }

        public void DB2REID(string recordName, string fieldName)
        {

        }

        public void DECODRAF(string recordName, string fieldName)
        {
            infoplus21_api.ERRBLOCK errMsg;
            //byte[][] ptbuff = new byte[2][];
            //ptbuff[0] = Encoding.Default.GetBytes(recordName.Trim());
            //ptbuff[1] = Encoding.Default.GetBytes(fieldName.Trim());
            //byte[] ptbuff = Encoding.Default.GetBytes(recordName.Trim() + fieldName.Trim()); //Encoding.Default.GetBytes(fieldName.Trim());
            infoplus21_api.NAMFTARR ptbuff = new infoplus21_api.NAMFTARR
            {
                RecordName = Encoding.Default.GetBytes(recordName.Trim()),
                FieldName = Encoding.Default.GetBytes(fieldName.Trim())
            };

            int recid;
            int ft;
            infoplus21_api.DECODRAF(ref ptbuff, 1000, out recid, out ft, out errMsg);
            Util.CheckResult(errMsg);
        }

        public void GETNMFDB(string recordName, string fieldName)
        {

            int recid = Util.GetRecordId(recordName);
            int ft = Util.GetFieldTag(fieldName);
            infoplus21_api.NAMFTARR obj;
            short numchars;
            infoplus21_api.GETNMFDB(recid, ft, out obj, out numchars);
        }

        public int DEFINID(string recordName)
        {
            int recid = Util.GetRecordId(recordName);
            return infoplus21_api.DEFINID(recid);
        }

        public void DELETREC(string recordName)
        {
            infoplus21_api.ERRBLOCK errMsg;
            int recid = Util.GetRecordId(recordName);
            infoplus21_api.DELETREC(recid, out errMsg);
            Util.CheckResult(errMsg);
        }

        public infoplus21_api.XTSBLOCK DSPDT2XTS(int day, int time)
        {
            infoplus21_api.XTSBLOCK xts;
            infoplus21_api.DSPDT2XTS(day, time, out xts);
            return xts;
        }
    }
}