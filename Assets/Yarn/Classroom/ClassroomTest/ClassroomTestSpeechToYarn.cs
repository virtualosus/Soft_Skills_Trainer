using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ClassroomTestSpeechToYarn : MonoBehaviour
{
    public InMemoryVariableStorage yarnInMemoryVariableStorage;

    //public InMemoryVariableStorage variableStorage;

    public VOLineController BoyVOLineController, GirlVOLineController;

    public float BoyVoiceLine, GirlVoiceLine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    [YarnCommand("boy_talk")]
    public void BoyToTalk()
    {
        Debug.LogError("Boy talking...");
        yarnInMemoryVariableStorage.TryGetValue("$boyVoiceLine", out BoyVoiceLine);
        Debug.LogError(BoyVoiceLine);
        //BoyVOLineController.VoiceLineToPlay = BoyVoiceLine;
        //BoyVOLineController.UpdateLineAndPlay();
        //Debug.LogError(BoyVoiceLine);

    }

    [YarnCommand("girl_talk")]
    public void GirlToTalk()
    {
        Debug.LogError("Girl talking...");
        yarnInMemoryVariableStorage.TryGetValue("$girlVoiceLine", out GirlVoiceLine);
        Debug.LogError(GirlVoiceLine);
        //GirlVOLineController.VoiceLineToPlay = GirlVoiceLine;
        //GirlVOLineController.UpdateLineAndPlay();
        //Debug.LogError(GirlVoiceLine);

    }

    //[YarnCommand("boy_talk")]
    //public void BoyToTalk()
    //{
    //    Debug.LogError("Boy talking...");
    //    variableStorage = GameObject.FindObjectOfType<InMemoryVariableStorage>();
    //    int testVariable;
    //    variableStorage.TryGetValue("$testVariable", out testVariable);
    //    BoyVOLineController.VoiceLineToPlay = BoyVoiceLine;
    //    BoyVOLineController.UpdateLineAndPlay();
    //    Debug.LogError(BoyVoiceLine);
    //}


}
