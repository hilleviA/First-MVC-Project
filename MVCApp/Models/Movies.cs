using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Models
{
    public class Movies
    {
        //Properties

        [Required(ErrorMessage = "Du måste ange titel på film")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Du måste ange Genre")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "Du måste ange release år")]
        public string ReleaseYear { get; set; }

        [Required(ErrorMessage = "Du måste ange datum")]
        [DataType(DataType.Date)]
        public DateTime ViewDate { get; set; }

        [Required(ErrorMessage = "Du måste sätta ett betyg")]
        public int Raiting { get; set; }


        //Constructor
        public Movies()
        {

        }

    }

    
}
