using System.ComponentModel.DataAnnotations;

namespace MgeniTrack.ViewModels
{
    public class InvitationCreateViewModel
    {
        [Required(ErrorMessage = "Visitor name is required.")]
        [Display(Name = "Visitor Full Name")]
        public string VisitorName { get; set; } = null!;

        [Phone(ErrorMessage = "Enter a valid phone number.")]
        [Display(Name = "Visitor Phone")]
        public string? VisitorPhone { get; set; }

        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        [Display(Name = "Visitor Email")]
        public string? VisitorEmail { get; set; }

        [Required(ErrorMessage = "Purpose of visit is required.")]
        [Display(Name = "Purpose of Visit")]
        public string PurposeOfVisit { get; set; } = null!;

        [Required(ErrorMessage = "Expected date is required.")]
        [Display(Name = "Expected Visit Date")]
        public DateOnly ExpectedDate { get; set; }
    }
}
