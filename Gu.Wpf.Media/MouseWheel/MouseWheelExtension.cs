namespace Gu.Wpf.Media.MouseWheel
{
    using System;
    using System.Windows.Input;
    using System.Windows.Markup;

    /// <inheritdoc />
    [MarkupExtensionReturnType(typeof(MouseWheelGesture))]
    public class MouseWheelExtension : MarkupExtension
    {
        public MouseWheelExtension(MouseWheelDirection direction)
        {
            this.Direction = direction;
            this.Modifier = ModifierKeys.None;
        }

        public MouseWheelExtension(MouseWheelDirection direction, ModifierKeys modifier)
        {
            this.Direction = direction;
            this.Modifier = modifier;
        }

        public MouseWheelDirection Direction { get; set; }

        public ModifierKeys Modifier { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new MouseWheelGesture(this.Modifier, this.Direction);
        }
    }
}