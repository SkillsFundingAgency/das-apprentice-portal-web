using SFA.DAS.ApprenticePortal.Web.Models;
using System.ComponentModel;
using System.Reflection;

namespace SFA.DAS.ApprenticePortal.Web.Helpers
{
    public static class Utilities
    {
        public static string GetApprenticeshipTypeDescription(int? value)
        {
            if (value != null)
            {
                ApprenticeshipType enumValue = (ApprenticeshipType)value;
                var enumName = enumValue.ToString();

                FieldInfo? field = typeof(ApprenticeshipType).GetField(enumName);

                if (field != null)
                {
                    DescriptionAttribute? attribute = field.GetCustomAttribute<DescriptionAttribute>();
                    if (attribute != null)
                    {
                        return attribute.Description;
                    }
                }

                return enumName;
            }
            return "";
        }
    }
}
