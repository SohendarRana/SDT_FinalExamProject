using System.ComponentModel.DataAnnotations;

namespace FinalExamProject.Models
{
    public class ReviewViewModel
    {
        public int id { get; set; }
        [Required]
        [StringLength(60, MinimumLength =3)]
        public string Restaurant { get; set; }

        [Required]
        [StringLength (60, MinimumLength =3)]
        public string Food { get; set; }

        [RegularExpression(@"^\d{1,7}(\.\d{1,2})?$", ErrorMessage = "Invalid Price")]
        public int prices { get; set; }
        public DateTime Date { get; set; }
        public IFormFile Image { get; set; }

    } 
}
