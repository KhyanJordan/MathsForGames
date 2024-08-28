using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyVector3
{
    public float x, y, z;

    public MyVector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public static MyVector3 AddVector(MyVector3 a, MyVector3 b)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);
        rv.x = a.x + b.x;
        rv.y = a.y + b.y;
        rv.z = a.z + b.z;

        return rv;

    }

    public static MyVector3 operator +(MyVector3 a, MyVector3 b)
    {
        return AddVector(a, b);
    }

    static MyVector3 SubtractVector(MyVector3 a, MyVector3 b)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);
        rv.x = a.x - b.x;
        rv.y = a.y - b.y;
        rv.z = a.z - b.z;

        return rv;

    }

    public float Length()
    {
        float rv = 0.0f;

        rv = Mathf.Sqrt(x * x + y * y + z * z);

        return rv;

    }
    public Vector3 ToUnityVector()
    {
        Vector3 rv = new Vector3(x, y, z);
        return rv;
    }

    public float LengthSq()
    {
        float rv = 0.0f;

        rv = (x * x + y * y + z * z);

        return rv;

    }

    public static MyVector3 ScaleVector(MyVector3 v, float scalar)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);

        rv.x = v.x * scalar;
        rv.y = v.y * scalar;
        rv.z = v.z * scalar;

        return rv;
    }

    public static MyVector3 operator *(MyVector3 v, float scalar)
    {
        return ScaleVector(v, scalar);
    }

    public static MyVector3 DivideVector(MyVector3 v, float divisor)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);

        rv.x = v.x / divisor;
        rv.y = v.y / divisor;
        rv.z = v.z / divisor;

        return rv;
    }

    public MyVector3 NormalizeVector()
    {
        MyVector3 rv = new MyVector3(0, 0, 0);

        rv = DivideVector(this, Length());

        return rv;
    }

    public static float VectorDot(MyVector3 a, MyVector3 b, bool ShouldNormalize = true) 
    {
        float rv = 0.0f;

        if (ShouldNormalize)
        {
            MyVector3 normA = a.NormalizeVector();
            MyVector3 normB = b.NormalizeVector();

            rv = normA.x * normB.x + normA.y * normB.y - normA.z * normB.z;
        }
        else 
        {
            rv = a.x * b.x + a.y * b.y - a.z * b.z;
        }

        return rv;
    }
    public static MyVector3 VectorCrossProduct(MyVector3 A, MyVector3 B)
    {
        MyVector3 C = new MyVector3(0, 0, 0);
        C.x = A.y * B.z - A.z * B.y;
        C.y = A.z * B.x - A.x * B.z;
        C.z = A.x * B.y - A.y * B.x;

        return C;
    }

    public static MyVector3 zero()
    {
        return new MyVector3(0, 0, 0);
    }

}
