using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VOLineController : MonoBehaviour
{

    public AudioSource NPCAudio;
    public AudioClip[] VoiceLines;

    public float VoiceLineToPlay;
   

    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    public void UpdateLineAndPlay()
    {
        NPCAudio.clip = VoiceLines[(int)VoiceLineToPlay];
        NPCAudio.Play();
        Debug.LogError("Playing voiceline " + (int)VoiceLineToPlay);
    }
}
