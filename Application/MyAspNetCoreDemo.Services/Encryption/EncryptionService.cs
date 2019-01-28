using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.DataProtection;

namespace MyAspNetCoreDemo.Services
{
    public class EncryptionService: IEncryptionService
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private const string Key = "cxz92k13md8f981hu6y7alkc";

        public EncryptionService(IDataProtectionProvider dataProtectionProvider)
        {
            _dataProtectionProvider = dataProtectionProvider;
        }

        public string Encrypt(string input)
        {
            var protector = _dataProtectionProvider.CreateProtector(Key);
            return protector.Protect(input);
        }

        public string Decrypt(string cipherText)
        {
            var protector = _dataProtectionProvider.CreateProtector(Key);
            return protector.Unprotect(cipherText);
        }
    }
}
