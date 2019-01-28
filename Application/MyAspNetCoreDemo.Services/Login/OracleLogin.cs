using System;
using System.Collections.Generic;
using System.Text;

namespace MyAspNetCoreDemo.Services
{
    public class OracleLogin: ILogin
    {
        public string Login(string userName, string password)
        {
            return $"{userName}使用oracle数据库登录";
        }
    }
}
