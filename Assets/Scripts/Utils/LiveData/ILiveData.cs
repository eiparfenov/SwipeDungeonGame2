using System;

namespace Utils.LiveData
{
    public interface ILiveData<T>
    {
        T Value { get; }
        event Action<T> onValueChanged;
        event Action<T, T> onValueChangedWithBuffer;
    }
}