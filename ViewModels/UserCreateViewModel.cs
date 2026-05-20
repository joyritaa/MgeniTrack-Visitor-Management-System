using System.ComponentModel.DataAnnotations;

namespace MgeniTrack.ViewModels
{
    public class UserCreateViewModel
    {
        public required string Firstname { get; set; }
        public required string Secondname { get; set;}
        public required string Gender { get; set; }

        [Required]
        [DataType(DataType.Password)]

        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{8,}$",
            ErrorMessage =
            "Password must contain:\n" +
            "- At least 8 characters\n" +
            "- One uppercase letter\n" +
            "- One lowercase letter\n" +
            "- One number\n" +
            "- One special character"
        )]
        public required string Password { get; set; }
        public required string IdNumber { get; set; }
        public required string Email { get; set; }
        
        [Phone]
        public required string PhoneNumber { get; set; }


        public int SelectedRoleId { get; set; }

        public string? Shift { get; set; }  // For Guard

    }
}
