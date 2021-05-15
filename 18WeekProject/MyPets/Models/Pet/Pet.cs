using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyPets.Models
{
    [Table("Pet")]
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public int Age { get; set; }
        public string Color { get; set; }
        public string Breed { get; set; }
        public string Sex { get; set; }

        public bool SpayNeuter { get; set; }


        [NotMapped]
        public IFormFile CoverPhoto { get; set; }
        public string CoverImageUrl { get; set; }

        [NotMapped]
        public IFormFile VetForm { get; set; }
        public string VetFormUrl { get; set; }


        public string FileUploaded { get; set; }


        public int UserId { get; set; }
        public User Users { get; set; }


        public List<History> Histories { get; set; }
    }
}
