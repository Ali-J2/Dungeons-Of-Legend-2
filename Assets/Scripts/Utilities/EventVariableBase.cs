namespace DungeonBrickStudios
{
    public abstract class EventVariableBase
    {
        public event EventDelegateEmpty onValueChangeEmpty;
        public delegate void EventDelegateEmpty();
        public event EventDelegateEmpty onValueChangeEmptyImmediate
        {
            add { onValueChangeEmpty += value; value(); }
            remove { value(); onValueChangeEmpty -= value; }
        }

        protected void TriggerOnValueChange()
        {
            if (onValueChangeEmpty != null)
                onValueChangeEmpty();
        }

        public abstract object objectValue { get; }
    }
}
