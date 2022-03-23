using System.Collections;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.Events;
using UnityEngine.SceneManagement;




public class Offline_YarnCommandController : MonoBehaviour
{
    public string sceneName;

    [Header("Voice")]
    public VoskResultText VoskResultText;

    [Header("Yarn")]
    public Offline_CharacterYarnLineHandler[] Offline_CharacterYarnLineHandler;
    public InMemoryVariableStorage yarnInMemoryVariableStorage;
    public GameObject[] characters;
    public GameObject[] children;
    public GameObject[] teacher;
    private string characterToTalk;
    private string sceneToLoad;

    [Header("Internet Connection")]
    public InternetTestController InternetTestController;

    [Header("Screenfade")]
    public OVRScreenFade OVRScreenFade;
    public float sceneNumber;

    [Header("Options Control")]
    public OptionController OptionController;
    public UnityEvent PlayerFinishedTalking;

    private void Awake()
    {
        if (sceneName == "Offline_Classroom")
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
    }

    [YarnCommand("check_internet_connection")]                                                 //YARN command to activate Wit.AI voice recognition and gather available options
    public IEnumerator CheckInternetConnection()
    {
        InternetTestController.CheckConnection();
        yield return new WaitForSeconds(1f);
        if (InternetTestController.appHasInternet)
        {
            yarnInMemoryVariableStorage.SetValue("$internetConnection", "true");
            yarnInMemoryVariableStorage.SetValue("$ping", InternetTestController.pingResult);
        }
        else
        {
            yarnInMemoryVariableStorage.SetValue("$internetConnection", "false");
            yarnInMemoryVariableStorage.SetValue("$ping", 0);
        }
        OVRScreenFade.FadeIn();

    }


    [YarnCommand("offline_activate_voice_recognition")]                                                 //YARN command to activate Wit.AI voice recognition and gather available options
    public void ActivateVoiceRecognition()
    {
        //Debug.LogError("Attempting voice recog...");
        StartCoroutine(OptionController.GatherOptions());
        VoskResultText.ListenForSpeech();
        //Debug.LogError("Attempt voice recog activation complete");
    }

    //[YarnCommand("offline_deactivate_voice_recognition")]                                                 //YARN command to activate Wit.AI voice recognition and gather available options
    //public void DeactivateVoiceRecognition()
    //{
    //    Debug.LogError("Deactiavting voice recog...");
    //    VoskSpeechToText.ToggleRecording();
    //    Debug.LogError("Deactivating voice recog complete");
    //}

    [YarnCommand("offline_NPC_start_talking")]                                                    //YARN command retrieving the current character and waiting for it's 'finishedTalking' event to trigger
    public void NPCStarttalking()
    {
        //Debug.LogError("Attempting NPC talking...");

        yarnInMemoryVariableStorage.TryGetValue("$characterToTalk", out characterToTalk);

        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i].tag == characterToTalk)
            {
                //Debug.LogError("Tag matched...");
                characters[i].GetComponentInChildren<Offline_CharacterYarnLineHandler>().CharacterSpeechPlayback();
            }
        }
    }

    [YarnCommand("offline_NPC_finish_talking_wait")]                                                    //YARN command retrieving the current character and waiting for it's 'finishedTalking' event to trigger
    public IEnumerator NPCWaitFinishTalking()
    {
        //Debug.LogError("Attempting NPC wait...");

        yarnInMemoryVariableStorage.TryGetValue("$characterToTalk", out characterToTalk);

        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i].tag == characterToTalk)
            {
                //Debug.LogError("NPC wait tag matched...");

                var trigger = false;
                System.Action action = () => trigger = true;
                characters[i].GetComponentInChildren<Offline_CharacterYarnLineHandler>().characterFinishedTalking.AddListener(action.Invoke);
                yield return new WaitUntil(() => trigger);
                characters[i].GetComponentInChildren<Offline_CharacterYarnLineHandler>().characterFinishedTalking.RemoveListener(action.Invoke);
            }
        }
    }

    [YarnCommand("offline_scene_change")]
    public void SceneChange()
    {
        if (sceneName == "Offline_Classroom")
        {
            StartCoroutine(SwitchCharacters());
        }
        if (sceneName == "Launch_Menu")
        {
            StartCoroutine(ChangeScene());
        }
    }

    [YarnCommand("offline_Quit_Application")]                                                          //YARN command to enter the application Quit procedure
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

    public IEnumerator ChangeScene()
    {
        OVRScreenFade.FadeOut();
        yield return new WaitForSeconds(1f);
        yarnInMemoryVariableStorage.TryGetValue("$launchSceneNumber", out sceneNumber);
        if (sceneNumber == 1)
        {
            sceneToLoad = "Classroom_Online";
            SceneManager.LoadScene(sceneToLoad);
        }
        if (sceneNumber == 2)
        {
            sceneToLoad = "Classroom_Offline";
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
