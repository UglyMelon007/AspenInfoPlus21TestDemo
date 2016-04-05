using System;
using System.Collections.Generic;
using System.Text;

namespace RTDB.IP21 {
    public class DBTimeReader {
        //获取实时数据库当前时间
        public static DateTime GetRTDBCurrentTime() {
            infoplus21_api.XTSBLOCK current_xts;
            infoplus21_api.GETDBXTIM(out current_xts);
            return Util.XTSBLOCK2Time(current_xts);
        }

    }
}
