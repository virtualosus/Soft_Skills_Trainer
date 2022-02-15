using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VOLineController : MonoBehaviour
{

    public AudioSource NPCAudio;
    public AudioClip[] VoiceLines;

    public int VoiceLineToPlay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLineAndPlay()
    {
        NPCAudio.clip = VoiceLines[VoiceLineToPlay];
        NPCAudio.Play();
    }
}
