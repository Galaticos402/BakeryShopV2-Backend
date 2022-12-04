using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryShop.Identity
{
    internal class DesignTimeIdentityContextFactory : IDesignTimeDbContextFactory<IdentityContext>
    {
        public IdentityContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<IdentityContext>();
            var connectionString = "Server=localhost;Database=BakeryShopV2_Identity;Trusted_Connection=True;";
            builder.UseSqlServer(connectionString);
            return new IdentityContext(builder.Options);
        }
    }
}
