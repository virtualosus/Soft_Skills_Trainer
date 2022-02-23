using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using Yarn.Markup;
using UnityEngine.UI;
using System.Linq;

public class LineManager : MonoBehaviour
{
    public SpeechManager char1SpeechManager, char2SpeechManager;

    public YarnProject myYarnProject;

    private List<string> allLinesIDList = new List<string>();
    private List<string> char1LineIDList = new List<string>();
    private List<string> char2LineIDList = new List<string>();
    public List<string> char1TextLineList = new List<string>();
    public List<string> char2TextLineList = new List<string>();

    //public string[] char1LineArray, char2LineArray;

    //public string lineIDsString, lineTextString;
    
    //public Text debugText;

    private void Awake()
    {
        GetLineIDs();
        SortLineIDs();
        GetLinesFromIDs();
    }

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //}

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







    //public void BoySpeechPlayback()
    //{
    //    if (BoySpeechManager.isReady)
    //    {

    //        //lineToBeSpoken = MarkupParseResult.Text;

    //        //lineToBeSpoken = 






    //        //lineToBeSpoken = LineView.sendline
    //        //SpeechManager.voiceName = (VoiceName)voicelist.value;
    //        //SpeechManager.VoicePitch = int.Parse(pitch.text);


    //        // Required to insure non-blocking code in the main Unity UI thread.
    //        //BoySpeechManager.SpeakWithSDKPlugin(lineToBeSpoken);


    //    }
    //    else
    //    {
    //        Debug.Log("SpeechManager is not ready. Wait until authentication has completed.");
    //    }
    //}

    //public void GirlSpeechPlayback()
    //{
    //    if (GirlSpeechManager.isReady)
    //    {
    //        //string msg = lineToBeSpoken;
    //        //SpeechManager.voiceName = (VoiceName)voicelist.value;
    //        //SpeechManager.VoicePitch = int.Parse(pitch.text);


    //        // Required to insure non-blocking code in the main Unity UI thread.
    //        //GirlSpeechManager.SpeakWithSDKPlugin(msg);


    //    }
    //    else
    //    {
    //        Debug.Log("SpeechManager is not ready. Wait until authentication has completed.");
    //    }
    //}

}
