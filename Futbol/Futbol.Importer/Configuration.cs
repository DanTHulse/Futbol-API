using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futbol.Importer
{
    public class Configuration
    {
        public AppSettingConfig AppSettings { get; set; }

        public ConnectionStringConfig ConnectionStrings { get; set; }

        public class AppSettingConfig
        {

        }

        public class ConnectionStringConfig
        {
            public string FutbolDbConnection { get; set; }
        }
    }
}
