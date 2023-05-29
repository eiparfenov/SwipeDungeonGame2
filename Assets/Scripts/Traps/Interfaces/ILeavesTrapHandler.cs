using UnityEngine;

namespace Traps.Interfaces
{
    public interface ILeavesTrapHandler
    {
        void OnTrapEnter(LeavesTrapEnterDto leavesTrapEnterDto);
    }

    public class LeavesTrapEnterDto
    {
        public float TrappedTime { get; init; }
        public Vector3 TrapPosition { get; init; }
        public float TrapEscapeRange { get; init; }
    }
}