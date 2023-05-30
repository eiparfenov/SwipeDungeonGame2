using System;
using Cysharp.Threading.Tasks;
using Shared;
using UnityEngine;
using Zenject;

namespace Maze
{
    public class CameraMover: IInitializable, IDisposable
    {
        private readonly Camera _camera;
        private readonly SignalBus _signalBus;
        private readonly GlobalSettings _globalSettings;

        public CameraMover(SignalBus signalBus, GlobalSettings globalSettings, Camera camera)
        {
            _signalBus = signalBus;
            _globalSettings = globalSettings;
            _camera = camera;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<RoomChanged>(MazeOnRoomChanged);
        }

        private async void MazeOnRoomChanged(RoomChanged obj)
        {
            var progress = 0f;
            var startPosition = _camera.transform.position;
            var targetPosition = (Vector3)(_globalSettings.RoomSize * obj.CellPos) + Vector3.back * 10;

            while (_camera && progress < 1f) 
            {
                progress += Time.deltaTime / _globalSettings.RoomExitTime;
                _camera.transform.position = Vector3.Lerp(startPosition, targetPosition, progress);
                await UniTask.Yield(PlayerLoopTiming.Update);
            }

            if (_camera)
            {
                _camera.transform.position = targetPosition;
            }

        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<RoomChanged>(MazeOnRoomChanged);
        }
    }
}