using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using Yarn.Markup;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;

public class LineManager : MonoBehaviour
{


    public YarnProject myYarnProject;

    public SpeechManager char1SpeechManager, char2SpeechManager;
    public ClassroomSpeechToYarn ClassroomTestSpeechToYarn;
    public LineView LineView;
    public AudioSource Char1AudioSource, Char2AudioSource;

    private List<string> allLinesIDList = new List<string>();
    private List<string> char1LineIDList = new List<string>();
    private List<string> char2LineIDList = new List<string>();
    //private List<string> char1NegativeLineIDList = new List<string>();
    //private List<string> char2NegativeLineIDList = new List<string>();
    //private List<string> char1NeutralLineIDList = new List<string>();
    //private List<string> char2NeutralLineIDList = new List<string>();
    //private List<string> char1PositiveLineIDList = new List<string>();
    //private List<string> char2PositiveLineIDList = new List<string>();

    //[Header("Stage 1 Lines")]
    //private List<string> char1NegativeLineStage1IDList = new List<string>();
    //private List<string> char2NegativeLineStage1IDList = new List<string>();
    //private List<string> char1NeutralLineStage1IDList = new List<string>();
    //private List<string> char2NeutralLineStage1IDList = new List<string>();
    //private List<string> char1PositiveLineStage1IDList = new List<string>();
    //private List<string> char2PositiveLineStage1IDList = new List<string>();

    //private List<string> char1NegativeLineStage2IDList = new List<string>();
    //private List<string> char2NegativeLineStage2IDList = new List<string>();
    //private List<string> char1NeutralLineStage2IDList = new List<string>();
    //private List<string> char2NeutralLineStage2IDList = new List<string>();
    //private List<string> char1PositiveLineStage2IDList = new List<string>();
    //private List<string> char2PositiveLineStage2IDList = new List<string>();

    //private List<string> char1NegativeLineStage3IDList = new List<string>();
    //private List<string> char2NegativeLineStage3IDList = new List<string>();
    //private List<string> char1NeutralLineStage3IDList = new List<string>();
    //private List<string> char2NeutralLineStage3IDList = new List<string>();
    //private List<string> char1PositiveLineStage3IDList = new List<string>();
    //private List<string> char2PositiveLineStage3IDList = new List<string>();

    [Header("All Lines")]
    public List<string> char1TextLineList = new List<string>();
    public List<string> char2TextLineList = new List<string>();
    //public List<string> char1NegativeLineList = new List<string>();
    //public List<string> char2NegativeLineList = new List<string>();
    //public List<string> char1NeutralLineList = new List<string>();
    //public List<string> char2NeutralLineList = new List<string>();
    //public List<string> char1PositiveLineList = new List<string>();
    //public List<string> char2PositiveLineList = new List<string>();

    //[Header("Stage 1 Lines")]
    //public List<string> char1NegativeLineStage1List = new List<string>();
    //public List<string> char2NegativeLineStage1List = new List<string>();
    //public List<string> char1NeutralLineStage1List = new List<string>();
    //public List<string> char2NeutralLineStage1List = new List<string>();
    //public List<string> char1PositiveLineStage1List = new List<string>();
    //public List<string> char2PositiveLineStage1List = new List<string>();
    
    //[Header("Stage 2 Lines")]
    //public List<string> char1NegativeLineStage2List = new List<string>();
    //public List<string> char2NegativeLineStage2List = new List<string>();
    //public List<string> char1NeutralLineStage2List = new List<string>();
    //public List<string> char2NeutralLineStage2List = new List<string>();
    //public List<string> char1PositiveLineStage2List = new List<string>();
    //public List<string> char2PositiveLineStage2List = new List<string>();

    //[Header("Stage 3 Lines")]
    //public List<string> char1NegativeLineStage3List = new List<string>();
    //public List<string> char2NegativeLineStage3List = new List<string>();
    //public List<string> char1NeutralLineStage3List = new List<string>();
    //public List<string> char2NeutralLineStage3List = new List<string>();
    //public List<string> char1PositiveLineStage3List = new List<string>();
    //public List<string> char2PositiveLineStage3List = new List<string>();


    public int char1LineCount, char2LineCount;

    public float waitTime;

    public UnityEvent Char1FinishedTalking, Char2FinishedTalking;

    //public string[] char1LineArray, char2LineArray;

    //public string lineIDsString, lineTextString;

    //public Text debugText;

    private void Awake()
    {
        GetLineIDs();
        SortLineIDs();
        GetLinesFromIDs();
    }


    public void GetLineIDs()
    {
        var lineID = myYarnProject.GetLocalization("En").GetLineIDs();
        //Debug.LogError(lineID);
        allLinesIDList = lineID.ToList();
    }

    public void SortLineIDs()
    {
        foreach (string tempString in allLinesIDList)
        {
            if (tempString.Contains("char1"))
            {
                char1LineIDList.Add(tempString);
            }
            if (tempString.Contains("char2"))
            {
                char2LineIDList.Add(tempString);
            }


            //if (tempString.Contains("char1Negative"))
            //{
            //    char1NegativeLineIDList.Add(tempString);
            //}
            //if (tempString.Contains("char2Negative"))
            //{
            //    char2NegativeLineIDList.Add(tempString);
            //}
            //if (tempString.Contains("char1Neutral"))
            //{
            //    char1NeutralLineIDList.Add(tempString);
            //}
            //if (tempString.Contains("char2Neutral"))
            //{
            //    char2NeutralLineIDList.Add(tempString);
            //}
            //if (tempString.Contains("char1Positive"))
            //{
            //    char1PositiveLineIDList.Add(tempString);
            //}
            //if (tempString.Contains("char2Positive"))
            //{
            //    char2PositiveLineIDList.Add(tempString);
            //}

            //if (tempString.Contains("char1NegativeStage1"))
            //{
            //    char1NegativeLineStage1IDList.Add(tempString);
            //}
            //if (tempString.Contains("char2NegativeStage1"))
            //{
            //    char2NegativeLineStage1IDList.Add(tempString);
            //}
            //if (tempString.Contains("char1NeutralStage1"))
            //{
            //    char1NeutralLineStage1IDList.Add(tempString);
            //}
            //if (tempString.Contains("char2NeutralStage1"))
            //{
            //    char2NeutralLineStage1IDList.Add(tempString);
            //}
            //if (tempString.Contains("char1PositiveStage1"))
            //{
            //    char1PositiveLineStage1IDList.Add(tempString);
            //}
            //if (tempString.Contains("char2PositiveStage1"))
            //{
            //    char2PositiveLineStage1IDList.Add(tempString);
            //}

            //if (tempString.Contains("char1NegativeStage2"))
            //{
            //    char1NegativeLineStage2IDList.Add(tempString);
            //}
            //if (tempString.Contains("char2NegativeStage2"))
            //{
            //    char2NegativeLineStage2IDList.Add(tempString);
            //}
            //if (tempString.Contains("char1NeutralStage2"))
            //{
            //    char1NeutralLineStage2IDList.Add(tempString);
            //}
            //if (tempString.Contains("char2NeutralStage2"))
            //{
            //    char2NeutralLineStage2IDList.Add(tempString);
            //}
            //if (tempString.Contains("char1PositiveStage2"))
            //{
            //    char1PositiveLineStage2IDList.Add(tempString);
            //}
            //if (tempString.Contains("char2PositiveStage2"))
            //{
            //    char2PositiveLineStage2IDList.Add(tempString);
            //}

            //if (tempString.Contains("char1NegativeStage3"))
            //{
            //    char1NegativeLineStage3IDList.Add(tempString);
            //}
            //if (tempString.Contains("char2NegativeStage3"))
            //{
            //    char2NegativeLineStage3IDList.Add(tempString);
            //}
            //if (tempString.Contains("char1NeutralStage3"))
            //{
            //    char1NeutralLineStage3IDList.Add(tempString);
            //}
            //if (tempString.Contains("char2NeutralStage3"))
            //{
            //    char2NeutralLineStage3IDList.Add(tempString);
            //}
            //if (tempString.Contains("char1PositiveStage3"))
            //{
            //    char1PositiveLineStage3IDList.Add(tempString);
            //}
            //if (tempString.Contains("char2PositiveStage3"))
            //{
            //    char2PositiveLineStage3IDList.Add(tempString);
            //}
        }
    }

    public void GetLinesFromIDs()
    {
        foreach (string tempString in char1LineIDList)
        {
            var text = myYarnProject.GetLocalization("En").GetLocalizedString(tempString);
            char1TextLineList.Add(text);
        }

        foreach (string tempString in char2LineIDList)
        {
            var text = myYarnProject.GetLocalization("En").GetLocalizedString(tempString);
            char2TextLineList.Add(text);
        }

        //foreach (string tempString in char1NegativeLineIDList)
        //{
        //    var text = myYarnProject.GetLocalization("En").GetLocalizedString(tempString);
        //    char1NegativeLineList.Add(text);
        //}

        //foreach (string tempString in char2NegativeLineIDList)
        //{
        //    var text = myYarnProject.GetLocalization("En").GetLocalizedString(tempString);
        //    char2NegativeLineList.Add(text);
        //}

        //foreach (string tempString in char1NeutralLineIDList)
        //{
        //    var text = myYarnProject.GetLocalization("En").GetLocalizedString(tempString);
        //    char1NeutralLineList.Add(text);
        //}

        //foreach (string tempString in char2NeutralLineIDList)
        //{
        //    var text = myYarnProject.GetLocalization("En").GetLocalizedString(tempString);
        //    char2NeutralLineList.Add(text);
        //}

        //foreach (string tempString in char1PositiveLineIDList)
        //{
        //    var text = myYarnProject.GetLocalization("En").GetLocalizedString(tempString);
        //    char1PositiveLineList.Add(text);
        //}

        //foreach (string tempString in char2PositiveLineIDList)
        //{
        //    var text = myYarnProject.GetLocalization("En").GetLocalizedString(tempString);
        //    char2PositiveLineList.Add(text);
        //}

        //foreach (string tempString in char1NegativeLineStage1IDList)
        //{
        //    var text = myYarnProject.GetLocalization("En").GetLocalizedString(tempString);
        //    char1NegativeLineStage1List.Add(text);
        //}

        //foreach (string tempString in char2NegativeLineStage1IDList)
        //{
        //    var text = myYarnProject.GetLocalization("En").GetLocalizedString(tempString);
        //    char2NegativeLineStage1List.Add(text);
        //}

        //foreach (string tempString in char1NeutralLineStage1IDList)
        //{
        //    var text = myYarnProject.GetLocalization("En").GetLocalizedString(tempString);
        //    char1NeutralLineStage1List.Add(text);
        //}

        //foreach (string tempString in char2NeutralLineStage1IDList)
        //{
        //    var text = myYarnProject.GetLocalization("En").GetLocalizedString(tempString);
        //    char2NeutralLineStage1List.Add(text);
        //}

        //foreach (string tempString in char1PositiveLineStage1IDList)
        //{
        //    var text = myYarnProject.GetLocalization("En").GetLocalizedString(tempString);
        //    char1PositiveLineStage1List.Add(text);
        //}

        //foreach (string tempString in char2PositiveLineStage1IDList)
        //{
        //    var text = myYarnProject.GetLocalization("En").GetLocalizedString(tempString);
        //    char2PositiveLineStage1List.Add(text);
        //}

        //foreach (string tempString in char1NegativeLineStage2IDList)
        //{
        //    var text = myYarnProject.GetLocalization("En").GetLocalizedString(tempString);
        //    char1NegativeLineStage2List.Add(text);
        //}

        //foreach (string tempString in char2NegativeLineStage2IDList)
        //{
        //    var text = myYarnProject.GetLocalization("En").GetLocalizedString(tempString);
        //    char2NegativeLineStage2List.Add(text);
        //}

        //foreach (string tempString in char1NeutralLineStage2IDList)
        //{
        //    var text = myYarnProject.GetLocalization("En").GetLocalizedString(tempString);
        //    char1NeutralLineStage2List.Add(text);
        //}

        //foreach (string tempString in char2NeutralLineStage2IDList)
        //{
        //    var text = myYarnProject.GetLocalization("En").GetLocalizedString(tempString);
        //    char2NeutralLineStage2List.Add(text);
        //}

        //foreach (string tempString in char1PositiveLineStage2IDList)
        //{
        //    var text = myYarnProject.GetLocalization("En").GetLocalizedString(tempString);
        //    char1PositiveLineStage2List.Add(text);
        //}

        //foreach (string tempString in char2PositiveLineStage3IDList)
        //{
        //    var text = myYarnProject.GetLocalization("En").GetLocalizedString(tempString);
        //    char2PositiveLineStage3List.Add(text);
        //}

        //foreach (string tempString in char1NegativeLineStage3IDList)
        //{
        //    var text = myYarnProject.GetLocalization("En").GetLocalizedString(tempString);
        //    char1NegativeLineStage3List.Add(text);
        //}

        //foreach (string tempString in char2NegativeLineStage3IDList)
        //{
        //    var text = myYarnProject.GetLocalization("En").GetLocalizedString(tempString);
        //    char2NegativeLineStage3List.Add(text);
        //}

        //foreach (string tempString in char1NeutralLineStage3IDList)
        //{
        //    var text = myYarnProject.GetLocalization("En").GetLocalizedString(tempString);
        //    char1NeutralLineStage3List.Add(text);
        //}

        //foreach (string tempString in char2NeutralLineStage3IDList)
        //{
        //    var text = myYarnProject.GetLocalization("En").GetLocalizedString(tempString);
        //    char2NeutralLineStage3List.Add(text);
        //}

        //foreach (string tempString in char1PositiveLineStage3IDList)
        //{
        //    var text = myYarnProject.GetLocalization("En").GetLocalizedString(tempString);
        //    char1PositiveLineStage3List.Add(text);
        //}

        //foreach (string tempString in char2PositiveLineStage3IDList)
        //{
        //    var text = myYarnProject.GetLocalization("En").GetLocalizedString(tempString);
        //    char2PositiveLineStage3List.Add(text);
        //}

        //var text = myYarnProject.GetLocalization("En").GetLocalizedString("line:01c1039");
        //lineTextString = text;
        //Debug.LogError(text);
    }



    public void Char1SpeechPlayback()
    {
        Debug.LogError("Char 1 speaking...");

        if (char1SpeechManager.isReady)
        {
            string lineToBeSpoken = char1TextLineList[char1LineCount];


            char1SpeechManager.SpeakWithSDKPlugin(lineToBeSpoken);
            char1LineCount++;
            //StartCoroutine(Char1VolTrim());
            StartCoroutine(Char1WaitForLineToFinish());

        }
        else
        {
            Debug.Log("SpeechManager is not ready. Wait until authentication has completed.");
        }
    }

    public IEnumerator Char1VolTrim()
    {
        Char1AudioSource.volume = 0;
        yield return new WaitForSeconds(waitTime);
        Char1AudioSource.volume = 1;
    }

    public IEnumerator Char1WaitForLineToFinish()
    {
        yield return new WaitUntil(() => Char1AudioSource.isPlaying);

        yield return new WaitUntil(() => !Char1AudioSource.isPlaying);
        LineView.OnContinueClicked();

        yield return new WaitForSeconds(0.5f);
        LineView.OnContinueClicked();
        Char1FinishedTalking.Invoke();

        Debug.LogError("Char1 finished talking");

        //yield return new WaitForSeconds(2f);
        //Char1FinishedTalking.Invoke();
        //Debug.LogError("Char1 finished talking");

    }

    public void Char2SpeechPlayback()
    {
        Debug.LogError("Char 2 speaking...");

        if (char2SpeechManager.isReady)
        {
            string lineToBeSpoken = char2TextLineList[char2LineCount];

            char2SpeechManager.SpeakWithSDKPlugin(lineToBeSpoken);
            char2LineCount++;
            StartCoroutine(Char2VolTrim());
            StartCoroutine(Char2WaitForLineToFinish());


        }
        else
        {
            Debug.Log("SpeechManager is not ready. Wait until authentication has completed.");
        }
    }

    public IEnumerator Char2VolTrim()
    {
        Char2AudioSource.volume = 0;
        yield return new WaitForSeconds(waitTime);
        Char2AudioSource.volume = 1;
    }


    public IEnumerator Char2WaitForLineToFinish()
    {
        yield return new WaitUntil(() => Char2AudioSource.isPlaying);

        yield return new WaitUntil(() => !Char2AudioSource.isPlaying);


        yield return new WaitForSeconds(0.5f);
        LineView.OnContinueClicked();



        Char2FinishedTalking.Invoke();     


        Debug.LogError("Char2 finished talking");

    }
}
