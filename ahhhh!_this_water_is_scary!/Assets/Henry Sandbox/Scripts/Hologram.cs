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
    public Interactable Interactable;
    public Vector3 currentPosition;

    [SerializeField] private Transform player;
    [SerializeField] private Transform pilotSeat;
    [SerializeField] private SteamVR_ActionSet deactivateOnGrab;

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

            if (Interactable.attachedToHand)
         {
             currentPosition = this.transform.parent.position;
             positionDifference = hologramReferencePoint.transform.position - currentPosition;
             ResetPlayerPosition();
         }
         else
         {
             positionDifference = new Vector3(0, 0, 0);
         }
    }

    private void ResetPlayerPosition()
    {
        player.position = pilotSeat.position;
    }
}
           

