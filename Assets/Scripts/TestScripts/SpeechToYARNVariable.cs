using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Voice.Demo.UIShapesDemo;
using Yarn.Unity;
using UnityEngine.Events;
using UnityEngine.UI;


public class SpeechToYARNVariable : MonoBehaviour
{
    public DialogueRunner dialogueRunner;

    public InMemoryVariableStorage yarnInMemoryVariableStorage;

    public ColorChanger colorChanger;

    public string lastColor;

    public UnityEvent colourChange;

    public float duration;



    // Update is called once per frame
    void Update()
    {
        if(lastColor == null ||lastColor != colorChanger.currentColor)
        {
            colourChange.Invoke();
        }
    }

    [YarnCommand("custom_wait")]
    public IEnumerator CustomWait()
    {
        Debug.Log("Coroutine started...");
        var trigger = false;
        System.Action action = () => trigger = true;
        colourChange.AddListener(action.Invoke);
        yield return new WaitUntil(() => trigger);
        colourChange.RemoveListener(action.Invoke);
        ColourSelection();
        Debug.Log("Coroutine finished!!");

    }


    public void ColourSelection()
    {
        lastColor = colorChanger.currentColor;

        if (colorChanger.currentColor == "red"|| colorChanger.currentColor == "Red")
        {
            YARNSetRed();
        }
        if(colorChanger.currentColor == "blue" || colorChanger.currentColor == "Blue")
        {
            YARNSetBlue();
        }
        if(colorChanger.currentColor == "orange" || colorChanger.currentColor == "Orange")
        {
            YARNSetOrange();
        }
        if(colorChanger.currentColor == "green" || colorChanger.currentColor == "Green")
        {

            YARNSetGreen();
        }
    }

    public void YARNSetRed()
    {
        yarnInMemoryVariableStorage.SetValue("$red", true);
        Debug.Log("YARN to red");

    }

    public void YARNSetBlue()
    {
        yarnInMemoryVariableStorage.SetValue("$blue", true);
        Debug.Log("YARN to blue");

    }

    public void YARNSetOrange()
    {
        yarnInMemoryVariableStorage.SetValue("$orange", true);
        Debug.Log("YARN to orange");

    }
    public void YARNSetGreen()
    {
        yarnInMemoryVariableStorage.SetValue("$green", true);
        Debug.Log("YARN to green");
    }

}
