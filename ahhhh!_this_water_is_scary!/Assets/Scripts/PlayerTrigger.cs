using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] private string colliderTag;
    public UnityEvent OnPlayerEnter;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == colliderTag)
        {
            OnPlayerEnter.Invoke();
        }  
    }
}
