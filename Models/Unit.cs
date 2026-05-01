using System;

namespace MgeniTrack.Models;

public partial class Unit
{
    public int UnitId { get; set; }
    public string UnitNumber { get; set; } = null!; 
    public string Block { get; set; } = null!;    
    public int FloorNumber { get; set; }
    public int UnitPosition { get; set; } 
    public string UnitType { get; set; } = null!; 
    public bool IsOccupied { get; set; } = false;
    public DateTime? CreatedAt { get; set; }

    // Navigation
    public virtual ICollection<Resident> Residents { get; set; } = new List<Resident>();
}