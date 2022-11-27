using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Button : MonoBehaviour
{
    public GameObject Door;
    public bool doorIsOpening;

    void Update()
    {
        if (doorIsOpening == transform)
        {
            Door.transform.Translate(Vector3.up * Time.deltaTime * 5);
        }
        if (Door.transform.position.y > 7)
        {
            //doorIsOpening = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        doorIsOpening = true;
    }
}