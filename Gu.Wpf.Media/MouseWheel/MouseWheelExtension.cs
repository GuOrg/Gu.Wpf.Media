// not sure about exposing this
namespace Gu.Wpf.Media
{
    using System;
    using System.Windows.Input;
    using System.Windows.Markup;

    /// <inheritdoc />
    [MarkupExtensionReturnType(typeof(MouseWheelGesture))]
    public class MouseWheelExtension : MarkupExtension
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MouseWheelExtension"/> class.
        /// </summary>
        /// <param name="direction"><see cref="MouseWheelDirection"/>.</param>
        public MouseWheelExtension(MouseWheelDirection direction)
            : this(ModifierKeys.None, direction)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseWheelExtension"/> class.
        /// </summary>
        /// <param name="direction"><see cref="MouseWheelDirection"/>.</param>
        /// <param name="modifiers"><see cref="ModifierKeys"/>.</param>
        public MouseWheelExtension(ModifierKeys modifiers, MouseWheelDirection direction)
            : this(new MouseWheelGesture(direction, modifiers))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseWheelExtension"/> class.
        /// </summary>
        /// <param name="gesture"><see cref="MouseWheelGesture"/>.</param>
        public MouseWheelExtension(MouseWheelGesture gesture)
        {
            this.Gesture = gesture;
        }

        /// <summary>
        /// Gets or sets the gesture <see cref="MouseWheelGesture"/>.
        /// </summary>
        [ConstructorArgument("gesture")]
        public MouseWheelGesture Gesture { get; set; }

        /// <summary>
        /// Gets or sets direction <see cref="MouseWheelDirection"/>.
        /// </summary>
        public MouseWheelDirection Direction
        {
            get => this.Gesture.Direction;
            set => this.Gesture.Direction = value;
        }

        /// <summary>
        /// Gets or sets modifiers <see cref="ModifierKeys"/>.
        /// </summary>
        public ModifierKeys Modifiers
        {
            get => this.Gesture.Modifiers;
            set => this.Gesture.Modifiers = value;
        }

        /// <inheritdoc />
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this.Gesture;
        }
    }
}
