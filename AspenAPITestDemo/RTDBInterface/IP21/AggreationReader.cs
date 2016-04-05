using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;

namespace RTDB.IP21
{
    internal class AggreationReader {
        //实时数据库刷新间隔，分钟
        private const int RTDB_REFRESH_STEP = 15;

        static public double GetDailyAverage(string tag, DateTime date)
        {
            DateTime timeOld = new DateTime(date.Year, date.Month, date.Day); // start of the day
            DateTime timeNew = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59); // end of the day
            return GetAggreagedData(tag, timeOld, timeNew, infoplus21_api.AG_DBL_AVG);
        }

        static public double GetDailyMax(string tag, DateTime date) {
            DateTime timeOld = new DateTime(date.Year, date.Month, date.Day); // start of the day
            DateTime timeNew = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59); // end of the day
            return GetAggreagedData(tag, timeOld, timeNew, infoplus21_api.AG_DBL_MAX);
        }

        static public double GetDailyMin(string tag, DateTime date) {
            DateTime timeOld = new DateTime(date.Year, date.Month, date.Day); // start of the day
            DateTime timeNew = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59); // end of the day
            return GetAggreagedData(tag, timeOld, timeNew, infoplus21_api.AG_DBL_MIN);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="timeOld"></param>
        /// <param name="timeNew"></param>
        /// <param name="interval">统计值步长</param>
        /// <param name="doubleCodes">可以一次包含</param>
        /// <returns></returns>
        static public IList<TimeAggreationPair> GetMaxMinAvgAtOnce(string tag, DateTime timeOld, DateTime timeNew, int interval) {
            //对于大于当前时间的统计值得求取会导致返回结果为零，因此，需要保证最大时间小于服务器当前时间。
            //实时数据库在最后一次保存到历史前，如果求统计值就会得零。
            DateTime dbCurrentTime = DBTimeReader.GetRTDBCurrentTime();
            if (timeNew > dbCurrentTime)
                timeNew = dbCurrentTime.AddMinutes(-1 * RTDB_REFRESH_STEP);
            //*********************************************************

            int ft = Util.FieldId("IP_TREND_VALUE") + 1; // !!! +1 is a must !!!

            infoplus21_api.XUSTS ptTimeOld = Util.Time2XUSTS(timeOld);
            infoplus21_api.XUSTS ptTimeNew = Util.Time2XUSTS(timeNew);

            infoplus21_api.XUSTS timeInterval;
            timeInterval.secs = interval;
            timeInterval.usecs = 0;
            // let interval be the same as time period
            int tmp = Convert.ToInt32((timeNew - timeOld).TotalSeconds / interval);
            int maxPeriods = (tmp) <= 1
                                 ? 10
                                 : tmp;
            AggregatedData doubles = AggregatedData.CreateData(AggregatedData.Type.DOUBLE);
            doubles.Codes = new short[] { infoplus21_api.AG_DBL_MAX, infoplus21_api.AG_DBL_MIN, infoplus21_api.AG_DBL_AVG, infoplus21_api.AG_DBL_SUM };

            AggregatedData times = AggregatedData.CreateData(AggregatedData.Type.TIME);
            times.Codes = new short[] { infoplus21_api.AG_TIME_START, infoplus21_api.AG_TIME_MIDDLE, infoplus21_api.AG_TIME_END };

            AggregatedData shorts = AggregatedData.CreateData(AggregatedData.Type.SHORT);
            shorts.Codes = new short[] { infoplus21_api.AG_SHRT_QLEVEL };

            int numPeriods;
            infoplus21_api.ERRBLOCK errMsg;

            infoplus21_api.RHIS21AGGREG(1, // int timeweight
                infoplus21_api.step,
                Util.TagId(tag), // int recid, 
                ft, // int ft, 
                ref ptTimeOld, // ref XUSTS ptTimeOld, 
                ref ptTimeNew, //ref XUSTS ptTimeNew, 
                ref timeInterval, // ref XUSTS ptInterval, 
                0, // int timealign, 
                0, // int dsadjust, 
                maxPeriods, // int maxperiods, 
                times.Codes.Length, // int numtimecodes, 
                doubles.Codes.Length, // int numdoublecodes, 
                shorts.Codes.Length, // int numshortcodes, 
                times.Codes, // short[] timecodes, 
                doubles.Codes, // short[] doublecodes, 
                shorts.Codes, // short[] shortcodes,
                times.AllocBuffer(maxPeriods), // IntPtr timevalues,
                doubles.AllocBuffer(maxPeriods), // IntPtr doublevalues, 
                shorts.AllocBuffer(maxPeriods), // IntPtr shortvalues, 
                out numPeriods, // out int numperiods, 
                out errMsg //out ERRBLOCK err); 
            );

            doubles.FreeBuffer();
            times.FreeBuffer();
            shorts.FreeBuffer();

            Util.CheckResult(errMsg);

            IList<TimeAggreationPair> results = new List<TimeAggreationPair>();
            DateTime occurTime = timeOld;
            for (int i = 0; i < numPeriods; i++) {
                occurTime = occurTime.AddSeconds(interval);
                double min = (double) doubles.DataAt(i, infoplus21_api.AG_DBL_MIN);
                double max = (double)doubles.DataAt(i, infoplus21_api.AG_DBL_MAX);
                double avg = (double)doubles.DataAt(i, infoplus21_api.AG_DBL_AVG);
                double sum = (double) doubles.DataAt(i, infoplus21_api.AG_DBL_SUM);
                results.Add(new TimeAggreationPair(occurTime, new AggreationData(min, avg, max, sum)));
            }
            return results;
        } 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="timeOld"></param>
        /// <param name="timeNew"></param>
        /// <param name="interval">间隔秒</param>
        /// <param name="doubleCode"></param>
        /// <returns></returns>
        static public IList<TimeValuePair> GetAggreagedData(string tag, DateTime timeOld, DateTime timeNew, int interval, short doubleCode) {
            //对于大于当前时间的统计值得求取会导致返回结果为零，因此，需要保证最大时间小于服务器当前时间。
            //实时数据库在最后一次保存到历史前，如果求统计值就会得零。
            DateTime dbCurrentTime = DBTimeReader.GetRTDBCurrentTime();
            if (timeNew > dbCurrentTime)
                timeNew = dbCurrentTime.AddMinutes(-1 * RTDB_REFRESH_STEP);
            //*********************************************************
            int ft = Util.FieldId("IP_TREND_VALUE") + 1; // !!! +1 is a must !!!

            infoplus21_api.XUSTS ptTimeOld = Util.Time2XUSTS(timeOld);
            infoplus21_api.XUSTS ptTimeNew = Util.Time2XUSTS(timeNew);

            infoplus21_api.XUSTS timeInterval;
            timeInterval.secs = interval;
            timeInterval.usecs = 0;
            // let interval be the same as time period
            int tmp = Convert.ToInt32((timeNew - timeOld).TotalSeconds / interval);
            int maxPeriods = (tmp) <= 1
                                 ? 10
                                 : tmp;
            AggregatedData doubles = AggregatedData.CreateData(AggregatedData.Type.DOUBLE);
            doubles.Codes = new short[] { doubleCode };

            AggregatedData times = AggregatedData.CreateData(AggregatedData.Type.TIME);
            times.Codes = new short[] { infoplus21_api.AG_TIME_START, infoplus21_api.AG_TIME_MIDDLE, infoplus21_api.AG_TIME_END };

            AggregatedData shorts = AggregatedData.CreateData(AggregatedData.Type.SHORT);
            shorts.Codes = new short[] { infoplus21_api.AG_SHRT_QLEVEL };

            int numPeriods;
            infoplus21_api.ERRBLOCK errMsg;

            infoplus21_api.RHIS21AGGREG(infoplus21_api.timeweight, // int timeweight
                infoplus21_api.step,
                Util.TagId(tag), // int recid, 
                ft, // int ft, 
                ref ptTimeOld, // ref XUSTS ptTimeOld, 
                ref ptTimeNew, //ref XUSTS ptTimeNew, 
                ref timeInterval, // ref XUSTS ptInterval, 
                0, // int timealign, 
                0, // int dsadjust, 
                maxPeriods, // int maxperiods, 
                times.Codes.Length, // int numtimecodes, 
                doubles.Codes.Length, // int numdoublecodes, 
                shorts.Codes.Length, // int numshortcodes, 
                times.Codes, // short[] timecodes, 
                doubles.Codes, // short[] doublecodes, 
                shorts.Codes, // short[] shortcodes,
                times.AllocBuffer(maxPeriods), // IntPtr timevalues,
                doubles.AllocBuffer(maxPeriods), // IntPtr doublevalues, 
                shorts.AllocBuffer(maxPeriods), // IntPtr shortvalues, 
                out numPeriods, // out int numperiods, 
                out errMsg //out ERRBLOCK err); 
            );

            doubles.FreeBuffer();
            times.FreeBuffer();
            shorts.FreeBuffer();

            Util.CheckResult(errMsg);

            //if (numPeriods <= 0)
            //{
            //    return 0;
            //}

            //string result = "period\t\tstart time\t\tmiddle time\t\tend time\t\tvalue";
            //for (int period = 0; period < numPeriods; period++)
            //{
            //    result += "\r\n" + period;
            //    DateTime start = Util.XUTS2Time((infoplus21_api.XUSTS)times.DataAt(period, infoplus21_api.AG_TIME_START));
            //    result += "\t\t" + start;
            //    DateTime middle = Util.XUTS2Time((infoplus21_api.XUSTS)times.DataAt(period, infoplus21_api.AG_TIME_MIDDLE));
            //    result += "\t\t" + middle;
            //    DateTime end = Util.XUTS2Time((infoplus21_api.XUSTS)times.DataAt(period, infoplus21_api.AG_TIME_END));
            //    result += "\t\t" + end;
            //    double value = (double)doubles.DataAt(period, doubleCode);
            //    result += "\t\t" + value;
            //}

            //doubles.DataAt(0, doubleCode);
            IList<TimeValuePair> results = new List<TimeValuePair>();
            DateTime occurTime = timeOld;
            for (int i = 0; i < numPeriods; i++) {
                occurTime = occurTime.AddSeconds(interval);
                results.Add(new TimeValuePair(occurTime, (double)doubles.DataAt(i, doubleCode)));
            }
            return results;

        }

        static public double GetAggreagedData(string tag, DateTime timeOld, DateTime timeNew, short doubleCode) {
            //对于大于当前时间的统计值得求取会导致返回结果为零，因此，需要保证最大时间小于服务器当前时间。
            //实时数据库在最后一次保存到历史前，如果求统计值就会得零。
            DateTime dbCurrentTime = DBTimeReader.GetRTDBCurrentTime();
            if (timeNew > dbCurrentTime)
                timeNew = dbCurrentTime.AddMinutes(-1 * RTDB_REFRESH_STEP);
            //*********************************************************
            int ft = Util.FieldId("IP_TREND_VALUE") + 1; // !!! +1 is a must !!!

            infoplus21_api.XUSTS ptTimeOld = Util.Time2XUSTS(timeOld);
            infoplus21_api.XUSTS ptTimeNew = Util.Time2XUSTS(timeNew);

            infoplus21_api.XUSTS timeInterval;
            timeInterval.secs = (int)((timeNew - timeOld).TotalSeconds);
            timeInterval.usecs = timeInterval.secs;
            // let interval be the same as time period

            int maxPeriods = 50; // we just want only one period
            AggregatedData doubles = AggregatedData.CreateData(AggregatedData.Type.DOUBLE);
            doubles.Codes = new short[] { doubleCode };

            AggregatedData times = AggregatedData.CreateData(AggregatedData.Type.TIME);
            times.Codes = new short[] { infoplus21_api.AG_TIME_START, infoplus21_api.AG_TIME_MIDDLE, infoplus21_api.AG_TIME_END };

            AggregatedData shorts = AggregatedData.CreateData(AggregatedData.Type.SHORT);
            shorts.Codes = new short[] { infoplus21_api.AG_SHRT_QLEVEL };

            int numPeriods;
            infoplus21_api.ERRBLOCK errMsg;

            infoplus21_api.RHIS21AGGREG(infoplus21_api.timeweight, // int timeweight
                infoplus21_api.step,
                Util.TagId(tag), // int recid, 
                ft, // int ft, 
                ref ptTimeOld, // ref XUSTS ptTimeOld, 
                ref ptTimeNew, //ref XUSTS ptTimeNew, 
                ref timeInterval, // ref XUSTS ptInterval, 
                0, // int timealign, 
                0, // int dsadjust, 
                maxPeriods, // int maxperiods, 
                times.Codes.Length, // int numtimecodes, 
                doubles.Codes.Length, // int numdoublecodes, 
                shorts.Codes.Length, // int numshortcodes, 
                times.Codes, // short[] timecodes, 
                doubles.Codes, // short[] doublecodes, 
                shorts.Codes, // short[] shortcodes,
                times.AllocBuffer(maxPeriods), // IntPtr timevalues,
                doubles.AllocBuffer(maxPeriods), // IntPtr doublevalues, 
                shorts.AllocBuffer(maxPeriods), // IntPtr shortvalues, 
                out numPeriods, // out int numperiods, 
                out errMsg //out ERRBLOCK err); 
            );

            doubles.FreeBuffer();
            times.FreeBuffer();
            shorts.FreeBuffer();

            Util.CheckResult(errMsg);

            //if (numPeriods <= 0)
            //{
            //    return 0;
            //}

            //string result = "period\t\tstart time\t\tmiddle time\t\tend time\t\tvalue";
            //for (int period = 0; period < numPeriods; period++)
            //{
            //    result += "\r\n" + period;
            //    DateTime start = Util.XUTS2Time((infoplus21_api.XUSTS)times.DataAt(period, infoplus21_api.AG_TIME_START));
            //    result += "\t\t" + start;
            //    DateTime middle = Util.XUTS2Time((infoplus21_api.XUSTS)times.DataAt(period, infoplus21_api.AG_TIME_MIDDLE));
            //    result += "\t\t" + middle;
            //    DateTime end = Util.XUTS2Time((infoplus21_api.XUSTS)times.DataAt(period, infoplus21_api.AG_TIME_END));
            //    result += "\t\t" + end;
            //    double value = (double)doubles.DataAt(period, doubleCode);
            //    result += "\t\t" + value;
            //}

            return (double)doubles.DataAt(0, doubleCode);
        }

        /// <summary>
        /// 获取多个数据位号的统计值
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="timeOld"></param>
        /// <param name="timeNew"></param>
        /// <param name="doubleCode"></param>
        /// <returns></returns>
        static public Dictionary<string, string> GetAggreagedData(IList tags, DateTime timeOld, DateTime timeNew, short doubleCode) {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (string tag in tags) {
                if (!result.ContainsKey(tag)) {
                 double value = GetAggreagedData(tag, timeOld, timeNew, doubleCode);
                result.Add(tag, value.ToString());
                }
            }
            return result;
        }
    }
}
