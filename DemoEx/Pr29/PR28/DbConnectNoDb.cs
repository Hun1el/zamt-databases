using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR28
{
    public static class DbConnectNoDb
    {
        public static string GetNoDb()
        {
            return $"host={PR28.Properties.Settings.Default.DbHost};" +
                   $"uid={PR28.Properties.Settings.Default.DbUser};" +
                   $"pwd={PR28.Properties.Settings.Default.DbPassword};";
        }
    }
}
