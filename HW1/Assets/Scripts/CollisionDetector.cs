using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        // Check if the collided object has a specific name
        if (collision.gameObject.name == "Platform")
        {
            Debug.Log("Movable object collided with a GameObject named 'Platform'");
            // Your code here
        }
        else if (collision.gameObject.name == "Wall1")
        {
            Debug.Log("Movable object collided with a GameObject named 'Wall1'");
            // Your code here
        }
        else if (collision.gameObject.name == "Wall2")
        {
            Debug.Log("Movable object collided with a GameObject named 'Wall2'");
            // Your code here
        }
        else if (collision.gameObject.name == "Wall3")
        {
            Debug.Log("Movable object collided with a GameObject named 'Wall3'");
            // Your code here
        }
        else if (collision.gameObject.name == "Wall4")
        {
            Debug.Log("Movable object collided with a GameObject named 'Wall4'");
            // Your code here
        }
    }
}
