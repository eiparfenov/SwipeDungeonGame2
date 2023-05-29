using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Utils.Extensions
{
    public static class UniTaskExt
    {
        public static async UniTask Delay(float seconds, DelayType delayType = DelayType.DeltaTime, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken))
        {
            await UniTask.Delay(TimeSpan.FromSeconds(seconds), delayType, delayTiming, cancellationToken);
        }

        public static async UniTask ContinueOnCancel(Func<UniTask> toExecute)
        {
            try
            {
                await toExecute();
            }
            catch (OperationCanceledException e) { }
        }
    }
}