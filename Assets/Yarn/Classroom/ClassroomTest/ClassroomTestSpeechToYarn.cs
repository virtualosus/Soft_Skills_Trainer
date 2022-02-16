using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ClassroomTestSpeechToYarn : MonoBehaviour
{
    public InMemoryVariableStorage yarnInMemoryVariableStorage;

    //public InMemoryVariableStorage variableStorage;

    public VOLineController BoyVOLineController, GirlVOLineController;

    public float boyVoiceLine, girlVoiceLine;


    [YarnCommand("boy_talk")]
    public void BoyToTalk()
    {
        Debug.LogError("Boy talking...");
        yarnInMemoryVariableStorage.TryGetValue("$boyVoiceLine", out boyVoiceLine);
        Debug.LogError(boyVoiceLine);
        BoyVOLineController.VoiceLineToPlay = boyVoiceLine;
        BoyVOLineController.UpdateLineAndPlay();

    }

    [YarnCommand("girl_talk")]
    public void GirlToTalk()
    {
        Debug.LogError("Girl talking...");
        yarnInMemoryVariableStorage.TryGetValue("$girlVoiceLine", out girlVoiceLine);
        Debug.LogError(girlVoiceLine);
        GirlVOLineController.VoiceLineToPlay = girlVoiceLine;
        GirlVOLineController.UpdateLineAndPlay();

    }




}
