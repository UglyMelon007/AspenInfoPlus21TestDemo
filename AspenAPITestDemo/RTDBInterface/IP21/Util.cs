using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.IO;

namespace RTDB.IP21
{
    internal static class Util
    {
        public static infoplus21_api.XUSTS Time2XUSTS(DateTime time)
        {
            DateTime s = new DateTime(1970, 1, 1);
            s = s.AddHours(8);

            infoplus21_api.XUSTS result = new infoplus21_api.XUSTS();
            result.secs = (int)(time - s).TotalSeconds;
            result.usecs = 300000;
            return result;
        }

        public static DateTime XUTS2Time(infoplus21_api.XUSTS xuts)
        {
            DateTime time = new DateTime(1970, 1, 1);
            time = time.AddHours(8);

            //增加跨度
            time = time.AddSeconds(xuts.secs);
            return time;
        }

        public static DateTime XTSBLOCK2Time(infoplus21_api.XTSBLOCK xts)
        {
            infoplus21_api.XUSTS xuts;
            infoplus21_api.XTS2XUST(ref xts, out xuts);

            return XUTS2Time(xuts);
        }

        public static uint TagId(string tagName)
        {
            infoplus21_api.ERRBLOCK errMsg;
            uint recid;
            char[] tagbyte = tagName.ToCharArray();
            infoplus21_api.DECODNAM(tagbyte, (ushort)tagName.Length, out recid, out errMsg);
            return recid;
        }

        public static uint FieldId(string fieldName)
        {
            infoplus21_api.ERRBLOCK errMsg;
            uint fieldId;
            byte[] propertybyte = Encoding.Default.GetBytes(fieldName.Trim());
            infoplus21_api.DECODFT(propertybyte, (short)fieldName.Length, out fieldId, out errMsg);
            return fieldId;
        }

        private static string ErrorMessage(infoplus21_api.ERRBLOCK errMsg)
        {
            byte[] err = new byte[78];
            int errSize;
            infoplus21_api.ERRMESS(ref errMsg, err, out errSize);
            return Encoding.ASCII.GetString(err).Substring(0, errSize);
        }

        public static void CheckResult(infoplus21_api.ERRBLOCK errMsg)
        {
            if (errMsg.ERRCODE == infoplus21_api.SUCCESS)
            {
                return;
            }

            IP21Exception ex = new IP21Exception(ErrorMessage(errMsg));
            ex.ErrCode = errMsg.ERRCODE;
            ex.Err1 = errMsg.ERR1;
            ex.Err2 = errMsg.ERR2;

            throw ex;
        }

        public static string getPointSubstring(string pointName)
        {
            string[] splitstr = pointName.Split('_');
            if (splitstr.Length >= 2)
                pointName = splitstr[0] + "_" + splitstr[1];
            return pointName;
        }

        public static string getAspenIPfromPointReg(string pointName)
        {
            writestr(pointName);
            string result = "";//默认ip地址,默认如果没有ip，则在调用方法后不设置原始ip
            pointName = getPointSubstring(pointName);
            IDictionary<string, string> IPdic = new Dictionary<string, string>();
            string xmlPath = AppDomain.CurrentDomain.BaseDirectory + "\\Config\\IptoPointConnection.xml";
            DataSet ds = new DataSet();
            ds.ReadXml(xmlPath);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dataRow in dt.Rows)
                IPdic.Add(dataRow[0].ToString(), dataRow[1].ToString());
            if (IPdic.Keys.Contains(pointName))
            {
                result = IPdic[pointName];
            }
            else
            {
                result = "10.5.5.43";
            }
            return result;
        }

        public static void writestr(string readme)
        {
            //StreamWriter dout = new StreamWriter(@"c:\" + "Ip21Log.txt", true);
            //dout.Write("\r\n事件：" + readme + "\r--------操作时间：" + System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss"));
            //dout.Close();
        }
    }
}