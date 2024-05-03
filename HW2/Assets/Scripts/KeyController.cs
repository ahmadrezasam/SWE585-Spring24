using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    private bool haveTheKey = false;
    private GameObject keyObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            keyObject = other.gameObject;
            Debug.Log("Press 'E' to pick up the key");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == keyObject)
        {
            keyObject = null;
            Debug.Log("Key is out of range");
        }
    }

    private void Update()
    {
        if (keyObject != null && Input.GetKeyDown(KeyCode.E))
        {
            if (haveTheKey)
            {
                DropKey();
            }
            else
            {
                PickUpKey();
            }
        }
    }

    private void PickUpKey()
    {
        haveTheKey = true;
        keyObject.SetActive(false); // Or you can destroy the keyObject if you don't need it anymore
        Debug.Log("Key picked up!");
    }

    private void DropKey()
    {
        haveTheKey = false;
        keyObject.SetActive(true);
        keyObject.transform.position = transform.position + transform.forward; // Adjust the position as needed
        Debug.Log("Key dropped!");
    }
}