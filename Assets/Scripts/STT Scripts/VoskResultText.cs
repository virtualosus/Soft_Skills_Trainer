using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;




public class VoskResultText : MonoBehaviour 
{
    public VoskSpeechToText VoskSpeechToText;
    public Text ResultText;

    public string ResultString;

    public UnityEvent ResultReceived;

    void Awake()
    {
        VoskSpeechToText.OnTranscriptionResult += OnTranscriptionResult;
    }

    private void OnTranscriptionResult(string obj)
    {
        Debug.Log(obj);
        //ResultText.text = "Recognized: ";
       var result = new RecognitionResult(obj);
        for (int i = 0; i < result.Phrases.Length; i++)
        {
            //if (i > 0)
            //{
            //    ResultText.text += "\n ---------- \n";
            //}

            ResultString = result.Phrases[0].Text;
            ResultText.text = "I heard: "+ result.Phrases[0].Text; //+ " | " + "Confidence: " + result.Phrases[0].Confidence;
            Debug.LogError(ResultString);
            ResultReceived.Invoke();
        }
    }
}
