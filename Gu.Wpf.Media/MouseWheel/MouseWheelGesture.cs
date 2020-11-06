namespace Gu.Wpf.Media
{
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using System.Windows.Markup;

    /// <inheritdoc />
    [TypeConverter(typeof(MouseWheelGestureConverter))]
    [ValueSerializer(typeof(MouseWheelGestureValueSerializer))]
#pragma warning disable INPC001 // Implement INotifyPropertyChanged.
    public class MouseWheelGesture : MouseGesture
#pragma warning restore INPC001 // Implement INotifyPropertyChanged.
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MouseWheelGesture"/> class.
        /// </summary>
        public MouseWheelGesture()
            : this(MouseWheelDirection.None, ModifierKeys.None)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseWheelGesture"/> class.
        /// </summary>
        /// <param name="direction">The <see cref="MouseWheelDirection"/>.</param>
        public MouseWheelGesture(MouseWheelDirection direction)
            //// ReSharper disable once IntroduceOptionalParameters.Global
            : this(direction, ModifierKeys.None)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseWheelGesture"/> class.
        /// </summary>
        /// <param name="direction">The <see cref="MouseWheelDirection"/>.</param>
        /// <param name="modifiers">Modifiers.</param>
        public MouseWheelGesture(MouseWheelDirection direction, ModifierKeys modifiers)
            : base(MouseAction.WheelClick, modifiers)
        {
            this.Direction = direction;
        }

        /// <summary>
        /// Hiding <see cref="MouseGesture.MouseAction"/> here.
        /// </summary>
        public new MouseAction MouseAction => base.MouseAction;

        /// <summary>
        /// Direction <see cref="MouseWheelDirection"/>.
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

        private bool MatchesDirection(MouseWheelEventArgs? e)
        {
            if (e is null)
            {
                return false;
            }

            return this.Direction switch
            {
                MouseWheelDirection.None => e.Delta == 0,
                MouseWheelDirection.Up => e.Delta > 0,
                MouseWheelDirection.Down => e.Delta < 0,
                _ => throw new InvalidEnumArgumentException(nameof(this.Direction), (int)this.Direction, typeof(MouseWheelDirection)),
            };
        }
    }
}
