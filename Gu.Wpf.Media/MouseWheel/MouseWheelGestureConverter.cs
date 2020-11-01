namespace Gu.Wpf.Media
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Windows.Input;

    /// <inheritdoc />
    public class MouseWheelGestureConverter : TypeConverter
    {
        private const char ModifiersDelimiter = '+';
        private static readonly TypeConverter ModifierKeysConverter = TypeDescriptor.GetConverter(typeof(ModifierKeys));

        /// <inheritdoc />
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        /// <inheritdoc />
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object source)
        {
            var text = source as string;
            if (text != null)
            {
                if (string.IsNullOrWhiteSpace(text))
                {
                    return new MouseWheelGesture(MouseWheelDirection.None, ModifierKeys.None);
                }

                int index = text.LastIndexOf(ModifiersDelimiter);
                if (index >= 0)
                {
                    var modifiersToken = text.Substring(0, index);
                    //// ReSharper disable once PossibleNullReferenceException
                    var modifiers = (ModifierKeys)ModifierKeysConverter.ConvertFrom(context, culture, modifiersToken);
                    var directionToken = text.Substring(index + 1);
                    MouseWheelDirection direction;
                    if (Enum.TryParse(directionToken, out direction))
                    {
                        return new MouseWheelGesture(direction, modifiers);
                    }

                    throw this.GetConvertFromException(source);
                }
                else
                {
                    MouseWheelDirection direction;
                    if (Enum.TryParse(text, out direction))
                    {
                        return new MouseWheelGesture(direction, ModifierKeys.None);
                    }

                    throw this.GetConvertFromException(source);
                }
            }

            throw this.GetConvertFromException(source);
        }

        /// <inheritdoc />
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            // We can convert to an InstanceDescriptor or to a string.
            if (destinationType == typeof(string))
            {
                // When invoked by the serialization engine we can convert to string only for known type
                var gesture = context?.Instance as MouseWheelGesture;
                if (gesture != null)
                {
                    if (System.Windows.Input.ModifierKeysConverter.IsDefinedModifierKeys(gesture.Modifiers) &&
                        gesture.MouseAction == MouseAction.WheelClick &&
                        MouseWheelDirectionExt.IsDefined(gesture.Direction))
                    {
                        return true;
                    }
                }
            }

            return base.CanConvertTo(context, destinationType);
        }

        /// <inheritdoc />
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType is null)
            {
                throw new ArgumentNullException(nameof(destinationType));
            }

            if (destinationType == typeof(string))
            {
                if (value is null)
                {
                    return string.Empty;
                }

                var gesture = value as MouseWheelGesture;
                if (gesture != null)
                {
                    if (gesture.Modifiers == ModifierKeys.None)
                    {
                        return gesture.Direction.ToName();
                    }

                    return $"{ModifierKeysConverter.ConvertTo(context, culture, gesture.Modifiers, destinationType)}+{gesture.Direction.ToName()}";
                }
            }

            throw this.GetConvertToException(value, destinationType);
        }
    }
}