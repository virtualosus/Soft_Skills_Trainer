using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCube : MonoBehaviour
{
    public GameObject cube;
    public bool on;


    public void CubeOnOff()
    {
        if (on)
        {
            CubeOff();
        }
        else
        {
            CubeOn();
        }
    }

    public void CubeOff()
    {
        cube.SetActive(false);
        on = false;

    }

    public void CubeOn()
    {
        cube.SetActive(true);
        on = true;

    }
}
