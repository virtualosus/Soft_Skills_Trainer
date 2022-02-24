using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using Yarn.Markup;
using UnityEngine.UI;
using System.Linq;

public class LineManager : MonoBehaviour
{
    public YarnProject myYarnProject;

    public SpeechManager char1SpeechManager, char2SpeechManager;
    public ClassroomTestSpeechToYarn ClassroomTestSpeechToYarn;
    public LineView LineView;

    private List<string> allLinesIDList = new List<string>();
    private List<string> char1LineIDList = new List<string>();
    private List<string> char2LineIDList = new List<string>();
    public List<string> char1TextLineList = new List<string>();
    public List<string> char2TextLineList = new List<string>();

    public int char1LineCount, char2LineCount;

    public bool audioPlaying;

    public AudioSource Char1AudioSource, Char2AudioSource;

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
            if (tempString.Contains("line:char1"))
            {
                char1LineIDList.Add(tempString);
            }
            if (tempString.Contains("line:char2"))
            {
                char2LineIDList.Add(tempString);
            }
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

            //SpeechManager.voiceName = (VoiceName)voicelist.value;
            //SpeechManager.VoicePitch = int.Parse(pitch.text);

            //Required to insure non - blocking code in the main Unity UI thread.
            char1SpeechManager.SpeakWithSDKPlugin(lineToBeSpoken);
            char1LineCount++;
            StartCoroutine(Char1WaitForLineToFinish());
        }
        else
        {
            Debug.Log("SpeechManager is not ready. Wait until authentication has completed.");
        }
    }

    public void Char2SpeechPlayback()
    {
        Debug.LogError("Char 2 speaking...");

        if (char2SpeechManager.isReady)
        {
            string lineToBeSpoken = char2TextLineList[char2LineCount];

            //SpeechManager.voiceName = (VoiceName)voicelist.value;
            //SpeechManager.VoicePitch = int.Parse(pitch.text);

            // Required to insure non-blocking code in the main Unity UI thread.
            char2SpeechManager.SpeakWithSDKPlugin(lineToBeSpoken);
            char2LineCount++;
            StartCoroutine(Char2WaitForLineToFinish());

        }
        else
        {
            Debug.Log("SpeechManager is not ready. Wait until authentication has completed.");
        }
    }

    public IEnumerator Char1WaitForLineToFinish()
    {
        yield return new WaitUntil(() => Char1AudioSource.isPlaying);

        yield return new WaitUntil(() => !Char1AudioSource.isPlaying);

        yield return new WaitForSeconds(0.5f);

        LineView.OnContinueClicked();
        //ClassroomTestSpeechToYarn.Char1FinishTalking();
        Debug.LogError("Char1 finished talking");
    }

    public IEnumerator Char2WaitForLineToFinish()
    {
        yield return new WaitUntil(() => Char2AudioSource.isPlaying);

        yield return new WaitUntil(() => !Char2AudioSource.isPlaying);

        yield return new WaitForSeconds(0.5f);
        LineView.OnContinueClicked();
        //ClassroomTestSpeechToYarn.Char2FinishTalking();

        Debug.LogError("Char2 finished talking");

    }
}
