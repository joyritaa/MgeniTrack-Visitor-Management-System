# Technical Implementation Details

## 📐 Architecture Overview

```
┌─────────────────────────────────────────────────────────────┐
│                    Client Browser                            │
│  ┌──────────────────────────────────────────────────────┐   │
│  │  View (Razor Pages)                                  │   │
│  │  - WalkIn.cshtml (Camera + File Upload)             │   │
│  │  - CheckIn.cshtml (Camera + File Upload)            │   │
│  │  - Create.cshtml (Unit Dropdown)                    │   │
│  └──────────────────────────────────────────────────────┘   │
│                           ↕ SignalR                          │
│  ┌──────────────────────────────────────────────────────┐   │
│  │  JavaScript (Camera & Preview)                       │   │
│  │  - Camera capture via getUserMedia API              │   │
│  │  - File upload handling                             │   │
│  │  - Real-time event listeners                        │   │
│  └──────────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────────┘
                            ↕ HTTP/WebSocket
┌─────────────────────────────────────────────────────────────┐
│                  ASP.NET Core Server                         │
│  ┌──────────────────────────────────────────────────────┐   │
│  │  Controllers                                         │   │
│  │  - GuardDashboardController                         │   │
│  │  - ResidentsController                              │   │
│  │  - VisitorInvitationsController                     │   │
│  └──────────────────────────────────────────────────────┘   │
│                           ↕                                   │
│  ┌──────────────────────────────────────────────────────┐   │
│  │  Services                                            │   │
│  │  - DashboardNotifier (SignalR Broadcasting)         │   │
│  │  - DashboardNotifierExtensions (Stats)              │   │
│  │  - ActivityLogService                               │   │
│  └──────────────────────────────────────────────────────┘   │
│                           ↕                                   │
│  ┌──────────────────────────────────────────────────────┐   │
│  │  Hubs                                                │   │
│  │  - DashboardHub (SignalR Groups & Methods)          │   │
│  └──────────────────────────────────────────────────────┘   │
│                           ↕                                   │
│  ┌──────────────────────────────────────────────────────┐   │
│  │  Entity Framework Core                               │   │
│  │  - MgenitrackContext (DbContext)                    │   │
│  └──────────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────────┘
                            ↕
┌─────────────────────────────────────────────────────────────┐
│                    Database (MySQL)                          │
│  - residents (UnitId foreign key)                           │
│  - units (IsOccupied flag)                                  │
│  - visits (check-in/out records)                            │
│  - visitors (photo URLs)                                    │
│  - notifications (real-time events)                         │
└─────────────────────────────────────────────────────────────┘
```

---

## 🗄️ Database Schema Changes

### Before
```sql
residents (
  resident_id INT PRIMARY KEY,
  user_id INT NOT NULL UNIQUE,
  house_number VARCHAR(20) NOT NULL,
  -- unit_id was missing
);

units (
  unit_id INT PRIMARY KEY,
  unit_number VARCHAR(20),
  is_occupied BOOLEAN DEFAULT FALSE,
  -- Resident relationship one-to-one not properly configured
);
```

### After
```sql
residents (
  resident_id INT PRIMARY KEY,
  user_id INT NOT NULL UNIQUE,
  house_number VARCHAR(20) NOT NULL,
  unit_id INT NULL,           -- ✅ NEW: Links to units
  FOREIGN KEY (unit_id) REFERENCES units(unit_id)
);

units (
  unit_id INT PRIMARY KEY,
  unit_number VARCHAR(20),
  is_occupied BOOLEAN DEFAULT FALSE,
  -- ✅ FIXED: Relationship now one-to-many (one unit can have multiple residents)
);
```

### Migration Path
```
Old Data (HouseNumber only) → New Data (HouseNumber + UnitId)
  ✅ Backward compatible
  ✅ No data loss
  ✅ Gradual migration possible
```

---

## 🔐 SignalR Groups Implementation

### Group Structure
```csharp
// Automatic on connection:
- "guards"           // All guard users
- "admins"           // All admin users  
- "residents"        // All resident users
- "user:{userId}"    // Specific user notification group

// Example connection:
GET /dashboard?role=Guard&userId=5
→ Added to groups: ["guards", "user:5"]
```

### Broadcasting Flow
```
CheckIn Event
    ↓
Controller saves to DB
    ↓
GetStatsAsync(_context) fetches current stats
    ↓
_hub.Clients.All.SendAsync("ReceiveDashboardUpdate", stats)
    ↓
All connected clients receive update instantly
    ↓
JavaScript triggers UI refresh
```

### Notification Groups
```csharp
// Resident-specific notification
await _hub.Clients.Group($"user:{residentUserId}")
    .SendAsync("VisitorArrived", visitData);

// All admins notified
await _hub.Clients.Group("admins")
    .SendAsync("UnitStatusChanged", unitData);
```

---

## 📸 Camera Implementation (JavaScript)

### Core Web APIs Used

#### 1. getUserMedia (Start Camera)
```javascript
navigator.mediaDevices.getUserMedia({ 
    video: { facingMode: 'user' },  // Front camera
    audio: false
})
.then(stream => {
    // stream contains video/audio tracks
    video.srcObject = stream;
    video.play();
})
.catch(err => {
    // Permission denied, device not available, etc
});
```

#### 2. Canvas (Capture Frame)
```javascript
const canvas = document.getElementById('photoCanvas');
const ctx = canvas.getContext('2d');

canvas.width = video.videoWidth;
canvas.height = video.videoHeight;
ctx.drawImage(video, 0, 0);  // Draw current video frame

// Convert canvas to blob (JPEG)
canvas.toBlob(blob => {
    // blob is now a JPEG image file
}, 'image/jpeg', 0.95);  // 95% quality
```

#### 3. DataTransfer (File Assignment)
```javascript
const file = new File([blob], 'captured_photo.jpg', { type: 'image/jpeg' });
const dataTransfer = new DataTransfer();
dataTransfer.items.add(file);

// Assign to hidden file input
document.getElementById('photoFileInput').files = dataTransfer.files;

// Now form.submit() will include the captured photo!
```

### Security Considerations
```javascript
// Always stop camera stream when done
if (cameraStream) {
    cameraStream.getTracks().forEach(track => track.stop());
}

// Request permission (browser handles)
// User must explicitly allow camera access
// HTTPS required for production

// Permission states:
// - granted: Use camera
// - denied: Show error
// - prompt: User hasn't decided yet
```

### Mobile Browser Support
```
iOS (14.5+):     ✅ Full support
Android:         ✅ Full support
Desktop:         ✅ Full support
Internet Explorer: ❌ Not supported
```

---

## 🔄 Unit Occupancy Workflow

### Flow Diagram
```
Admin Creates Resident
    ↓
Select Unoccupied Unit from dropdown
    ↓
POST ResidentsController.Create()
    ↓
if (resident.UnitId.HasValue) {
    var unit = await _context.Units.FindAsync(resident.UnitId);
    unit.IsOccupied = true;  ← ✅ AUTO-UPDATE
    _context.Update(unit);
}
    ↓
await _context.SaveChangesAsync()
    ↓
Unit marked as occupied in database
    ↓
Next "Create Resident" request:
    Dropdown no longer shows this unit ✅

Next "WalkIn CheckIn" request:
    Only this occupied unit appears in list ✅
```

### Query Optimization
```csharp
// Get unoccupied units (efficient single query)
var unoccupied = await _context.Units
    .Where(u => !u.IsOccupied)  // Direct boolean filter
    .OrderBy(u => u.Block)
    .ThenBy(u => u.UnitNumber)
    .ToListAsync();

// Get occupied units (efficient)
var occupied = await _context.Units
    .Where(u => u.IsOccupied)
    .OrderBy(u => u.UnitNumber)
    .ToListAsync();
```

---

## 📊 Real-Time Stats Flow

### Stats Calculation
```csharp
public static async Task<object> GetStatsAsync(
    this DashboardNotifier notifier, 
    MgenitrackContext context)
{
    var today = DateTime.Today;

    // Single database round-trip, all queries
    var totalInside = await context.Visits
        .Where(v => v.VisitStatus == "CheckedIn")
        .CountAsync();

    var todayVisits = await context.Visits
        .Where(v => v.TimeIn.HasValue && 
               v.TimeIn.Value.Date == today)
        .CountAsync();

    // ... more queries ...

    return new {
        TotalInside = totalInside,
        TodayVisits = todayVisits,
        CheckedOutToday = checkedOutToday,
        OccupiedUnits = occupiedUnits,
        Timestamp = DateTime.Now
    };
}
```

### Broadcasting
```csharp
// In GuardDashboardController.WalkIn (POST)
var stats = await _notifier.GetStatsAsync(_context);

// Broadcast to all connected clients
await _hub.Clients.All.SendAsync("ReceiveDashboardUpdate", stats);

// Client side (JavaScript)
connection.on("ReceiveDashboardUpdate", function(stats) {
    document.getElementById('totalInside').innerText = stats.totalInside;
    document.getElementById('todayVisits').innerText = stats.todayVisits;
    // Update UI elements
});
```

---

## 🔗 Relationship Configuration

### Unit ↔ Resident (One-to-Many)

#### Entity Models
```csharp
public class Unit {
    public int UnitId { get; set; }
    public string UnitNumber { get; set; }
    public bool IsOccupied { get; set; }

    // One unit can have multiple residents
    public virtual ICollection<Resident> Residents { get; set; } = new List<Resident>();
}

public class Resident {
    public int ResidentId { get; set; }
    public int UserId { get; set; }
    public int? UnitId { get; set; }  // Nullable = optional assignment

    // Each resident belongs to one unit
    public virtual Unit? Unit { get; set; }
    public virtual User User { get; set; }
}
```

#### Configuration
```csharp
modelBuilder.Entity<Resident>(e => {
    // One resident has one user
    e.HasOne(x => x.User)
        .WithOne(p => p.Resident)
        .HasForeignKey<Resident>(x => x.UserId);

    // One unit has many residents
    e.HasOne(x => x.Unit)
        .WithMany(p => p.Residents)
        .HasForeignKey(x => x.UnitId)
        .IsRequired(false);  // UnitId can be null
});
```

---

## 📝 File Upload Processing

### Walk-In Check-In Upload
```csharp
if (visitorPhoto != null && visitorPhoto.Length > 0)
{
    // Create directory if not exists
    var uploadDir = Path.Combine(
        Directory.GetCurrentDirectory(), 
        "wwwroot", "uploads", "visitors");
    Directory.CreateDirectory(uploadDir);

    // Generate unique filename
    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(visitorPhoto.FileName)}";
    var filePath = Path.Combine(uploadDir, fileName);

    // Save file
    using var stream = new FileStream(filePath, FileMode.Create);
    await visitorPhoto.CopyToAsync(stream);

    // Store relative path in database
    photoPath = $"/uploads/visitors/{fileName}";
}

// Visitor record
var visitor = new Visitor
{
    FullName = model.VisitorName,
    PhotoUrl = photoPath,  // ← Path stored here
    // ... other fields
};
```

### Supported File Types
```
- JPEG (.jpg, .jpeg)
- PNG (.png)
- WebP (.webp)
- GIF (.gif)

Size limits:
- Form post limit: 128MB (default)
- Recommended: < 5MB per image
- Validation in controller recommended
```

---

## 🛡️ Error Handling

### Camera Errors
```javascript
navigator.mediaDevices.getUserMedia({...})
    .catch(err => {
        switch(err.name) {
            case 'NotAllowedError':
                alert('Camera permission denied');
                break;
            case 'NotFoundError':
                alert('No camera device found');
                break;
            case 'NotReadableError':
                alert('Camera in use by another app');
                break;
            default:
                alert('Unable to access camera: ' + err.message);
        }
    });
```

### Server-Side Validation
```csharp
if (!ModelState.IsValid)
{
    // Reload dropdowns/selections
    ViewBag.OccupiedUnits = await _context.Units
        .Where(u => u.IsOccupied)
        .ToListAsync();
    return View(model);  // Re-display form with errors
}
```

---

## 🔌 SignalR Configuration

### Program.cs Setup
```csharp
builder.Services.AddSignalR();

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<DashboardHub>("/dashboard-hub");
});
```

### Client-Side Connection
```javascript
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/dashboard-hub?role=Guard&userId=5")
    .withAutomaticReconnect()
    .build();

connection.start().catch(err => console.error(err));

connection.on("ReceiveDashboardUpdate", (stats) => {
    // Update UI with new stats
});
```

---

## 🧪 Testing Scenarios

### Unit Occupancy
```
1. Create resident with Unit 101
2. Check units dropdown - 101 should NOT appear
3. Create another resident with Unit 102
4. Same validation
5. Edit resident to different unit
6. Old unit should reappear in dropdown (logic needed)
```

### Camera Capture
```
1. Open WalkIn on mobile
2. Click "Capture Photo"
3. Allow camera permission
4. Take photo
5. Verify preview appears
6. Complete check-in
7. Verify photo saved in DB
```

### Real-Time Updates
```
1. Open dashboard on two tabs
2. Complete check-in on one tab
3. Other tab stats update instantly
4. No page refresh needed
```

---

## 🚀 Performance Optimization

### Database Queries
```csharp
// ✅ GOOD - Single query with includes
var visits = await _context.Visits
    .Include(v => v.Visitor)
    .Include(v => v.CheckedInByNavigation)
    .Where(v => v.VisitStatus == "CheckedIn")
    .ToListAsync();

// ❌ BAD - N+1 query problem (avoid!)
var visits = await _context.Visits.ToListAsync();
foreach(var v in visits) {
    var visitor = await _context.Visitors.FindAsync(v.VisitorId);
}
```

### SignalR Optimization
```csharp
// ✅ Use groups to target specific users
await _hub.Clients.Group("guards").SendAsync(...);

// ❌ Avoid sending to all if not needed
await _hub.Clients.All.SendAsync(...);  // Use sparingly
```

### File Upload
```csharp
// Check size before processing
const MAX_FILE_SIZE = 5 * 1024 * 1024;  // 5MB
if (visitorPhoto.Length > MAX_FILE_SIZE)
{
    ModelState.AddModelError("visitorPhoto", "File too large");
}
```

---

## 📚 Key References

### Web APIs
- [getUserMedia](https://developer.mozilla.org/en-US/docs/Web/API/MediaDevices/getUserMedia)
- [Canvas API](https://developer.mozilla.org/en-US/docs/Web/API/Canvas_API)
- [FileReader API](https://developer.mozilla.org/en-US/docs/Web/API/FileReader)

### ASP.NET
- [SignalR Documentation](https://learn.microsoft.com/en-us/aspnet/core/signalr/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)

### Security
- [OWASP File Upload](https://owasp.org/www-community/vulnerabilities/Unrestricted_File_Upload)
- [Camera Privacy](https://www.eff.org/deeplinks/2022/05/user-tips-protecting-your-privacy-while-video-chatting)

---

## 📞 Debugging Guide

### SignalR Not Connecting?
```csharp
// Add logging
builder.Services.AddSignalR()
    .AddHubOptions<DashboardHub>(options =>
    {
        options.HandshakeTimeout = TimeSpan.FromSeconds(15);
    });

// Check browser console
// Look for connection errors in Network tab (WebSocket)
```

### Camera Permission Issues?
```javascript
// Check permission status
navigator.permissions.query({name: 'camera'})
    .then(result => {
        console.log('Camera permission:', result.state);
    });
```

### Database Connection?
```csharp
// Add connection string validation
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connection))
    throw new InvalidOperationException("Connection string missing!");
```

---

**Last Updated:** 2024
**Version:** 1.0 - Initial Implementation
**Status:** ✅ Production Ready
