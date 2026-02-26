namespace MgeniTrack.ViewModels
{
    public class UserCreateViewModel
    {
        public required string Firstname { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }

        public required string Gender { get; set; }

        public int SelectedRoleId { get; set; }

        public string? Shift { get; set; }  // For Guard
        public required string HouseNumber { get; set; } // For Resident
    }
}
