using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProgramSub1
{
    //q = w + v 
    //v = (x, y, z)

    class Quaternion
    {
        //実部
        public float w;
        //虚部
        public Vector3 v;

        /*コンストラクタ*/
        public Quaternion()
        {
            w = 0;
            v = Vector3.zero;
        }
        public Quaternion(float w, float x, float y, float z)
        {
            this.w = w;
            this.v.x = x;
            this.v.y = y;
            this.v.z = z;
        }
        public Quaternion(float w, Vector3 v)
        {
            this.w = w;
            this.v = v;
        }

        /*演算子のオーバーロード*/
        public static Quaternion operator+(Quaternion q1, Quaternion q2)
        {
            return new Quaternion(q1.w + q2.w, q1.v + q2.v);
        }
        public static Quaternion operator-(Quaternion q1, Quaternion q2)
        {
            return new Quaternion(q1.w - q2.w, q1.v - q2.v);
        }
        public static Quaternion operator*(Quaternion q1, Quaternion q2)
        {
            float myW;
            Vector3 myV;
            myW = (q1.w * q2.w) - Vector3.Dot(q1.v, q2.v);
            myV = (q2.v * q1.w) + (q1.v * q2.w) + Vector3.Cross(q1.v, q2.v); 
            return new Quaternion(myW, myV);
        }
        public static Quaternion operator*(Quaternion q1, float a)
        {
            return new Quaternion(q1.w * a, q1.v * a);
        }

        //共役
        public Quaternion GetConjugate(Quaternion q)
        {
            return new Quaternion(q.w, v * -1);
        }

        /// <summary>
        /// Quatenionの回転
        /// </summary>
        /// <param name="theta">回転量(弧度)</param>
        /// <param name="axis">回転軸(単位ベクトル)</param>
        /// <returns></returns>
        public Quaternion Rotation(float theta,Vector3 axis)
        {
            //q'= rqr*
            //q = (w, X)
            //r = (cosθ/2, vsinθ/2)
            //r* = (cosθ/2, v-sinθ/2)
            Quaternion r = new Quaternion((float)Math.Cos(theta / 2), axis * (float)Math.Sin(theta / 2));

            return this * r * GetConjugate(r);
        }

        /// <summary>
        /// Quaternionの回転
        /// </summary>
        /// <param name="theta">回転量(弧度)</param>
        /// <param name="axis">回転軸(単位ベクトル)</param>
        /// <param name="q">回転させたいQuaternion</param>
        /// <returns></returns>
        public Quaternion Rotation(float theta, Vector3 axis,Quaternion q)
        {
            //q'= rqr*
            //q = (w, X)
            //r = (cosθ/2, vsinθ/2)
            //r* = (cosθ/2, v-sinθ/2)
            Quaternion r = new Quaternion((float)Math.Cos(theta / 2), axis * (float)Math.Sin(theta / 2));

            return q * r * GetConjugate(r);
        }

        //内積
        public static float Dot(Quaternion q1, Quaternion q2)
        {
            return ((q1.w * q2.w) + (q1.v.x * q2.v.x) + (q1.v.y * q2.v.y) + (q1.v.z * q2.v.z));
        }

        //球面線形補間
        public Quaternion Slerp(Quaternion from, Quaternion to, float t)
        {
            //そもそも計算がいらない
            if (t >= 1) return to;
            if (t <= 0) return from;

            //fromとtoの内積を求め、sinθを出す
            //「DirectX 3Dリアルタイムアニメーション」より
            //sinθ = √1 - (q1・q2)^2
            float dot = Dot(from, to);
            float temp = 1 - (dot * dot);

            //ルートの中身が負の値だと虚数になってしまう
            if(temp <= 0) return from;

            float sinTheta = (float)Math.Sqrt(temp);

            //sinθが０の場合同じ方向を向いている
            if (sinTheta == 0.0) return from;

            //本来cosθ = (q1・q2) / abs(q1) * abs(q2)なのだが
            //球面線形補間では単位球面上で考えるため長さは１となり
            //cosθ = (q1・q2)となる
            float theta = (float)Math.Acos(dot);
            //q1,q2間を動くqは下記の式で求められる
            //        sin(1-t)θ          sin tθ
            //q =    ---------- q1   +  ----------q2
            //          sinθ              sinθ
            float temp1 = (float)Math.Sin((1 - t) * theta) / sinTheta;
            float temp2 = (float)Math.Sin(t * theta) / sinTheta;
            return (from * temp1) + (to * temp2);
        }
    }
}
