using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.Events;
using Oculus.Voice.Demo.UIShapesDemo;
using Oculus.Voice;


public class ClassroomSpeechToYarn : MonoBehaviour
{
    [Header("Voice")]
    [SerializeField] private AppVoiceExperience appVoiceExperience;
    public InteractionHandler interactionHandler;

    public InMemoryVariableStorage yarnInMemoryVariableStorage;

    public LineManager LineManager;

    public LineView LineView;

    public UnityEvent PlayerFinishedTalking;

    public GameObject indicator;

    private void Awake()
    {
        indicator.SetActive(false);
    }

    [YarnCommand("activate_voice_recognition")]           //yarn command to activate Wit.AI
    public void ActivateVoiceRecognition()
    {
        //Debug.LogError("Attempting voice recog...");
        indicator.SetActive(true);
        interactionHandler.ToggleActivation();
        //Debug.LogError("Attempt voice recog activation complete");
    }



    [YarnCommand("wait_for_speech_recog")]
    public IEnumerator CustomWait()
    {
        //Debug.Log("Coroutine started...");
        var trigger = false;
        System.Action action = () => trigger = true;
        PlayerFinishedTalking.AddListener(action.Invoke);
        yield return new WaitUntil(() => trigger);
        PlayerFinishedTalking.RemoveListener(action.Invoke);
        LineView.OnContinueClicked();
        //Debug.Log("Coroutine finished!!");
    }

        
    [YarnCommand("girl_talk")]                          //yarn command retrieving required voiceline number from YARN and setting clip and initiating Audio in VOLineController
    public void GirlToTalk()
    {
        LineManager.Char1SpeechPlayback();
    }

    [YarnCommand("char1_finish_talking_wait")]                         
    public IEnumerator Char1FinishTalking()
    {
        //Debug.Log("Coroutine started...");
        var trigger = false;
        System.Action action = () => trigger = true;
        LineManager.Char1FinishedTalking.AddListener(action.Invoke);
        yield return new WaitUntil(() => trigger);
        LineManager.Char1FinishedTalking.RemoveListener(action.Invoke);
        //LineView.OnContinueClicked();
        //Debug.Log("Coroutine finished!!");
        //
     }


        [YarnCommand("boy_talk")]                           //yarn command retrieving required voiceline number from YARN and setting clip and initiating Audio in VOLineController
    public void BoyToTalk()
    {
        LineManager.Char2SpeechPlayback();
    }

    [YarnCommand("char2_finish_talking_wait")]                         
    public IEnumerator Char2FinishTalking()
    {
        //Debug.Log("Coroutine started...");
        var trigger = false;
        System.Action action = () => trigger = true;
        LineManager.Char2FinishedTalking.AddListener(action.Invoke);
        yield return new WaitUntil(() => trigger);
        LineManager.Char2FinishedTalking.RemoveListener(action.Invoke);
        //LineView.OnContinueClicked();
        //Debug.Log("Coroutine finished!!");
        //
    }

    [YarnCommand ("Quit_Application")]
    public void CloseApplication()
    {
        Application.Quit();
    }

}
