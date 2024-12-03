using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectChange : MonoBehaviour
{
    public GameObject[] cubeArray;
    private int count;
    public GameObject cubeObj;

    void Start()
    {
        count = 0;
        cubeObj = GameObject.Instantiate(cubeArray[count]) as GameObject;
    }

    public void CubeSet()
    {
        Destroy(cubeObj);
        count++;
        cubeObj = GameObject.Instantiate(cubeArray[count]) as GameObject;
        if (count == 2)
            count = -1;
    }
}