namespace Gu.Wpf.Media
{
    using System;
    using System.Windows.Input;
    using System.Windows.Markup;

    // not sure about exposing this
    /// <inheritdoc />
    [MarkupExtensionReturnType(typeof(MouseWheelGesture))]
    internal class MouseWheelExtension : MarkupExtension
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MouseWheelExtension"/> class.
        /// </summary>
        /// <param name="direction"><see cref="MouseWheelDirection"/></param>
        public MouseWheelExtension(MouseWheelDirection direction)
            : this(ModifierKeys.None, direction)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseWheelExtension"/> class.
        /// </summary>
        /// <param name="direction"><see cref="MouseWheelDirection"/></param>
        /// <param name="modifiers"><see cref="ModifierKeys"/></param>
        public MouseWheelExtension(ModifierKeys modifiers, MouseWheelDirection direction)
            : this(new MouseWheelGesture(direction, modifiers))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseWheelExtension"/> class.
        /// </summary>
        public MouseWheelExtension(MouseWheelGesture gesture)
        {
            this.Gesture = gesture;
        }

        public MouseWheelGesture Gesture { get; set; }

        /// <summary>
        /// Direction <see cref="MouseWheelDirection"/>
        /// </summary>
        public MouseWheelDirection Direction
        {
            get { return this.Gesture.Direction; }
            set { this.Gesture.Direction = value; }
        }

        /// <summary>
        /// Modifiers <see cref="ModifierKeys"/>
        /// </summary>
        public ModifierKeys Modifiers
        {
            get { return this.Gesture.Modifiers; }
            set { this.Gesture.Modifiers = value; }
        }

        /// <inheritdoc />
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this.Gesture;
        }
    }
}