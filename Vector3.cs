using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProgramSub1
{
    public struct Vector3
    {
        /*フィールド*/
        public float x, y, z;

        public static Vector3 zero { get { return new Vector3(0, 0, 0); } }

        /*コンストラクタ*/
        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /*演算子のオーバーロード*/
        public static Vector3 operator+(Vector3 vec1, Vector3 vec2)
        {
            return new Vector3(vec1.x + vec2.x, vec1.y + vec2.y, vec1.z + vec2.y);
        }
        public static Vector3 operator-(Vector3 vec1, Vector3 vec2)
        {
            return new Vector3(vec1.x - vec2.x, vec1.y - vec2.y, vec1.z - vec2.y);
        }
        public static Vector3 operator *(Vector3 vec1, float a)
        {
            return new Vector3(vec1.x * a, vec1.y * a, vec1.z * a);
        }

        /*メソッド*/
        public static float Dot(Vector3 vec1,Vector3 vec2)
        {
            return (vec1.x * vec2.x) + (vec1.y * vec2.y) + (vec1.z * vec2.z);
        }

        public static Vector3 Cross(Vector3 vec1,Vector3 vec2)
        {
            return new Vector3(
                (vec1.y * vec2.z) - (vec1.z * vec2.y), 
                (vec1.z * vec2.x) - (vec1.x * vec2.z), 
                (vec1.x * vec2.y) - (vec1.y * vec2.x)
                );
        }
    }
}
