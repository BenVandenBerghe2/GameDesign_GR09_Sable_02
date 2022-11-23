using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SC_NPC : MonoBehaviour
{
    [SerializeField] private GameObject dialogueGameObject;
    [SerializeField] private GameObject currencyGameObject;
    [SerializeField] private GameObject buttonGameObject;
    [SerializeField] private GameObject staminaGameObject;
    private int dialogueCount = 0;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Text nameText;
    public bool missionAccepted;
    public bool catRetrieved;

   
    private void Update()
    {  
        Dialogue();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKey(KeyCode.E))
        {
            dialogueGameObject.SetActive(true);
            currencyGameObject.SetActive(false);
            staminaGameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!missionAccepted)
        {
            ResetDialogue();
        }
        else if (missionAccepted)
        {
            AdvanceDialogue();
        }
    }


    private void Dialogue()
    {
        if (dialogueCount == 0 && Input.GetKey(KeyCode.E))
        {
            text.text = "Your neighbour seems a little altered";
            dialogueCount++;
        }
        if (dialogueCount == 1 && Input.GetKey(KeyCode.E))
        {
            nameText.text = "The Man In The Funny Hat";
            text.text = "Oh player, my cat Fabricio has snucked into the old temple, would you be so kind to bring it to me?";
            buttonGameObject.gameObject.SetActive(true);
        }
        if (dialogueCount > 2 && catRetrieved)
        {
            nameText.text = "The Man In The Funny Hat";
            text.text = "Te como la cara, thank you very much!";
        }
    }


    public void Yes()
    {
        text.text = "Excelent! I will wait for your here. You can use my old bike to get there is behind me.";
        //activate mission and compass
        missionAccepted = true;
        buttonGameObject.gameObject.SetActive(false);
    }
    public void No()
    {
        text.text = "Me cago en tus muertos pisoteados.";
        buttonGameObject.gameObject.SetActive(false);
    }

    private void AdvanceDialogue()
    {
        dialogueGameObject.SetActive(false);
        currencyGameObject.SetActive(true);
        staminaGameObject.SetActive(true);
        dialogueCount = 2;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void ResetDialogue()
    {
        dialogueGameObject.SetActive(false);
        currencyGameObject.SetActive(true);
        staminaGameObject.SetActive(true);
        dialogueCount = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
