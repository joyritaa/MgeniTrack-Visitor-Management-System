using System.ComponentModel.DataAnnotations;

namespace MgeniTrack.ViewModels
{
    public class GuardCheckInViewModel
    {
        public int InvitationId { get; set; }

        // Pre-filled from the invitation
        public string VisitorName { get; set; } = null!;
        public string? VisitorPhone { get; set; }
        public string PurposeOfVisit { get; set; } = null!;
        public string HouseNumber { get; set; } = null!;
        public string ResidentName { get; set; } = null!;
        public DateOnly? ExpectedDate { get; set; }

        // Guard fills these in at the gate
        [Display(Name = "Visitor ID Number")]
        public string? IdNumber { get; set; }

        [Display(Name = "Car Registration")]
        public string? CarRegistration { get; set; }

        [Required]
        [Display(Name = "Number of Occupants")]
        [Range(1, 20, ErrorMessage = "Must be between 1 and 20.")]
        public int NumberOfOccupants { get; set; } = 1;

        [Display(Name = "Check-in Method")]
        public string CheckInMethod { get; set; } = "Manual";
    }
}
