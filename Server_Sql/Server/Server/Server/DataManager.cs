using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 用户
/// </summary>
public class UserBase
{
    public ServerUser serverUser;  //服务器端
    public int ID;
    public int password;
    public UserBase(int id,int password, ServerUser serverUser)
    {
        this.ID = id;
        this.password = password;
        this.serverUser = serverUser;
    }
}
public class DataManager:Single<DataManager>
{

    public List<UserBase> allUserList = new List<UserBase>();
    public void CreatUser(int id, int password, ServerUser serverUser)
    {
        UserBase userBase = new UserBase(id,password,serverUser);
        allUserList.Add(userBase);
    }
    public void RemoveUser(int id)
    {
        allUserList.ForEach(temp =>
        {
            if (temp.ID==id)
            {
                temp.serverUser.Close();
                allUserList.Remove(temp);
            }
        });
    }
    /// <summary>
    /// 是否已在线
    /// </summary>
    /// <returns></returns>
    public bool isOnlined(int id)
    {
        if (allUserList.Find(temp=>temp.ID==id)!=null)
        {
            return true;
        }
        return false;
    }
}

