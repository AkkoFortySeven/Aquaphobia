using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsButton : MonoBehaviour
{
    [SerializeField] private Transform buttonTop;
    [SerializeField] private Transform buttonLowerLimit;
    [SerializeField] private Transform buttonUpperLimit;
    [SerializeField] private float threshold;
    [SerializeField] private float force = 10f;
    [SerializeField] private bool isPressed;

    private float upperLowerDiff;
    private bool prevPressedState;

    [Header("Audio")]
    public AudioSource pressedSound;
    public AudioSource releasedSound;

    [Header("Events")]
    public UnityEvent onPressed;
    public UnityEvent onReleased;
    
    private void Start() 
    {
        Physics.IgnoreCollision(GetComponent<Collider>(),buttonTop.GetComponent<Collider>());

        if (transform.eulerAngles != Vector3.zero)
        {
            Vector3 savedAngle = transform.eulerAngles;
            transform.eulerAngles = Vector3.zero;
            upperLowerDiff = buttonUpperLimit.position.y - buttonLowerLimit.position.y;
            transform.eulerAngles = savedAngle;
        }
        else
        {
            upperLowerDiff = buttonUpperLimit.position.y - buttonLowerLimit.position.y;
        }
    }

    private void Update() 
    {
        buttonTop.transform.localPosition = new Vector3(0, buttonTop.transform.localPosition.y, 0);
        buttonTop.transform.localEulerAngles = new Vector3(0, 0, 0);

        if (buttonTop.localPosition.y >= 0) buttonTop.transform.position = new Vector3(buttonUpperLimit.position.x, buttonUpperLimit.position.y, buttonUpperLimit.position.z);
        else buttonTop.GetComponent<Rigidbody>().AddForce(buttonTop.transform.up * force * Time.fixedDeltaTime);

        if (buttonTop.localPosition.y <= buttonLowerLimit.localPosition.y) buttonTop.transform.position = buttonLowerLimit.position; //DIFFERENT FROM THE GUYS ONE IF IT BREAKS CHANGE THIS
        
        if (Vector3.Distance(buttonTop.position, buttonLowerLimit.position) < upperLowerDiff * threshold)
        {
            isPressed = true;
        }
        else
        {
            isPressed = false;
        }

        if (isPressed && prevPressedState != isPressed) Pressed();
        if (!isPressed && prevPressedState != isPressed) Released();
    }

    void Pressed()
    {
        prevPressedState = isPressed;
        pressedSound.pitch = 1;
        pressedSound.Play();
        onPressed.Invoke();
    }

    void Released()
    {
        prevPressedState = isPressed;
        releasedSound.pitch = Random.Range(0.8f, 0.9f);
        releasedSound.Play();
        onReleased.Invoke();
    }
}
