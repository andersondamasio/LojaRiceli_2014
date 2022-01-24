using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace _2_Library.Utils
{
    public class Static
    {
        public static string urlHttp = HttpContext.Current.Request.Url.Scheme + Uri.SchemeDelimiter + HttpContext.Current.Request.Url.Host;
        
        public static string resultadoNomeAmigavel = string.Empty;

          //Calcula o frete usando a base local ou não
          public static bool baseLocal = true;
    }
}
