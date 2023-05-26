using System;
using UnityEngine;
using Utils.LiveData;
using Zenject;

namespace Shared.SavingServices
{
    public class GemsService: IInitializable, IDisposable
    {
        private const string PlayerPrefsKey = "GemsCount";
        private readonly MutableLiveData<int> _gemsCount = new();
        public ILiveData<int> GemsCount => _gemsCount;

        public void Initialize()
        {
            _gemsCount.Value = PlayerPrefs.GetInt(PlayerPrefsKey, 0);
        }

        public void Dispose()
        {
            PlayerPrefs.SetInt(PlayerPrefsKey, _gemsCount.Value);
            PlayerPrefs.Save();
        }
    }
}