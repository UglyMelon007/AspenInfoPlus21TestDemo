using System;
using System.Text;
using System.Runtime.InteropServices;

namespace RTDB.IP21
{
    internal static class infoplus21_api
    {
        public const int H21_GET_TIMES = 1;
        public const int H21_GET_TIMES2 = 2;
        public const int H21_GET_BEST_FIT = 3;
        public const int H21_GET_ACTUALS = 4;
        public const int H21_GET_ACTUALS_AND_CURRENT = 5;
        public const int H21_GET_TIMES_EXTENDED = 6;
        public const int H21_GET_TIMES2_EXTENDED = 7;

        public const int step = 0;
        public const int outsiders = 0;
        public const int timeweight = 0;

        public const int DTYPREAL = -5;
        public const int DTYPDUBL = -6;
        public const int DTYPTIME = -7;
        public const int DTYPXTIM = -8; /* Defined by XTSBLOCK structure */
        public const int DTYPUSTS = -17; /* Defined by XUSTS structure */
        public const int SUCCESS = 0;

        public const short AG_TIME_START = 21;//* time at beginning of period                     */
        public const short AG_TIME_MIDDLE = 22;/* time at middle of period                        */
        public const short AG_TIME_END = 23;/* time at end of period                           */
        public const short AG_TIME_AFTER = 24;/* time after end of period (start of next period) */
        public const short AG_DBL_GOOD = 41;/* good values                                     */
        public const short AG_DBL_NG = 42;/* not good values                                 */
        public const short AG_DBL_MIN = 43;/* minimum                                         */
        public const short AG_DBL_MAX = 44;/* maximum                                         */
        public const short AG_DBL_RNG = 45;/* range                                           */
        public const short AG_DBL_SUM = 46;/* sum                                             */
        public const short AG_DBL_AVG = 47;/* average                                         */
        public const short AG_DBL_VAR = 48;/* variance                                        */
        public const short AG_DBL_STD = 49;/* standard deviation                              */
        public const short AG_SHRT_QLEVEL = 81;/* quality "level" for period                      */

        public const int C_TRUE = 1; /* true value for c*/
        public const int C_FALSE = 0; /*false value for c*/

        [StructLayout(LayoutKind.Sequential)]
        public struct ERRBLOCK
        {
            // [MarshalAs(UnmanagedType.I2,SizeConst=16)]  
            public short ERRCODE;   /* Error code returned by ACCESS routines */
            // [MarshalAs(UnmanagedType.I8, SizeConst = 64)] 
            public long ERR1;
            //[MarshalAs(UnmanagedType.I8, SizeConst = 64)] 
            public long ERR2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct XUSTS
        {
            //[MarshalAs(UnmanagedType.I8, SizeConst = 32)]
            public int secs;    /* seconds since EPOCH (January 1, 1970 GMT) */
            //[MarshalAs(UnmanagedType.I8, SizeConst = 32)]
            public int usecs;   /* microseconds in the second */
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct XTSBLOCK
        {
            //[MarshalAs(UnmanagedType.I8, SizeConst = 64)]
            public int XTSFAST;    /* seconds since EPOCH (January 1, 1970 GMT) */
            //[MarshalAs(UnmanagedType.I8, SizeConst = 64)]
            public int XTSSLOW;   /* microseconds in the second */
        }
        //typedef struct _XUSTS {
        //        int  secs;       /* seconds since EPOCH (January 1, 1970 GMT) */
        //        int  usecs;      /* microseconds in the second */
        //        } XUSTS; 
        //typedef struct _XTSBLOCK {                
        //long XTSFAST,    /* SETCIM timestamp              */
        //     XTSSLOW;    /* # of 4 year blocks since 1900 */
        //} XTSBLOCK;      /* Extended Timestamp Block      */


        //连接到一个或多个节点 通过阅读配置文件创建一个内部节点列表，
        //并且连接至所有节点 
        //若至少成功连接一个节点返回1 若连接失败返回0
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool INISETC();

        //在程序退出之前调用ENDSETC() 
        //关闭所有服务节点连接 消毁服务列表函数并释放所有资源
        //程序不应该调用它除非当程序退出时
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ENDSETC();

        //工作方工类似DECODNAM()
        //只是它在接受一个记录或字段名后返回的是一个记录ID和一个字段标记
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DECODRAF();

        #region 记录操作
        //通过一个外部任务激活多条记录
        //ACTRECS(numrec, recids, actprio, oldprio. numact, error)
        //numrec
        //数据类型： long word
        //读写权限： 只用输入
        //传递类型： 值传递
        //描述：numrec是要被激活记录的数字（被包含在recids数组中）
        //recids
        //数据类型： long word array
        //读写权限： 只写
        //传递类型： 引用传递
        //描述： recids是一个包含要被激活的记录ID的数组
        //actprio
        //数据类型： short word
        //读写权限： 只写
        //传递类型：值传递
        //描述： actprio是所有被激活记录的活动权限
        //oldprio
        //数据类型：short word array
        //读写权限：只读
        //传递类型：引用传递
        //描述：oldprio是一个可选的旧的激活优先权。返回每个被成功激活的记录的旧的活动权限。如果这个记录以前没被激活则返回0.如果oldprio为NULL,则没有优先权被返回
        //numact
        //数据类型：long word
        //读写权限：只读
        //传递类型：引用传递
        //描述：numact是成功被激活的记录的数字
        //error 
        //数据类型： ERRBLOCK
        //读写权限： 只读
        //传递类型：引用传递
        //描述：error返回一个被定义在setcim.h中的错误代码
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ACTRECS();

        //ASCII2XTS 将ASCII时间转换为一个扩展的时间戳
        //ASCII2XTS(ptbuff, sizebuff, xts, error)
        //ptbuff
        //数据类型：character array
        //读写权限：只写
        //传递类型：引用传递
        //描述：ptbuff指定包含ASCII时间数据的buffer地址
        //sizebuff
        //数据类型：short word
        //读写权限：只写
        //传递类型: 值传递
        //描述：sizebuff指定这个字节在buff中的位置
        //xts
        //数据类型：XTSBLOCK
        //读写权限：只读
        //传递类型：引用传递
        //描述：xts是被转换的扩展时间
        //error
        //数据类型：byte
        //读写权限：只读
        //传递类型：引用传递
        //描述：error为0无错，反之error为1
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ASCII2XTS();

        //ASCIIDB2I 将一个ASCII字符串转换为一个数字值。此转换基于指定的格式化整数字段。如果这个格式字段为一个delta time，这个字符串必须包谷一个有效的delta time
        //ASCIIDB2I(recid, ft, ptbuff, numchars, indata, error)
        //recid
        //数据类型： long word
        //读写权限：只写
        //传递类型：值传递
        //描述：recid是数据库要被转换的记录ID
        //ft
        //数据类型：long word
        //读写权限：只写
        //传递类型：值传递
        //描述：ft是Êè
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ASCIIDB2I();

        #endregion

        //得到时间
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GETDBXTIM(out XTSBLOCK current_xts);

        //时间转化
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void XTS2XUST(ref XTSBLOCK xts, out XUSTS xusts);

        //初始化网络
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int DaInitialize(int login);

        //选择服务器
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int DaSelectServer(int ServerIndex, out ERRBLOCK ErrorBlock);

        //添加服务器连接两个作用：
		//连接一个不存在于最初配置文件列表里的数据库
		//不使用配置文件，只在需要的时候连接数据库。
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int DaAddServer(byte[] constring, out ERRBLOCK errMsg);

        //取得属性ID
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DECODFT(byte[] property, short propertyLength, out int propertyId, out ERRBLOCK errMsg);

        //根据一个记录名字获取它的记录ID
        //DECODNAM(ptbuff, numchars, recid, error)
        //ptbuff
        //数据类型 character array
		//读写权限 只写
		//传递类型 引用传递
		//描述 ptbuff是包含相应记录名字的buffer的地址
		//numchars 
		//数据类型 short word
		//读写权限 只写
		//传递类型 值传递
		//描述 numchars是ptbuff的字符个数
		//recid 
		//数据类型 long word
		//读写权限 只读
		//传递类型 引用传递
		//描述 recid是这个记录的记录ID。返回0是这个记录名为空，返回-1是这个记录没找到
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DECODNAM(byte[] tag, short tagLength, out int tagId, out ERRBLOCK errMsg);

        //读单个记录值
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DB2REAL(int tagId, int propertyId, out float Value, out ERRBLOCK errMsg);

        //读单个记录的描述
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DB2CHBF(int tagId, int propertyId, byte[] ptdbfr, int numbytes, out ERRBLOCK errMsg);

        //读单个记录的时间状态
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DB2XTIM(int tagId, int propertyId, out XTSBLOCK xtsdata, out ERRBLOCK errMsg);
        
        //从多个历史区域里读取多条记录，
		//按时间顺序读取记录（时间最早的在第一条），
		//并将读到的值放入一个数组里。
        //#define H21_GET_ACTUALS                4;
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void RHIS21DATA(int mode, int step, int outSiders, int tagId, int propertyId, ref XUSTS startTime, ref XUSTS endTime, int numfts, int[] fts, short[] datatypes, int maxoccus, int[] keylevels, XUSTS[] keyTimes, IntPtr[] ptdatas, out int occsok, out int ftsok, out ERRBLOCK errMsg);

        //读统计值
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        //        public static extern void RHIS21AGGREG(int timeweight, int step, int recid, int ft, ref XUSTS ptTimeOld, ref XUSTS ptTimeNew, ref XUSTS ptInterval, int timealign, int dsadjust, int maxperiods, int numtimecodes, int numdoublecodes, int numshortcodes, short[] timecodes, short[] doublecodes, short[] shortcodes, out XUSTS[,] timevalues, out double[,] doublevalues, out short[] shortvalues, out int numperiods, out ERRBLOCK err);
        public static extern void RHIS21AGGREG(int timeweight, int step, int recid, int ft, ref XUSTS ptTimeOld, ref XUSTS ptTimeNew, ref XUSTS ptInterval, int timealign, int dsadjust, int maxperiods, int numtimecodes, int numdoublecodes, int numshortcodes, short[] timecodes, short[] doublecodes, short[] shortcodes, IntPtr timevalues, IntPtr doublevalues, IntPtr shortvalues, out int numperiods, out ERRBLOCK err);

        //解析错误信息
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern void ERRMESS(ref ERRBLOCK errMsg, byte[] err, out int errsz);

        //断开
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DaShutdown();
    }
}
