using System.Security.Cryptography;

namespace kotl.RSAkeys
{
    public static class keyGetterRSA
    {
       public static RSA GetPublicKey()
        {
            var rsaReadFile = File.ReadAllText("RSAkeys/private_key.pem");
            var rsa = RSA.Create();
            rsa.ImportFromPem(rsaReadFile);
            return rsa;
        }
        public static RSA GetPrivateKey()
        {
            var rsaReadFile = File.ReadAllText("RSAkeys/private_key.pem");
            var rsa = RSA.Create();
            rsa.ImportFromPem(rsaReadFile);
            return rsa;
        }
    }
}
