using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Pickup : MonoBehaviour
{
    [SerializeField] private LayerMask pickupMask;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform pickupTarget;
    [Space]
    [SerializeField] private float pickupRange;
    private Rigidbody currentObject;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentObject)
            {
                currentObject.useGravity = true;
                currentObject = null;
                return;
            }
            Ray cameraRay = playerCamera.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
            if (Physics.Raycast(cameraRay, out RaycastHit HitInfo, pickupRange, pickupMask))
            {
                currentObject = HitInfo.rigidbody;
                currentObject.useGravity = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (currentObject)
        {
            Vector3 DirectionToPoint = pickupTarget.position - currentObject.position;
            float DistanceToPoint = DirectionToPoint.magnitude;

            currentObject.velocity = DirectionToPoint * 12f * DistanceToPoint;
        }
    }
}