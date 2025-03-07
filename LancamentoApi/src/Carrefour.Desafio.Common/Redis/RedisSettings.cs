using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrefour.Desafio.Common.Redis
{
    public class RedisSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public int Database { get; set; } = 0;
    }
}
