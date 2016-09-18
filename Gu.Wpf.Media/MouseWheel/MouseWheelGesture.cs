namespace Gu.Wpf.Media.MouseWheel
{
    using System;
    using System.ComponentModel;
    using System.Windows.Input;

    /// <inheritdoc />
    [TypeConverter(typeof(MouseWheelGestureConverter))]
    public class MouseWheelGesture : MouseGesture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MouseWheelGesture"/> class.
        /// </summary>
        /// <param name="direction">The <see cref="MouseWheelDirection"/></param>
        public MouseWheelGesture(MouseWheelDirection direction)
            // ReSharper disable once IntroduceOptionalParameters.Global
            : this(direction, ModifierKeys.None)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseWheelGesture"/> class.
        /// </summary>
        /// <param name="direction">The <see cref="MouseWheelDirection"/></param>
        /// <param name="modifiers">Modifiers</param>
        public MouseWheelGesture(MouseWheelDirection direction, ModifierKeys modifiers)
            : base(MouseAction.WheelClick, modifiers)
        {
            this.Direction = direction;
        }

        /// <summary>
        /// Direction <see cref="MouseWheelDirection"/>
        /// </summary>
        public MouseWheelDirection Direction { get; set; }

        /// <inheritdoc />
        public override bool Matches(object targetElement, InputEventArgs inputEventArgs)
        {
            if (!base.Matches(targetElement, inputEventArgs))
            {
                return false;
            }

            if (this.MatchesDirection(inputEventArgs as MouseWheelEventArgs))
            {
                inputEventArgs.Handled = true;
                return true;
            }

            return false;
        }

        private bool MatchesDirection(MouseWheelEventArgs e)
        {
            if (e == null)
            {
                return false;
            }

            switch (this.Direction)
            {
                case MouseWheelDirection.None:
                    return e.Delta == 0;
                case MouseWheelDirection.Up:
                    return e.Delta > 0;
                case MouseWheelDirection.Down:
                    return e.Delta < 0;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
