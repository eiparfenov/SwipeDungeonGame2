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

        public static async UniTask WaitForAction(Func<Action> actionGetter, PlayerLoopTiming playerLoopTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default)
        {
            var action = actionGetter();
            
            var finished = false;
            var finishedAction = new Action(() => finished = true);
            action += finishedAction;

            await ContinueOnCancel(() => UniTask.WaitUntil(() => finished, playerLoopTiming, cancellationToken));
            action -= finishedAction;
        }

        public static async UniTask<T> WaitForNewValue<T>(Func<Action<T>> actionGetter, Predicate<T> predicate = null,
            PlayerLoopTiming playerLoopTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default)
        {
            var action = actionGetter();
            
            var finished = false;
            T result = default;
            var finishedAction = new Action<T>(value =>
            {
                if (predicate == null || predicate(value))
                {
                    result = value;
                    finished = true;
                }
            });
            action += finishedAction;

            await ContinueOnCancel(() => UniTask.WaitUntil(() => finished, playerLoopTiming, cancellationToken));
            action -= finishedAction;
            return result;
        }
    }
}