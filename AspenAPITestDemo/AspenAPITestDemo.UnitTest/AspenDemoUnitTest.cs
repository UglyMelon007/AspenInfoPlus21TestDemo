using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RTDB.IP21;

namespace AspenAPITestDemo.UnitTest
{
    [TestClass]
    public class AspenDemoUnitTest
    {
        private Connection _connection;
        private string _tagName = "TIJ_HCU01_TI3363";

        [TestInitialize]
        public void Initial()
        {
            var factory = RtdbConnectionFactory.GetRtdbConnectionFactory();
            _connection = factory.GetConnection();
        }

        /// <summary>
        /// 获取当前值(测试成功）
        /// </summary>
        [TestMethod]
        public void GetCurrentDataUnitTest()
        {
            var result = _connection.GetCurrentData(new[] { "TIJ_HCU01_TI3363" });
        }

        /// <summary>
        /// RHIS21DATA(从历史数据库取实际值)
        /// </summary>
        [TestMethod]
        public void GetActualsUnitTest()
        {
            var resutl = _connection.GetActuals(_tagName, new DateTime(2005, 04, 16, 12, 54, 56), 100);
            Console.WriteLine(resutl.ToString());
        }

        /// <summary>
        /// ASCIIDB2I
        /// </summary>
        [TestMethod]
        public void ASCIIDB2IUnitTest()
        {
            var result = _connection.ASCIIDB2I(_tagName);
        }

        /// <summary>
        /// CHBF2DB
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
            _connection.CHKFREE(_tagName);
        }

        /// <summary>
        /// CHKFTREC（检测一个记录是否存在一个指定的字段内）,测试成功
        /// </summary>
        [TestMethod]
        public void CHKFTRECUnitTest()
        {
            _connection.CHKFTREC(_tagName);
        }

        /// <summary>
        ///COPYREC (将一个记录复制到另一个记录内（另一个记录得存在)
        /// </summary>
        [TestMethod]
        public void COPYRECUnitTest()
        {
            _connection.COPYREC(_tagName);
        }

        /// <summary>
        /// CREATEREC(使用定义的记录和ID创建一个新的记录)
        /// </summary>
        [TestMethod]
        public void CREATERECUnitTest()
        {
            _tagName = "hellotestapi";
            _connection.CREATEREC(_tagName);
        }

        /// <summary>
        /// D2ASCIIDB(将一个真实值转换为由指定数据库字段指定的格式的ASCII
        /// </summary>
        public void D2ASCIIDBUnitTest()
        {
            ulong recid = 1234;
            ulong ft = 1235;
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

        [TestMethod]
        public void DB2DUBLUnitTest()
        {
            _connection.DB2DUBL(_tagName, "IP_Value");
        }
    }
}
