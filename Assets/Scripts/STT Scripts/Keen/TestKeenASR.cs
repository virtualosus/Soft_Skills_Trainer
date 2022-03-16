using UnityEngine;
using System.Collections;
using KeenResearch;
using UnityEngine.UI;




public class TestKeenASR : MonoBehaviour
{
    AudioSource audioData;

    public Text ResultText;

    public string resultString;

    void Start()
    {
        // setup a few things before we initialize the SDK
        KeenASR.SetLogLevel(KeenASR.LogLevelInfo);
        KeenASR.onInitializedReceived += KeenASRInitialized;

        // init the SDK with the ASR bundle name
        Debug.Log("Keen: Initializing KeenASR Plugin");
        // Initialization works in a slghtly different manner on Android and iOS. On Android
        // it will be done asynchroniously, whereas on iOS it's done 
        KeenASR.Initialize("keenB2mQT-nnet3chain-en-us");
    }

    //Update is called once per frame
    void Update()
    {

        if (KeenASR.Instance != null && KeenASR.Instance.GetRecognizerState() == KeenASR.RecognizerStateListening)
        {
            // here we just show how input levels change; you would poll the input levels somewhere else
            // and use it to drive a UI component (not print in the log file)
            //Debug.Log ("\tLevel: " + KeenASR.Instance.InputLevel ());
        }
    }

    // After recognizer is initalized we can setup other resources (this could be done else where and in some cases
    // multiple decoding graphs may be setup and used independently
    public void KeenASRInitialized(bool status)
    {
        if (!status)
        {
            Debug.Log("KeenASR SDK was not initialized properly");
            return;
        }

        KeenASR recognizer = KeenASR.Instance;
        // setup events with the instance of the recognizer
        recognizer.onFinalASRResultReceived += FinalASRResult;
        recognizer.onPartialASRResultReceived += PartialASRResult;
        recognizer.onRecognizerReadyToListenAfterInterruptReceived += KeenASRReadyToListenAfterInterrupt;
        recognizer.onUnwindAppAudioBeforeAudioInterruptReceived += UnwindAppAudio;

        //recognizer.PerformEchoCancellation(true);
        //recognizer.SetCreateJSONMetadata (true);

        string dgName = "sampleDG";

        Debug.Log("Keen: Creating decoding graph");
        string[] phrases = new string[] { "YES", "NO", "MAYBE", "SURE", "HOW ARE YOU", "I AM GOOD", "I'M GOOD", "I DON'T FEEL GOOD", "I FEEL GOOD", "I AM OKAY", "I'M OKAY", "I AM ALRIGHT", "I'M ALRIGHT" };
        // we don't have to recreate the decoding graph every time; we can instead just
        // check for the existance of the graph with specific name. Note however, that with
        // the latter approach you WILL NEED to force recreation of the graph if you change
        // the list of input phrases
        recognizer.CreateCustomDecodingGraphFromSentences(dgName, phrases);

        // we now use this decoding graph for recognition. Multiple decoding graphs can
        // exist on the device and be switched back and forth
        recognizer.PrepareForListeningWithCustomDecodingGraph(dgName);

        // when set to true, SDK will create audio recordings capturing audio that was 
        // passed to the engine (between start and end listening)
        // you can get the file pat via GetLastRecordingFilename(), once the recognizer 
        // stopped listening (e.g. in onFinalASRResultReceived callback
        recognizer.SetCreateAudioRecordings(true);

        // VAD (Voice Activity Detection) is used to automatically stop listening
        // It can be changed at any time (e.g. slightly reduced in partial callbacks, based 
        // on semantic interpretation of the partial result)
        // final result will be reported after this many seconds end silence
        recognizer.SetVADParameter(KeenASR.VadParamTimeoutEndSilenceForGoodMatch, 1f);
        recognizer.SetVADParameter(KeenASR.VadParamTimeoutEndSilenceForAnyMatch, 1f);
        // also review  KeenASR.VadParamTimeoutForNoSpeech and KeenASR.VadParamTimeoutMaxDuration
        // which also control when stopListening kicks in automatically
    }

    public void FinalASRResult(ASRResult result)
    {
        Debug.Log("Keen FINAL RESULT:" + result.cleanText + ", conf: " + result.confidence + ", numWords: " + result.words.Length);
        foreach (ASRWord word in result.words)
        {
            if (word.isTag)
                ResultText.text = "Word " + word.text + " is a tag word";
                Debug.Log("Word " + word.text + " is a tag word");
            if (word.confidence < 0.8)
                Debug.Log("Word " + word.text + " has LOW confidence");
        }

        KeenASR recognizer = KeenASR.Instance;
        //if (recognizer != null)
        //	recognizer.ResetSpeakerAdaptation ();
        // For testing/demo purposes only; it's unlikely you would need to call this method from within
        // the FinalASRResult callback
        //		Debug.Log("final callback recognizer state returns: " + KeenASR.Instance.GetRecognizerState());
        //Debug.Log("Audio file saved in: " + KeenASR.Instance.GetLastRecordingFilename());
    }

    public void PartialASRResult(string result)
    {
        Debug.Log("Keen PARTIAL RESULT:" + result);
        // For testing/demo purposes only; it's unlikely you would need to call this method from within
        // the FinalASRResult callback
        //		Debug.Log("partial callback, GetRecognizerState returns: " + KeenASR.Instance.GetRecognizerState());
    }

    public void UnwindAppAudio()
    {
        Debug.Log("Unwinding app audio");
    }

    public void KeenASRReadyToListenAfterInterrupt()
    {
        Debug.Log("App ready to listen again...");
        // TODO reanable UI elements, etc.
        KeenASR recognizer = KeenASR.Instance;
    }


    void OnGUI()
    {
        GUIStyle buttonStyle = new GUIStyle();
        buttonStyle.fontSize = 70;
        if (GUI.Button(new Rect(100, 450, 400, 100), "Start Listening", buttonStyle))
        {
#if UNITY_IPHONE || UNITY_ANDROID
            Debug.Log("KeenASR about to start listening");
            KeenASR.Instance.StartListening();
            Debug.Log("KeenASR started to listen. State is " + KeenASR.Instance.GetRecognizerState());
            //            Debug.Log("KeenASR started to listen. ");
#endif
        }
    }

    public void YARNStartListening()
    {
        KeenASRInitialized(true);

#if UNITY_IPHONE || UNITY_ANDROID
        Debug.Log("KeenASR about to start listening");
        KeenASR.Instance.StartListening();
        Debug.Log("KeenASR started to listen. State is " + KeenASR.Instance.GetRecognizerState());
        //            Debug.Log("KeenASR started to listen. ");
#endif
    }


    public void YARNStopListening()
    {
        KeenASR.Instance.StopListening();
        
    }

    private void Awake()
    {
    }
}