using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    Vector3[] modelSpaceVertices;
    Vector3[] movedVertices;
    public float orbitSpeed; 
    public float scale;
    public float orbitRadius;
    public float axilRotateSpeed;
    float yawAngle;
    Matrix4by4 YawMatrix;
    Matrix4by4 OrbitSpeedMatrix;
    Matrix4by4 PitchMatrix;
    Matrix4by4 RollMatrix;
    Matrix4by4 RotMatrix;
    Matrix4by4 translateMatrix;
    Matrix4by4 transformationMatrix;
    Matrix4by4 scaleMatrix;
    MeshFilter MF;
    public Matrix4by4 LocationMatrix;
    void Start()
    {
        MF = GetComponent<MeshFilter>();
        modelSpaceVertices = MF.mesh.vertices;
        movedVertices =  new Vector3[modelSpaceVertices.Length];

    }

    //void Update()
    //{
    //    yawAngle += Time.deltaTime;


    //    scaleMatrix = Matrix4by4.ScaleMatrix(scale, scale, scale);
    //    PitchMatrix = Matrix4by4.PitchMatrix(0);
    //    YawMatrix = Matrix4by4.YawMatrix(yawAngle * axilRotateSpeed);
    //    RollMatrix = Matrix4by4.RollMatrix(0);
    //    Matrix4by4 mmm = YawMatrix * (PitchMatrix * RollMatrix);
    //    translateMatrix = Matrix4by4.TranslateMatrix(orbitRadius * orbitSpeed * Time.deltaTime + 1f, 0, 0);
    //    Debug.Log(translateMatrix.values[0, 3]);
    //    Debug.Log(translateMatrix.values[1, 3]);
    //    Debug.Log(translateMatrix.values[2, 3]);
    //    OrbitYawMatrix = Matrix4by4.YawMatrix(yawAngle * orbitSpeed);


    //    //transformationMatrix =  (scaleMatrix * (YawMatrix * translateMatrix)) * OrbitYawMatrix;
    //    //transformationMatrix =  (scaleMatrix * (YawMatrix * translateMatrix));
    //    transformationMatrix =  (scaleMatrix * (mmm * translateMatrix));
    //    //Debug.Log(transformationMatrix.values[0, 3]);
    //    //Debug.Log(transformationMatrix.values[1, 3]);
    //    //Debug.Log(transformationMatrix.values[2, 3]);

    //    for (int i = 0; i < modelSpaceVertices.Length; i++)
    //    {
    //        movedVertices[i] = transformationMatrix * modelSpaceVertices[i];
    //    }
    //    MF.mesh.vertices = movedVertices;
    //    Planet.transform.position = new Vector3(transformationMatrix.values[0, 3], transformationMatrix.values[1, 3], transformationMatrix.values[2, 3]);

    //}

    void Update()
    {
        yawAngle += Time.deltaTime;

        scaleMatrix = Matrix4by4.ScaleMatrix(scale, scale, scale);

        YawMatrix = Matrix4by4.YawMatrix(yawAngle * axilRotateSpeed);
        PitchMatrix = Matrix4by4.PitchMatrix(0);
        RollMatrix = Matrix4by4.RollMatrix(0);
        RotMatrix = YawMatrix * (PitchMatrix * RollMatrix);

        translateMatrix = Matrix4by4.TranslateMatrix(orbitRadius + 10f, 0, 0);

        OrbitSpeedMatrix = Matrix4by4.YawMatrix(yawAngle * orbitSpeed);

        transformationMatrix = OrbitSpeedMatrix * translateMatrix * RotMatrix * scaleMatrix;
        LocationMatrix = OrbitSpeedMatrix * translateMatrix;

        //Debug.Log(transformationMatrix.values[0, 3]);
        //Debug.Log(transformationMatrix.values[1, 3]);
        //Debug.Log(transformationMatrix.values[2, 3]);
        for (int i = 0; i < modelSpaceVertices.Length; i++)
        {
            Vector4 a = new Vector4(modelSpaceVertices[i].x,modelSpaceVertices[i].y,modelSpaceVertices[i].z, 1);
            movedVertices[i] = transformationMatrix * a;
        }
        MF.mesh.vertices = movedVertices;
        MF.mesh.RecalculateNormals();
        MF.mesh.RecalculateBounds();
       

    }
}
