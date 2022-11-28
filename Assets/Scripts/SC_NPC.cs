using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SC_NPC : MonoBehaviour
{
    [SerializeField] private GameObject dialogueGameObject;
    [SerializeField] private GameObject currencyGameObject;
    [SerializeField] private GameObject buttonGameObject;
    [SerializeField] private GameObject staminaGameObject;
    [SerializeField] private GameObject cat;
    [SerializeField] private GameObject catLocation;
    private int dialogueCount = 0;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Text nameText;
    public bool missionAccepted;
    public bool catCaptured;
    private bool catSpawned;
    [SerializeField]
    private GameObject _Player;

    [SerializeField]
    private SC_Currency _currency;

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

        if (other.gameObject.tag == "Player" && Input.GetKey(KeyCode.E) && catCaptured)
        {
            dialogueCount = 2;
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
            text.text = "Oh player, my cat Fabricio has snuck into the old temple, would you be so kind to bring it to me?";
            buttonGameObject.gameObject.SetActive(true);
        }
        if (dialogueCount >= 2 && catCaptured)
        {
            nameText.text = "The Man In The Funny Hat";
            text.text = "Te como la cara, thank you very much! You can have this money";
            SpawnCat();
            _currency.AddMoney(50);
        }
    }

    private void SpawnCat()
    {
        if (!catSpawned)
        {
            catSpawned = true;
            Vector3 catLocation = new Vector3(transform.position.x + 3, transform.position.y - 1, transform.position.z + 3);
            Instantiate(cat, catLocation, Quaternion.identity);
        }        
    }

    public void Yes()
    {
        text.text = "Excelent! I will wait for your here. You can use my old bike to get there, is in front of me.";
        //activate mission and compass
        missionAccepted = true;
        _Player.GetComponent<SC_TPSController>().ObjectiveSet(catLocation.transform);
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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Destroy(buttonGameObject.gameObject);
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
