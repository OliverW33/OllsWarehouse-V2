using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OllsWarehouse.Areas.Identity.Data
{
    public class ApplicationRole : IdentityRole
    {
        public string userRole { get; set; }
    }
}
