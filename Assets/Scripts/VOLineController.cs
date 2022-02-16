using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VOLineController : MonoBehaviour
{

    public AudioSource NPCAudio;
    public AudioClip[] voiceLines;

    public float voiceLineToPlay, voiceLineClipLength;
   

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
        NPCAudio.clip = voiceLines[(int)voiceLineToPlay];
        voiceLineClipLength = NPCAudio.clip.length;
        NPCAudio.Play();
        Debug.LogError("Playing voiceline " + (int)voiceLineToPlay);
    }
}
