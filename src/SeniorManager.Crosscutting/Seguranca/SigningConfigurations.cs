using SeniorManager.Crosscutting.Interfaces;
using System;
using System.Security.Cryptography;
using System.Text;


namespace SeniorManager.Crosscutting.Seguranca
{
    public class SigningConfigurations : ISigningConfigurations
    {
        private readonly byte[] key = new byte[8] { 8, 16, 6, 12, 0x00, 3, 21, 1 };
        private readonly byte[] iv = new byte[8] { 8, 16, 6, 12, 0x00, 3, 21, 1 };

        public string GerarHash(string stringValue)
        {
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform transform = algorithm.CreateEncryptor(key, iv);
            byte[] inputbuffer = Encoding.UTF8.GetBytes(stringValue);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Convert.ToBase64String(outputBuffer);
        }

        public bool VerificarHash(string value, string hash)
        {
            var hashValue = ConverterHash(hash);
            return value == hashValue;
        }

        public string ConverterHash(string value)
        {
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform transform = algorithm.CreateDecryptor(key, iv);
            byte[] inputbuffer = Convert.FromBase64String(value);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Encoding.UTF8.GetString(outputBuffer);
        }
    }
}
