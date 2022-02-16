using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.Events;


public class ClassroomTestSpeechToYarn : MonoBehaviour
{
    public InMemoryVariableStorage yarnInMemoryVariableStorage;

    public VOLineController BoyVOLineController, GirlVOLineController;

    public float boyVoiceLine, girlVoiceLine;

    public UnityEvent NPCFinishedTalking;



    [YarnCommand("boy_talk")]
    public void BoyToTalk()
    {
        //Debug.LogError("Boy talking...");
        yarnInMemoryVariableStorage.TryGetValue("$boyVoiceLine", out boyVoiceLine);
        //Debug.LogError(boyVoiceLine);
        BoyVOLineController.voiceLineToPlay = boyVoiceLine;
        BoyVOLineController.UpdateLineAndPlay();

    }

    [YarnCommand("girl_talk")]
    public void GirlToTalk()
    {
        //Debug.LogError("Girl talking...");
        yarnInMemoryVariableStorage.TryGetValue("$girlVoiceLine", out girlVoiceLine);
        //Debug.LogError(girlVoiceLine);
        GirlVOLineController.voiceLineToPlay = girlVoiceLine;
        GirlVOLineController.UpdateLineAndPlay();

    }


    [YarnCommand("boy_NPC_finish_talking_wait")]
    public IEnumerator BoyNPCFinishTalking()
    {
        Debug.Log("Coroutine started...");
        yield return new WaitForSeconds((BoyVOLineController.voiceLineClipLength) + 0.5f);
        Debug.Log("Coroutine finished!!");
    }

    [YarnCommand("girl_NPC_finish_talking_wait")]
    public IEnumerator GirlNPCFinishTalking()
    {
        Debug.Log("Coroutine started...");
        yield return new WaitForSeconds((GirlVOLineController.voiceLineClipLength) + 0.5f);
        Debug.Log("Coroutine finished!!");
    }




}
