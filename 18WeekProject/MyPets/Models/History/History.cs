using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyPets.Models
{
    public class History
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Summary { get; set; }
        public double Weight { get; set; }



        public int PetId { get; set; }
        public Pet Pet { get; set; }
    }
}
