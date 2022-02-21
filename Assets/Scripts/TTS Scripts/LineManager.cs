using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using Yarn.Markup;

public class LineManager : MonoBehaviour
{

    public SpeechManager BoySpeechManager, GirlSpeechManager;
    //public LineView LineView;
    public string lineToBeSpoken;
    public MarkupParseResult MarkupParseResult;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLine();
    }

    public void UpdateLine()
    {
        lineToBeSpoken = MarkupParseResult.Text;
        //Debug.LogError("updating line...");
    }




    public void BoySpeechPlayback()
    {
        if (BoySpeechManager.isReady)
        {

            //lineToBeSpoken = MarkupParseResult.Text;

            //lineToBeSpoken = 
                





            //lineToBeSpoken = LineView.sendline
            //SpeechManager.voiceName = (VoiceName)voicelist.value;
            //SpeechManager.VoicePitch = int.Parse(pitch.text);


            // Required to insure non-blocking code in the main Unity UI thread.
            BoySpeechManager.SpeakWithSDKPlugin(lineToBeSpoken);
            

        }
        else
        {
            Debug.Log("SpeechManager is not ready. Wait until authentication has completed.");
        }
    }

    public void GirlSpeechPlayback()
    {
        if (GirlSpeechManager.isReady)
        {
            string msg = lineToBeSpoken;
            //SpeechManager.voiceName = (VoiceName)voicelist.value;
            //SpeechManager.VoicePitch = int.Parse(pitch.text);


            // Required to insure non-blocking code in the main Unity UI thread.
            GirlSpeechManager.SpeakWithSDKPlugin(msg);


        }
        else
        {
            Debug.Log("SpeechManager is not ready. Wait until authentication has completed.");
        }
    }

}
