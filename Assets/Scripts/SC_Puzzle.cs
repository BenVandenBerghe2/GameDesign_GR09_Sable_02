using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Puzzle : MonoBehaviour
{
    [SerializeField] private SC_Lock1 lock1;
    [SerializeField] private SC_Lock2 lock2;
    [SerializeField] private SC_Lock3 lock3;
    [SerializeField] private GameObject Door;
    public bool doorOpen;

    // Update is called once per frame
    void Update()
    {
        if (lock1.open && lock2.open && lock3.open)
        {
            doorOpen = true;
            Door.transform.Translate(Vector3.down * Time.deltaTime * 5);
            Debug.Log("puzzle complete, door opened");
        }
    }
}