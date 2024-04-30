using System;

namespace Utils.SmartTypes
{
    public interface IObservableProperty<T>
    {
        public T Value { get; }
        public event Action<T> OnValueChanged;
    }
}