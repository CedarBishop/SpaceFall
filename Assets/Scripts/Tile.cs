using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tile : MonoBehaviour
{
    public Material newMaterial;

    protected void ChangeMaterial ()
    {
        GetComponent<MeshRenderer>().material = newMaterial;
    }
}
