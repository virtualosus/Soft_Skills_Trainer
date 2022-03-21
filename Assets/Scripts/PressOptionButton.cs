using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using Oculus.Voice.Demo.UIShapesDemo;

public class PressOptionButton : MonoBehaviour
{
    public VoskSpeechToText VoskSpeechToText;

    public GameObject Button;

    public OptionController OptionController;

    public Offline_SpeechToOptionCompare Offline_SpeechToOptionCompare;


    public void Awake()
    {
        OptionController = GameObject.FindObjectOfType<OptionController>();
        Offline_SpeechToOptionCompare = GameObject.FindObjectOfType<Offline_SpeechToOptionCompare>();
        VoskSpeechToText = GameObject.FindObjectOfType<VoskSpeechToText>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Hand")
        {
            StartCoroutine(ButtonPressWait());
        }
    }

    public IEnumerator ButtonPressWait()
    {
        VoskSpeechToText.ToggleRecording();
        Offline_SpeechToOptionCompare.optionCounter++;
        yield return new WaitForSeconds(0.5f);
        if (Button.tag == "0")
        {
            OptionController.OptionOneSelect();
        }
        if (Button.tag == "1")
        {
            OptionController.OptionTwoSelect();
        }
        if (Button.tag == "2")
        {
            OptionController.OptionThreeSelect();
        }
        Debug.LogError("Option button pressed!!");

    }

}
