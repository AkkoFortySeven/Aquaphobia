using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SubmarineMovementController : MonoBehaviour
{
    private Interactable interactable;

    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Hologram hologram;

    [SerializeField]
    private GameObject hologramInteractable;

    [SerializeField]
    Vector3 positionDifference;

    [SerializeField] private Transform tracker;



    [SerializeField]
    private float zMinThreshold;
    [SerializeField]
    private float yMinThreshold;
    [SerializeField]
    private float xMinThreshold;

    [SerializeField]
    private float zMaxThreshold;
    [SerializeField]
    private float yMaxThreshold;
    [SerializeField]
    private float xMaxThreshold;

    [Header("Movement Settings")]

    private float minThrustSpeed;
    private float minRotateSpeed;
    private float minFloatSpeed;

    [SerializeField]
    private float maxThrustSpeed;

    [SerializeField]
    public float maxRotateSpeed;
    [SerializeField]
    public float maxFloatSpeed;

    private Vector3 EulerAngleLeftVelocity;
    private Vector3 EulerAngleRightVelocity;

    [SerializeField]
    private float currentThrustSpeed;
    [SerializeField]
    private float currentRotateSpeed;
    [SerializeField]
    private float currentFloatSpeed;

    public float thrustPercentageChange;
    public float rotatePercentageChange;
    public float floatPercentageChange;
    void Awake()
    {
        //rb = GetComponent<Rigidbody>();
        hologram = hologramInteractable.GetComponent<Hologram>();
        minFloatSpeed = 0f;
        minThrustSpeed = 0f;
        minRotateSpeed = 0f;
        interactable = GetComponent<Interactable>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        
            positionDifference = hologram.positionDifference;
            calculateLerpScale();
            MovementDirection();
        
        
    }

    // private void MovementDirection()
    // {
    //     //var savePos = rb.transform.rotation;
    //     //rb.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

    //    if (hologram.positionDifference.y < -yMinThreshold)
    //     {  //down
    //         currentFloatSpeed = Mathf.Lerp(minFloatSpeed, maxFloatSpeed, floatPercentageChange);
    //         rb.AddForce(rb.transform.up * currentFloatSpeed);
    //     }

        

    //     if (hologram.positionDifference.y > yMinThreshold)
    //     {  //up
    //         currentFloatSpeed = Mathf.Lerp(minFloatSpeed, maxFloatSpeed, floatPercentageChange);
    //         rb.AddForce(-rb.transform.up * currentFloatSpeed);
    //     }

        

    //     if (hologram.positionDifference.z < -zMinThreshold)
    //     {  //backward
    //         currentThrustSpeed = Mathf.Lerp(minThrustSpeed, maxThrustSpeed, thrustPercentageChange);
            
    //         rb.AddForce(rb.transform.forward * currentThrustSpeed);
    //     }

        
    //     if (hologram.positionDifference.z > zMinThreshold)
    //     {  //forward
    //         currentThrustSpeed = Mathf.Lerp(minThrustSpeed, maxThrustSpeed, thrustPercentageChange);
            
    //         rb.AddForce(-rb.transform.forward * currentThrustSpeed);
    //     }

        
    //     Vector3 rot = new Vector3(0f, 0.5f, 0f);
    //     if (hologram.positionDifference.x < -xMinThreshold)
    //     {  //left
    //         // currentRotateSpeed = Mathf.Lerp(minRotateSpeed, maxRotateSpeed, rotatePercentageChange);
    //         // EulerAngleLeftVelocity = new Vector3(0, currentRotateSpeed, 0);
    //         // Quaternion deltaLeftRotation = Quaternion.Euler(
    //         //         EulerAngleLeftVelocity * Time.fixedDeltaTime
    //         //     );

            
            
    //         //rb.transform.rotation = Quaternion.Euler(new Vector3(rb.transform.rotation.x, rb.transform.rotation.y + 0.5f, rb.transform.rotation.z));

    //         rb.transform.Rotate(0f, -0.3f, 0f, Space.Self);

            
    //         //rb.MoveRotation(rb.rotation * Quaternion.Euler(rot));
    //     }

    //     if (hologram.positionDifference.x > xMinThreshold)
    //     {  //right
    //         // currentRotateSpeed = Mathf.Lerp(minRotateSpeed, maxRotateSpeed, rotatePercentageChange);
    //         // EulerAngleRightVelocity = new Vector3(0, -currentRotateSpeed, 0);
    //         // Quaternion deltaRightRotation = Quaternion.Euler(
    //         //         EulerAngleRightVelocity * Time.fixedDeltaTime
    //         //     );

    //         //rb.transform.rotation = savePos;
    //         Vector3 pososso = new Vector3(0f, 1f, 0f);
    //         rb.transform.Rotate(pososso, 1f, Space.Self);
            
    //         //rb.transform.rotation = Quaternion.Euler(new Vector3(rb.transform.rotation.x, rb.transform.rotation.y - 0.1f, rb.transform.rotation.z));
            
    //         //rb.MoveRotation(rb.rotation * Quaternion.Euler(-rot));
    //     }

        
    // }

    private void MovementDirection()
    {
        //var savePos = rb.transform.rotation;
        //rb.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

       if (tracker.localPosition.y < -yMinThreshold)
        {  //down
            currentFloatSpeed = Mathf.Lerp(minFloatSpeed, maxFloatSpeed, floatPercentageChange);
            rb.AddForce(-rb.transform.up * currentFloatSpeed);
        }

        

        if (tracker.localPosition.y > yMinThreshold)
        {  //up
            currentFloatSpeed = Mathf.Lerp(minFloatSpeed, maxFloatSpeed, floatPercentageChange);
            rb.AddForce(rb.transform.up * currentFloatSpeed);
        }

        

        if (tracker.localPosition.z < -zMinThreshold)
        {  //backward
            currentThrustSpeed = Mathf.Lerp(minThrustSpeed, maxThrustSpeed, thrustPercentageChange);
            
            rb.AddForce(-rb.transform.forward * currentThrustSpeed);
        }

        
        if (tracker.localPosition.z > zMinThreshold)
        {  //forward
            currentThrustSpeed = Mathf.Lerp(minThrustSpeed, maxThrustSpeed, thrustPercentageChange);
            
            rb.AddForce(rb.transform.forward * currentThrustSpeed);
        }

        float dist = Vector3.Distance(tracker.position, hologram.hologramReferencePoint.transform.position);
        if (tracker.localPosition.x < -xMinThreshold)
        {  //left
            // currentRotateSpeed = Mathf.Lerp(minRotateSpeed, maxRotateSpeed, rotatePercentageChange);
            // EulerAngleLeftVelocity = new Vector3(0, currentRotateSpeed, 0);
            // Quaternion deltaLeftRotation = Quaternion.Euler(
            //         EulerAngleLeftVelocity * Time.fixedDeltaTime
            //     );

            
            
            //rb.transform.rotation = Quaternion.Euler(new Vector3(rb.transform.rotation.x, rb.transform.rotation.y + 0.5f, rb.transform.rotation.z));

            //rb.transform.Rotate(0f, -rotatePercentageChange, 0f, Space.Self);
			rb.transform.Rotate(0f, tracker.localPosition.x, 0f, Space.Self);

            
            //rb.MoveRotation(rb.rotation * Quaternion.Euler(rot));
        }

		
        if (tracker.localPosition.x > xMinThreshold)
        {  //right
            // currentRotateSpeed = Mathf.Lerp(minRotateSpeed, maxRotateSpeed, rotatePercentageChange);
            // EulerAngleRightVelocity = new Vector3(0, -currentRotateSpeed, 0);
            // Quaternion deltaRightRotation = Quaternion.Euler(
            //         EulerAngleRightVelocity * Time.fixedDeltaTime
            //     );

            
           
            //rb.transform.Rotate(0f, rotatePercentageChange, 0f, Space.Self);
			rb.transform.Rotate(0f, tracker.localPosition.x, 0f, Space.Self);
            
            //rb.transform.rotation = Quaternion.Euler(new Vector3(rb.transform.rotation.x, rb.transform.rotation.y - 0.1f, rb.transform.rotation.z));
            
            //rb.MoveRotation(rb.rotation * Quaternion.Euler(-rot));
        }

        
    }

    // public void MoveUp()
    // {
    //     rb.AddForce(Vector3.up * maxFloatSpeed);
    //     Debug.Log("RIGHT HAND ACTIVE");
    // }

    // public void MoveDown()
    // {
    //     rb.AddForce(-Vector3.up * maxFloatSpeed);
    //     Debug.Log("LEFT HAND ACTIVE");
    // }

    private void calculateLerpScale()
    {
        if (positionDifference.z > zMinThreshold)
        {
            thrustPercentageChange = (positionDifference.z - zMinThreshold) / zMaxThreshold;
            if (thrustPercentageChange > 1)
            {
                thrustPercentageChange = 1;
            }
        }

        if (positionDifference.z < -zMinThreshold)
        {
            thrustPercentageChange = (-positionDifference.z - zMinThreshold) / zMaxThreshold;
            if (thrustPercentageChange > 1)
            {
                thrustPercentageChange = 1;
            }
        }
        if (positionDifference.x > xMinThreshold)
        {
            rotatePercentageChange = (positionDifference.x - xMinThreshold) / xMaxThreshold;
            if (rotatePercentageChange > 1)
            {
                rotatePercentageChange = 1;
            }
        }
        if (positionDifference.x < -xMinThreshold)
        {
            rotatePercentageChange = (-positionDifference.x - xMinThreshold) / xMaxThreshold;
            if (rotatePercentageChange > 1)
            {
                rotatePercentageChange = 1;
            }
        }
        if (positionDifference.y > yMinThreshold)
        {
            floatPercentageChange = (positionDifference.y - yMinThreshold) / yMaxThreshold;
            if (floatPercentageChange > 1)
            {
                floatPercentageChange = 1;
            }
        }
        if (positionDifference.y < -yMinThreshold)
        {
            floatPercentageChange = (-positionDifference.y - yMinThreshold) / yMaxThreshold;
            if (floatPercentageChange > 1)
            {
                floatPercentageChange = 1;
            }
        }
    }
}
