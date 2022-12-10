using System.Security.Cryptography;
using System.Text;

namespace CrudUsuarios.Helper
{
    public static class Criptografia
    {
        public static string GerarHash(this string senha)
        {
            var hash = SHA1.Create();
            var enconding = new ASCIIEncoding();
            var array = enconding.GetBytes(senha);

            array = hash.ComputeHash(array);

            var strHexa = new StringBuilder();

            foreach(var item in array)
            {
                strHexa.Append(item.ToString("x2"));
            }
            return strHexa.ToString();
        }
    }
}
