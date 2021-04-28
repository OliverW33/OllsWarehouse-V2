using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OllsWarehouse.Models
{
    public class MovieShoppingCart
    { 
        [Key]
        public int movieID { get; set; }
        public string movieImage { get; set; }
        public string movieTitle { get; set; }
        public int movieAmount { get; set; }
        public decimal moviePrice { get; set; }
        public DateTime orderDateAndTime { get; set; }


    }
}
