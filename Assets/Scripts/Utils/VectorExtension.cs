using UnityEngine;
using System.Collections;

namespace Utils
{

    public static class VectorExtension
    {
        public static float Remap(this float value, in float start1, in float stop1, in float start2, in float stop2) => start2 + (stop2 - start2) * ((value - start1) / (stop1 - start1));

        public static float Remap(this float value, in (float, float) input, in (float, float) output) => output.Item1 + (output.Item2 - output.Item1) * ((value - input.Item1) / (input.Item2 - input.Item1));

        public static Vector2 ToXZ(this in Vector3 vector) => new Vector2(vector.x, vector.z);
        public static Vector2 ToVector2(this in Vector3 vector) => vector;
        public static Vector3 ToVector3(this in Vector2 vector) => vector;
        public static Vector3 ToVector3(this in Vector2 vector, float z) => new Vector3(vector.x, vector.y, z);
        public static Vector3 ToVector3FromXZ(this in Vector2 xzVector) => new Vector3(xzVector.x, 0, xzVector.y);
        public static Vector3 ToVector3FromXZ(this in Vector2 xzVector, float y) => new Vector3(xzVector.x, y, xzVector.y);
        public static Vector3 AdaptY(this in Vector3 xzVector, in float y) => new Vector3(xzVector.x, y, xzVector.z);

        public static Vector3 Round(this in Vector3 vector, in float scale) => new Vector3(
            Mathf.Round(vector.x / scale) * scale,
            Mathf.Round(vector.y / scale) * scale,
            Mathf.Round(vector.z / scale) * scale);

        public static Vector2 Decrease(this in Vector2 vector, in float amount) => new Vector2(
            Mathf.Sign(vector.x) * Mathf.Max(Mathf.Abs(vector.x) - amount, 0),
            Mathf.Sign(vector.y) * Mathf.Max(Mathf.Abs(vector.y) - amount, 0));
        public static Vector3 Decrease(this in Vector3 vector, in float amount) => new Vector3(
            Mathf.Sign(vector.x) * Mathf.Max(Mathf.Abs(vector.x) - amount, 0),
            Mathf.Sign(vector.y) * Mathf.Max(Mathf.Abs(vector.y) - amount, 0),
            Mathf.Sign(vector.z) * Mathf.Max(Mathf.Abs(vector.z) - amount, 0));

        //projection2D
        public static Vector2 ProjectionToXAxis(this Vector2 vector, Vector2 start, float xAxisValue)
        {
            return new Vector2(
                xAxisValue,
                (vector.y - start.y) * (xAxisValue - start.x) / (vector.x - start.x) + start.y);
        }
        public static Vector2 ProjectionToYAxis(this Vector2 vector, Vector2 start, float yAxisValue)
        {
            return new Vector2(
                (vector.x - start.x) * (yAxisValue - start.y) / (vector.y - start.y) + start.x,
                yAxisValue);
        }

        //projection3D
        public static Vector3 ProjectionToZAxis(this Vector3 vector, Vector3 start, float zAxisValue)
        {
            return new Vector3(
                (vector.x - start.x) * (zAxisValue - start.z) / (vector.z - start.z) + start.x,
                (vector.y - start.y) * (zAxisValue - start.z) / (vector.z - start.z) + start.y,
                zAxisValue);
        }
        public static Vector3 ProjectionToXAxis(this Vector3 vector, Vector3 start, float xAxisValue)
        {
            return new Vector3(
                xAxisValue,
                (vector.y - start.y) * (xAxisValue - start.x) / (vector.x - start.x) + start.y,
                (vector.z - start.z) * (xAxisValue - start.x) / (vector.x - start.x) + start.z);
        }
        public static Vector3 ProjectionToYAxis(this Vector3 vector, Vector3 start, float yAxisValue)
        {
            return new Vector3(
                (vector.x - start.x) * (yAxisValue - start.y) / (vector.y - start.y) + start.x,
                yAxisValue,
                (vector.z - start.z) * (yAxisValue - start.y) / (vector.y - start.y) + start.z);
        }

        public static Vector3 ToAbs(this Vector3 vector)
        {
            return new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
        }

        public static Vector3 IntersectionPoint(Vector3 origin, Vector3 target, Vector3 center, float radius)
        {
            var centerDir = center - origin;
            var forwardLength = Vector3.Project(centerDir, (target - origin).normalized);
            var orthogonal = Vector3.Distance(origin + forwardLength, center);

            if (orthogonal > radius)
                orthogonal = radius;

            var innerForward = Mathf.Sqrt((radius * radius) - (orthogonal * orthogonal));
            var dist = forwardLength.magnitude - innerForward;

            return origin + (target - origin).normalized * dist;
        }

        ////angle
        //public static Vector2 RadianToVector2(this float radian)
        //{
        //    return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
        //}

        //public static Vector2 DegreeToVector2(this float degree)
        //{
        //    return RadianToVector2(degree * Mathf.Deg2Rad);
        //}
        //public static Vector3 SnapTo(this Vector3 v3, float snapAngle)
        //{
        //    float angle = Vector3.Angle(v3, Vector3.up);

        //    if (angle < snapAngle / 2.0f)
        //        return Vector3.up * v3.magnitude;

        //    if (angle > 180.0f - snapAngle / 2.0f)
        //        return Vector3.down * v3.magnitude;

        //    float t = Mathf.Round(angle / snapAngle);
        //    float deltaAngle = (t * snapAngle) - angle;

        //    Vector3 axis = Vector3.Cross(Vector3.up, v3);
        //    Quaternion q = Quaternion.AngleAxis(deltaAngle, axis);
        //    return q * v3;
        //}

        //public static float GetDegreeFloatFromVector2(Vector2 vector2)
        //{
        //    return Mathf.Rad2Deg * Mathf.Atan2(vector2.y, vector2.x);
        //}

        //public static Vector2 SnapDirection(this Vector2 vector2)
        //{
        //    return DegreeToVector2(Mathf.Round(GetDegreeFloatFromVector2(vector2) / 90) * 90);
        //}
    }




}
