using System;
using System.Collections.Generic;
using System.Text;
using RTDB.IP21;

namespace RTDB.IP21
{
    internal class HistoryReader
    {
        // 根据一个时间点和位号取内插值
        public static double ReadInterpolated(string tag, DateTime time)
        {
            IList<TimeValuePair> results = ListHistoryData(infoplus21_api.H21_GET_TIMES, tag, time, 3600, 10);

            return results.Count == 0 ? 0 : results[results.Count - 1].Value;
        }


        // 根据一个时间点和位号和跨度值取实际值
        public static double ReadActuals(string tag, DateTime time, int span)
        {
            IList<TimeValuePair> results = ListAllHistory(infoplus21_api.H21_GET_ACTUALS, tag, time, span);

            return results.Count == 0 ? 0 : results[results.Count - 1].Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="timeOld"></param>
        /// <param name="timeNew"></param>
        /// <param name="interval">例如：01:00:00或者00:01:00</param>
        /// <returns></returns>
        public static IList<TimeValuePair> ListHistoryDataByInterval(string tag, DateTime timeOld, DateTime timeNew, string IP21PeriodInterval)
        {
            int interval = int.Parse(TimeSpan.Parse(IP21PeriodInterval).TotalSeconds.ToString());
            //设定开始和结束时间
            infoplus21_api.XUSTS endTime = Util.Time2XUSTS(timeNew);
            infoplus21_api.XUSTS startTime = Util.Time2XUSTS(timeOld);
            ulong maxOccus = Convert.ToUInt32((timeNew - timeOld).TotalSeconds / interval);
            Field trendTime = Field.CreateField("IP_TREND_TIME");
            Field trendValue = Field.CreateField("IP_TREND_VALUE");

            ulong[] fts = new ulong[] { trendTime.Id, trendValue.Id };
            ushort[] dataTypes = new ushort[] { trendTime.DataType, trendValue.DataType };
            ulong proDocc = fts[1] + 1;

            //设定返回值
            infoplus21_api.ERRBLOCK errMsg;
            ulong occsok;
            ushort ftsok;
            IntPtr[] point = new IntPtr[] { trendTime.AllocBuffer(maxOccus), trendValue.AllocBuffer(maxOccus) };

            infoplus21_api.RHIS21DATA(infoplus21_api.H21_GET_TIMES, // int mode
                infoplus21_api.step,  // int step
                infoplus21_api.outsiders, // int outsiders
                Util.TagId(tag), // int tagId
                proDocc, // int propertyId
                ref startTime, // ref XUSTS startTime
                ref endTime, // ref XUSTS endTime
                (ushort)fts.Length, // int numfts
                fts, // int[] fts, 
                dataTypes, // int[] datatypes
                maxOccus, // int maxoccus
                null, // int[] keylevels
                null, // XUSTS[] keyTimes
                point, // IntPtr[] ptdatas
                out occsok, // out int occsok
                out ftsok, // out int ftsok
                out errMsg // out ERRBLOCK errMsg
                );

            trendTime.FreeBuffer();
            trendValue.FreeBuffer();

            Util.CheckResult(errMsg);

            IList<TimeValuePair> results = new List<TimeValuePair>();
            for (int i = 0; i < (int)occsok; i++)
            {
                DateTime occurTime = Util.XTSBLOCK2Time((infoplus21_api.XTSBLOCK)trendTime.DataAt(i));
                results.Add(new TimeValuePair(occurTime, (double)trendValue.DataAt(i)));
            }

            return results;
        }

        /// <summary>
        /// 在指定时间段内，使用固定步长取实时值及插值
        /// </summary>
        /// <param name="tag">位号</param>
        /// <param name="timeOld">开始时间</param>
        /// <param name="timeNew">结束时间</param>
        /// <param name="interval">步长，单位秒</param>
        /// <returns></returns>
        public static IList<TimeValuePair> ListHistoryDataByInterval(string tag, DateTime timeOld, DateTime timeNew, int interval)
        {
            //设定开始和结束时间
            infoplus21_api.XUSTS endTime = Util.Time2XUSTS(timeNew);
            infoplus21_api.XUSTS startTime = Util.Time2XUSTS(timeOld);
            ulong maxOccus = Convert.ToUInt32((timeNew - timeOld).TotalSeconds / interval);
            Field trendTime = Field.CreateField("IP_TREND_TIME");
            Field trendValue = Field.CreateField("IP_TREND_VALUE");

            ulong[] fts = new ulong[] { trendTime.Id, trendValue.Id };
            ushort[] dataTypes = new ushort[] { trendTime.DataType, trendValue.DataType };
            ulong proDocc = fts[1] + 1;

            //设定返回值
            infoplus21_api.ERRBLOCK errMsg;
            ulong occsok;
            ushort ftsok;
            IntPtr[] point = new IntPtr[] { trendTime.AllocBuffer(maxOccus), trendValue.AllocBuffer(maxOccus) };

            infoplus21_api.RHIS21DATA(infoplus21_api.H21_GET_TIMES, // int mode
                infoplus21_api.step,  // int step
                infoplus21_api.outsiders, // int outsiders
                Util.TagId(tag), // int tagId
                proDocc, // int propertyId
                ref startTime, // ref XUSTS startTime
                ref endTime, // ref XUSTS endTime
                (ushort)fts.Length, // int numfts
                fts, // int[] fts, 
                dataTypes, // int[] datatypes
                maxOccus, // int maxoccus
                null, // int[] keylevels
                null, // XUSTS[] keyTimes
                point, // IntPtr[] ptdatas
                out occsok, // out int occsok
                out ftsok, // out int ftsok
                out errMsg // out ERRBLOCK errMsg
                );

            trendTime.FreeBuffer();
            trendValue.FreeBuffer();

            Util.CheckResult(errMsg);

            IList<TimeValuePair> results = new List<TimeValuePair>();
            for (int i = 0; i < (int)occsok; i++)
            {
                DateTime occurTime = Util.XTSBLOCK2Time((infoplus21_api.XTSBLOCK)trendTime.DataAt(i));
                results.Add(new TimeValuePair(occurTime, (double)trendValue.DataAt(i)));
            }

            return results;
        }


        /// <summary>
        /// 读取历史数据
        /// </summary>
        /// <param name="mode">1-取内插值（平均分配时间段）  4-取实际值</param>
        /// <param name="tag">要读取的标签</param>
        /// <param name="time">开始时间</param>
        /// <param name="span">持续时间，单位为秒</param>
        /// <param name="properties">要读取的属性</param>
        /// <param name="maxOccus">要读取的最大发生次数</param>
        public static IList<TimeValuePair> ListHistoryData(int mode, string tag, DateTime time, int span, ulong maxOccus)
        {
            //设定开始和结束时间
            infoplus21_api.XUSTS endTime = Util.Time2XUSTS(time);
            infoplus21_api.XUSTS startTime = Util.Time2XUSTS(time.AddSeconds(0 - span));

            Field trendTime = Field.CreateField("IP_TREND_TIME");
            Field trendValue = Field.CreateField("IP_TREND_VALUE");

            ulong[] fts = new ulong[] { trendTime.Id, trendValue.Id };
            ushort[] dataTypes = new ushort[] { trendTime.DataType, trendValue.DataType };
            ulong proDocc = fts[1] + 1;

            //设定返回值
            infoplus21_api.ERRBLOCK errMsg;
            ulong occsok;
            ushort ftsok;
            IntPtr[] point = new IntPtr[] { trendTime.AllocBuffer(maxOccus), trendValue.AllocBuffer(maxOccus) };

            infoplus21_api.RHIS21DATA(mode, // int mode
                infoplus21_api.step,  // int step
                infoplus21_api.outsiders, // int outsiders
                Util.TagId(tag), // int tagId
                proDocc, // int propertyId
                ref startTime, // ref XUSTS startTime
                ref endTime, // ref XUSTS endTime
                (ushort)fts.Length, // int numfts
                fts, // int[] fts, 
                dataTypes, // int[] datatypes
                maxOccus, // int maxoccus
                null, // int[] keylevels
                null, // XUSTS[] keyTimes
                point, // IntPtr[] ptdatas
                out occsok, // out int occsok
                out ftsok, // out int ftsok
                out errMsg // out ERRBLOCK errMsg
                );

            trendTime.FreeBuffer();
            trendValue.FreeBuffer();

            Util.CheckResult(errMsg);

            IList<TimeValuePair> results = new List<TimeValuePair>();
            for (int i = 0; i < (int)occsok; i++)
            {
                DateTime occurTime = Util.XTSBLOCK2Time((infoplus21_api.XTSBLOCK)trendTime.DataAt(i));
                results.Add(new TimeValuePair(occurTime, (double)trendValue.DataAt(i)));
            }

            return results;
        }

        /// <summary>
        /// 读取指定时间段内的全部历史数据
        /// </summary>
        /// <param name="mode">1-取内插值（平均分配时间段）  4-取实际值</param>
        /// <param name="tag">要读取的标签</param>
        /// <param name="time">开始时间</param>
        /// <param name="span">持续时间，单位为秒</param>
        /// <param name="properties">要读取的属性</param>
        public static IList<TimeValuePair> ListAllHistory(int mode, string tag, DateTime time, int span)
        {
            //设定点数,初始设为100，逐级递增
            ulong maxOccus = 100;

            while (true)
            {
                IList<TimeValuePair> results = ListHistoryData(mode, tag, time, span, maxOccus);
                if (results.Count == 0) // no data
                {
                    return results;
                }
                else if ((ulong)results.Count < maxOccus) // no more data
                {
                    return results; // all have been fetched
                }
                else
                {
                    maxOccus += 100; // to get more data
                }
            }
        }
    }
}