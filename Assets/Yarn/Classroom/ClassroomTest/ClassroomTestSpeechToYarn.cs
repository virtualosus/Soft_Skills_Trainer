using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.Events;
using Oculus.Voice.Demo.UIShapesDemo;
using Oculus.Voice;


public class ClassroomTestSpeechToYarn : MonoBehaviour
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
        Debug.LogError("Attempting voice recog...");
        indicator.SetActive(true);
        interactionHandler.ToggleActivation();
        Debug.LogError("Attempt voice recog activation complete");
    }


    [YarnCommand("wait_for_speech_recog")]
    public IEnumerator CustomWait()
    {
        Debug.Log("Coroutine started...");
        var trigger = false;
        System.Action action = () => trigger = true;
        PlayerFinishedTalking.AddListener(action.Invoke);
        yield return new WaitUntil(() => trigger);
        PlayerFinishedTalking.RemoveListener(action.Invoke);
        LineView.OnContinueClicked();
        Debug.Log("Coroutine finished!!");
    }

        
    [YarnCommand("girl_talk")]                          //yarn command retrieving required voiceline number from YARN and setting clip and initiating Audio in VOLineController
    public void GirlToTalk()
    {
        LineManager.Char1SpeechPlayback();
    }

        
    [YarnCommand("boy_talk")]                           //yarn command retrieving required voiceline number from YARN and setting clip and initiating Audio in VOLineController
    public void BoyToTalk()
    {
        LineManager.Char2SpeechPlayback();
    }


    ////[YarnCommand("girl_NPC_finish_talking_wait")]       //custom YARN wait based on length of current GIRL voice line plus half a second
    //public IEnumerator Char1FinishTalking()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    //LineView.OnContinueClicked();
    //}


    ////[YarnCommand("boy_NPC_finish_talking_wait")]        //custom YARN wait based on length of current BOY voice line plus half a second
    //public IEnumerator Char2FinishTalking()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    //LineView.OnContinueClicked();
    //}
}
