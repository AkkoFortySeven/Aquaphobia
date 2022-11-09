using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSoundManager : MonoBehaviour
{
    [SerializeField]
    private string nameID;

    private AudioSource audioSource;

    [SerializeField]
	private SubmarineMovementController subController;

	[SerializeField]
	private GameObject submarine;

    [SerializeField]
	private float minPitch;
	[SerializeField]
	private float maxPitch;

    private void Awake(){
        subController = submarine.GetComponent<SubmarineMovementController>();
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = 0f;
        audioSource.enabled = false;
        audioSource.playOnAwake = true;
    }

    private void FixedUpdate(){
        if(nameID == "floatEngineSound"){
            MovementNoise(subController.floatPercentageChange);
        }
        if(nameID == "thrustEngineSound"){
            MovementNoise(subController.thrustPercentageChange);
        }
        if(nameID == "rotateEngineSound"){
            MovementNoise(subController.rotatePercentageChange);
        }
    }

    private void MovementNoise(float percentageChange){
		float calculateMinPitch = minPitch / maxPitch;
        float calculateMaxPitch = maxPitch / maxPitch;
        
        if (percentageChange  < 0.01)
        {
            audioSource.enabled = false;
            audioSource.pitch = 0;
        }
        else if (percentageChange < calculateMinPitch)
        {
            audioSource.enabled = true;
            audioSource.pitch = minPitch;
        }
        else if (percentageChange > calculateMaxPitch)
        {
            audioSource.enabled = true;
            audioSource.pitch = maxPitch;
        }
        else
        {
            audioSource.enabled = true;
            audioSource.pitch = percentageChange * maxPitch;
        }
    }
}
