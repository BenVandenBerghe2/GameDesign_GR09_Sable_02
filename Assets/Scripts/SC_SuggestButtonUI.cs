using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_SuggestButtonUI : MonoBehaviour
{
    [SerializeField] private Text buttonText;
    [SerializeField] private string buttonToPress;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            buttonText.text = buttonToPress + " to interact";
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            buttonText.text = "";
        }
    }
}
