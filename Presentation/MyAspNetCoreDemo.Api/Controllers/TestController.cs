using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreDemo.Services;

namespace MyAspNetCoreDemo.Web.Controllers
{

    public class TestController : BaseController
    {
        private readonly ILogin _login;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IEncryptionService _encryptionService;
        //多个注入
        public TestController(IEnumerable<ILogin>  login, IEncryptionService encryptionService, IHostingEnvironment hostingEnvironment)
        {
            //_login = login.First();
            _encryptionService = encryptionService;
            _hostingEnvironment = hostingEnvironment;
        }


        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {

         //   RequestServices.GetRequiredService<ILogin>()



            string encryptStr = _encryptionService.Encrypt("嗯");
            string decryptStr = _encryptionService.Decrypt(encryptStr);
           // string str= _login.Login("小朱", "");
            return new string[] { "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(string input)
        {
            string encryptStr =_encryptionService.Encrypt(input);
            string decryptStr = _encryptionService.Decrypt(encryptStr);
            return $"加密:{encryptStr},解密:{decryptStr}";
        }

        /// <summary>
        /// 文件流下载
        /// </summary>
        /// <returns></returns>
        [HttpGet("file")]
        public async Task<ActionResult> GetFile()
        {
            var path = Path.Combine(_hostingEnvironment.WebRootPath, "sa.txt");
            var bytes = await GetFilByte(path);

            return File(bytes, "application/octet-stream", "sa.txt");
        }

        private async Task<byte[]> GetFilByte(string path)
        {
            using (FileStream fsRead = new FileStream(path, FileMode.Open))
            {
                //2.创建缓冲区
                byte[] bytes = new byte[fsRead.Length];
                //3.开始读取， 返回值是读取到的长度。
                await fsRead.ReadAsync(bytes, 0, bytes.Length);
                return bytes;
            }
        }
    }
}
