using System;
using System.Collections.Generic;
using System.Text;
using VeloxSoft.Config;

namespace VeloxSoft.Services
{
    public class ServicioVentas
    {
        private readonly DatabaseConfig _dbConfig;

        public ServicioVentas(DatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

    }
}
