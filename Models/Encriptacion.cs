using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
namespace Models
{
    public class Encriptacion
    {
        public static string clave = "abcdefghijklmnopqrstuvwxyz123456";
        public Encriptacion() { }

        public static string encriptar(string cadena)
        {

            byte[] cadenaBytes = Encoding.UTF8.GetBytes(cadena);
            byte[] claveBytes = Encoding.UTF8.GetBytes(clave);
            RijndaelManaged rij = new RijndaelManaged();
            rij.Mode = CipherMode.ECB;
            rij.BlockSize = 256;
            rij.Padding = PaddingMode.Zeros;
            ICryptoTransform encriptador;
            encriptador = rij.CreateEncryptor(claveBytes, rij.IV);
            MemoryStream memStream = new MemoryStream();
            CryptoStream cifradoStream;
            cifradoStream = new CryptoStream(memStream, encriptador, CryptoStreamMode.Write);
            cifradoStream.Write(cadenaBytes, 0, cadenaBytes.Length);
            cifradoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memStream.ToArray();
            memStream.Close();
            cifradoStream.Close();
            return Convert.ToBase64String(cipherTextBytes);
        }

        public static string desencriptar(string cadena)
        {

            byte[] cadenaBytes = Convert.FromBase64String(cadena);
            byte[] claveBytes = Encoding.UTF8.GetBytes(clave);
            RijndaelManaged rij = new RijndaelManaged();
            rij.Mode = CipherMode.ECB;
            rij.BlockSize = 256;
            rij.Padding = PaddingMode.Zeros;
            ICryptoTransform desencriptador;
            desencriptador = rij.CreateDecryptor(claveBytes, rij.IV);
            MemoryStream memStream = new MemoryStream(cadenaBytes);
            CryptoStream cifradoStream;
            cifradoStream = new CryptoStream(memStream, desencriptador, CryptoStreamMode.Read);
            StreamReader lectorStream = new StreamReader(cifradoStream);
            string resultado = lectorStream.ReadToEnd();
            memStream.Close();
            cifradoStream.Close();
            return resultado;

        }

    }
}
