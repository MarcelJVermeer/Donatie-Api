using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donatie.Applicatie.DTOs
{
    public class GeefDonatie
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public double Bedrag { get; set; }

        [Required]
        public string Afzender { get; set; }

        [Required]
        public DateTime Datum { get; set; }
    }
}
