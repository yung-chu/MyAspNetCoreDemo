using System;

namespace MyAspNetCoreDemo.Services
{
    public interface ILogin
    {
        string Login(string userName,string password);
    }
}
