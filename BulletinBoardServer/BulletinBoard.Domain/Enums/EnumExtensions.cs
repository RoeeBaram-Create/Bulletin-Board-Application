using System.ComponentModel;
using System.Reflection;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        Type type = value.GetType();

        // הוספנו ? אחרי string כדי לציין שהשם יכול להיות null
        string? name = Enum.GetName(type, value);

        if (name != null)
        {
            // הוספנו ? אחרי FieldInfo כדי לפתור את אזהרה CS8600
            FieldInfo? field = type.GetField(name);

            if (field != null)
            {
                // שימוש ב-GetCustomAttribute בצורה בטוחה
                var attribute = field.GetCustomAttribute<DescriptionAttribute>();

                if (attribute != null)
                {
                    return attribute.Description;
                }
            }
        }

        // במקרה שאין תיאור, מחזירים את שם ה-Enum או מחרוזת ריקה במקום null
        return name ?? value.ToString();
    }
}
