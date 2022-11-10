using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageAudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    AudioClip firstMessage;
    bool firstMessagePlayed;

    AudioClip secondMessage;

    AudioClip thirdMessage;
    bool thirdMessagePlayed;
    AudioClip fourthMessage;
    bool fourthMessagePlayed;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.GetComponent<AudioSource>();
        audioSource.playOnAwake = true;
        firstMessagePlayed = false;
        StartCoroutine(playStartingMessages());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PlayThirdMessage()
    {
        if (!thirdMessagePlayed)
        {
            audioSource.PlayOneShot(thirdMessage);
            thirdMessagePlayed = true;
        }

    }

    void PlayFourthMessage()
    {
        if(!fourthMessagePlayed){
        audioSource.PlayOneShot(fourthMessage);
        fourthMessagePlayed = true;
        }
    }

    IEnumerator playStartingMessages()
    {
        if (!firstMessagePlayed)
        {
            audioSource.PlayOneShot(firstMessage);
            yield return new WaitForSeconds(firstMessage.length);
            firstMessagePlayed = true;
            audioSource.PlayOneShot(secondMessage);
        }
    }
}
