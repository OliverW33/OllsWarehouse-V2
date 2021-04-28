using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OllsWarehouse.Areas.Identity.Data;

namespace OllsWarehouse.Data
{
    public class OllsWarehouseDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public OllsWarehouseDbContext(DbContextOptions<OllsWarehouseDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
