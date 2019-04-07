using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPIApp.Models
{
    public class Trip
    {
        [Key]
        public int Id { get; set; }


        public string Name { get; set; }


        public DateTime StartDate { get; set; }


        public DateTime EndDate { get; set; }
    }
}
