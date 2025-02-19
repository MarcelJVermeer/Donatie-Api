﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Donatie.Applicatie.DTOs
{
    public class MaakDonatie
    {
        [Required]
        [JsonPropertyName("bedrag")]
        public double  Bedrag { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [JsonPropertyName("afzender")]
        public string Afzender { get; set; }
    }
}
