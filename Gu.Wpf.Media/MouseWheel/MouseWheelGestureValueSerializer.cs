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
            var result = false;
            var gesture = value as MouseWheelGesture;

            if (gesture != null)
            {
                if (ModifierKeysConverter.IsDefinedModifierKeys(gesture.Modifiers) &&
                    gesture.MouseAction == MouseAction.WheelClick &&
                    MouseWheelDirectionExt.IsDefined(gesture.Direction))
                {
                    result = true;
                }
            }

            return result;
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