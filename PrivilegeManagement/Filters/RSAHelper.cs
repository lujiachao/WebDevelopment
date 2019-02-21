using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using XC.RSAUtil;

namespace PrivilegeManagement.Filters
{
    public class RSAHelper
    {
        private static RSAUtilBase _RSAUtilBase;

        public RSAHelper(RSAUtilBase rSAUtilBase)
        {
            _RSAUtilBase = rSAUtilBase;
        }

        public static string RSAEncryupt(RSAUtilBase rSAUtilBase, RSAEncryptionPadding rSAEncryptionPadding, string data) => _RSAUtilBase.Encrypt(data, rSAEncryptionPadding);

        public static string RSADecrypt(RSAUtilBase rSAUtilBase, RSAEncryptionPadding rSAEncryptionPadding, string data) => _RSAUtilBase.Decrypt(data, rSAEncryptionPadding);

        public static string RSASignData(RSAUtilBase rSAUtilBase, HashAlgorithmName hashAlgorithmName, RSASignaturePadding rSASignaturePadding, string data) => _RSAUtilBase.SignData(data, hashAlgorithmName, rSASignaturePadding);

        public static bool RSAVerifyData(RSAUtilBase rSAUtilBase, HashAlgorithmName hashAlgorithmName, RSASignaturePadding rSASignaturePadding, string data, string sign) =>
            _RSAUtilBase.VerifyData(data,sign, hashAlgorithmName, rSASignaturePadding);

    }
}
