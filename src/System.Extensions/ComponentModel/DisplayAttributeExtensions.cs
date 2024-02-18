using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace System.ComponentModel;

public static class DisplayAttributeExtensions
{
    public static string GetDisplayName(this Enum enumValue)
    {
        if (enumValue == null)
        {
            throw new ArgumentNullException(nameof(enumValue));
        }

        var display = enumValue.GetType()
          .GetMember(enumValue.ToString())
          .First()
          .GetCustomAttribute<DisplayAttribute>();

        if (display == null)
        {
            return enumValue.ToString();
        }
        else
        {
            return display.GetName();
        }
    }
}
