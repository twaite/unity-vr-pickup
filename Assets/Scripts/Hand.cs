using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hand : MonoBehaviour
{
    [SteamVR_DefaultAction("Squeeze")]
    public SteamVR_Action_Boolean GrabPinchAction = null;

    private SteamVR_Behaviour_Pose _grabPinchPose = null;
    private FixedJoint _joint = null;

    private Grabbable _currentGrabbable = null;
    public List<Grabbable> _contactGrabbables = new List<Grabbable>();

    private void Awake()
    {
        _grabPinchPose = GetComponent<SteamVR_Behaviour_Pose>();
        _joint = GetComponent<FixedJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        // Down
        if (GrabPinchAction.GetStateDown(_grabPinchPose.inputSource))
        {
            Pickup();
        }

        // Up
        if (GrabPinchAction.GetStateUp(_grabPinchPose.inputSource))
        {
            Drop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Grabbable"))
        {
            _contactGrabbables.Add(other.gameObject.GetComponent<Grabbable>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Grabbable"))
        {
            _contactGrabbables.Remove(other.gameObject.GetComponent<Grabbable>());
        }
    }

    public void Pickup()
    {
        // Get nearest
        _currentGrabbable = GetNearestGrabbable();

        // Null check
        if (!_currentGrabbable)
            return;

        // Already held check
        if (_currentGrabbable._activeController)
            _currentGrabbable._activeController.Drop();

        //  Position
        _currentGrabbable.transform.position = transform.position;

        // Attach
        Rigidbody targetBody = _currentGrabbable.GetComponent<Rigidbody>();
        _joint.connectedBody = targetBody;

        // Set active hand
        _currentGrabbable._activeController = this;
    }

    public void Drop()
    {
        // Null check
        if(!_currentGrabbable)
            return;

        // Apply velocity
        Rigidbody targetBody = _currentGrabbable.GetComponent<Rigidbody>();
        targetBody.velocity = _grabPinchPose.GetVelocity();
        targetBody.angularVelocity = _grabPinchPose.GetAngularVelocity();

        // Detatch
        _joint.connectedBody = null;

        // Clear
        _currentGrabbable._activeController = null;
        _currentGrabbable = null;
    }

    private Grabbable GetNearestGrabbable()
    {
        Grabbable nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0.0f;

        foreach(Grabbable grabbable in _contactGrabbables)
        {
            distance = (grabbable.transform.position - transform.position).sqrMagnitude;

            if(distance < minDistance)
            {
                minDistance = distance;
                nearest = grabbable;
            }
        }

        return nearest;
    }
}
