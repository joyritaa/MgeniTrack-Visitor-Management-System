# Deployment Checklist

## ✅ Pre-Deployment Verification

### 1. Code Compilation
- [x] Build successful with no errors
- [x] No compilation warnings
- [x] All NuGet packages restored
- [x] No deprecated APIs used

### 2. Database Schema
```sql
-- Verify UnitId column exists in residents table
ALTER TABLE residents ADD COLUMN unit_id INT(11) NULL;
ALTER TABLE residents ADD CONSTRAINT fk_residents_unit 
  FOREIGN KEY (unit_id) REFERENCES units(unit_id);

-- Verify relationship is one-to-many (not one-to-one)
-- This is automatically handled by EF Core configuration
```

### 3. File System Requirements
```
✓ Create directory: wwwroot/uploads/visitors/
  chmod 755 wwwroot/uploads/visitors/  (Linux)

✓ Permissions: Application pool user must have write access

✓ Disk space: Ensure sufficient space for photo uploads
  Recommendation: 10GB+ available
```

### 4. SignalR Configuration
```csharp
// Verify in Program.cs:
builder.Services.AddSignalR();

app.UseRouting();
app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
    endpoints.MapHub<DashboardHub>("/dashboard-hub");
});
```

### 5. HTTPS Configuration
```
✓ SSL certificate installed (required for camera)
✓ Browser can access https://your-domain
✓ Mixed content warnings resolved
✓ Certificate valid for domain name
```

### 6. Database Backup
```bash
# MySQL/MariaDB
mysqldump -u user -p database > backup_$(date +%Y%m%d_%H%M%S).sql

# SQL Server
BACKUP DATABASE [YourDatabase] 
  TO DISK = N'C:\Backups\backup_$(Get-Date -Format yyyyMMdd_HHmmss).bak'
```

---

## 🔧 Configuration Checklist

### appsettings.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=...;Database=mgenitrack;..."
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  },
  "AllowedHosts": "*"
}
```

### appsettings.Production.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=PROD_SERVER;Database=mgenitrack_prod;..."
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "AllowedHosts": "your-domain.com"
}
```

### Program.cs
```csharp
// Verify these services are registered:
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<MgenitrackContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));
builder.Services.AddSignalR();
builder.Services.AddScoped<DashboardNotifier>();
builder.Services.AddScoped<ActivityLogService>();
```

---

## 🧪 Functional Testing

### Unit Creation Flow
- [ ] Open Residents → Create
- [ ] Select user
- [ ] Dropdown shows only unoccupied units
- [ ] Select a unit
- [ ] Unit details display correctly
- [ ] Unit is marked occupied after creation
- [ ] Create second resident - previously created unit no longer appears

### Walk-In Check-In - Camera
- [ ] Open Walk-In Check-In form
- [ ] Click "Capture Photo" button
- [ ] Camera modal appears
- [ ] Camera feed shows correctly
- [ ] Click "Capture" - photo is captured
- [ ] Photo preview appears in form
- [ ] Submit form - photo saves correctly

### Walk-In Check-In - File Upload
- [ ] Open Walk-In Check-In form
- [ ] Click file input
- [ ] Select image from device
- [ ] Photo preview appears
- [ ] Submit form - photo saves correctly

### Pre-Registered Check-In - Camera
- [ ] Open Pre-Registered List
- [ ] Click on invitation → Check-In
- [ ] Camera button available
- [ ] Camera capture works
- [ ] Photo appears in preview
- [ ] Submit check-in - photo saves

### Unit Dropdown - Walk-In
- [ ] Only occupied units appear
- [ ] Units grouped by block (A, B, C)
- [ ] Unit count displays correctly
- [ ] Info panel shows populated correctly

### Real-Time Updates
- [ ] Open dashboard on two devices/tabs
- [ ] Complete check-in on device 1
- [ ] Device 2 dashboard updates instantly (no refresh)
- [ ] Stats update correctly
- [ ] No connection errors in console

### Mobile Testing
- [ ] Access on Android device via HTTPS
- [ ] Camera capture works on Android
- [ ] File upload works on Android
- [ ] Access on iOS device via HTTPS
- [ ] Camera capture works on iOS
- [ ] File upload works on iOS

---

## 🔐 Security Checklist

### Authentication
- [ ] Only authorized roles can access dashboards
- [ ] Visitors cannot access admin features
- [ ] Session timeouts configured
- [ ] CSRF protection enabled (ValidateAntiForgeryToken present)

### Data Protection
- [ ] Database connection encrypted
- [ ] Passwords hashed in database
- [ ] No sensitive data in logs
- [ ] Photo uploads validated for type/size

### Camera Security
- [ ] Camera access requires user permission
- [ ] HTTPS enforced
- [ ] No camera data stored without consent
- [ ] Camera stream stopped when modal closed

### File Upload Security
```csharp
// Validate file type
var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
var fileExtension = Path.GetExtension(visitorPhoto.FileName).ToLower();
if (!allowedExtensions.Contains(fileExtension))
{
    ModelState.AddModelError("visitorPhoto", "Invalid file type");
}

// Validate file size
const int MAX_SIZE = 5 * 1024 * 1024;  // 5MB
if (visitorPhoto.Length > MAX_SIZE)
{
    ModelState.AddModelError("visitorPhoto", "File too large");
}

// Rename file to prevent execution
var fileName = $"{Guid.NewGuid()}{fileExtension}";
```

---

## 📊 Performance Verification

### Database Performance
```sql
-- Check indexes exist
SHOW INDEXES FROM units WHERE Column_name = 'is_occupied';
SHOW INDEXES FROM residents WHERE Column_name = 'unit_id';

-- Test query performance
EXPLAIN SELECT * FROM units WHERE is_occupied = 1;
EXPLAIN SELECT * FROM residents WHERE unit_id IS NOT NULL;
```

### Server Resources
- [ ] CPU usage < 70% during peak hours
- [ ] Memory usage < 80% available
- [ ] Disk I/O acceptable
- [ ] Network bandwidth sufficient

### Response Times
- [ ] Dashboard load: < 2 seconds
- [ ] Check-in form load: < 1 second
- [ ] Camera capture: < 3 seconds
- [ ] Real-time update: < 500ms

---

## 📋 Deployment Steps

### Step 1: Pre-Deployment
```bash
# 1. Backup database
mysqldump -u user -p database > backup_$(date +%Y%m%d_%H%M%S).sql

# 2. Test build locally
dotnet build
dotnet test

# 3. Verify configurations
# - Check appsettings.json
# - Check connection string
# - Check upload directory exists
```

### Step 2: Database Migration
```bash
# If using EF Core migrations
dotnet ef database update

# If manually:
# 1. Add unit_id column to residents
# 2. Create foreign key
# 3. Update foreign key configuration
# 4. Run database integrity check
```

### Step 3: File System Setup
```bash
# Create upload directory
mkdir -p wwwroot/uploads/visitors

# Set permissions (Linux/Mac)
chmod 755 wwwroot/uploads/visitors
chmod 777 wwwroot/uploads/visitors  # If app pool needs write

# Verify directory readable/writable
ls -la wwwroot/uploads/visitors/
```

### Step 4: Application Deployment
```bash
# Build release version
dotnet publish -c Release

# Deploy to server
# - Copy published files to deployment directory
# - Verify configuration files
# - Restart IIS/application

# For Linux with systemd:
sudo systemctl restart mgenitrack
```

### Step 5: Verification
```bash
# 1. Check application is running
curl https://your-domain/health

# 2. Test database connection
# - Try creating a resident
# - Verify unit marked occupied

# 3. Test camera functionality
# - Open on mobile device
# - Test camera access

# 4. Monitor logs
tail -f /var/log/mgenitrack/app.log
```

---

## 🚨 Rollback Plan

### If Issues Found

#### Quick Rollback (Within Hours)
```bash
# 1. Restore from backup file
mysql -u user -p database < backup_20240120_143000.sql

# 2. Redeploy previous version
# - Keep backup of current deployed files
# - Copy previous version back to deployment directory
# - Restart application
```

#### Database Rollback
```sql
-- If UnitId column causes issues, can temporarily hide
-- ALTER TABLE residents DROP COLUMN unit_id;

-- But preserve backup first!
ALTER TABLE residents ADD COLUMN unit_id_backup INT(11);
UPDATE residents SET unit_id_backup = unit_id;
```

---

## 📞 Post-Deployment Support

### Monitoring

**Application Metrics to Track:**
```
- Active user connections (SignalR)
- Check-in success rate
- Average response times
- Database query times
- File upload success rate
- Camera usage rate
```

**Log Files to Monitor:**
```
- Application error logs
- Database connection errors
- SignalR connection issues
- File upload errors
- Camera access denials
```

### Support Resources

**For Issues:**
1. Check application logs
2. Review technical documentation (TECHNICAL_DETAILS.md)
3. Check browser console (F12) for client errors
4. Verify database connectivity
5. Check file system permissions

**Common Issues & Solutions:**

| Issue | Solution |
|-------|----------|
| Camera not working | Verify HTTPS, check browser permissions |
| Photos not saving | Check wwwroot/uploads/visitors/ exists and has write permissions |
| Real-time updates not working | Verify SignalR mapping in Program.cs, check WebSocket connection |
| Unit dropdown empty | Verify units table has unoccupied units (IsOccupied = false) |
| Database errors | Check connection string, verify database is running |

---

## ✅ Final Verification Checklist

Before marking as ready for production:

- [ ] All tests passing
- [ ] No compilation errors or warnings
- [ ] Database migrations applied successfully
- [ ] File system directories created with correct permissions
- [ ] HTTPS configured and working
- [ ] Camera functionality tested on multiple devices
- [ ] Real-time updates working (SignalR connected)
- [ ] Unit occupancy logic working correctly
- [ ] Photo upload and display working
- [ ] Security validation in place
- [ ] Performance acceptable
- [ ] Backup created
- [ ] Documentation complete
- [ ] Team trained on new features
- [ ] Monitoring configured

---

## 📞 Emergency Contacts

**For Production Issues:**
1. **Database Team:** Check database connectivity
2. **Infrastructure Team:** Verify server resources, HTTPS certificate
3. **Security Team:** Review file upload and camera access security

---

**Deployment Date:** ___________
**Deployed By:** ___________
**Verified By:** ___________
**Notes:**

```
_____________________________________________________________________________

_____________________________________________________________________________

_____________________________________________________________________________
```

---

**Status:** ✅ Ready for Production (after checklist completion)
