using UnityEngine;

namespace Extensions
{
    public static class VectorExtensions
    {
        public static bool IsZeroLenght(this Vector2 vec)
        {
            return vec.x + vec.y < 0.001f;
        }
        
        public static bool IsZeroLenght(this Vector3 vec)
        {
            return vec.x + vec.y + vec.z < 0.001f;
        }

        public static bool IsEqualZero(this float f) => f is < 0.001f and > -0.001f;
    }
}