using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BikeMounting : MonoBehaviour
{
    [SerializeField]
    private GameObject _Parent;

    private bool _OnBike = false;
    [SerializeField]
    private GameObject _Player;

    private void OnTriggerStay(Collider other)
    {
        
        if (other.tag == "Player")
        {           
            if (Input.GetKeyDown(KeyCode.E)||Input.GetKeyUp(KeyCode.E))
            {
                _Parent.GetComponent<SC_HoverMotor>().enabled = true;
                _Player.SetActive(false);
                _Parent.transform.GetChild(1).gameObject.SetActive(true);
                _Parent.GetComponent<CharacterController>().enabled = true;
                _OnBike = true;
            }
        }
    }

    private void Update()
    {
        if (_OnBike)
        {
            if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.LeftShift))
            {
                _Player.gameObject.SetActive(true);
                _Parent.GetComponent<SC_HoverMotor>().enabled = false;
                _Parent.GetComponent<CharacterController>().enabled = false;
                _OnBike = false;
                _Player.transform.position = _Parent.transform.position;
                _Parent.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }
}
