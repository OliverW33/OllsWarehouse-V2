﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OllsWarehouse.Models
{
    public class Films
    {
        [Key]
        public int movieID { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string movieTitle { get; set; }

        [Required]
        [Column(TypeName = "varchar(5)")]
        public string movieCertificate { get; set; }

        [Column(TypeName = "text")]
        public string movieDescription { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string movieImage { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal moviePrice { get; set; }

        public int Stars { get; set; }

        [DataType(DataType.Date)]
        public DateTime movieReleaseDate { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string movieTrailer { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string movieGenre { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string movieBackgroundImage { get; set; }


    }
}
