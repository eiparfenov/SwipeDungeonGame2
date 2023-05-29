using UnityEngine;

namespace Shared
{
    public record RoomChanged(Vector2Int PreviousCellPos ,Vector2Int CellPos);
}