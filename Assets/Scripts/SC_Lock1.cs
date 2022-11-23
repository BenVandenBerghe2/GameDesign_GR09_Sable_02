using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Lock1 : MonoBehaviour
{
    public bool open;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Key1")
        {
            open = true;
        }      
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Key1")
        {
            open = false;
        }
    }
}

