using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;





public class VoskResultText : MonoBehaviour 
{
    public VoskSpeechToText VoskSpeechToText;
    public Text ResultText;

    public string ResultString;

    public float recordTime;

    public UnityEvent ResultReceived;

    IEnumerator ListeningForSpeech, RetryListeningForSpeech;


    void Awake()
    {
        VoskSpeechToText.OnTranscriptionResult += OnTranscriptionResult;
        ListeningForSpeech = ListenForSpeechRoutine();
        RetryListeningForSpeech = RetryCountdown();
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
            //Debug.LogError(ResultString);
            ResultReceived.Invoke();
        }
    }

    public void ListenForSpeech()
    {
        StartCoroutine(ListeningForSpeech);
    }

    public void RetryListenForSpeech()
    {
        StartCoroutine(RetryListeningForSpeech);
    }

    public void CancelListenForSpeech()
    {
        StopCoroutine(ListeningForSpeech);
        StopCoroutine(RetryListeningForSpeech);
    }

    public IEnumerator ListenForSpeechRoutine()
    {
        VoskSpeechToText.ToggleRecording();
        yield return new WaitForSeconds(recordTime);
        VoskSpeechToText.ToggleRecording();
    }

    public IEnumerator RetryCountdown()
    {
        ResultText.text = "I heard " + ResultString + ", which is not a recognised response, trying again in 3...";
        yield return new WaitForSeconds(1f);
        ResultText.text = "I heard " + ResultString + ", which is not a recognised response, trying again in 2...";
        yield return new WaitForSeconds(1f);
        ResultText.text = "I heard " + ResultString + ", which is not a recognised response, trying again in 1...";
        yield return new WaitForSeconds(1f);
        ListenForSpeech();
    }

}
