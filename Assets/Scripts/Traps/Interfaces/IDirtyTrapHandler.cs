namespace Traps.Interfaces
{
    public interface IDirtyTrapHandler
    {
        void OnTrapEnter(float speedK);
        void OnTrapExit(float speedK);
    }
}