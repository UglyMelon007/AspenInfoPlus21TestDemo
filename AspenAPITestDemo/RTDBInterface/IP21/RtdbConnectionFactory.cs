using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Configuration;
using System.Web;

namespace RTDB.IP21
{
    public class RtdbConnectionFactory
    {
        private static Dictionary<string, Connection> connections = new Dictionary<string, Connection>();
        private static RtdbConnectionFactory _rtdbConnectionFactory;
        private static Dictionary<string, string> connStringIPMap = new Dictionary<string, string>();

        private RtdbConnectionFactory()
        {
            //初始化connections,使用配制文件
            string xmlPath = AppDomain.CurrentDomain.BaseDirectory + "\\Config\\rtdbConnection.xml";
            DataSet ds = new DataSet();
            ds.ReadXml(xmlPath);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dataRow in dt.Rows)
            {
                if (!connections.ContainsKey(dataRow[0].ToString()))
                {
                    connections.Add(dataRow[0].ToString(), Connection.OpenConnection(dataRow[0].ToString()));
                }
            }
        }

        public Connection GetConnection()
        {
            return GetConnection(getServerIp());
        }

        //根据应用的配置文件获取默认的ip地址
        private string getServerIp()
        {
            string result = "10.5.5.43";//默认ip地址
            try
            {
                HttpContext ht = HttpContext.Current;
                string connectionString = ht == null ? Connection.currentServerIp : ht.Session["RTDBconnectionString"].ToString();
                if (connectionString != null)
                {
                    result = connectionString;
                }
                else
                {
                    result = ConfigurationManager.AppSettings["Connectionstring1"];
                }
            }
            catch (Exception)
            {
                result = ConfigurationManager.AppSettings["Connectionstring1"];
            }
            result = TranslateConnString2Ip(result);
            return result;
        }

        private string TranslateConnString2Ip(string result)
        {
            if (connStringIPMap.Count == 0)
            {
                connStringIPMap.Add("Dsn=pen163".ToUpper(), "10.5.24.163");
                connStringIPMap.Add("Dsn=pen43".ToUpper(), "10.5.5.43");
                connStringIPMap.Add("Dsn=diesel".ToUpper(), "10.5.24.164");
                connStringIPMap.Add("Dsn=pen165".ToUpper(), "10.5.24.165");
                connStringIPMap.Add("Dsn=pen164".ToUpper(), "10.5.24.164");
                connStringIPMap.Add("dsn=aspen".ToUpper(), "10.5.24.165");
                connStringIPMap.Add("dsn=petrol".ToUpper(), "10.5.5.43");
                connStringIPMap.Add("dsn=cata".ToUpper(), "10.5.24.165");
                connStringIPMap.Add("dsn=coaloil".ToUpper(), "10.5.5.43");
                connStringIPMap.Add("Dsn=coke".ToUpper(), "10.5.24.165");
                connStringIPMap.Add("Dsn=cokegas".ToUpper(), "10.5.5.43");
                connStringIPMap.Add("Dsn=penhydro2".ToUpper(), "10.5.5.43");
                connStringIPMap.Add("Dsn=press".ToUpper(), "10.5.24.165");
                connStringIPMap.Add("dsn=regn".ToUpper(), "10.5.24.165");
                connStringIPMap.Add("dsn=resid".ToUpper(), "10.5.5.43");
                connStringIPMap.Add("dsn=szorb".ToUpper(), "10.5.24.165");
                connStringIPMap.Add("dsn=vgo".ToUpper(), "10.5.24.164");
                connStringIPMap.Add("dsn=sru".ToUpper(), "10.5.24.164");
                connStringIPMap.Add("dsn=pen8".ToUpper(), "10.246.101.8");
            }
            if (connStringIPMap.ContainsKey(result.ToUpper()))
                return connStringIPMap[result.ToUpper()];
            else
                return result;

        }

        public Connection GetConnection(string serverIp)
        {
            serverIp = TranslateConnString2Ip(serverIp);
            if (connections.ContainsKey(serverIp))
            {
                return connections[serverIp];
            }
            else
            {
                throw new Exception("初始化的ip地址中不包含" + serverIp + "，请检查配置文件中的serverip项中是否包含此ip地址。");
            }
        }

        public static RtdbConnectionFactory GetRtdbConnectionFactory()
        {
            if (_rtdbConnectionFactory == null)
                _rtdbConnectionFactory = new RtdbConnectionFactory();
            return _rtdbConnectionFactory;
        }

        public void CloseAllConnection()
        {
            foreach (var connection in connections)
            {
                connection.Value.Close();
            }
        }
    }
}
