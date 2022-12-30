using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryShop.Data.Config.impl
{
    public class FileConfiguration : IFileConfiguration
    {
        public readonly IConfiguration _configuration;
        public FileConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetConnectionString()
        {
            return _configuration.GetSection("FileStorage:ConnectionString").Value;
        }

        public string GetContainerName()
        {
            return _configuration.GetSection("FileStorage:ContainerName").Value;
        }
    }
}
