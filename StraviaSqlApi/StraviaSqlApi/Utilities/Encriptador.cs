using System.Security.Cryptography;
using System.Text;

namespace StraviaSqlApi.Utilities;

//Clase utilizada para encriptar contraseñas
public class Encriptador
{
    //Funcion que encripta las contraseñas
    public static string Encrypt(string password)
    {
        //Algoritmo para encriptar en SHA256
        SHA256 sha256 = SHA256Managed.Create();
        ASCIIEncoding encoding = new ASCIIEncoding();
        byte[] stream = null;
        StringBuilder sb = new StringBuilder();
        stream = sha256.ComputeHash(encoding.GetBytes(password));
        for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
        return sb.ToString();
    }
}