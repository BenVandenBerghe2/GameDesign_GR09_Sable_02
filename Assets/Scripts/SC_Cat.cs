using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_Cat : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    [SerializeField] private Text buttonText;
    [SerializeField] private SC_NPC npc;

    private void OnTriggerEnter(Collider other)
    {
        source.PlayOneShot(clip);        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKey(KeyCode.E) && !npc.catCaptured)
        {
            npc.catCaptured = true;
            Destroy(this.gameObject);
            buttonText.text = "";

        }
    }
}
