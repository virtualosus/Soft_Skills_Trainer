using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.Events;


public class OptionController : MonoBehaviour
{
    public LineView LineView;
    public YarnCommandController ClassroomSpeechToYarn;

    public GameObject[] Options;

    public int OptionToSelect, tagNumber;

    public bool optionSelected;

    public UnityEvent OptionSelected;

    public IEnumerator GatherOptions()
    {
        tagNumber = 0;
        yield return new WaitForSeconds(0.5f);
        Options = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            Options[i] = transform.GetChild(i).gameObject;
            Options[i].tag = tagNumber.ToString();
            tagNumber++;
        }
        Debug.LogError(Options.Length + " available options listed");
    }

    public void OptionOneSelect()
    {
        OptionToSelect = 0;
        OptionSelectCoroutine();
        //StartCoroutine(OptionSelectCoroutine());
        Debug.LogError("Option 1 selected");
    }

    public void OptionTwoSelect()
    {
        OptionToSelect = 1;
        OptionSelectCoroutine();
        //StartCoroutine(OptionSelectCoroutine());
        Debug.LogError("Option 2 selected");
    }

    public void OptionThreeSelect()
    {
        OptionToSelect = 2;
        OptionSelectCoroutine();
        //StartCoroutine(OptionSelectCoroutine());
        Debug.LogError("Option 3 selected");
    }

    public void OptionSelectCoroutine()
    {
        optionSelected = true;
        Options[OptionToSelect].GetComponent<OptionView>().InvokeOptionSelected();
        Options[OptionToSelect].GetComponent<Animator>().Play("Selected");
       // OptionSelected.Invoke();
        //yield return new WaitForSeconds(1f);
        //LineView.OnContinueClicked();
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
