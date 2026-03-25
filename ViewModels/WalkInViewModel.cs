using System.ComponentModel.DataAnnotations;

namespace MgeniTrack.ViewModels
{
    public class WalkInViewModel
    {
        [Required(ErrorMessage = "Visitor name is required.")]
        [Display(Name = "Visitor Full Name")]
        public string VisitorName { get; set; } = null!;

        [Display(Name = "ID / Passport Number")]
        public string? IdNumber { get; set; }

        [Display(Name = "Contact Number")]
        public string? ContactNumber { get; set; }

        [Required(ErrorMessage = "House number is required.")]
        [Display(Name = "House Number Being Visited")]
        public string HouseNumber { get; set; } = null!;

        [Required(ErrorMessage = "Purpose of visit is required.")]
        [Display(Name = "Purpose of Visit")]
        public string PurposeOfVisit { get; set; } = null!;

        [Display(Name = "Car Registration")]
        public string? CarRegistration { get; set; }

        [Required]
        [Range(1, 20, ErrorMessage = "Must be between 1 and 20.")]
        [Display(Name = "Number of Occupants")]
        public int NumberOfOccupants { get; set; } = 1;
    }
}