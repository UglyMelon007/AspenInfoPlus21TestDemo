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
        public static extern void GETNAMDB(ref int recid, out NAMERR nambuff, out short numchars);

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
        /// 返回一组满足指定条件的记录ID和名字
        /// </summary>
        /// <param name="lastrecid">long word(int),引用传递ref，</param>
        /// <param name="rectype">long word(int),值传递，是一个已定义的记录ID或一个记录类型标识（README.md 记录类型标识)</param>
        /// <param name="alphaorder">long word(int),值传递，若为0，则按记录ID排序，反之则按字母顺序排列</param>
        /// <param name="grouplist">byte array(byte[]),引用传递(ref)，一个识别用户组成员资格的byte数组</param>
        /// <param name="groupsize">long word(int),值传递，grouplist的大小</param>
        /// <param name="viewreq">long word(int),值传递，若用户必须有查看记录权限则为非0，反之为0</param>
        /// <param name="modifyreq">long word(int),值传递，若户必须有修改记录权限则为非0，反之为0</param>
        /// <param name="maxrecs">long word(int),值传递，list里面可放的最多记录个数</param>
        /// <param name="recids">long word array(int[]),引用传递out,通过这个函数返回的一组记录ID,记录个数必须少于maxrecs</param>
        /// <param name="recusabs">byte array(byte[]), 引用传递out,指出recids中可用的记录。若记录不可用则为0，若记录可用则非0。长度小于maxrecs</param>
        /// <param name="recnames">NAMEARR array(NAMEARR[]),引用传递out,包含recids中记录的记录名。个数至少和maxrecs一样</param>
        /// <param name="namesizes">short word array(short[]),引用传递out，包含recnames中记录名的长度。个数至少和maxrecs一样</param>
        /// <param name="numrecs">long word(ing),引用传递out,recids中返回记录的真实个数，</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GETRECLIST(ref int lastrecid, int rectype, int alphaorder, ref byte[] grouplist, int groupsize, int viewreq, int modifyreq, int maxrecs, out int[] recids, out byte[] recusabs, out NAMEARR[] recnames, out short[] namesizes, out int numrecs);

        /// <summary>
        /// 验证用户是否有wantedRecPeris所指定的记录权限。
        /// </summary>
        /// <param name="recid">long word(int),值传递，包含指定字段的记录ID</param>
        /// <param name="wantedRecPermis">long word(int),值传递，包含要去检查的记录权限</param>
        /// <param name="granted">long word(int),引用传递out，若拥有指定权限则为TRUE</param>
        /// <param name="availRecPermis">long word(int),引用传递out，用户拥有这个记录的权限</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GETRECPERMIS(int recid, int wantedRecPermis, out int granted, out int availRecPermis, out ERRBLOCK error);

        /// <summary>
        /// 获取指定记录内指定字段的的写权限的值.用户必须有相应的读权限才能去阅读其写权限
        /// </summary>
        /// <param name="recid">long word(int),值传递，包含指定字段的记录ID</param>
        /// <param name="ft">long word(int),值传递，要获取读权限值的记录标识 </param>
        /// <param name="writeLevel">long word(int),引用传递out,字段的写权限（字段的几种写权限看REMADE.md)</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GETWRITELEVEL(int recid, int ft, out int writeLevel, out ERRBLOCK error);

        /// <summary>
        ///得到并或者改变这个历史重复区域的内一个记录的最早允许时间(这都是啥呀，看不懂 :dog: )
        /// </summary>
        /// <param name="id">long word(int),值传递,记录ID</param>
        /// <param name="ft">long word(int),值传递,重复区域内一个字段的字段标识或或重复区域内的字段标识个数</param>
        /// <param name="newoldest">long word(int),值传递,新的最早时间(new oldest)是允许的从1970年开始计算的秒数</param>
        /// <param name="oldoldest">long word(int),引用传递out,上一个被允许的最早时间</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void HISOLDESTOK(int id, int ft, int newoldest, out int oldoldest, out ERRBLOCK error);

        /// <summary>
        /// 将一个整数转换为一个数据库字段指定格式的ASCII
        /// </summary>
        /// <param name="recid">long word(int),值传递，要转换的整数的记录ID</param>
        /// <param name="ft">long word(int),值传递，一个整数字段的字段标识。这个字段的格式将被使用去格式化intdata的整数值</param>
        /// <param name="intdata">long word(int),值传递，要被转换的整数</param>
        /// <param name="ptbuff">character array(byte[]),引用传递ref,转换后的结果的缓冲地址</param>
        /// <param name="maxchars">short word(short),值传递，指定缓冲的最大大小</param>
        /// <param name="numchars">short word(short),引用传递out,被写入ptbuff内的字符个数，最大为maxchars</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void I2ASCIIDB(int recid, int ft, int intdata, ref byte[] ptbuff, short maxchars, out short numchars, out ERRBLOCK error);

        /// <summary>
        /// 将一个记录ID和一个字段标识写入数据库
        /// </summary>
        /// <param name="recid">long word(int),值传递，包含要写入的字段的记录ID</param>
        /// <param name="ft">long word(int),值传递，要写入字段的字段标识</param>
        /// <param name="idftdata">IDANDFT(IDANDFT),值传递，包含记录ID和字段标识的值</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void IDFT2DB(int recid, int ft, IDANDFT idftdata, out ERRBLOCK error);

        /// <summary>
        /// 在程序里打开一个Aspen InfoPlus.21连接。这个东西必须被调用在调用其它函数之前。
        /// </summary>
        /// <returns>若AspenInfoPlus.21启动返回1，若AspenInfoPlus.21关闭返回0</returns>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int INISETC();

        /// <summary>
        /// 初始化一个GCS/InfoPlus.21控制服务进程和调用程序之间的进程间通信接口。这个函数应与ENDCONS成对出现
        /// </summary>
        /// <param name="console">long word(int),值传递，已由GSC/InfoPlus.21控制台会话所使用的感兴趣的控制台任务记录的记录ID</param>
        /// <param name="descriptor">Console(console),引用传递(out),是一个结构的引用来识别已被初始化的接口。这个结构需要调用其它GCS/InfoPlus.21控制台存取方法。这个数据类型被定义在头文件console.h中</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），表明是否初始化成功。如果error.ERRCODE没有被设置为SUCCESS，这个结构引用descriptor将不能被使用.error.ERRCODE的可用值（README.md)</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void INITCONS(int console, out Console descriptor, out ERRBLOCK error);

        /// <summary>
        /// 在两个已存在事件之间插入一个新的事件并更新事件序号。数据可以指定写入新事件内的字段.若字段没有指定数据则为其默认值。
        /// </summary>
        /// <param name="recid">long word(int),值传递，要被存取的记录的记录ID</param>
        /// <param name="ft">long word(int),值传递，记录的非历史重复区域内一个字段的字段标识(记录可以有超过一个重复区域）.需要指定事件序号来找一个有效的字段，这个序号通常被设置为1.</param>
        /// <param name="numfts">short word(short),值传递，每个新字段要被更新的字段个数。若为0，则所有字段都为默认值</param>
        /// <param name="fts">long word(int)，引用传递传递ref，一组包含事件中要被更新的所有字段的字段标识</param>
        /// <param name="datats">short word(short),引用传递传递ref，一组包含fts内数据的数据类型</param>
        /// <param name="numoccs">long word(int),值传递，要插入的事件个数</param>
        /// <param name="occnum">long word(int),值传递，插入事件的事件个数</param>
        /// <param name="ptdatas">short word(short),引用传递out,一组数据指针.每个字段所需要的数据。分别对应相应的事件</param>
        /// <param name="occsinserted">short word(short),引用传递out,插入成功的事件个数</param>
        /// <param name="occsok">short word(short),引用传递out,若numfts不等于0则等于occsinserted。如果出现一个错误并且ftsok少于numfts,occsinserted事件</param>
        /// <param name="ftsok">short word(short),引用传递out,返回的字段个数等于numfts,若出现错误则少于numfts</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递out,返回InfoPlus.21错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void INSOCCS(int recid, int ft, short numfts, ref int fts, ref short datats, int numoccs, int occnum, out short ptdatas, out short occsinserted, out short occsok, out short ftsok, out ERRBLOCK error);

        /// <summary>
        /// 将一条信息添加到一个日志记录
        /// </summary>
        /// <param name="recid">long word(int),值传递,如果信息被记入由logctrlid指定的日志控件内的所有日志记录列表内recid为0。如果recid不为0，则为要写入记录的记录Id.(具体详细看pdf)</param>
        /// <param name="logctrlid">long word(int),值传递,日志控件记录的记录ID</param>
        /// <param name="ptmess">character array(byte[]),引用传递ref,信息组（就是一个字符串）</param>
        /// <param name="numchars">short word(short),值传递，信息的字符个数</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void LOGMESS(int recid, int logctrlid, ref byte[] ptmess, short numchars, out ERRBLOCK error);

        /// <summary>
        /// 将一个长整型写入数据库(datatype=DTPLONG)
        /// </summary>
        /// <param name="recid">long word(int)，值类型传递，包含字段的记录ID</param>
        /// <param name="ft">long word(int)，值类型传递，要写入字段的字段标识</param>
        /// <param name="intdata">long word(int)，值类型传递，要写入的长整型值</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void LONG2DB(int recid, int ft, int intdata, out ERRBLOCK error);

        /// <summary>
        /// 使一个记录不可用。这个记录不能被其它记录引用。
        /// </summary>
        /// <param name="recid">long word(int)，值传递，记录ID</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MAKUNUSA(int recid, out ERRBLOCK error);

        /// <summary>
        /// 使一个记录可用
        /// </summary>
        /// <param name="recid">long word(int)，值传递，记录ID</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MAKUSABL(int recid, out ERRBLOCK error);

        /// <summary>
        /// 从一个数据库记录的一个重复区域的多个事件里阅读多个字段
        /// </summary>
        /// <param name="recid">long word(int),值传递，包含重复区域的记录ID</param>
        /// <param name="numfts">short word(short)，值传递，每个事件里读的字段个数</param>
        /// <param name="ptfts">long word array(int[]),引用传递ref, 包含从每个事件内阅读的字段的字段标识。</param>
        /// <param name="ptdtypes">short word array(short[]),引用传递ref，要去阅读的字段的类型标识，数组内的每一个元素是一个定义在setcim.h头文件内的数据类型</param>
        /// <param name="frstoc">short word(short),值传递开始阅读的事件编号</param>
        /// <param name="lastoc">short word(short),值传递，结束阅读的事件编号</param>
        /// <param name="ptdatas">array of address(object[])，引用传递out,一组阅读结果。这个数组必须足够大来包含所有阅读结果。</param>
        /// <param name="occsok">short word(short),引用传递out,实际阅读的事件个数</param>
        /// <param name="ftsok">short word(short),引用传递out,实际阅读的字段个数。若没有错误，则等于numfts</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MRDBOCCS(int recid, short numfts, ref int[] ptfts, ref short[] ptdtypes, short frstoc, short lastoc, out object[] ptdatas, out short occsok, out short ftsok, out ERRBLOCK error);

        /// <summary>
        /// 从数据库内多个记录内读取多个字段
        /// </summary>
        /// <param name="numvalus">short word(short),值传递，要去阅读的值的个数</param>
        /// <param name="fields">IDANDFT(IDANDFT),引用传递ref，一组要去阅读的记录和字段ID</param>
        /// <param name="ptdtypes">short word array(short[]),引用传递ref，包含要去阅读字段的数据类型</param>
        /// <param name="ptdatas">type aligned address(object[])，引用传递out,阅读结果。  </param>
        /// <param name="numok">short word(short)，引用传递(ref),阅读的值的真实个数</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MRDBVALS(short numvalus, ref IDANDFT[] fields, ref short[] ptdtypes, out object[] ptdatas, out short numok, out ERRBLOCK error);

        /// <summary>
        /// 将多个事件的多个字段写入一个数据库记录的重复区域内
        /// </summary>
        /// <param name="recid">long word(int)，值传递，包含重复区域的记录ID</param>
        /// <param name="numfts">short word(short),值传递，写入每个事件内的字段个数</param>
        /// <param name="ptfts">long word array(int[]),引用传递，包含要写入每个事件的字段的字段标识。</param>
        /// <param name="ptdtypes">short word array(short[]),引用传递ref,一组要写入的字段的类型标识</param>
        /// <param name="frstoc">short word(short),值传递，开始写入事件的事件编号</param>
        /// <param name="lastoc">short word(short),值传递，结束事件的事件编号</param>
        /// <param name="ptdatas">arrray of addresses(object[])，引用传递out,一组地址.</param>
        /// <param name="occsok">short word(short)，引用传递out,被写入的真实事件个数</param>
        /// <param name="ftsok">short word(short),引用传递out,被写入的真实字段个数</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void WDBOCCS(int recid, short numfts, ref int[] ptfts, out short[] ptdtypes, short frstoc, short lastoc, out object[] ptdatas, out short occsok, out short ftsok, out ERRBLOCK error);

        /// <summary>
        /// 将多个字段写入数据库的多个记录内
        /// </summary>
        /// <param name="numvalus">short word(short),值传递，要写入的值的个数</param>
        /// <param name="fields">IDANDFT array(IDANDFT[]),引用传递ref,一组要被写入的记录ID和字段ID</param>
        /// <param name="ptdtypes">short word array(short[]),引用传递ref,一个数组分别对应fields内字段的数据类型</param>
        /// <param name="ptdatas">type aligned address(object[]),要被写入的最终数据</param>
        /// <param name="numok">short word(short),引用传递out,被写入的真实个数</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MWDBVALS(short numvalus, ref IDANDFT[] fields, ref short[] ptdtypes, ref object[] ptdatas, out short numok, out ERRBLOCK error);

        /// <summary>
        /// 返回一个定义记录的记录名大小
        /// </summary>
        /// <param name="defid">long word(int),值传递,定义记录的记录ID</param>
        /// <returns>short word(short),返回定义的记录记录名字段的大小</returns>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern short NAMSZDEF(int defid);

        /// <summary>
        /// 根据一个指定的记录和指定字段查找下一个引用
        /// </summary>
        /// <param name="idcheck">long word(int），值传递，要被检查的记录ID</param>
        /// <param name="ftcheck">long word(int),值传递，要被检查的字段标识。如果为0，查找idcheck包含的定义记录的所有引用。若不为0，查找这个字段的所有引用</param>
        /// <param name="ptiduse">long word(int),引用传递out,当进入时，如果ptiduse为0，开始时，若ptiduse为0，则从最初开始查找。如果不等于0，则从指定的记录后（ptiduse,ptftuse)开始查找.</param>
        /// <param name="ptftuse">long word(int),引用传递out,退出时，ptftuse是idcheck记录的字段的字段标识</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void NXTREFER(int idcheck, int ftcheck, out int ptiduse, out int ptftuse);

        /// <summary>
        /// 将一个数值转换为一个数据库字段指定的格式的ASCII
        /// </summary>
        /// <param name="recid">long word(int),值传递，字段标识的记录ID</param>
        /// <param name="ft">long word(int),值传递，定义这个格式的字段的字段标识</param>
        /// <param name="realdata">single/double precision real(double),值传递，要被转换的数字。</param>
        /// <param name="ptbuff">character array(byte[]),引用传递(out)，转换完的结果</param>
        /// <param name="maxchars">short word(short),值传递，ptbuff的最大个数</param>
        /// <param name="numchars">short word(short)，引用传递out,实际应该写入的字符个数。若比maxchars大则只有maxchars个字符被写入</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void R2ASCIIDB(int recid, int ft, double realdata, out byte[] ptbuff, short maxchars, out short numchars, ERRBLOCK error);

        /// <summary>
        /// 读取数据库内的一个字段并转换为ASCII.ASCII转换格式由字段的显示格式定义
        /// </summary>
        /// <param name="recid">long word(int),值传递，包含数据的记录ID</param>
        /// <param name="ft">long word(int)，值传递，包含数据的字段标识</param>
        /// <param name="ptbuff">character array (byte[]), 引用传递out,转换完成的结果</param>
        /// <param name="maxchars">short word(short),值传递，ptbuff的最大个数</param>
        /// <param name="numchars">short word(short)，引用传递out,实际应该写入的字符个数。若比maxchars大则只有maxchars个字符被写入</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void RDBASCII(int recid, int ft, out byte[] ptbuff, short maxchars, out short numchars, ERRBLOCK error);

        /// <summary>
        /// 从一个数据库记录内阅读多个事件的单精度字段
        /// </summary>
        /// <param name="recid">long word(int)，值传递，要被访问的记录ID.</param>
        /// <param name="ft">long word(int)，值传递，重复区域内事件编号为0的字段标识</param>
        /// <param name="frstoc">short word(short),值传递，开始阅读的事件编号</param>
        /// <param name="lastoc">shor word(short),值传递,结束的事件编号</param>
        /// <param name="datatype">short word(short),值传递，被定义在setcim.h头文件中的数据类型</param>
        /// <param name="ptdatas">type aligned address（obejct),引用传递out,阅读的数据</param>
        /// <param name="numok">short word(short),引用传递out,真正阅读的事件个数</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void RDBOCCS(int recid, int ft, short frstoc, short lastoc, short datatype, out object[] ptdatas, out short numok, ERRBLOCK error);

        /// <summary>
        /// 从一个数据库记录内阅读多个值
        /// </summary>
        /// <param name="recid">long word(int)，值传递，要被访问的记录ID</param>
        /// <param name="numvalus">short word(short),值传递，要读取的值的个数</param>
        /// <param name="ptfts">long word array(int[]),引用传递，一组要去阅读的字段标识</param>
        /// <param name="ptdtypes">short word array(short[]),引用传递，ptfts类字段的数据类型。</param>
        /// <param name="ptdatas">type aligned address(object[]),引用传递，读取的数据。通常是一个结构。</param>
        /// <param name="numok">short word(short),引用传递out,取到数据的个数</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void RDBVALS(int recid, short numvalus, ref int[] ptfts, ref short[] ptdtypes, out object[] ptdatas, out short numok, ERRBLOCK error);

        /// <summary>
        /// 将一个数字写入数据库。 (data type = DTYPREAL).
        /// </summary>
        /// <param name="recid">long word(int),值传递，包含数据的记录的记录ID</param>
        /// <param name="ft">long word(int),值传递，包含数据的字段的字段标识</param>
        /// <param name="realdata">single precision real(float),要被写入数据库的数据</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void REALADD2DB(int recid, int ft, float realdata, ERRBLOCK error);

        /// <summary>
        /// 获得数据库内最可用的记录ID
        /// </summary>
        /// <returns>long word(int),返回数据库内最可用ID</returns>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RECIDMAX();

        /// <summary>
        /// 如果给定的记录类型为给定的类型返回TRUE
        /// </summary>
        /// <param name="recid">long word(int),值传递，记录ID</param>
        /// <param name="rectype">long word(int),值传递，为recid定义的记录的记录ID，或表(记录类型标识(TWO)，README.md)中所标识的记录类型</param>
        /// <returns>integer(int),如果为指定的类型则返回一个非0值，反之则返回0</returns>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RECTYPEOK(int recid, int rectype);

        /// <summary>
        /// 将一个记录ID写入数据库(data type = DTYREID).
        /// </summary>
        /// <param name="recid">long word(int),值传递，包含字段的记录的记录ID</param>
        /// <param name="ft">long word(int),值传递，要被写入哪个字段的字段标识</param>
        /// <param name="iddata">long word(int),值传递，记录ID的值</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void REID2DB(int recid, int ft, int iddata, out ERRBLOCK error);

        /// <summary>
        /// 将一个快照磁盘文件读入数据库(warning:这个方法将覆盖当前数据库)
        /// </summary>
        /// <param name="ptfname">character array(byte[]),引用传递ref,包含文件名的byte串</param>
        /// <param name="numchars">short word(short),值传递，pftname的长度</param>
        /// <param name="blksin">long word(int),引用传递out,阅读的磁盘块个数</param>
        /// <param name="wordsin">long word(int),引用传递out,读取的单词个数</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void RESTORSNAP(ref byte[] ptfname, short numchars, out int blksin, out int wordsin, out ERRBLOCK error);

        /// <summary>
        /// 以有用的时候再对比改下
        /// </summary>
        /// <param name="timeweight"></param>
        /// <param name="step"></param>
        /// <param name="recid"></param>
        /// <param name="ft"></param>
        /// <param name="ptTimeOld"></param>
        /// <param name="ptTimeNew"></param>
        /// <param name="ptInterval"></param>
        /// <param name="timealign"></param>
        /// <param name="dsadjust"></param>
        /// <param name="maxperiods"></param>
        /// <param name="numtimecodes"></param>
        /// <param name="numdoublecodes"></param>
        /// <param name="numshortcodes"></param>
        /// <param name="timecodes"></param>
        /// <param name="doublecodes"></param>
        /// <param name="shortcodes"></param>
        /// <param name="timevalues"></param>
        /// <param name="doublevalues"></param>
        /// <param name="shortvalues"></param>
        /// <param name="numperiods"></param>
        /// <param name="error"></param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void RHIS21AGGREG(int timeweight, int step, int recid, int ft, ref XUSTS ptTimeOld, ref XUSTS ptTimeNew, ref XUSTS ptInterval, int timealign, int dsadjust, int maxperiods, int numtimecodes, int numdoublecodes, int numshortcodes, ref short[] timecodes, ref short[] doublecodes, ref short[] shortcodes, out XUSTS[] timevalues, out double[] doublevalues, out short[] shortvalues, out int numperiods, out ERRBLOCK error);

        /// <summary>
        /// 以有用时对比
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="step"></param>
        /// <param name="outsiders"></param>
        /// <param name="recid"></param>
        /// <param name="ft"></param>
        /// <param name="timeold"></param>
        /// <param name="timenew"></param>
        /// <param name="numfts"></param>
        /// <param name="fts"></param>
        /// <param name="datatypes"></param>
        /// <param name="maxoccs"></param>
        /// <param name="keylevels"></param>
        /// <param name="keytimes"></param>
        /// <param name="ptdatas"></param>
        /// <param name="ptoccsok"></param>
        /// <param name="ftsok"></param>
        /// <param name="error"></param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void RHIS21DATA(int mode, int step, int outsiders, int recid, int ft, ref XUSTS timeold, ref XUSTS timenew, short numfts, ref int[] fts, ref short[] datatypes, int maxoccs, out short[] keylevels, out XUSTS[] keytimes, out string[] ptdatas, out int ptoccsok, out short ftsok, out ERRBLOCK error);

        /// <summary>
        /// 阅读多个事件里的多个历史字段，按时间先后顺序（最早的最先开始）阅读，并且将读取的值存进多个数据组里面。同时阅读内存重复区域和磁盘历史重复区域。
        /// </summary>
        /// <param name="spare1">Integer(int)，值传递，目前未使用，应该为0</param>
        /// <param name="spare2">Integer(int)，值传递，目前未使用，应该为0</param>
        /// <param name="spare3">Integer(int)，值传递，目前未使用，应该为0</param>
        /// <param name="spare4">Integer(int)，值传递，目前未使用，应该为0</param>
        /// <param name="ecid">long word(int),值传递，包含历史重复区域的记录ID</param>
        /// <param name="ft">long word(int),值传递，历史重复区域内字段的字段标识（必须有一个可用的事件编号，例如0）或一个计数字段标签（必须有一个事件编号）</param>
        /// <param name="timenew">XUSTS(XUSTS),引用传递ref,是一个扩展的时间戳单位微秒.从啥时候开始阅读</param>
        /// <param name="timeold">XUSTS(XUSTS),引用传递ref,是一个扩展的时间戳单位微秒.到哪个时间结束</param>
        /// <param name="numfts">short word(short)，值传递，从每个事件里阅读的事件个数。是fts,ptdatas的成员个数</param>
        /// <param name="fts">long word array(int[]),引用传递ref,一组要去阅读的字段标识。</param>
        /// <param name="datatypes">short word array(short[]),引用传递ref,一组要去阅读的字段字段类型</param>
        /// <param name="maxoccs">long word(int),值传递，要阅读的最大事件个数。</param>
        /// <param name="keylevels">short word array(short[]),引用传递out,</param>
        /// <param name="keytimes">XUSTS array(XUSTS[]),引用传递out,</param>
        /// <param name="ptdatas">pointer array(string[]),引用传递out,一个字符串数组，每个字段对应一个字符串</param>
        /// <param name="occsok">long word(int),引用传递out,返回阅读的事件个数。</param>
        /// <param name="ftsok">short word(short),引用传递out,返回阅读的字段个数。</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void RHIS21REV(int spare1, int spare2, int spare3, int spare4, int ecid, int ft, ref XUSTS timenew, ref XUSTS timeold, short numfts, ref int[] fts, ref short[] datatypes, int maxoccs, out short[] keylevels, out XUSTS[] keytimes, out string[] ptdatas, out int occsok, out short ftsok, out ERRBLOCK error);

        /// <summary>
        /// 返回根文件夹的ID
        /// </summary>
        /// <returns>返回根文件夹的ID</returns>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ROOTFOLDER();

        /// <summary>
        /// 将一个数据库快照写入一个磁盘文件
        /// </summary>
        /// <param name="ptfname">character array(byte[]),引用传递ref,包含字段名的byte串</param>
        /// <param name="numchars">short word(short)，值父传递，ptfname的长度</param>
        /// <param name="blksout">long word(int),引用传递out,写入的磁盘块数</param>
        /// <param name="wordsin">long word(int),引用传递out,数据库写入磁盘的单词个数</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SAVESNAP(ref short[] ptfname, short numchars, out int blksout, out int wordsin, out ERRBLOCK error);

        /// <summary>
        /// 将一个short类型数据写入数据库 (data type = DTYPSHRT).
        /// </summary>
        /// <param name="recid">long word(int),值传递，包含字段ft的记录的记录ID</param>
        /// <param name="ft">long word(int),值传递，数据要写入的字段的字段标识</param>
        /// <param name="shrtdata">short word(short),值传递，要写入的数据</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SHRT2DB(int recid, int ft, short shrtdata, out ERRBLOCK error);

        /// <summary>
        /// 将一个时间戳转换为当前InfoPlus.21ASCII时间日期格式
        /// </summary>
        /// <param name="timestmp">long word(int),值传递，要格式化的时间戳</param>
        /// <param name="ptbuff">character(short),引用传递，返回的格式化时间</param>
        /// <param name="sizebuff">short word(short),值传递，指定ptbuff的大小。有效大小为15,18,20。设置为其它将导致出错并设为1</param>
        /// <param name="error">byte(byte),引用传递(out),0没错，1有错</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TMST2ASCII(int timestmp, out short ptbuff, short sizebuff, out byte error);

        /// <summary>
        /// 将一个AspenInfoPlus.21时间戳转换为相应的扩展时间戳。
        /// </summary>
        /// <param name="timestamp">long word(int),值传递，要被转换的时间戳。</param>
        /// <param name="xts">XTSBLOCK(XTSBLOCK),引用传递（out),转换完的结果</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TS2XTS(int timestamp, XTSBLOCK xts);

        /// <summary>
        /// 检查一个记录是不是可用状态
        /// </summary>
        /// <param name="recid">long word(int),要检查的记录的记录ID</param>
        /// <returns>若记录可用返回true</returns>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte VALIDUSA(int recid);

        /// <summary>
        /// 将输入转换成ASCII并将它写入数据库字段中。将转换成相应的字段的字段类型。
        /// </summary>
        /// <param name="recid">long word(int),值传递，包含数据的数据ID</param>
        /// <param name="ft">long word(int),值传递，包含数据的字段的字段标识</param>
        /// <param name="ptbuff">character array(byte[]),引用传递ref,包含ASCII数据的byte数组</param>
        /// <param name="numchars">short word(short),值传递，指定ptbuff的字符个数</param>
        /// <param name="operat">byte(byte),值传递，如果为true,o.o</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void WDBASCII(int recid, int ft, ref byte[] ptbuff, short numchars, byte operat, out ERRBLOCK error);

        /// <summary>
        /// 将一个值写入数据库中一个记录内一个字段的多个事件中
        /// </summary>
        /// <param name="recid">long word(int),值传递，要读取的记录的记录ID</param>
        /// <param name="ft">long word(int),值传递，重复区域的字段的字段标识并且它的事件编号设为0.如果这个字段是一个COS字段指针并且如果每个事件被写入的数据一致，激活的生成将一致</param>
        /// <param name="frstoc">short word(short),值传递,从哪个事件编号开始写</param>
        /// <param name="lastoc">short word(short),值传递，写到哪个事件编号结束</param>
        /// <param name="datatype">short word(short),值传递，字段的数据类型（被定义在sectim.h头文件中）。</param>
        /// <param name="ptdatas">type aligned address(object[]),引用传递ref,要写入的数据.数据的边界值是连续的。</param>
        /// <param name="numok">short word（short),引用传递out,实际被写入数据的事件个数</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void WDBOCCS(int recid, int ft, short frstoc, short lastoc, short datatype, ref object[] ptdatas, out short numok, out ERRBLOCK error);

        /// <summary>
        /// 将多个数据写进数据库内一个字段中
        /// </summary>
        /// <param name="recid">long word(int),值传递，要读取的记录的记录ID</param>
        /// <param name="numvalus">short word(short),值传递，要写的数据的个数</param>
        /// <param name="ptfts">long word array(int[]),引用传递ref,一串要被写入数据的字段的字段标识。</param>
        /// <param name="ptdtypes">short word array(short[]),引用传递ref,ptfts中字段的字段类型。</param>
        /// <param name="ptdatas">type aligned address(object[]),引用传递ref,要写入的数据。 </param>
        /// <param name="numok">short word(short),引用传递out,写入成功的数据个数。</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void WDBVALS(int recid, short  numvalus, ref int[] ptfts, ref short[] ptdtypes, ref object[] ptdatas, out short numok, out ERRBLOCK error);

        /// <summary>
        /// 写多个历史字段的一个
        /// </summary>
        /// <param name="mode">integer(int),值传递，请求的类型。有效的值:WHIS_TYPE_MODIFY − Modify,WHIS_TYPE_ADDNEW,WHIS_TYPE_UPDATE(存在则修改，不存在则添加)</param>
        /// <param name="recid">long word(int)，值传递，包含历史重复区域的记录的记录ID</param>
        /// <param name="ft">long word(int),值传递，历史重复区域的中的字段的字段标识 </param>
        /// <param name="numfts">short word(short),值传递，每个事件中被写入的事件个数。</param>
        /// <param name="fts">long word array(int[]),一组字段的字段标识。</param>
        /// <param name="datatypes">short word array(short[]),一组数据类型</param>
        /// <param name="ptdatas">pointer array(string[]),引用传递ref,一组要被写入的数据</param>
        /// <param name="keylevel">short word(short),值传递，要被写入事件的优先级别。若没设置则默认为-1</param>
        /// <param name="keytime">XUSTS(XUSTS),引用传递ref，事件被更新或创建的时间</param>
        /// <param name="ftsok">short word（short),引用传递out,返回被写入的字段个数</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void WHIS21DAT(int mode, int  recid, int  ft, short numfts, ref int[] fts, ref short[] datatypes, ref string[] ptdatas, short keylevel, XUSTS keytime, short  ftsok, out ERRBLOCK error);

        /// <summary>
        /// 将一个外部时间写入数据库
        /// </summary>
        /// <param name="recid">long word(int),包含字段的记录的记录ID</param>
        /// <param name="ft">long word(int),值传递，要被写入字段的字段标识</param>
        /// <param name="xtsdata">XTSBLOCK(XTSBLOCK),引用传递ref,要写入的时间戳</param>
        /// <param name="error">ERRBLOCK(ERRBLOCK),引用传递（out），返回在setcim.h中定义的错误编码</param>
        /// <returns></returns>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte XTIM2DB(int recid, int ft, ref XTSBLOCK xtsdata, out ERRBLOCK error);

        /// <summary>
        /// 将一个外部时间戳转换为当前AspenInfoPlus.21ASCII时间日期格式
        /// </summary>
        /// <param name="xts">XTSBLOCK(XTSBLOCK),引用传递ref,要被格式化的时间戳</param>
        /// <param name="ptbuff">character array(byte[]),引用传递out,格式化后的时间戳</param>
        /// <param name="sizebuff">short word(short),值传递，指定ptbuff的字符个数。有效大小为15,18,20.任何其它大小都被设置为1</param>
        /// <param name="error">byte(byte),引用传递out,没错则为0，反之则为1</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void XTS2ASCII(ref XTSBLOCK xts, out byte[] ptbuff, short sizebuff, out byte error);

        /// <summary>
        /// 将一个AspenInfoPlus.21扩展时间戳转换为当前的天与时间。这个方法自动解决转换中出现的偏差
        /// </summary>
        /// <param name="xts">XTSBLOCK(XTSBLOCK),引用传递ref,要被转换的外部时间戳</param>
        /// <param name="dspdate">long word(int),引用传递out,返回指定时间所在当前世纪第几天</param>
        /// <param name="dsptime">long word(int),引用传递out,返回在第几天的的第几秒（精确到0.1)</param>
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void XTS2DSPDT(ref XTSBLOCK xts, out int dspdate, out int dsptime);
        #endregion

        ////读统计值
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        //        public static extern void RHIS21AGGREG(int timeweight, int step, int recid, int ft, ref XUSTS ptTimeOld, ref XUSTS ptTimeNew, ref XUSTS ptInterval, int timealign, int dsadjust, int maxperiods, int numtimecodes, int numdoublecodes, int numshortcodes, short[] timecodes, short[] doublecodes, short[] shortcodes, out XUSTS[,] timevalues, out double[,] doublevalues, out short[] shortvalues, out int numperiods, out ERRBLOCK err);
        public static extern void RHIS21AGGREG(int timeweight, int step, int recid, int ft, ref XUSTS ptTimeOld, ref XUSTS ptTimeNew, ref XUSTS ptInterval, int timealign, int dsadjust, int maxperiods, int numtimecodes, int numdoublecodes, int numshortcodes, short[] timecodes, short[] doublecodes, short[] shortcodes, IntPtr timevalues, IntPtr doublevalues, IntPtr shortvalues, out int numperiods, out ERRBLOCK err);

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

        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void RHIS21DATA(int mode, int step, int outSiders, int tagId, int propertyId, ref XUSTS startTime, ref XUSTS endTime, short numfts, int[] fts, short[] datatypes, int maxoccus, short[] keylevels, XUSTS[] keyTimes, IntPtr[] ptdatas, out int occsok, out short ftsok, out ERRBLOCK errMsg);

        //断开
        [DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DaShutdown();

        #region

        ////在程序退出之前调用ENDSETC() 
        ////关闭所有服务节点连接 消毁服务列表函数并释放所有资源
        ////程序不应该调用它除非当程序退出时
        //[DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        //public static extern void ENDSETC();

        ////连接到一个或多个节点 通过阅读配置文件创建一个内部节点列表，
        ////并且连接至所有节点 
        ////若至少成功连接一个节点返回1 若连接失败返回0
        //[DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        //public static extern bool INISETC();

        ////得到时间
        //[DllImport("infoplus21_api.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        //public static extern void GETDBXTIM(out XTSBLOCK current_xts);

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

        ////解析错误信息
        //[DllImport("infoplus21_api.dll", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        //public static extern void ERRMESS(ref ERRBLOCK errMsg, out byte[] err, out int errsz);
        #endregion

    }
}
