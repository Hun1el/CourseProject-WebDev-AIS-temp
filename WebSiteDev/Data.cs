using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteDev
{
    public static class Data
    {
        public static string GetConnectionString()
        {
            return $"host=localhost;" +
                   $"uid=root;" +
                   $"pwd=;" +
                   $"database=DBWebShop";
        }

        public static string GetConnectionStringNoDB()
        {
            return $"host=localhost;" +
                   $"uid=root;" +
                   $"pwd=";
        }
    }
}
