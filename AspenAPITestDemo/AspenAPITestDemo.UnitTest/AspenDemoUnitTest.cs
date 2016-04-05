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
        //private static Dictionary<string, RTDBHelper> connections = new Dictionary<string, RTDBHelper>();

        //[TestInitialize]
        //public void RtdbConnectionFactory()
        //{
        //    //初始化connections,使用配制文件
        //    string xmlPath = AppDomain.CurrentDomain.BaseDirectory + "\\Config\\rtdbConnection.xml";
        //    DataSet ds = new DataSet();
        //    ds.ReadXml(xmlPath);
        //    DataTable dt = ds.Tables[0];
        //    foreach (DataRow dataRow in dt.Rows)
        //    {
        //        if (!connections.ContainsKey(dataRow[0].ToString()))
        //        {
        //            connections.Add(dataRow[0].ToString(), RTDBHelper.OpenConnection(dataRow[0].ToString()));
        //        }
        //    }
        //}

        [TestMethod]
        public void GetActualsUnitTest()
        {
            var v = RtdbConnectionFactory.GetRtdbConnectionFactory();
            var c = v.GetConnection();
            c.GetActuals("TIJ_HCU01_TI3363", new DateTime(2005, 04, 16, 12, 54, 56), 3000);
        }
    }
}
