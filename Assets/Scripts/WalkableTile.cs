using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WalkableTile : Tile
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            ChangeMaterial();

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            print("Exited");
            if (!GetComponent<Rigidbody>())
            {
                gameObject.AddComponent<Rigidbody>().useGravity = true;

            }
            else
            {
                GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }
}
