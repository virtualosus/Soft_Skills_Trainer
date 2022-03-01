using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.Events;


public class OptionController : MonoBehaviour
{
    public LineView LineView;
    public ClassroomSpeechToYarn ClassroomSpeechToYarn;

    public GameObject[] Options;

    public int OptionToSelect;

    public bool optionSelected;

    public UnityEvent OptionSelected;

    public void GatherAvailableOptions()
    {
        Options = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            Options[i] = transform.GetChild(i).gameObject;
        }
        Debug.Log("Options listed");
    }

    public void OptionOneSelect()
    {
        Debug.LogError("Option 1 selected");

        OptionToSelect = 0;
        StartCoroutine(OptionSelectCoroutine());
        Debug.LogError("Option 1 selected");
    }

    public void OptionTwoSelect()
    {
        OptionToSelect = 1;
        StartCoroutine(OptionSelectCoroutine());
        Debug.LogError("Option 2 selected");
    }

    public void OptionThreeSelect()
    {
        OptionToSelect = 2;
        StartCoroutine(OptionSelectCoroutine());
        Debug.LogError("Option 3 selected");
    }

    public IEnumerator OptionSelectCoroutine()
    {
        optionSelected = true;
        Options[OptionToSelect].GetComponent<OptionView>().InvokeOptionSelected();
       // OptionSelected.Invoke();
        yield return new WaitForSeconds(1f);
        LineView.OnContinueClicked();
    }

    //public void CheckIfOptionSelected()
    //{
    //    if (optionSelected)
    //    {
    //        optionSelected = false;
    //    }
    //    else
    //    {
    //        ClassroomSpeechToYarn.ActivateVoiceRecognition();
    //    }
    //}

}