using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RTDB.IP21;

namespace AspenAPITestDemo.UnitTest
{
    [TestClass]
    public class AspenDemoUnitTest
    {
        private Connection _connection;
        private string _recordName = "TIJ_HCU01_TI3363";
        private string _fieldName = "IP_Value";

        [TestInitialize]
        public void Initial()
        {
            var factory = RtdbConnectionFactory.GetRtdbConnectionFactory();
            _connection = factory.GetConnection();
        }

        /// <summary>
        /// (获取当前值）测试成功
        /// </summary>
        [TestMethod]
        public void GetCurrentDataUnitTest()
        {
            Dictionary<string, string> result = _connection.GetCurrentData(new[] { "TIJ_HCU01_TI3363" });
            foreach (KeyValuePair<string, string> keyValuePair in result)
            {
                Console.WriteLine(keyValuePair.Key + ":" + keyValuePair.Value);
            }
        }

        /// <summary>
        /// RHIS21DATA(从历史数据库取实际值)
        /// </summary>
        [TestMethod]
        public void GetActualsUnitTest()
        {
            Console.WriteLine(_connection.GetActuals(_recordName, new DateTime(2005, 04, 16, 12, 54, 56), 100));
        }

        /// <summary>
        /// ASCIIDB2I
        /// </summary>
        [TestMethod]
        public void ASCIIDB2IUnitTest()
        {
            var result = _connection.ASCIIDB2I(_recordName);
        }

        /// <summary>
        /// CHBF2DB(将一串字符或数据缓存写入数据库字段中。)
        /// </summary>
        [TestMethod]
        public void CHBF2DBUnitTest()
        {
            _connection.CHBF2DB("hello");
        }

        /// <summary>
        /// CHKFREE（检测一个记录ID是否可用） 测试成功 
        /// </summary>
        [TestMethod]
        public void CHKFREEUnitTest()
        {
            _connection.CHKFREE(_recordName);
        }

        /// <summary>
        /// CHKFTREC（检测一个记录是否存在一个指定的字段内）测试成功
        /// </summary>
        [TestMethod]
        public void CHKFTRECUnitTest()
        {
            _connection.CHKFTREC(_recordName);
        }

        /// <summary>
        ///COPYREC (将一个记录复制到另一个记录内（另一个记录得存在)
        /// </summary>
        [TestMethod]
        public void COPYRECUnitTest()
        {
            _connection.COPYREC(_recordName);
        }

        /// <summary>
        /// CREATEREC(使用定义的记录和ID创建一个新的记录)
        /// </summary>
        [TestMethod]
        public void CREATERECUnitTest()
        {
            _recordName = "hellotestapi";
            _connection.CREATEREC(_recordName);
        }

        /// <summary>
        /// D2ASCIIDB(将一个真实值转换为由指定数据库字段指定的格式的ASCII
        /// </summary>
        public void D2ASCIIDBUnitTest()
        {
            int recid = 1234;
            int ft = 1235;
            double realData = 123.123;
            _connection.D2ASCIIDB(recid, ft, realData);
        }

        /// <summary>
        /// DATA2ASCII( 将一个值通过给定的格式化记录转换为指定的ASCI)
        ///一些参数不明白
        /// </summary>
        [TestMethod]
        public void DATA2ASCIIUnitTest()
        {
            _connection.DATA2ASCII("hello");
        }

        /// <summary>
        /// DB2DUBL(从数据库中读取一个双精度的数据 ）测试成功
        /// </summary>
        [TestMethod]
        public void DB2DUBLUnitTest()
        {
            Console.WriteLine(_connection.DB2DUBL(_recordName, _fieldName));
        }

        [TestMethod]
        public void DB2REALUnitTest()
        {
            Console.WriteLine(_connection.GetValue(_recordName));
        }

        /// <summary>
        /// DB2IDFT(从数据库中读取一个记录ID和字段标识)
        /// </summary>
        [TestMethod]
        public void DB2IDFTUnitTest()
        {
            _connection.DB2IDFT(_recordName, _fieldName);
        }

        /// <summary>
        ///DECODRAF (将一个记录名及其相关联的字段名解码为其相应的记录ID和字段标识)
        /// </summary>
        [TestMethod]
        public void DECODRAFUnitTest()
        {
            _connection.DECODRAF(_recordName, _fieldName);
        }

        [TestMethod]
        public void GETNMFDBUnitTest()
        {
            _connection.GETNMFDB(_recordName, _fieldName);
        }

        /// <summary>
        /// DEFINID (判断记录ID是否有效,若为0，则这个记录ID是效的) 测试成功
        /// </summary>
        [TestMethod]
        public void DEFINIDUnitTest()
        {
            Console.WriteLine(_connection.DEFINID(_recordName));
        }

        /// <summary>
        /// DELETREC(删除一个记录，这个记录必须为不可用状态) 测试成功
        /// </summary>
        [TestMethod]
        public void DELETRECUnitTest()
        {
            _connection.DELETREC(_recordName);
        }

        /// <summary>
        /// DSPDT2XTS( 将一个“day of century"时间格式化为一个Aspen InfoPlus.21扩展时间戳)测试成功
        /// </summary>
        [TestMethod]
        public void DSPDT2XTSUnitTest()
        {
            int day = 123;
            int time = 311;
            var result = _connection.DSPDT2XTS(day, time);
            Console.WriteLine(result.XTSFAST + "\r\n" + result.XTSSLOW);
        }
    }
}
