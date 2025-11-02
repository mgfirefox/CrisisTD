using System.Runtime.CompilerServices;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public static class DebugUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawWireSphere(Vector3 position, float radius)
        {
            DrawWireSphere(position, radius, Color.white);
        }

        public static void DrawWireSphere(Vector3 position, float radius, Color color,
            float duration = 0.0f, int segmentCount = 32)
        {
            if (segmentCount <= 0)
            {
                return;
            }

            float step = Mathf.PI * 2 / segmentCount;

            Vector3 lastPointX =
                position + new Vector3(0, Mathf.Sin(0.0f) * radius, Mathf.Cos(0.0f) * radius);
            Vector3 lastPointY = position +
                                 new Vector3(Mathf.Sin(0.0f) * radius, 0.0f,
                                     Mathf.Cos(0.0f) * radius);
            Vector3 lastPointZ = position +
                                 new Vector3(Mathf.Sin(0.0f) * radius, Mathf.Cos(0.0f) * radius,
                                     0.0f);

            for (int i = 1; i <= segmentCount; i++)
            {
                float angle = step * i;

                Vector3 nextPointX = position + new Vector3(0.0f, Mathf.Sin(angle) * radius,
                    Mathf.Cos(angle) * radius);
                Vector3 nextPointY = position + new Vector3(Mathf.Sin(angle) * radius, 0.0f,
                    Mathf.Cos(angle) * radius);
                Vector3 nextPointZ = position + new Vector3(Mathf.Sin(angle) * radius,
                    Mathf.Cos(angle) * radius, 0.0f);

                Debug.DrawLine(lastPointX, nextPointX, color, duration);
                Debug.DrawLine(lastPointY, nextPointY, color, duration);
                Debug.DrawLine(lastPointZ, nextPointZ, color, duration);

                lastPointX = nextPointX;
                lastPointY = nextPointY;
                lastPointZ = nextPointZ;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawPyramid(Vector3 position, Vector3 targetPosition, Vector2 radius)
        {
            DrawPyramid(position, targetPosition, radius, Color.white);
        }

        public static void DrawPyramid(Vector3 position, Vector3 targetPosition, Vector2 radius,
            Color color, float duration = 0.0f)
        {
            Vector3 forward = Vector3Utility.GetTranslation(position, targetPosition);

            float distance = forward.magnitude;
            if (distance < 0.001f)
            {
                return;
            }

            forward.Normalize();

            Vector3 right = Vector3.Cross(forward, Vector3.up);
            if (right.sqrMagnitude < 0.001f)
            {
                right = Vector3.Cross(forward, Vector3.forward);
            }
            right.Normalize();

            Vector3 up = Vector3.Cross(right, forward).normalized;

            Vector3 center = position + forward * distance;

            Vector3 topRight = center + right * radius.x + up * radius.y;
            Vector3 topLeft = center - right * radius.x + up * radius.y;
            Vector3 bottomRight = center + right * radius.x - up * radius.y;
            Vector3 bottomLeft = center - right * radius.x - up * radius.y;

            Debug.DrawLine(position, center, color, duration);

            Debug.DrawLine(position, topRight, color, duration);
            Debug.DrawLine(position, topLeft, color, duration);
            Debug.DrawLine(position, bottomRight, color, duration);
            Debug.DrawLine(position, bottomLeft, color, duration);

            Debug.DrawLine(topLeft, topRight, color, duration);
            Debug.DrawLine(topRight, bottomRight, color, duration);
            Debug.DrawLine(bottomRight, bottomLeft, color, duration);
            Debug.DrawLine(bottomLeft, topLeft, color, duration);
        }

        public static void DrawCone(Vector3 position, Vector3 targetPosition, Vector2 radius,
            Color color, float duration = 0.0f, int segmentCount = 32)
        {
            if (segmentCount <= 0)
            {
                return;
            }

            Vector3 forward = Vector3Utility.GetTranslation(position, targetPosition);

            float distance = forward.magnitude;
            if (distance < 0.001f)
            {
                return;
            }

            forward.Normalize();

            Vector3 right = Vector3.Cross(forward, Vector3.up);
            if (right.sqrMagnitude < 0.001f)
            {
                right = Vector3.Cross(forward, Vector3.forward);
            }
            right.Normalize();

            Vector3 up = Vector3.Cross(right, forward).normalized;

            Vector3 center = position + forward * distance;

            var ringPoints = new Vector3[segmentCount];
            for (int i = 0; i < segmentCount; i++)
            {
                float angle = i / (float)segmentCount * Mathf.PI * 2f;
                float x = Mathf.Cos(angle) * radius.x;
                float y = Mathf.Sin(angle) * radius.y;

                ringPoints[i] = center + right * x + up * y;
            }

            for (int i = 0; i < segmentCount; i++)
            {
                Debug.DrawLine(position, ringPoints[i], color, duration);
            }

            for (int i = 0; i < segmentCount; i++)
            {
                int next = (i + 1) % segmentCount;
                Debug.DrawLine(ringPoints[i], ringPoints[next], color, duration);
            }

            Debug.DrawLine(position, center, color, duration);
        }
    }
}
