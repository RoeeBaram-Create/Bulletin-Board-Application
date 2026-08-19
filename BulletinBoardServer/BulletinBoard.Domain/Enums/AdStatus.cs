using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBoard_.Domain.Enums
{
    public enum AdStatus
    {
        Draft = 0,      // טיוטה
        Active = 1,     // פעיל
        Pending = 2,    // ממתין לאישור מנהל
        Sold = 3,       // נמכר
        Expired = 4,    // פג תוקף
        Hidden = 5      // הוסר על ידי המשתמש
    }
}
