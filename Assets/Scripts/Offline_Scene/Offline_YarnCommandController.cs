using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.Events;
using Oculus.Voice.Demo.UIShapesDemo;
using Oculus.Voice;


public class Offline_YarnCommandController : MonoBehaviour
{
    [Header("Voice")]

    public VoskSpeechToText VoskSpeechToText;
    public GameObject indicator;
    public Offline_OptionController Offline_OptionController;
    public UnityEvent PlayerFinishedTalking;

    [Header("Yarn")]
    public InMemoryVariableStorage yarnInMemoryVariableStorage;
    public GameObject[] characters;
    public string characterToTalk;


    [Header("Screenfade")]
    public OVRScreenFade OVRScreenFade;
    public float sceneNumber;
    public GameObject[] children;
    public GameObject[] teacher;

    private void Awake()
    {
        indicator.SetActive(false);

        for (int i = 0; i < children.Length; i++)
        {
            children[i].SetActive(false);
        }
        for (int i = 0; i < teacher.Length; i++)
        {
            teacher[i].SetActive(true);
        }

    }

    [YarnCommand("offline_activate_voice_recognition")]                                                 //YARN command to activate Wit.AI voice recognition and gather available options
    public void ActivateVoiceRecognition()
    {
        Debug.LogError("Attempting voice recog...");
        StartCoroutine(Offline_OptionController.GatherOptions());
        indicator.SetActive(true);
        VoskSpeechToText.ToggleRecording();
        Debug.LogError("Attempt voice recog activation complete");
    }

    [YarnCommand("offline_NPC_finish_talking_wait")]                                                    //YARN command retrieving the current character and waiting for it's 'finishedTalking' event to trigger
    public IEnumerator NPCWaitFinishTalking()
    {
        yarnInMemoryVariableStorage.TryGetValue("$characterToTalk", out characterToTalk);

        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i].tag == characterToTalk)
            {
                yield return new WaitUntil(() => characters[i].GetComponent<AudioSource>().isPlaying);
                yield return new WaitUntil(() => !characters[i].GetComponent<AudioSource>().isPlaying);
            }
        }
    }

    [YarnCommand("offline_scene_change")]
    public void SceneChange()
    {
        StartCoroutine(SwitchCharacters());
    }

    [YarnCommand ("offline_Quit_Application")]                                                          //YARN command to enter the application Quit procedure
    public void CloseApplication()
    {
        OVRScreenFade.FadeOut();
        Application.Quit();
    }

    public IEnumerator SwitchCharacters()
    {
        OVRScreenFade.FadeOut();
        yield return new WaitForSeconds(1f);
        yarnInMemoryVariableStorage.TryGetValue("$sceneNumber", out sceneNumber);
        if (sceneNumber == 1)
        {
            for (int i = 0; i < children.Length; i++)
            {
                children[i].SetActive(true);
            }
            for (int i = 0; i < teacher.Length; i++)
            {
                teacher[i].SetActive(false);
            }
        }
        if (sceneNumber == 2)
        {
            for (int i = 0; i < children.Length; i++)
            {
                children[i].SetActive(false);
            }
            for (int i = 0; i < teacher.Length; i++)
            {
                teacher[i].SetActive(true);
            }
        }
        OVRScreenFade.FadeIn();
    }

    //[YarnCommand("wait_for_speech_recog")]                                                   //yarn command to wait for player to finish talking - for YARN line view mode
    //public IEnumerator CustomWait()
    //{
    //    //Debug.Log("Coroutine started...");
    //    var trigger = false;
    //    System.Action action = () => trigger = true;
    //    PlayerFinishedTalking.AddListener(action.Invoke);
    //    yield return new WaitUntil(() => trigger);
    //    PlayerFinishedTalking.RemoveListener(action.Invoke);
    //    //Debug.Log("Coroutine finished!!");
    //}

    //[YarnCommand("girl_talk")]                          //OLD TEST COMMANDS - yarn command retrieving required voiceline number from YARN and setting clip and initiating Audio in VOLineController
    //public void GirlToTalk()
    //{
    //    LineManager.Char1SpeechPlayback();
    //}

    //[YarnCommand("char1_finish_talking_wait")]                         
    //public IEnumerator Char1FinishTalking()
    //{
    //    //Debug.Log("Coroutine started...");
    //    var trigger = false;
    //    System.Action action = () => trigger = true;
    //    LineManager.Char1FinishedTalking.AddListener(action.Invoke);
    //    yield return new WaitUntil(() => trigger);
    //    LineManager.Char1FinishedTalking.RemoveListener(action.Invoke);
    //    //LineView.OnContinueClicked();
    //    //Debug.Log("Coroutine finished!!");
    //    //
    // }


    //[YarnCommand("boy_talk")]                           //yarn command retrieving required voiceline number from YARN and setting clip and initiating Audio in VOLineController
    //public void BoyToTalk()
    //{
    //    LineManager.Char2SpeechPlayback();
    //}

    //[YarnCommand("char2_finish_talking_wait")]                         
    //public IEnumerator Char2FinishTalking()
    //{
    //    //Debug.Log("Coroutine started...");
    //    var trigger = false;
    //    System.Action action = () => trigger = true;
    //    LineManager.Char2FinishedTalking.AddListener(action.Invoke);
    //    yield return new WaitUntil(() => trigger);
    //    LineManager.Char2FinishedTalking.RemoveListener(action.Invoke);
    //    //LineView.OnContinueClicked();
    //    //Debug.Log("Coroutine finished!!");
    //    //
    //}

}
