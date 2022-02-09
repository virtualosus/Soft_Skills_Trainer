using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Voice.Demo.UIShapesDemo;
using Yarn.Unity;
using UnityEngine.Events;


public class SpeechToYARNVariable : MonoBehaviour
{
    public DialogueRunner dialogueRunner;

    public InMemoryVariableStorage yarnInMemoryVariableStorage;

    public ColorChanger colorChanger;

    public string lastColor;

    public UnityEvent colourChange;

    public void Awake()
    {
        dialogueRunner.AddCommandHandler("custom_wait", (UnityEvent waitForEvent) => { return StartCoroutine(CustomWait(waitForEvent)); });

    }

    // Update is called once per frame
    void Update()
    {
        if(lastColor != colorChanger.currentColor)
        {

            ColourSelection();
        }
    }

    private IEnumerator CustomWait(UnityEvent unityEvent)
    {        
        yield return new WaitForSeconds(1);
    }

    public void ColourSelection()
    {
        lastColor = colorChanger.currentColor;

        if (colorChanger.currentColor == "red")
        {

            YARNSetRed();
        }
        else if(colorChanger.currentColor == "blue")
        {
            YARNSetBlue();
        }
        else if(colorChanger.currentColor == "orange")
        {
            YARNSetOrange();
        }
        else if(colorChanger.currentColor == "green")
        {
            YARNSetGreen();
        }
    }

    public void YARNSetRed()
    {
        yarnInMemoryVariableStorage.SetValue("$red", true);
    }

    public void YARNSetBlue()
    {
        yarnInMemoryVariableStorage.SetValue("$blue", true);
    }

    public void YARNSetOrange()
    {
        yarnInMemoryVariableStorage.SetValue("$orange", true);
    }
    public void YARNSetGreen()
    {
        yarnInMemoryVariableStorage.SetValue("$green", true);
    }

}
