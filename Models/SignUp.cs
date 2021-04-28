using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OllsWarehouse.Models
{
    public class SignUp
    {
        [Required]
        public string firstName { get; set; }

        [Required]
        public string lastName { get; set; }

        [Required]
        public string userName { get; set; }

        [Required]
        public string userPassword { get; set; }

        [Required]
        [Compare("userPassword")]
        public string userPasswordConfirmed { get; set; }

        [Required]
        public string userEmail { get; set; }

        [Required]
        public DateTime userDateOfBirth { get; set; }



    }
}
