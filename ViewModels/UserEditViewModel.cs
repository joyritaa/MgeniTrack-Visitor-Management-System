namespace MgeniTrack.ViewModels
{
    public class UserEditViewModel
    {
        public int UserId { get; set; }

        public required string Firstname { get; set; }
        public required string Secondname { get; set; }
        public required string Gender { get; set; }
        public required string IdNumber { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }

        public int SelectedRoleId { get; set; }

        public string? Shift { get; set; }
        public string? HouseNumber { get; set; }

        // Optional password change
        public string? NewPassword { get; set; }
    }
}
