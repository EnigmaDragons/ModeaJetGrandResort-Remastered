using UnityEngine;

namespace EnigmaDragons.NodeSystem
{
    public static class RectExtensions
    {
        public static Rect WithOffset(this Rect rect, Vector2 offset)
            => new Rect(rect.x + offset.x, rect.y + offset.y, rect.width, rect.height);
    }
}