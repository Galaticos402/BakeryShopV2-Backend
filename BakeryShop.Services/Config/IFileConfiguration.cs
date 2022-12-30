using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryShop.Data.Config
{
    public interface IFileConfiguration
    {
        string GetConnectionString();
        string GetContainerName();
    }
}
