using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;

public class OptionSelectTest : MonoBehaviour
{
    //public OptionsListView OptionsListView;
    //public DialogueOption DialogueOption;

    public LineView LineView;

    public GameObject[] Options;

    public bool option1, option2, option3;

    public CanvasGroup OptionsCanvas;

    public int OptionToSelect;

    private void OnValidate()
    {
        if (option1)
        {
            OptionOneSelect();
            option1 = false;
        }

        if (option2)
        {
            OptionTwoSelect();
            option2 = false;
        }

        if (option3)
        {
            OptionThreeSelect();
            option3 = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //OptionsTest();

    }

    public void OptionsTest()
    {
        Debug.LogError("OPTIONS TEST");
        Options = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            Options[i] = transform.GetChild(i).gameObject;
        }
    }

    public void OptionOneSelect()
    {
        Debug.LogError("Option 1 selected");
        OptionToSelect = 0;
        StartCoroutine(OptionSelectTestCoroutine());
        //HideCanvas();

    }

    public void OptionTwoSelect()
    {
        Debug.LogError("Option 2 selected");
        OptionToSelect = 1;
        StartCoroutine(OptionSelectTestCoroutine());
        //HideCanvas();

    }

    public void OptionThreeSelect()
    {
        Debug.LogError("Option 3 selected");
        OptionToSelect = 2;
        StartCoroutine(OptionSelectTestCoroutine());
        //HideCanvas();

    }

    public IEnumerator OptionSelectTestCoroutine()
    {
        Options[OptionToSelect].GetComponent<OptionView>().InvokeOptionSelected();
        yield return new WaitForSeconds(1f);

        LineView.OnContinueClicked();
    }

    //public void ShowCanvas()
    //{
    //    OptionsCanvas.alpha = 1;
    //}

    //public void HideCanvas()
    //{
    //    OptionsCanvas.alpha = 0;
    //}

}
