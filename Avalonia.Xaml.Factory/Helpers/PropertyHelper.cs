using System.Reflection;

namespace Avalonia.Xaml.Factory.Helpers
{
    public static class PropertyHelper
    {
        /// <summary>
        /// Проверяет, имеет ли свойство публичный setter.
        /// Свойства с приватными сеттерами игнорируются.
        /// </summary>
        /// <param name="property">AvaloniaProperty для проверки.</param>
        /// <returns>True, если свойство можно изменить публично, иначе false.</returns>
        public static bool CanSetProperty(AvaloniaProperty property)
        {
            // Используем рефлексию для получения информации о свойстве
            var propertyInfo = property.OwnerType.GetProperty(property.Name, BindingFlags.Public | BindingFlags.Instance);

            // Проверяем наличие сеттера и его уровень доступа
            if (propertyInfo != null && propertyInfo.CanWrite)
            {
                // Проверяем, является ли сеттер публичным
                var setMethod = propertyInfo.GetSetMethod(nonPublic: true);
                return setMethod != null && setMethod.IsPublic;
            }

            return false;
        }
    }
}