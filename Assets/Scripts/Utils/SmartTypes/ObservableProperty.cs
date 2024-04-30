using System;

namespace Utils.SmartTypes
{
    public class ObservableProperty<T> : IObservableProperty<T>
    {
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                if (Equals(_value, value)) return;
                _value = value;
                OnValueChanged?.Invoke(_value);
            }
        }

        public event Action<T> OnValueChanged;

        public ObservableProperty(T initialValue)
        {
            _value = initialValue;
        }
    }
}