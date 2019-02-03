using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hand : MonoBehaviour
{
    [SteamVR_DefaultAction("Squeeze")]
    public SteamVR_Action_Boolean GrabGripAction = null;

    private SteamVR_Behaviour_Pose _grabGripPose = null;
    private FixedJoint _joint = null;

    private Grabbable _currentGrabbable = null;
    public List<Grabbable> _contactGrabbables = new List<Grabbable>();

    private void Awake()
    {
        _grabGripPose = GetComponent<SteamVR_Behaviour_Pose>();
        _joint = GetComponent<FixedJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        // Down
        if (GrabGripAction.GetStateDown(_grabGripPose.inputSource))
        {
            print(_grabGripPose.inputSource + " Trigger down");
            Pickup();
        }

        // Up
        if (GrabGripAction.GetStateUp(_grabGripPose.inputSource))
        {
            print(_grabGripPose.inputSource + " Trigger up");
            Drop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other + " collided with controller");
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

    }

    public void Drop()
    {

    }

    private Grabbable GetNearestGrabbable()
    {
        return null;
    }
}
