using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Compass : MonoBehaviour
{
    private Transform _target;
    [SerializeField]
    private GameObject _character;

    private void Start()
    {
    }
    private void Update()
    {
        _target = _character.GetComponent<SC_TPSController>().GetObjective();
        Vector3 TargetPos = new Vector3(_target.position.x, this.transform.position.y, _target.position.z);
        transform.LookAt(TargetPos, Vector3.up);
    }
}
