using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VOLineController : MonoBehaviour
{

    public AudioSource NPCAudio;
    public AudioClip[] voiceLines;

    public float voiceLineToPlay, voiceLineClipLength;


    public void UpdateLineAndPlay()                                 //selecting Audio clip to play with data passed in from SpeechToYARN and playing
    {
        NPCAudio.clip = voiceLines[(int)voiceLineToPlay];
        voiceLineClipLength = NPCAudio.clip.length;
        NPCAudio.Play();
        //Debug.LogError("Playing voiceline " + (int)voiceLineToPlay);
    }
}
