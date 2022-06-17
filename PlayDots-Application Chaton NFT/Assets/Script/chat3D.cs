using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class chat3D : MonoBehaviour
{
    public List<Color> backColors = new List<Color>();
    public List<Material> catMat = new List<Material>();
    public Camera cam;
    public bool use;
    
    /*color exa
     * 1 = D866DE
     * 2 = 6374EB
     * 3 = 61DF8E
     * 4 = CEE369
     * 5 = D766AE
     * 6 = BD4251
     * 7 = 41C3C3
     * 8 = 9C461A
     */

    private void Start()
    {
        use = false;
    }

    public void SetUp3DCat(int id)
    { 
    cam.backgroundColor = backColors[id];
    gameObject.GetComponent<MeshRenderer>().material = catMat[id];
    use = true;
    }

    public void stopRotate()
    {
        use = false;
    }

    private void Update()
    {
        if (use)
        {
            gameObject.transform.Rotate(0,2,0);
        }
    }
}
