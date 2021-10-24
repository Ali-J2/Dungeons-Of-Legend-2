namespace DungeonBrickStudios
{
    public class EventVariable<TSource, TValue> : EventVariableBase
    {
        protected bool triggerSameValue;
        protected readonly TSource source;
        protected TValue _value;
        public TValue eventStackValue { protected set; get; }
        public override object objectValue => value;

        public virtual TValue value
        {
            get { return _value; }
            set
            {
                if (!triggerSameValue)
                {
                    if (_value == null && value == null)
                        return;

                    if (_value != null && _value.Equals(value))
                        return;
                }

                _value = value;

                EventVariableProperties<TSource, TValue> eventProperty = new EventVariableProperties<TSource, TValue>(this, source, _value);
                EventVariableManager.instance.AddEvent(eventProperty);
            }
        }

        public EventVariable(TSource source, TValue startValue, bool triggerSameValue = false)
        {
            this.triggerSameValue = triggerSameValue;
            this._value = startValue;
            this.eventStackValue = startValue;
            this.source = source;
        }

        public void ReplaceOnValueChange(EventDelegateSource callback, EventVariable<TSource, TValue> oldEventVariable, bool callImmedietly = true)
        {
            oldEventVariable.onValueChangeSource -= callback;
            onValueChangeSource += callback;

            if (callImmedietly)
                callback(source, oldEventVariable.eventStackValue, eventStackValue);
        }

        public void ReplaceOnValueChange(EventDelegate callback, EventVariable<TSource, TValue> oldEventVariable, bool callImmedietly = true)
        {
            oldEventVariable.onValueChange -= callback;
            onValueChange += callback;

            if (callImmedietly)
                callback(oldEventVariable.eventStackValue, eventStackValue);
        }

        public event EventDelegate onValueChange;
        public event EventDelegateSource onValueChangeSource;

        public delegate void EventDelegate(TValue oldValue, TValue newValue);
        public event EventDelegate onValueChangeImmediate
        {
            add { onValueChange += value; value(default, eventStackValue); }
            remove { value(eventStackValue, default); onValueChange -= value; }
        }
        
        public delegate void EventDelegateSource(TSource source, TValue oldValue, TValue newValue);
        public event EventDelegateSource onValueChangeSourceImmediate
        {
            add { onValueChangeSource += value; value(source, default, eventStackValue); }
            remove { value(source, eventStackValue, default); onValueChangeSource -= value; }
        }

        public virtual void TriggerOnValueChange(TSource source, TValue newValue)
        {
            TValue oldValue = eventStackValue;
            eventStackValue = newValue;

            if (onValueChangeSource != null)
                onValueChangeSource.Invoke(source, oldValue, newValue);

            if (onValueChange != null)
                onValueChange.Invoke(oldValue, newValue);

            TriggerOnValueChange();
        }
        
        public override string ToString()
        {
            string valueString = value != null ? value.ToString() : "null";
            return string.Format("EventVariable({0})", valueString);
        }
    }
}