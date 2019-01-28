using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreDemo.Services;

namespace MyAspNetCoreDemo.Web.Controllers
{

    public class ValuesController : BaseController
    {
        private readonly ILogin _login;

        private readonly IEncryptionService _encryptionService;
        //多个注入
        public ValuesController(IEnumerable<ILogin>  login, IEncryptionService encryptionService)
        {
            _login = login.First();
            _encryptionService = encryptionService;
        }


        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            string encryptStr = _encryptionService.Encrypt("嗯");
            string decryptStr = _encryptionService.Decrypt(encryptStr);
            string str= _login.Login("小朱", "");
            return new string[] { str, "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(string input)
        {
            string encryptStr =_encryptionService.Encrypt(input);
            string decryptStr = _encryptionService.Decrypt(encryptStr);
            return $"加密:{encryptStr},解密:{decryptStr}";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
