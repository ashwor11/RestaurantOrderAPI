﻿using OtpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Vertification.OtpVertification.OtpNet
{
    public class OtpNetVertificationHelper : IOtpVertificationHelper
    {
        public Task<string> ConvertSecretKeyToString(byte[] secretKey)
        {
            string base32String = Base32Encoding.ToString(secretKey);
            return Task.FromResult(base32String);
        }

        public Task<byte[]> GenerateSecretKey()
        {
            byte[] key = KeyGeneration.GenerateRandomKey(20);
            string base32String = Base32Encoding.ToString(key);
            byte[] base32Bytes = Base32Encoding.ToBytes(base32String);

            return Task.FromResult(base32Bytes);
        }

        public Task<bool> VerifyCode(byte[] secretKey, string code)
        {
            Totp totp = new(secretKey);
            string totpCode = totp.ComputeTotp(DateTime.UtcNow);
            bool result = totpCode.Equals(code);
            return Task.FromResult(result);
        }

        public string CreateOtpUri(string secretKey, string email, string applicationName)
        {
            OtpUri uri = new(OtpType.Totp,secretKey, email, applicationName);
            return uri.ToString();
        }
    }
}
