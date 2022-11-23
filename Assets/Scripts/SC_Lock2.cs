using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Lock2 : MonoBehaviour
{
    public bool open;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Key2")
        {
            open = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Key2")
        {
            open = false;
        }
    }
}
