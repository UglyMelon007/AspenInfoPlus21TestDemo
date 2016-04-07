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
        public const int DTYPDUBL = 6;
        public const int DTYPTIME = -7;
        public const int DTYPXTIM = 8; /* Defined by XTSBLOCK structure */
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

        [StructLayout(LayoutKind.Sequential)]
        public struct IDANDFT
        {
            public ulong RecordId;
            public ulong FiledId;
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

        #region 记录操作(英文不好也不知道对不对, 仅供参考 o.o)
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

        /// <summary>
        /// 将一个ASCII字符串转换为一个数字值。此转换基于指定的格式化整数字段。如果这个格式字段为一个delta time，这个字符串必须包谷一个有效的delta time
        /// </summary>
        /// <param name="reicd">long word(ulong), 值传递,数据库要被转换的记录ID</param>
        /// <param name="ft">long word(ulong),值传递，字段类型的整数编码</param>
        /// <param name="ptbuff">character array(char[]),引用传递，包含这个ASCII数据的缓存地址</param>
        /// <param name="numchars">short word(ushort),值传递，缓存里的字符数</param>
        /// <param name="indata">long word(ulong)，引用传递（out关键字），转换结果</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out关键字），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ASCIIDB2I(ulong reicd, ulong ft, char[] ptbuff, ushort numchars, out ulong indata, out ERRBLOCK error);

        /// <summary>
        /// 将一串字符或数据缓存写入数据库字段中。
        /// </summary>
        /// <param name="reicd">long word(ulong),值传递，指定包含这个数据的记录ID</param>
        /// <param name="ft">long word(ulong),值传递，指定包含这个数据的字段标识</param>
        /// <param name="ptbfr">character array(char[]),引用传递，缓存源地址</param>
        /// <param name="numbytes">short word(ushort),值传递，指定缓存字节数.对于字符类型字段，若numbytes为0，这个字段将被用空填充，对于便签本类字段，numbytes必须为这个字段长度</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CHBF2DB(ulong reicd, ulong ft, char[] ptbfr, ushort numbytes, out ERRBLOCK error);

        /// <summary>
        /// 验证一个记录ID是否可用
        /// </summary>
        /// <param name="freeid">long word(ulong),值传递，指定被测试的记录ID</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CHKFREE(ulong freeid, out ERRBLOCK error);

        /// <summary>
        /// 验证一个记录是否已被存在一个字段内
        /// </summary>
        /// <param name="ftcheck">long word(ulong),值传递，感兴趣的字段的字段标识</param>
        /// <param name="recid">long word(ulong),值传递，要被检查的记录ID</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CHKFTREC(ulong ftcheck, ulong recid, out ERRBLOCK error);

        /// <summary>
        /// 将一个已存在记录用一个新的记录名与ID复制到另一个记录内(另一个记录得是已存在记录)
        /// </summary>
        /// <param name="recid">long word(ulong),值传递，将要被复制的记录ID</param>
        /// <param name="newid">long word(ulong),值传递，被指定的新的记录ID</param>
        /// <param name="substids">long word(ulong), 值传递，若recid的记录或记录与字段引用要被newid替换到新的记录中，则为非零，若为0则表示引用不改变</param>
        /// <param name="ptname">character array(char[]),引用传递，包含新记录名的缓存地址</param>
        /// <param name="numchars">short word(ushort), 值传递，指定缓冲区里的字符个数</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void COPYREC(ulong recid, ulong newid, ulong substids, char[] ptname, ushort numchars, out ERRBLOCK error);

        /// <summary>
        /// 使用定义的记录和ID创建一个新的记录
        /// </summary>
        /// <param name="recid">long word(ulong),值传递，新记录的ID(这个ID必须不被其他记录使用）</param>
        /// <param name="defid">long word(ulong),值传递，指定为recid定义记录的记录ID</param>
        /// <param name="ptname">character array(char[]),引用传递，包含新的记录名的缓冲区地址 </param>
        /// <param name="numchars">short word(ushort),引用传递，缓冲区的字符个数</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CREATEREC(ulong recid, ulong defid, char[] ptname, ushort numchars, out ERRBLOCK error);

        /// <summary>
        /// 将一个实际值转换为由数据库字段指定格式的ASCII
        /// </summary>
        /// <param name="recid">long word(ulong),值传递，字段标识的记录ID</param>
        /// <param name="ft">long word(ulong),值传递，定义这个格式的字段的字段标识</param>
        /// <param name="realdata">double precision real，引用传递(ref)，要被转换的真实值</param>
        /// <param name="ptbuff">character array(char[]), 引用传递(out)，接收ASCII数据的缓冲区地址</param>
        /// <param name="maxchars">short word(ushort),值传递，指定缓冲区的最大字符数</param>
        /// <param name="numchars">short word(ushort),引用传递（out），缓冲区被写入的字符个数，若写入的字符数大于指定最大字符数,则超过的部分不显示</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void D2ASCIIDB(ulong recid, ulong ft, ref double realdata, out char[] ptbuff, ushort maxchars, out ushort numchars, out ERRBLOCK error);

        /// <summary>
        /// 将一个值通过给定的格式化记录转换为指定的ASCII
        /// </summary>
        /// <param name="ptdata">Pointer to data（string),引用传递，要去格式化的数据的地址。这个值必须datatype是指定的数据类型</param>
        /// <param name="formid">long word(ulong),值传递，为0或者用来格式化为ASCII字符串的格式化记录的ID</param>
        /// <param name="datatype">short word(ushort), 值传递，是定义在setcim.h文件中的数据类型</param>
        /// <param name="scpd_flag">byte(byte),值传递，只有当数据类型是真实的时候才被使用。0表示这个字段被用来作为字符串数据格式化,1表示这个字段被用来作为便签字段格式化。</param>
        /// <param name="ptbuff">character array(char[]),引用传递（out），接收转换完成的ASCII数据的缓冲区</param>
        /// <param name="maxchars">short word(ushort),值传递，指定ptbuff中被使用的最大ASCII字符个数</param>
        /// <param name="numchars">short word(ushort),引用传递（out），是转换完成的字符个数，如果字符个数大于maxchars,只有maxchars内被使用，如果这个字段没有相应的格式化记录，那么numchars将总是等于maxchars+1</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DATA2ASCII(string ptdata, ulong formid, ushort datatype, byte scpd_flag, out char[] ptbuff, ushort maxchars, out ushort numchars, out ERRBLOCK error);

        /// <summary>
        /// 从数据库读取一个字符或数据缓存(已存在）
        /// </summary>
        /// <param name="recid">long word(ulong)值传递，包含这个数据的记录id</param>
        /// <param name="ft">long word(ulong)，值传递，包含这个数据的字段的字段标识</param>
        /// <param name="ptdbfr">character array(char[]),引用传递（out）,读取结果的缓存地址</param>
        /// <param name="numbytes">short word(ushort), 指定缓存里的字节数</param>
        /// <param name="error">errblock(errblock),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DB2CHBF(ulong recid, ulong ft, out char[] ptdbfr, ushort numbytes, out ERRBLOCK error);

        /// <summary>
        /// 从数据库中读取一个双精度的数据
        /// </summary>
        /// <param name="recid">long word(ulong)值传递，包含这个数据的记录的记录ID</param>
        /// <param name="ft">long word(ulong),值传递，包含这个数据的字段的字段标识</param>
        /// <param name="dubldata">double precision(double),引用传递（out），由记录ID指定的双精度的数据值</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DB2DUBL(ulong recid, ulong ft, out double dubldata, out ERRBLOCK error);

        /// <summary>
        /// 从数据库中读取一个记录ID和字段标识 (data type = DTYPIDFT)
        /// </summary>
        /// <param name="recid">long word(ulong)</param>
        /// <param name="ft"></param>
        /// <param name="idftdata"></param>
        /// <param name="error"></param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DB2IDFT(ulong recid, ulong ft, out IDANDFT idftdata, out ERRBLOCK error);

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
        public static extern void DECODFT(byte[] property, short propertyLength, out ulong propertyId, out ERRBLOCK errMsg);

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
        public static extern void DECODNAM(char[] tag, ushort tagLength, out ulong tagId, out ERRBLOCK errMsg);

        //读单个记录值
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DB2REAL(ulong tagId, ulong propertyId, out float Value, out ERRBLOCK errMsg);

        ////读单个记录的描述
        //[DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        //public static extern void DB2CHBF(ulong tagId, ulong propertyId, byte[] ptdbfr, ulong numbytes, out ERRBLOCK errMsg);

        //读单个记录的时间状态
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DB2XTIM(int tagId, int propertyId, out XTSBLOCK xtsdata, out ERRBLOCK errMsg);

        //从多个历史区域里读取多条记录，
        //按时间顺序读取记录（时间最早的在第一条），
        //并将读到的值放入一个数组里。
        //#define H21_GET_ACTUALS                4;
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void RHIS21DATA(int mode, int step, int outSiders, ulong tagId, ulong propertyId, ref XUSTS startTime, ref XUSTS endTime, ushort numfts, ulong[] fts, ushort[] datatypes, ulong maxoccus, ushort[] keylevels, XUSTS[] keyTimes, IntPtr[] ptdatas, out ulong occsok, out ushort ftsok, out ERRBLOCK errMsg);

        //读统计值
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        //        public static extern void RHIS21AGGREG(int timeweight, int step, int recid, int ft, ref XUSTS ptTimeOld, ref XUSTS ptTimeNew, ref XUSTS ptInterval, int timealign, int dsadjust, int maxperiods, int numtimecodes, int numdoublecodes, int numshortcodes, short[] timecodes, short[] doublecodes, short[] shortcodes, out XUSTS[,] timevalues, out double[,] doublevalues, out short[] shortvalues, out int numperiods, out ERRBLOCK err);
        public static extern void RHIS21AGGREG(int timeweight, int step, ulong recid, ulong ft, ref XUSTS ptTimeOld, ref XUSTS ptTimeNew, ref XUSTS ptInterval, int timealign, int dsadjust, ulong maxperiods, ulong numtimecodes, ulong numdoublecodes, ulong numshortcodes, short[] timecodes, short[] doublecodes, short[] shortcodes, IntPtr timevalues, IntPtr doublevalues, IntPtr shortvalues, out ulong numperiods, out ERRBLOCK err);

        //解析错误信息
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern void ERRMESS(ref ERRBLOCK errMsg, byte[] err, out int errsz);

        //断开
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DaShutdown();
    }
}
