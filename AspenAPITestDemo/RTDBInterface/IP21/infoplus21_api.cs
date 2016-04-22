using System;
using System.Text;
using System.Runtime.InteropServices;

namespace RTDB.IP21
{
    public static class infoplus21_api
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

        [StructLayout(LayoutKind.Sequential)]
        public struct IDANDFT
        {
            public int RecordId;
            public int FiledId;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NAMFTARR
        {
            public byte[] RecordName;
            public byte[] FieldName;
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

        ////在程序退出之前调用ENDSETC() 
        ////关闭所有服务节点连接 消毁服务列表函数并释放所有资源
        ////程序不应该调用它除非当程序退出时
        //[DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        //public static extern void ENDSETC();

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
        /// <param name="reicd">long word(int), 值传递,数据库要被转换的记录ID</param>
        /// <param name="ft">long word(int),值传递，字段类型的整数编码</param>
        /// <param name="ptbuff">character array(byte[]),引用传递，包含这个ASCII数据的缓存地址</param>
        /// <param name="numchars">short word(short),值传递，缓存里的字符数</param>
        /// <param name="indata">long word(int)，引用传递（out关键字），转换结果</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out关键字），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ASCIIDB2I(int reicd, int ft, byte[] ptbuff, short numchars, out int indata, out ERRBLOCK error);

        /// <summary>
        /// 将一串字符或数据缓存写入数据库字段中。
        /// </summary>
        /// <param name="reicd">long word(int),值传递，指定包含这个数据的记录ID</param>
        /// <param name="ft">long word(int),值传递，指定包含这个数据的字段标识</param>
        /// <param name="ptbfr">character array(byte[]),引用传递，缓存源地址</param>
        /// <param name="numbytes">short word(short),值传递，指定缓存字节数.对于字符类型字段，若numbytes为0，这个字段将被用空填充，对于便签本类字段，numbytes必须为这个字段长度</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CHBF2DB(int reicd, int ft, byte[] ptbfr, short numbytes, out ERRBLOCK error);

        /// <summary>
        /// 验证一个记录ID是否可用
        /// </summary>
        /// <param name="freeid">long word(int),值传递，指定被测试的记录ID</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CHKFREE(int freeid, out ERRBLOCK error);

        /// <summary>
        /// 验证一个记录是否已被存在一个字段内
        /// </summary>
        /// <param name="ftcheck">long word(int),值传递，感兴趣的字段的字段标识</param>
        /// <param name="recid">long word(int),值传递，要被检查的记录ID</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CHKFTREC(int ftcheck, int recid, out ERRBLOCK error);

        /// <summary>
        /// 将一个已存在记录用一个新的记录名与ID复制到另一个记录内(另一个记录得是已存在记录)
        /// </summary>
        /// <param name="recid">long word(int),值传递，将要被复制的记录ID</param>
        /// <param name="newid">long word(int),值传递，被指定的新的记录ID</param>
        /// <param name="substids">long word(int), 值传递，若recid的记录或记录与字段引用要被newid替换到新的记录中，则为非零，若为0则表示引用不改变</param>
        /// <param name="ptname">character array(byte[]),引用传递，包含新记录名的缓存地址</param>
        /// <param name="numchars">short word(short), 值传递，指定缓冲区里的字符个数</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void COPYREC(int recid, int newid, int substids, byte[] ptname, short numchars, out ERRBLOCK error);

        /// <summary>
        /// 使用定义的记录和ID创建一个新的记录
        /// </summary>
        /// <param name="recid">long word(int),值传递，新记录的ID(这个ID必须不被其他记录使用）</param>
        /// <param name="defid">long word(int),值传递，指定为recid定义记录的记录ID</param>
        /// <param name="ptname">character array(byte[]),引用传递，包含新的记录名的缓冲区地址 </param>
        /// <param name="numchars">short word(short),引用传递，缓冲区的字符个数</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CREATEREC(int recid, int defid, byte[] ptname, short numchars, out ERRBLOCK error);

        /// <summary>
        /// 将一个实际值转换为由数据库字段指定格式的ASCII
        /// </summary>
        /// <param name="recid">long word(int),值传递，字段标识的记录ID</param>
        /// <param name="ft">long word(int),值传递，定义这个格式的字段的字段标识</param>
        /// <param name="realdata">double precision real，引用传递(ref)，要被转换的真实值</param>
        /// <param name="ptbuff">character array(byte[]), 引用传递(out)，接收ASCII数据的缓冲区地址</param>
        /// <param name="maxchars">short word(short),值传递，指定缓冲区的最大字符数</param>
        /// <param name="numchars">short word(short),引用传递（out），缓冲区被写入的字符个数，若写入的字符数大于指定最大字符数,则超过的部分不显示</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void D2ASCIIDB(int recid, int ft, ref double realdata, out byte[] ptbuff, short maxchars, out short numchars, out ERRBLOCK error);

        /// <summary>
        /// 将一个值通过给定的格式化记录转换为指定的ASCII
        /// </summary>
        /// <param name="ptdata">Pointer to data（string),引用传递，要去格式化的数据的地址。这个值必须datatype是指定的数据类型</param>
        /// <param name="formid">long word(int),值传递，为0或者用来格式化为ASCII字符串的格式化记录的ID</param>
        /// <param name="datatype">short word(short), 值传递，是定义在setcim.h文件中的数据类型</param>
        /// <param name="scpd_flag">byte(byte),值传递，只有当数据类型是真实的时候才被使用。0表示这个字段被用来作为字符串数据格式化,1表示这个字段被用来作为便签字段格式化。</param>
        /// <param name="ptbuff">character array(byte[]),引用传递（out），接收转换完成的ASCII数据的缓冲区</param>
        /// <param name="maxchars">short word(short),值传递，指定ptbuff中被使用的最大ASCII字符个数</param>
        /// <param name="numchars">short word(short),引用传递（out），是转换完成的字符个数，如果字符个数大于maxchars,只有maxchars内被使用，如果这个字段没有相应的格式化记录，那么numchars将总是等于maxchars+1</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DATA2ASCII(string ptdata, int formid, short datatype, byte scpd_flag, out byte[] ptbuff, short maxchars, out short numchars, out ERRBLOCK error);

        /// <summary>
        /// 从数据库读取一个字符或数据缓存(已存在）
        /// </summary>
        /// <param name="recid">long word(int)值传递，要读取的这个数据所在记录的记录ID</param>
        /// <param name="ft">long word(int)，值传递，要读取数据所在字段的字段标识</param>
        /// <param name="ptdbfr">character array(byte[]),引用传递（out）,读取结果的缓存地址</param>
        /// <param name="numbytes">short word(short), 指定缓存里的字节数</param>
        /// <param name="error">errblock(errblock),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DB2CHBF(int recid, int ft, out byte[] ptdbfr, short numbytes, out ERRBLOCK error);

        /// <summary>
        /// 从数据库中读取一个双精度的数据
        /// </summary>
        /// <param name="recid">long word(int)值传递，要读取的这个数据所在记录的记录ID</param>
        /// <param name="ft">long word(int),值传递，要读取数据所在字段的字段标识</param>
        /// <param name="dubldata">double precision(double),引用传递（out），由记录ID指定的双精度的数据值</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DB2DUBL(int recid, int ft, out double dubldata, out ERRBLOCK error);

        /// <summary>
        /// 从数据库中读取一个记录ID和字段标识 (data type = DTYPIDFT)
        /// </summary>
        /// <param name="recid">long word(int),值传递，要读取的这个数据所在记录的记录ID</param>
        /// <param name="ft">long word(int),值传递，要读取数据所在字段的字段标识 </param>
        /// <param name="idftdata">IDANDFT(IDANDFT),引用传递（out),包含记录ID与字段标识值</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DB2IDFT(int recid, int ft, out IDANDFT idftdata, out ERRBLOCK error);

        /// <summary>
        /// 从数据库读取一个长的无符号整型数字(data type = DTYPLONG). 
        /// </summary>
        /// <param name="recid">long word(int),值传递，要读取的这个数据所在记录的记录ID</param>
        /// <param name="ft">long word(int),值传递，要读取数据所在字段的字段标识</param>
        /// <param name="intdata">long word(int),引用传递(out),返回的读取结果</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DB2LONG(int recid, int ft, out int intdata, out ERRBLOCK error);

        /// <summary>
        /// 从数据库读取一个单精度的真实值(data type = DTYPREAL). 
        /// </summary>
        /// <param name="recid">long word(int),值传递,要读取的这个数据所在记录的记录ID</param>
        /// <param name="ft">long word(int),值传递，要读取数据所在字段的字段标识</param>
        /// <param name="realdata">sing precision real(float),返回的读取结果</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DB2REAL(int recid, int ft, out float realdata, out ERRBLOCK error);

        /// <summary>
        /// 从数据库中读取一个记录ID(data type = DTYPREID). 
        /// </summary>
        /// <param name="recid">long word(int),值传递，要读取的这个数据所在记录的记录ID</param>
        /// <param name="ft">long word(int),值传递，要读取数据所在字段的字段标识</param>
        /// <param name="iddata">long word(int),引用传递(out),返回的记录ID值</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DB2REID(int recid, int ft, out int iddata, out ERRBLOCK error);

        /// <summary>
        /// 从数据库中读取一个单字节数字（这里用short双字节）
        /// </summary>
        /// <param name="recid">long word(int),值传递,要读取的这个数据所在记录的记录ID</param>
        /// <param name="ft">long word(int),值传递，要读取数据所在字段的字段标识</param>
        /// <param name="shrtdata">short word(short),引用传递（out),返回的读取结果</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DB2SHRT(int recid, int ft, out short shrtdata, out ERRBLOCK error);

        /// <summary>
        /// 从数据库中读取一个扩展时间戳
        /// </summary>
        /// <param name="recid">long word(int),值传递，要读取的这个数据所在记录的记录ID</param>
        /// <param name="ft">long word(int),值传递，要读取数据所在字段的字段标识</param>
        /// <param name="xtsdata">XTSBLOCK(XTSBLOCK),引用传递（out),返回的读取结果</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DB2XTIM(int recid, int ft, out XTSBLOCK xtsdata, out ERRBLOCK error);

        /// <summary>
        /// 将一个字段名字解码为相应的字段标识
        /// </summary>
        /// <param name="ptbuff">character array(byte[]), 引用传递，包含字段类型名的缓存地址</param>
        /// <param name="numchars">short word(short), 值传递，缓存里面的字符个数</param>
        /// <param name="ft">long word(int), 引用传递（out),根据字段名返回的字段标识，若没有相应的字段名，则返回0</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DECODFT(byte[] ptbuff, short numchars, out int ft, out ERRBLOCK error);

        /// <summary>
        /// 将一个记录名解码为相应的记录ID
        /// </summary>
        /// <param name="ptbuff">character array(byte[]), 引用传递，包含记录名的缓存地址(</param>
        /// <param name="numchars">short word(short),值传递，缓冲区里的字符个数</param>
        /// <param name="recid">long word(int),引用传递（out），根据记录名返回的记录ID,若记录名为空返回0，若记录名不存在返回-1</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DECODNAM(byte[] ptbuff, short numchars, out int recid, out ERRBLOCK error);

        /// <summary>
        /// 将一个记录名及其相关联的字段名解码为其相应的记录ID和字段标识
        /// </summary>
        /// <param name="ptbuff">NAMFTARR(不知），引用传递，包含记录与字段名的缓冲区</param>
        /// <param name="numchars">short word(short),值传递，缓冲区内可用的最大ASCII字符个数</param>
        /// <param name="recid">long word(int),引用传递（out），记录相应的记录ID</param>
        /// <param name="ft">long word(int), 引用传递（out），字段相应的字段标识</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DECODRAF(ref NAMFTARR ptbuff, short numchars, out int recid, out int ft, out ERRBLOCK error);

        /// <summary>
        /// 判断记录ID是否有效,若为0，则这个记录ID是效的
        /// </summary>
        /// <param name="recid">要判断的记录ID</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int DEFINID(int recid);

        /// <summary>
        /// 删除一个记录，这个记录必须为不可用状态
        /// </summary>
        /// <param name="recid">long word(int),值传递，要被删除的记录的记录ID</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        /// <returns></returns>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DELETREC(int recid, out ERRBLOCK error);

        /// <summary>
        /// 删除存在于两个已存在的事件之间的事件。它改变从最早点到被删除点的所有事件的事件个数。这个被删除的点由一个事件编号指定，也就是说，由事件编号指定的事件之前的事件不改变，之后的事件在这个点删除后将整体上移
        /// </summary>
        /// <param name="recid">long word(int),值传递，被访问的记录的记录ID</param>
        /// <param name="ft">long word(int),值传递，记录的非历史重复区域里一个字段的字段标识(记录可以有很多重复区域）。一个有效的字段要指定其事件个数，通常设置为1</param>
        /// <param name="numoccs">long word(int),值传递，要被删除的事件个数</param>
        /// <param name="occnum">long word(int),从指定的事件编号开始删除</param>
        /// <param name="occsdeleted">short word(short),引用传递（out），删除成功的个数</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DELOCCS(int recid, int ft, int numoccs, int occnum, out short occsdeleted, out ERRBLOCK error);

        /// <summary>
        /// 将一个“day of century"时间格式化为一个Aspen InfoPlus.21扩展时间戳
        /// </summary>
        /// <param name="day">long word(int),值传递，在一个世纪里的天数</param>
        /// <param name="time">long word(int),值传递，一个世纪一天十分秒的个数(谁知道这是啥意思。。。）</param>
        /// <param name="xts">XTSBLOCK(XTSBLOCK），引用传递（out），格式化后的结果</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DSPDT2XTS(int day, int time, out XTSBLOCK xts);

        /// <summary>
        /// 将一个double类型数据写入数据库(data type = DTYPDUBL)
        /// </summary>
        /// <param name="recid">long word(int),值传递，要写入记录的记录ID</param>
        /// <param name="ft">long word(int), 值传递，要写入字段的字段标识</param>
        /// <param name="dubldata">double precision real(double),要存入数据库的数据</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DUBLADD2DB(int recid, int ft, double dubldata, out ERRBLOCK error);

        /// <summary>
        /// 终止一个活动GCS/InfoPlus.21控制台服务进程和正在调用应用程序之间的一个进程间通信接口
        /// </summary>
        /// <param name="descriptor">Console(Console),引用传递，为一个标识这个接口已经被初始化的结构的一个引用。这个结构必须调用INITCONS来初始化。输出时，这个结构不再形容一个有效的接口。Console类型被定义在文件名为console.h的头文件中</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        //[DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        //public static extern void ENDCONS(Console descriptor, out ERRBLOCK error);


        /// <summary>
        ////在程序退出之前调用ENDSETC() 
        ////关闭所有服务节点连接 消毁服务列表函数并释放所有资源
        ////程序不应该调用它除非当程序退出时
        /// </summary>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ENDSETC();

        /// <summary>
        /// 返回一个与数据库错误块相对应的ASCII错误信息
        /// </summary>
        /// <param name="error">ERRBLOCK(ERRBLOCK)，引用传递，包含一个要被转换成ASCII的错误编码的错误块</param>
        /// <param name="error_msg">ERRARRAY(byte[])，引用传递(out),为被转换成ASCII错误信息的数组</param>
        /// <param name="errsz">short word(short),引用传递（out），返回信息的字符个数，剩下的字符被用空填充</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ERRMESS(ref ERRBLOCK error, out byte[] error_msg, out  short errsz);

        /// <summary>
        /// 返回一个字段的数据库信息
        /// </summary>
        /// <param name="recid">long word(int),值传递，这个记录ID</param>
        /// <param name="ft">long word(int),值传递，记录中要被检查的字段标识。</param>
        /// <param name="seq">long word(int),值传递，出现过的历史序列号，同上，非必需参数</param>
        /// <param name="flddefninfo">FIELDDEFN(未知），引用传递，关于字段信息的缓冲地址</param>
        /// <param name="stop">short word(short),引用传递out,磁盘历史状态码</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void FIELDDEFNINFO(int recid, int ft, int seq, out  FIELDDEFN flddefninfo, out  short stop, out ERRBLOCK error);

        /// <summary>
        /// 返回一个字段的数据库信息 
        /// </summary>
        /// <param name="recid">long word(int),值传递，记录ID</param>
        /// <param name="ft">long word(int),值传递，记录中被检查的字段标识</param>
        /// <param name="datatype">short word(short),引用传递out，被定义在setcim.h文件内的数据类型指示</param>
        /// <param name="dspchars">short word(short),引用传递out，这个字段的ASCII码个数</param>
        /// <param name="inchars">short word(short),引用传递out，out允许插入入的最大字符个数</param>
        /// <param name="unusacha">Byte(byte),引用传递out,如果这个记录不可用这个字段可被改变，返回TRUE</param>
        /// <param name="usacha">Byte(byte),引用传递out,如果这个记录可用这个字段可被改变，返回TRUE</param>
        /// <param name="nowopcha">Byte(byte),引用传递out,如果当前状态下这个字段可被改变，返回TRUE</param>
        /// <param name="resizable">Byte(byte),引用传递out,如果这个字段是一个重复区域字段返回TRUE</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void FIELDINFO(int recid, int ft, out short datatype, out short dspchars, out short inchars, out byte unusacha, out byte usacha, out byte nowopcha, out byte resizable, out ERRBLOCK error);

        /// <summary>
        /// 得到一个记录内指定序号的字段的字段标识，和这个记录内的字段总个数
        /// </summary>
        /// <param name="recid">long word(int),值传递，记录ID</param>
        /// <param name="fldnum">long word(int),值传递，记录内的字段序号</param>
        /// <param name="fldftid">long word(int),引用传递（out),字段标识</param>
        /// <param name="numflds">long word(int),引用传递（out),返回的字段个数</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void FLDFT(int recid, int fldnum, out int fldftid, out int numflds);

        /// <summary>
        /// 若记录们不存在于文件夹内，将他们加进去
        /// </summary>
        /// <param name="folderid">long word(int),值传递，文件记录的ID</param>
        /// <param name="records">long word(int[]),引用传递,要被加入文件夹的一组记录ID</param>
        /// <param name="numrecs">short(short),值传递，要被加入文件夹的记录个数</param>
        /// <param name="numok">short(short),引用传递out,添加成功的记录个数</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void FOLDERIN(int folderid, int[] records, short numrecs, out short numok, out ERRBLOCK error);

        /// <summary>
        /// 从一个文件夹内移除一个记录
        /// </summary>
        /// <param name="folderid">long word(int),值传递，文件夹的记录ID</param>
        /// <param name="records">long word(int[]),引用传递，要从文件夹内移除的的一组记录的记录ID</param>
        /// <param name="numrecs">short(short),值传递，要被移除的记录的个数</param>
        /// <param name="numok">short(short),引用传递，移除成功的记录个数</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void FOLDEROUT(int folderid, int[] records, short numrecs, out short numok, out ERRBLOCK error);

        /// <summary>
        /// 将一个字段名字转换为字段标识。若字段名无效则返回0
        /// </summary>
        /// <param name="ptbuff">character array(byte[]),引用传递，包含字段名字的缓冲地址</param>
        /// <param name="numchars">short word(short),引用传递，缓冲的字符个数</param>
        /// <returns>返回类型long word(int),若字段名字无效则返回0，否则返回字段标识</returns>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int FTNAME2FT(byte[] ptbuff, short numchars);

        /// <summary>
        /// 验证用户是否有参数wantedDbpermis指定的数据库权限。若用户有指定的权限，granted被设置为true。参数availDbpermis包含这个用户可用的唯一权限
        /// </summary>
        /// <param name="wantedDbPermis">long word(int)，值传递，要被检查的数据库权限</param>
        /// <param name="granted">long word(int),引用传递out,若用户有指定权限则granted为true,否则为false</param>
        /// <param name="availDbPermis">long word(int),引用传递out,包含用户可用的数据库权限</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GETDBPERMIS(int wantedDbPermis, out int granted, out int availDbPermis, ERRBLOCK error);

        /// <summary>
        /// 返回数据库当前时间
        /// </summary>
        /// <param name="xtime">XTSBLOCK(XTSBLOCK),引用传递out,数据库的当前时间（xtime  is the current Aspen InfoPlus.21 time as an extended timestamp.）</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GETDBXTIM(out XTSBLOCK xtime);

        /// <summary>
        /// 验证一个用户是否有一个指定记录指定字段的写入权限
        /// </summary>
        /// <param name="recid">long word(int),值传递，包含要检查字段的记录ID</param>
        /// <param name="ft">long word(int),值传递，以ASCII格式命名的字段标识</param>
        /// <param name="wantedFldPermis">long word(int),值传递，包含要被检查的字段权限</param>
        /// <param name="granted">long word(int),引用传递（out），若有相应的权限则返回TRUE</param>
        /// <param name="availFldPermis">long word(int),引用传递（out），包含这个用户对这个字段的可用权限</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GETFLDPERMIS(int recid, int ft, int wantedFldPermis, out int granted, out int availFldPermis, out ERRBLOCK error);

        /// <summary>
        /// 根据指定的记录ID和字段标识返回字段名字
        /// </summary>
        /// <param name="recid">long word(int),值传递，指定的记录ID</param>
        /// <param name="ft">long word(int),值传递，指定的字段标识</param>
        /// <param name="ftbuff">FINMARR(FINMARR),引用传递out,包含字段名字的字符缓冲</param>
        /// <param name="numchars">short word(short)，字段名字的大小</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GETFTDB(int recid, int ft, FINMARR ftbuff, out short numchars);

        /// <summary>
        /// 根据记录ID返回记录名字
        /// </summary>
        /// <param name="recid">long word(int),引用传递ref,记录ID,若无效，则为0</param>
        /// <param name="nambuff">NAMERR(NAMERR),引用传递out,包含记录名的字符缓冲</param>
        /// <param name="numchars">short word(short),引用传递out,记录名字的大小</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GETNAMDB( ref int recid, out NAMERR nambuff, out short numchars);

        /// <summary>
        /// 通过指定的记录ID和字段标识返回记录名和字段名
        /// </summary>
        /// <param name="recid">long word(int),值传递，包含指定字段的记录ID</param>
        /// <param name="ft">long word(int),值传递，字段标识（The occurrence number is required.）</param>
        /// <param name="nmftbuff">NAMFTARR(NAMFTARR),引用传递，包含记录名与字段名的字符缓冲（including the occurrence if in a repeat area）</param>
        /// <param name="numchars">short word(short),引用传递out，被使用的缓冲大小</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GETNMFDB(int recid, int ft, out NAMFTARR nmftbuff, out short numchars);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lastrecid"></param>
        /// <param name="rectype"></param>
        /// <param name="alphaorder"></param>
        /// <param name="grouplist"></param>
        /// <param name="groupsize"></param>
        /// <param name="viewreq"></param>
        /// <param name="modifyreq"></param>
        /// <param name="maxrecs"></param>
        /// <param name="recids"></param>
        /// <param name="recusabs"></param>
        /// <param name="recnames"></param>
        /// <param name="namesizes"></param>
        /// <param name="numrecs"></param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GETRECLIST(ref int lastrecid, int rectype, int alphaorder, byte[] grouplist, int groupsize, int viewreq, int modifyreq, int maxrecs, out int[] recids, out byte[] recusabs, out NAMEARR[] recnames, out short[] namesizes, out int numrecs);

        #endregion

        ////得到时间
        //[DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        //public static extern void GETDBXTIM(out XTSBLOCK current_xts);

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

        ////取得属性ID
        //[DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        //public static extern void DECODFT(byte[] property, short propertyLength, out int propertyId, out ERRBLOCK errMsg);

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
        //[DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        //public static extern void DECODNAM(byte[] tag, short tagLength, out int tagId, out ERRBLOCK errMsg);

        ////读单个记录值
        //[DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        //public static extern void DB2REAL(int tagId, int propertyId, out float Value, out ERRBLOCK errMsg);

        ////读单个记录的描述
        //[DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        //public static extern void DB2CHBF(int tagId, int propertyId, byte[] ptdbfr, int numbytes, out ERRBLOCK errMsg);

        //读单个记录的时间状态
        //[DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        //public static extern void DB2XTIM(int tagId, int propertyId, out XTSBLOCK xtsdata, out ERRBLOCK errMsg);

        //从多个历史区域里读取多条记录，
        //按时间顺序读取记录（时间最早的在第一条），
        //并将读到的值放入一个数组里。
        //#define H21_GET_ACTUALS                4;

        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void RHIS21DATA(int mode, int step, int outSiders, int tagId, int propertyId, ref XUSTS startTime, ref XUSTS endTime, short numfts, int[] fts, short[] datatypes, int maxoccus, short[] keylevels, XUSTS[] keyTimes, IntPtr[] ptdatas, out int occsok, out short ftsok, out ERRBLOCK errMsg);

        //读统计值
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        //        public static extern void RHIS21AGGREG(int timeweight, int step, int recid, int ft, ref XUSTS ptTimeOld, ref XUSTS ptTimeNew, ref XUSTS ptInterval, int timealign, int dsadjust, int maxperiods, int numtimecodes, int numdoublecodes, int numshortcodes, short[] timecodes, short[] doublecodes, short[] shortcodes, out XUSTS[,] timevalues, out double[,] doublevalues, out short[] shortvalues, out int numperiods, out ERRBLOCK err);
        public static extern void RHIS21AGGREG(int timeweight, int step, int recid, int ft, ref XUSTS ptTimeOld, ref XUSTS ptTimeNew, ref XUSTS ptInterval, int timealign, int dsadjust, int maxperiods, int numtimecodes, int numdoublecodes, int numshortcodes, short[] timecodes, short[] doublecodes, short[] shortcodes, IntPtr timevalues, IntPtr doublevalues, IntPtr shortvalues, out int numperiods, out ERRBLOCK err);

        ////解析错误信息
        //[DllImport("infoplus21_api.dll", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        //public static extern void ERRMESS(ref ERRBLOCK errMsg, out byte[] err, out int errsz);

        //断开
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DaShutdown();
    }
}
