using System;
using System.Collections.Generic;
using System.Text;

namespace MyAspNetCoreDemo.Services
{
    public class SqlServerLogin: ILogin
    {
        public string Login(string userName, string password)
        {
            return $"{userName}使用sqlServer数据库登录";
        }
    }
}
