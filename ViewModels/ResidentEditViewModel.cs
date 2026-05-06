namespace MgeniTrack.ViewModels
{
    public class ResidentEditViewModel
    {
        public int ResidentId { get; set; }

        public int UserId { get; set; }
        public required string UserName { get; set; }  // display only

        public int UnitId { get; set; }

        public required string CurrentUnitNumber { get; set; } // display only
    }
}