# Quick Setup Guide - New Features

## 🚀 Getting Started

All features are now implemented and integrated. Here's how to use them:

---

## 1️⃣ Creating a New Resident

**Path:** Admin Panel → Residents → Create

**New Flow:**
1. Select a user from dropdown
2. **NEW:** Click the "Select Unoccupied Unit" dropdown
3. Choose from available units (shows block, type, floor)
4. Unit will automatically be marked as occupied
5. Enter house number (auto-filled from unit selection)
6. Click "Create Resident"

**Result:** 
- Resident created and linked to Unit
- Unit automatically marked as `IsOccupied = true`
- Resident can now receive visitor check-ins

---

## 2️⃣ Walk-In Visitor Check-In (WITH CAMERA)

**Path:** Guard Dashboard → Walk-In Check-In

**New Photo Features:**
- **Upload Photo:** Click file input to upload from device
- **NEW - Take Photo:** Click "📷 Capture Photo" button
  - Camera modal opens
  - Click "📸 Capture" to take photo
  - Photo automatically appears in preview
  - Close modal when done

**Photo Results:**
- Shows preview of selected/captured photo
- Supports both device photos and camera captures
- Photo is saved with visitor record

---

## 3️⃣ Pre-Registered Visitor Check-In (WITH CAMERA)

**Path:** Guard Dashboard → Pre-Registered → Check-In Visitor

**Same camera features as Walk-In:**
- Upload from device
- Capture using camera
- Real-time preview

---

## 4️⃣ Real-Time Dashboard Updates

**Automatic Updates:**
- ✅ Visitor check-ins update all guard dashboards instantly
- ✅ Dashboard stats refresh in real-time
- ✅ Unit occupancy changes broadcast to admins
- ✅ Resident notifications sent immediately

**No manual refresh needed!**

---

## 📋 Feature Checklist

### ✅ Unit Management
- [x] Unoccupied units show in dropdown
- [x] Units auto-marked as occupied on resident creation
- [x] Occupied-only units in check-in forms
- [x] Visual block grouping (A, B, C)

### ✅ Camera Support
- [x] File upload still available
- [x] Real-time camera capture
- [x] Works on WalkIn check-in
- [x] Works on Pre-registered check-in
- [x] Works on mobile devices (iOS 14.5+, Android)

### ✅ Real-Time Updates
- [x] SignalR dashboard updates
- [x] Group-based broadcasting
- [x] Live stats refresh
- [x] Instant visitor notifications

---

## 🔧 Browser Requirements

**For Camera Features:**
- Chrome/Edge: ✅ Full support
- Firefox: ✅ Full support
- Safari: ✅ Full support (iOS 14.5+)
- **HTTPS Required:** Camera access needs secure connection

**Testing Locally:**
- Use `https://localhost:####` (Visual Studio provides HTTPS)
- Or test on actual device connected to network

---

## ⚙️ Mobile Testing

**Android:**
1. On Android device, navigate to `https://your-server:port`
2. Click "Capture Photo"
3. Camera opens, take photo
4. Works with front/rear cameras

**iOS:**
1. On iOS device (14.5+), navigate to `https://your-server:port`
2. Click "Capture Photo"
3. Camera opens, take photo
4. Works with front/rear cameras

---

## 📊 SignalR Dashboard Features

**Real-Time Data Shown:**
```
{
  "TotalInside": 5,           // Currently checked-in visitors
  "TodayVisits": 23,          // Today's total visits
  "CheckedOutToday": 18,      // Checked out today
  "OccupiedUnits": 42,        // Occupied units
  "Timestamp": "2024-01-20T14:30:00Z"
}
```

**Who Sees What:**
- **Guards:** All check-in/out updates, stats
- **Admins:** All updates + unit status changes
- **Residents:** Only their visitor notifications

---

## 🆘 Troubleshooting

### Camera Not Working?
- ✅ Check HTTPS is enabled
- ✅ Browser asks for permission - click "Allow"
- ✅ Check camera device is available
- ✅ Try refreshing page and trying again

### Photos Not Uploading?
- ✅ Check file size isn't too large
- ✅ Ensure `wwwroot/uploads/visitors/` directory exists
- ✅ Check directory permissions (write access needed)

### Real-Time Updates Not Working?
- ✅ Check SignalR is configured in `Program.cs`
- ✅ Verify connection string in `appsettings.json`
- ✅ Check browser console for errors (F12)
- ✅ Restart application

### Unit Dropdown Empty?
- ✅ All units are occupied - create new units or mark existing as vacant
- ✅ Check database connection

---

## 📝 Database Considerations

### Required Changes:
✅ **Already Applied:**
- `Residents` table now has `UnitId` column
- `Units` table relationship updated to one-to-many
- All existing data preserved

### Backup Recommendation:
Before deploying, backup your database:
```sql
-- MySQL/MariaDB
CREATE TABLE residents_backup AS SELECT * FROM residents;
CREATE TABLE units_backup AS SELECT * FROM units;
```

---

## 🎯 Common Workflows

### Creating Full Resident Profile
1. Go to Residents → Create
2. Select User
3. Select Unoccupied Unit
4. Auto-filled HouseNumber (from unit)
5. Click Create
6. ✅ Unit automatically marked occupied

### Checking In Walk-In Visitor
1. Guard Dashboard → Walk-In
2. Fill visitor details
3. **Option A:** Upload existing photo
4. **Option B:** Click "Capture Photo" → Take photo with camera
5. Select occupied unit
6. Select purpose
7. Click "Check In"
8. ✅ Dashboard updates for all users in real-time

### Checking In Pre-Registered Visitor
1. Guard Dashboard → Pre-Registered
2. Select invitation from list
3. Click "Check In"
4. Verify visitor details
5. **Option A:** Upload existing photo
6. **Option B:** Click "Capture Photo" → Take photo
7. Click "Confirm Check-In"
8. ✅ Resident notified instantly

---

## 📞 Support

For issues or questions:
1. Check browser console (F12) for errors
2. Check application logs
3. Verify all features are enabled in settings
4. Review IMPLEMENTATION_SUMMARY.md for technical details

---

## Version Info

**Implementation Date:** 2024
**Features:** Real-Time Updates, Unit Management, Camera Support
**Status:** ✅ Ready for Production

---

**Happy visitor management! 🎉**
