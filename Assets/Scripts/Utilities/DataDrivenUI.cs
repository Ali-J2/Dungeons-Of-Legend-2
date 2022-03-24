namespace DungeonBrickStudios
{
    using UnityEngine;

    public abstract class DataDrivenUI<T> : MonoBehaviour
    {
        private readonly EventVariable<DataDrivenUI<T>, T> _data;
        public T data
        {
            set { _data.value = value; }
            get { return _data.value; }
        }

        protected DataDrivenUI()
        {
            _data = new EventVariable<DataDrivenUI<T>, T>(this, default);
        }

        private void OnEnable()
        {
            _data.onValueChangeImmediate += OnValueChanged_Data;
            OnEnableSub();
        }

        private void OnDisable()
        {
            _data.onValueChangeImmediate -= OnValueChanged_Data;
            OnDisableSub();
        }

        protected virtual void OnEnableSub() { }
        protected virtual void OnDisableSub() { }

        protected abstract void OnValueChanged_Data(T oldValue, T newValue);
    }
}
