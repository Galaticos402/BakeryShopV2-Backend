using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryShop.BusinessObject
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BakeryShopContext>
    {
        public BakeryShopContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<BakeryShopContext>();
            var connectionString = "Server=localhost;Database=BakeryShopV2;Trusted_Connection=True;";
            builder.UseSqlServer(connectionString);
            return new BakeryShopContext(builder.Options);
        }
    }
}
