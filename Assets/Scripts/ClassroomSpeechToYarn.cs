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
    public InteractionHandler interactionHandler;
    public UnityEvent PlayerFinishedTalking;
    public GameObject indicator;

    [Header("Yarn")]
    public InMemoryVariableStorage yarnInMemoryVariableStorage;
    public CharacterYarnLineHandler[] characterYarnLineHandlers;
    public string characterToTalk;
    //public LineManager LineManager;

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

        for (int i = 0; i < characterYarnLineHandlers.Length ; i++ )                             //checks all characters in array and makes sure tags are set to the character name
        {
            characterYarnLineHandlers[i].tag = characterYarnLineHandlers[i].characterName;
            //Debug.LogError(characterYarnLineHandlers[i].tag);
        }
    }

    [YarnCommand("activate_voice_recognition")]                                                 //YARN command to activate Wit.AI voice recognition
    public void ActivateVoiceRecognition()
    {
        //Debug.LogError("Attempting voice recog...");
        indicator.SetActive(true);
        interactionHandler.ToggleActivation();
        //Debug.LogError("Attempt voice recog activation complete");
    }


    [YarnCommand("NPC_start_talking")]                                                          //YARN command retrieving current character set in YARN, finding the relevant character and activating its audio playback
    public void NPCStartToTalk()
    {
        yarnInMemoryVariableStorage.TryGetValue("$characterToTalk", out characterToTalk);
        //Debug.LogError(characterToTalk + " is set to talk.");
        for (int i = 0; i < characterYarnLineHandlers.Length; i++)
        {
            if(characterYarnLineHandlers[i].tag == characterToTalk)
            {
                characterYarnLineHandlers[i].CharacterSpeechPlayback();
            }
        }
    }


    [YarnCommand("NPC_finish_talking_wait")]                                                    //YARN command retrieving the current character and waiting for it's 'finishedTalking' event to trigger
    public IEnumerator NPCWaitFinishTalking()
    {
        for (int i = 0; i < characterYarnLineHandlers.Length; i++)
        {
            if (characterYarnLineHandlers[i].tag == characterToTalk)
            {
                var trigger = false;
                System.Action action = () => trigger = true;
                characterYarnLineHandlers[i].characterFinishedTalking.AddListener(action.Invoke);
                yield return new WaitUntil(() => trigger);
                characterYarnLineHandlers[i].characterFinishedTalking.RemoveListener(action.Invoke);
            }
        }
    }

    [YarnCommand("scene_change")]
    public void SceneChange()
    {
        OVRScreenFade.FadeIn();
        yarnInMemoryVariableStorage.TryGetValue("$sceneNumber", out sceneNumber);
        if(sceneNumber == 1)
        {
            for(int i = 0; i < children.Length; i++)
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
        StartCoroutine(Wait());
        OVRScreenFade.FadeOut();

    }

    [YarnCommand ("Quit_Application")]                                                          //YARN command to enter the application Quit procedure
    public void CloseApplication()
    {
        OVRScreenFade.FadeOut();
        Application.Quit();
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
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
