using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBoard_.Domain.Enums
{
    public enum Category
    {
        [Description("חשמל")]
        Electronics = 1,
        [Description("רכב")]
        Vehicles = 2,
        [Description("חיות מחמד")]
        Pets = 3,
        [Description("רהיטים")]
        Furniture = 4
    }
}
