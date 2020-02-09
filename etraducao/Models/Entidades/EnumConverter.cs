using System.Linq;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace etraducao.Models.Entidades
{
    public static class EnumConverter
    {
        public static string DisplayName(this TiposDeSolicitacao enumValue)
        {
            return enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault()?.GetCustomAttribute<DisplayAttribute>()?.Name ?? enumValue.ToString();
        }

    }
}
