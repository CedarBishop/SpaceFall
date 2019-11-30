using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnWalkableTile : Tile
{
    public void AddGravity()
    {
        if (!GetComponent<Rigidbody>())
        {
            gameObject.AddComponent<Rigidbody>();
            ChangeMaterial();
        }

    }
}
