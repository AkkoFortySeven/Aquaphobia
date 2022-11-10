using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;
using Valve.VR;

[RequireComponent(typeof(Collider))]
public class Hologram : MonoBehaviour
{

    public GameObject hologram;
    public GameObject hologramReferencePoint;
    public Interactable interactable;
    public Vector3 currentPosition;
    public SubmarineMovementController subEngine;

    [SerializeField] private Transform player;
    [SerializeField] private Transform pilotSeat;
    [SerializeField] private SteamVR_ActionSet deactivateOnGrab;
    public SteamVR_Input_Sources hand;

    public SteamVR_Action_Single trig = SteamVR_Input.GetAction<SteamVR_Action_Single>("submarine", "Grab");
    public float triggerThreshold = 0.8f;

    [SerializeField]
    private bool isControllerDefaultPosition;


    public Vector3 positionDifference;

    // Start is called before the first frame update
    private void Awake()
    {
        positionDifference = new Vector3(0f, 0f, 0f);
        isControllerDefaultPosition = true;
    }

    private void Update()
    {
           /*
            currentPosition = hologram.transform.position;
            positionDifference = transform.TransformPoint(hologramReferencePoint.transform.position) - transform.TransformPoint(currentPosition);
          */

            if (interactable.attachedToHand)
         {
            CheckHand();
             currentPosition = this.transform.parent.position;
             positionDifference = hologramReferencePoint.transform.position - currentPosition;
             ResetPlayerPosition();
         }
         else
         {
             positionDifference = new Vector3(0, 0, 0);
         }
    }

    void CheckHand()
    {
        if (trig.GetAxis(SteamVR_Input_Sources.RightHand) > triggerThreshold)
        {
            subEngine.MoveUp();
        }
        else if (trig.GetAxis(SteamVR_Input_Sources.LeftHand) > triggerThreshold)
        {
            subEngine.MoveDown();
        }
    }

    // var connectedHand = interactable.attachedToHand.handType;

            

    //         if (connectedHand == SteamVR_Input_Sources.LeftHand)
    //         {
    //             subEngine.MoveDown();
                
    //         }
    //         else if (connectedHand == SteamVR_Input_Sources.RightHand)
    //         {
    //             subEngine.MoveUp();
                
    //         }

    private void ResetPlayerPosition()
    {
        player.position = pilotSeat.position;
    }
}
           

