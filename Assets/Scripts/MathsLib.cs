using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathsLib
{
    public static float VectorToRadians(Vector2 V)
    {
        float rv = 0.0f;

        rv = Mathf.Atan(V.y / V.x);

        return rv;
    }

    public static Vector2 RadiansToVector(float angle)
    {
        Vector2 rv = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        return rv;
    }

    public static MyVector3 EulerAnglesToDirection(MyVector3 EulerAngles)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);

        rv.x = Mathf.Cos(EulerAngles.y) * Mathf.Cos(EulerAngles.x);
        rv.y = Mathf.Sin(EulerAngles.x);
        rv.z = Mathf.Cos(EulerAngles.x * Mathf.Sin(EulerAngles.y));

        //Wont work in unity needs changing for some reason

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

    public static MyVector3 VecLerp(MyVector3 A, MyVector3 B, float t)
    {
        MyVector3 C = new MyVector3(0, 0, 0);
        C.x = A.x * (1.0F - t) + B.x * t;
        C.y = A.y * (1.0F - t) + B.y * t;
        C.z = A.z * (1.0F - t) + B.z * t;
        return C;

    }

    public static MyVector3 RotateVertexAroundAxis(float Angle, MyVector3 V, MyVector3 N)
    {
        MyVector3 NPrime = (N * Mathf.Cos(Angle)) +
            V * MyVector3.VectorDot(N, V) * (1.0f - Mathf.Cos(Angle)) +
            VectorCrossProduct (V,N) * Mathf.Sin(Angle);

        return NPrime;
    }
}


public class Matrix4by4
{
    public float[,] values;
    public Matrix4by4(Vector4 column1, Vector4 column2, Vector4 column3, Vector4 column4)
    {
        values = new float[4, 4];

        values[0, 0] = column1.x;
        values[1, 0] = column1.y;
        values[2, 0] = column1.z;
        values[3, 0] = column1.w;

        values[0, 1] = column2.x;
        values[1, 1] = column2.y;
        values[2, 1] = column2.z;
        values[3, 1] = column2.w;

        values[0, 2] = column3.x;
        values[1, 2] = column3.y;
        values[2, 2] = column3.z;
        values[3, 2] = column3.w;

        values[0, 3] = column4.x;
        values[1, 3] = column4.y;
        values[2, 3] = column4.z;
        values[3, 3] = column4.w;


    }
    public Matrix4by4(MyVector3 column1, MyVector3 column2, MyVector3 column3, MyVector3 column4)
    {
        values = new float[4, 4];

        values[0, 0] = column1.x;
        values[1, 0] = column1.y;
        values[2, 0] = column1.z;
        values[3, 0] = 0;

        values[0, 1] = column2.x;
        values[1, 1] = column2.y;
        values[2, 1] = column2.z;
        values[3, 1] = 0;

        values[0, 2] = column3.x;
        values[1, 2] = column3.y;
        values[2, 2] = column3.z;
        values[3, 2] = 0;

        values[0, 3] = column4.x;
        values[1, 3] = column4.y;
        values[2, 3] = column4.z;
        values[3, 3] = 1;
    }

    public static Vector4 operator *(Matrix4by4 lhs, Vector4 rhs)
    {
        Vector4 rv = new Vector4();

        rv.x = lhs.values[0, 0] * rhs.x + lhs.values[0, 1] * rhs.y + lhs.values[0, 2] * rhs.z + lhs.values[0, 3] * rhs.w;
        rv.y = lhs.values[1, 0] * rhs.x + lhs.values[1, 1] * rhs.y + lhs.values[1, 2] * rhs.z + lhs.values[1, 3] * rhs.w;
        rv.z = lhs.values[2, 0] * rhs.x + lhs.values[2, 1] * rhs.y + lhs.values[2, 2] * rhs.z + lhs.values[2, 3] * rhs.w;
        rv.w = lhs.values[3, 0] * rhs.x + lhs.values[3, 1] * rhs.y + lhs.values[3, 2] * rhs.z + lhs.values[3, 3] * rhs.w;
        
        return rv;
        }

    public static Matrix4by4 Identity
    {
        get
        {
            return new Matrix4by4
                (new Vector4(1, 0, 0, 0),
                 new Vector4(0, 1, 0, 0),
                 new Vector4(0, 0, 1, 0),
                 new Vector4(0, 0, 0, 1));
        }
    }

    public static Matrix4by4 operator *(Matrix4by4 lhs, Matrix4by4 rhs)
    {
        Matrix4by4 rv = Identity;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                float sum = 0;
                for (int k = 0; k < 4; k++)
                {
                    sum += lhs.values[i, k] * rhs.values[k, j];
                }
                rv.values[i, j] = sum;
            }
        }
        return rv;
    }

    public Matrix4by4 TranslationInverse()
    {
        Matrix4by4 rv = Identity;

        rv.values[0, 3] = -values[0, 3];
        rv.values[1, 3] = -values[1, 3];
        rv.values[2, 3] = -values[2, 3];
        return rv;
    }
    public Matrix4by4 ScaleInverse()
    {
        Matrix4by4 rv = Identity;

        rv.values[0, 0] = 1.0f / values[0, 0];
        rv.values[1, 1] = 1.0f / values[1, 1];
        rv.values[2, 2] = 1.0f / values[2, 2];
        return rv;
    }

    public static Matrix4by4 TranslateMatrix(float x, float y, float z)
    {
        Matrix4by4 rv;
        rv = new Matrix4by4(new MyVector3(1, 0, 0), new MyVector3(0, 1, 0), new MyVector3(0, 0, 1), new MyVector3(x, y, z));
        return rv;
    }

    public static Matrix4by4 ScaleMatrix(float x, float y, float z)
    {
        Matrix4by4 rv = Identity;
        rv = new Matrix4by4(new MyVector3(x, 0, 0), new MyVector3(0, y, 0), new MyVector3(0, 0, z), new MyVector3(0, 0, 0));
        return rv;
    }

    public static Matrix4by4 YawMatrix(float angle)
    {
        Matrix4by4 rv = new Matrix4by4(
            new MyVector3(Mathf.Cos(angle), 0, -Mathf.Sin(angle)),
            new MyVector3(0, 1, 0),
            new MyVector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)),
            MyVector3.zero()
            );
        return rv;
    }

    public static Matrix4by4 PitchMatrix(float angle)
    {
        Matrix4by4 rv = new Matrix4by4(
            new MyVector3(1, 0, 0),
            new MyVector3(0, Mathf.Cos(angle), Mathf.Sin(angle)),
            new MyVector3(0, -Mathf.Sin(angle), Mathf.Cos(angle)),
            MyVector3.zero()
            );
        return rv;
    }
    public static Matrix4by4 RollMatrix(float angle)
    {
        Matrix4by4 rv = new Matrix4by4(
            new MyVector3(Mathf.Cos(angle),Mathf.Sin(angle), 0),
            new MyVector3(-Mathf.Sin(angle), Mathf.Cos(angle), 0),
            new MyVector3(0, 0, 1),
            MyVector3.zero()
            );
        return rv;
    }

    public MyVector3 GetColumn(int column)
    {
        MyVector3 rv = new MyVector3(values[0, column], values[1, column], values[2, column]);
        return rv;
    }

}

public class Quat
{
    public float x, y, z, w;
    public MyVector3 v;

    public Quat()
    {
        w = 0.0f;
        v = new MyVector3(0, 0, 0);

    }
    public Quat(float angle, MyVector3 Axis)
    {
        float halfAngle = angle / 2;
        w = Mathf.Cos(halfAngle);
        x = Axis.x * Mathf.Sin(halfAngle);
        y = Axis.y * Mathf.Sin(halfAngle);
        z = Axis.z * Mathf.Sin(halfAngle);
    }

    public Quat(MyVector3 Position)
    {
        w = 0.0f;
        v = new MyVector3(Position.x, Position.y, Position.z);

    }

    public static Quat operator *(Quat lhs, Quat rhs)
    {
        Quat rv = new Quat();

        float x = lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y;
        float y = lhs.w * rhs.y - lhs.x * rhs.z + lhs.y * rhs.w + lhs.z * rhs.x;
        float z = lhs.w * rhs.z + lhs.x * rhs.y - lhs.y * rhs.x + lhs.z * rhs.w;
        float w = lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z;

        rv.x = x;
        rv.y = y;
        rv.z = z;
        rv.w = w;
        return rv;
    }
}