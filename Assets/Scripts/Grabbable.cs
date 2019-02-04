using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
public class Grabbable : MonoBehaviour
{
    [HideInInspector]
    public Hand _activeController = null;
}
