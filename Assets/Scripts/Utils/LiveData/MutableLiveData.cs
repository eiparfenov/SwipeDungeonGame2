using System;

namespace Utils.LiveData
{
    public class MutableLiveData<T>: ILiveData<T>
    {
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                if(Equals(_value, value)) return;
                
                onValueChanged?.Invoke(value);
                onValueChangedWithBuffer?.Invoke(_value, value);
                _value = value;
            }
        }

        public event Action<T> onValueChanged;
        public event Action<T, T> onValueChangedWithBuffer;

        public MutableLiveData()
        {
            _value = default;
        }

        public MutableLiveData(T value)
        {
            _value = value;
        }
    }
}