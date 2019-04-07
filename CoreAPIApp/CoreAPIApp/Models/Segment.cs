﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPIApp.Models
{
    public class Segment
    {
        [Key]
        public int Id { get; set; }

        public int TripId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTimeOffset StartDateTime { get; set; }

        public DateTimeOffset EndDateTime { get; set; }
    }
}
