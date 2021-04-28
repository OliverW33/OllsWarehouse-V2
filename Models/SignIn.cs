using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OllsWarehouse.Models
{
    public class SignIn
    {
        [Required]
        public string userName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string userPassword { get; set; }

        [Required]
        public bool rememberMe { get; set; }

    }
}
