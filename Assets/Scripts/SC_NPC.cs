using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_NPC : MonoBehaviour
{
    [SerializeField] private GameObject dialogueGameObject;


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKey(KeyCode.E))
        {
            dialogueGameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

        }
    }
}
