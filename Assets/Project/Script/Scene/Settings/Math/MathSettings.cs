using System;
using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    [CreateAssetMenu(fileName = "MathSettings", menuName = "Settings/Math")]
    public class MathSettings : ScriptableObject
    {
        [SerializeField]
        [MinValue(0)]
        [MaxValue(int.MaxValue)]
        private int precision;

        public int Precision => precision;
        public float Epsilon => Mathf.Pow(10, -precision);

        public float Round(float f)
        {
            return (float)Round((double)f);
        }

        public double Round(double d)
        {
            return Math.Round(d, precision, MidpointRounding.AwayFromZero);
        }

        public Vector3 Round(Vector3 v)
        {
            return new Vector3(Round(v.x), Round(v.y), Round(v.z));
        }

        public Quaternion Round(Quaternion q)
        {
            return new Quaternion(Round(q.x), Round(q.y), Round(q.z), Round(q.w));
        }
    }
}
