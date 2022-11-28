using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ControlsImage : MonoBehaviour
{
    private bool controlUIBool;
    [SerializeField] private GameObject controlUIGameObject;  

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            controlUIBool = !controlUIBool;
            controlUIGameObject.SetActive(controlUIBool);
        }
    }
}
