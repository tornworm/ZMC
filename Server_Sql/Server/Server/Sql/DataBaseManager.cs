using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class DataBaseManager : Single<DataBaseManager>
{
    //数据库IP地址
    private string IP = "127.0.0.1";
    //数据库端口号
    private int port = 3306;
    //数据库用户名
    private string username = "root";
    //数据库密码
    private string password = "root";
    //数据库模式
    private string schema = "user";
    //数据库连接
    MySqlConnection sqlConnection;
    /// <summary>
    /// 数据库选型
    /// </summary>
    internal enum DataBaseType
    {
        MySQL = 1,
        Orcale = 2,
        SQLServer = 3
    }
    /// <summary>
    /// 数据库选库
    /// </summary>
    public enum DataBaseSchema
    {
        user = 1,//用户数据库
        acution = 2,//拍卖行数据库
        map = 3,//服务器地图数据库
    }
    public void InitSQL()
    {

        Connect();

    }
    //数据库普通连接
    public void Connect()
    {
        string conStr =
                    "server=" + IP
                  + ";port=" + port
                  + ";database=" + schema
                  + ";user=" + username
                  + ";password=" + password
                  + ";";
        sqlConnection = new MySqlConnection(conStr);
        sqlConnection?.Open();
        Console.WriteLine("连接成功");
    }
    /// <summary>
    /// 数据库选库连接
    /// </summary>
    /// <param name="dataBaseSchema"></param>
    public void Connect(DataBaseSchema dataBaseSchema)
    {
        //暂时一个库
    }
    /// <summary>
    /// 关闭数据库
    /// </summary>
    public void Close()
    {

        sqlConnection?.Close();

    }
    
    /// <summary>
    /// 数据库查询(根据ID查找所有属性)
    /// </summary>
    /// <param name="库名"></param>
    /// <param name="表名"></param>
    /// <param name="ID"></param>
    public List<string> Inquiry(string table, int ID)
    {
        List<string> list = new List<string>();
        list.Clear();
        MySqlCommand cmd = new MySqlCommand("SELECT * FROM " + table + " WHERE ID=" + ID, sqlConnection);
        MySqlDataReader reader = cmd.ExecuteReader();

        //string str = "";
        while (reader.Read())
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {

               // str += reader.GetString(i);
                list.Add(reader.GetString(i));
            }

        }
        return list;
    }
    public void Insert()
    {
        var ss = from r in db.Am_recProScheme
                 select r;
    }
}


