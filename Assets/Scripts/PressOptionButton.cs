using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using Oculus.Voice.Demo.UIShapesDemo;

public class PressOptionButton : MonoBehaviour
{


    //public VoskResultText VoskResultText;

    public InteractionHandler InteractionHandler;

    public GameObject Button;

    public OptionController OptionController;

    public SpeechToOptionCompare SpeechToOptionCompare;


    public void Awake()
    {
        OptionController = GameObject.FindObjectOfType<OptionController>();
        SpeechToOptionCompare = GameObject.FindObjectOfType<SpeechToOptionCompare>();
        InteractionHandler = GameObject.FindObjectOfType<InteractionHandler>();
        //VoskResultText = GameObject.FindObjectOfType<VoskResultText>();


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

        //VoskResultText.CancelListenForSpeech();
        //Offline_SpeechToOptionCompare.optionCounter++;
        yield return new WaitForSeconds(0.5f);
        SpeechToOptionCompare.optionCounter++;
        if (Button.tag == "0")
        {
            OptionController.OptionOneSelect();
            InteractionHandler.buttonPressed = true;
            InteractionHandler.CancelVoiceAttempt();
        }
        if (Button.tag == "1")
        {
            OptionController.OptionTwoSelect();
            InteractionHandler.buttonPressed = true;
            InteractionHandler.CancelVoiceAttempt();

        }
        if (Button.tag == "2")
        {
            OptionController.OptionThreeSelect();
            InteractionHandler.buttonPressed = true;
            InteractionHandler.CancelVoiceAttempt();

        }
        Debug.LogError("Option button pressed!!");

    }
}
