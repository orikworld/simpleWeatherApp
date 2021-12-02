using System;
using System.Globalization;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;

namespace simpleWeatherApp.Core.Client.Behaviors
{
    public class EventToCommandBehavior : BehaviorBase<VisualElement>
    {
        #region Private Fields

        private Delegate _handler;

        private EventInfo _eventInfo;

        #endregion

        #region Bindable Properties

        public static readonly BindableProperty EventNameProperty =
            BindableProperty.Create(nameof(EventName), typeof(string), typeof(EventToCommandBehavior));

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(EventToCommandBehavior));

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(EventToCommandBehavior));

        public static readonly BindableProperty EventArgsConverterProperty =
            BindableProperty.Create(nameof(EventArgsConverter), typeof(IValueConverter), typeof(EventToCommandBehavior));

        public static readonly BindableProperty EventArgsConverterParameterProperty =
            BindableProperty.Create(nameof(EventArgsConverterParameter), typeof(object), typeof(EventToCommandBehavior));

        #endregion

        #region Properties

        public string EventName
        {
            get => (string)GetValue(EventNameProperty);
            set => SetValue(EventNameProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public IValueConverter EventArgsConverter
        {
            get => (IValueConverter)GetValue(EventArgsConverterProperty);
            set => SetValue(EventArgsConverterProperty, value);
        }

        public object EventArgsConverterParameter
        {
            get => GetValue(EventArgsConverterParameterProperty);
            set => SetValue(EventArgsConverterParameterProperty, value);
        }

        #endregion

        #region Protected Methods

        protected override void OnAttachedTo(VisualElement bindable)
        {
            base.OnAttachedTo(bindable);

            _eventInfo = AssociatedObject.GetType().GetRuntimeEvent(EventName);

            if (_eventInfo == null)
            {
                throw new ArgumentException($"EventToCommand: Can't find any event named '{EventName}' on attached type");
            }

            AddEventHandler(_eventInfo, AssociatedObject);
        }

        protected override void OnDetachingFrom(VisualElement bindable)
        {
            if (_handler != null)
            {
                _eventInfo.RemoveEventHandler(AssociatedObject, _handler);
            }

            base.OnDetachingFrom(bindable);
        }

        #endregion

        #region Private Methods

        private void AddEventHandler(EventInfo eventInfo, object item)
        {
            var methodInfo = GetType().GetTypeInfo().GetDeclaredMethod("OnFired");
            _handler = methodInfo.CreateDelegate(eventInfo.EventHandlerType, this);

            eventInfo.AddEventHandler(item, _handler);
        }

        private void OnFired(object sender, EventArgs eventArgs)
        {
            if (Command != null)
            {
                var parameter = CommandParameter;

                if (parameter == null && eventArgs != null && eventArgs != EventArgs.Empty)
                {
                    parameter = eventArgs;

                    if (EventArgsConverter != null)
                    {
                        parameter = EventArgsConverter.Convert(eventArgs, typeof(object), EventArgsConverterParameter,
                            CultureInfo.InvariantCulture);
                    }
                }

                if (Command.CanExecute(parameter))
                {
                    Command.Execute(parameter);
                }
            }
        }

        #endregion
    }
}
