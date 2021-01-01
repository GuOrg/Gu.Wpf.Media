namespace Gu.Wpf.Media
{
    using System.ComponentModel;
    using System.Windows.Input;
    using System.Windows.Markup;

    /// <summary>
    /// MouseWheelGesture - Converter class for converting between a string and the Type of a MouseWheelGesture.
    /// </summary>
    public class MouseWheelGestureValueSerializer : ValueSerializer
    {
        private static readonly TypeConverter MouseWheelGestureTypeConverter = TypeDescriptor.GetConverter(typeof(MouseWheelGesture));

        /// <inheritdoc />
        public override bool CanConvertFromString(string value, IValueSerializerContext context)
        {
            return true;
        }

        /// <inheritdoc />
        public override bool CanConvertToString(object value, IValueSerializerContext context)
        {
            if (value is MouseWheelGesture { MouseAction: MouseAction.WheelClick } gesture &&
                ModifierKeysConverter.IsDefinedModifierKeys(gesture.Modifiers) &&
                MouseWheelDirectionExt.IsDefined(gesture.Direction))
            {
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        public override object ConvertFromString(string value, IValueSerializerContext context)
        {
            return MouseWheelGestureTypeConverter.ConvertFromString(value);
        }

        /// <inheritdoc />
        public override string ConvertToString(object value, IValueSerializerContext context)
        {
            return MouseWheelGestureTypeConverter.ConvertToInvariantString(value);
        }
    }
}
