using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using Oculus.Voice.Demo.UIShapesDemo;

public class PressOptionButton : MonoBehaviour
{
    public GameObject Button;

    public OptionController OptionController;

    public InteractionHandler InteractionHandler;

    public SpeechToOptionCompare SpeechToOptionCompare;


    public void Awake()
    {
        OptionController = GameObject.FindObjectOfType<OptionController>();
        InteractionHandler = GameObject.FindObjectOfType<InteractionHandler>();
        SpeechToOptionCompare = GameObject.FindObjectOfType<SpeechToOptionCompare>();
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
        StartCoroutine(InteractionHandler.ButtonPressed());
        InteractionHandler.CancelVoiceAttempt();
        SpeechToOptionCompare.optionCounter++;
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
