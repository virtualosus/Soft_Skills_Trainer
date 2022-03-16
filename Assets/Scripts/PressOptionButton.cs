using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using Oculus.Voice.Demo.UIShapesDemo;

public class PressOptionButton : MonoBehaviour
{
    public GameObject Button;

    public Offline_OptionController Offline_OptionController;

    public Offline_SpeechToOptionCompare Offline_SpeechToOptionCompare;


    public void Awake()
    {
        Offline_OptionController = GameObject.FindObjectOfType<Offline_OptionController>();
        Offline_SpeechToOptionCompare = GameObject.FindObjectOfType<Offline_SpeechToOptionCompare>();
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

        Offline_SpeechToOptionCompare.optionCounter++;
        yield return new WaitForSeconds(0.5f);
        if (Button.tag == "0")
        {
            Offline_OptionController.OptionOneSelect();
        }
        if (Button.tag == "1")
        {
            Offline_OptionController.OptionTwoSelect();
        }
        if (Button.tag == "2")
        {
            Offline_OptionController.OptionThreeSelect();
        }
        Debug.LogError("Option button pressed!!");

    }

}
