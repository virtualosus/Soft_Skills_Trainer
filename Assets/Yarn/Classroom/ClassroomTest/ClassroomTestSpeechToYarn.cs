using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
//using UnityEngine.Events;


public class ClassroomTestSpeechToYarn : MonoBehaviour
{
    public InMemoryVariableStorage yarnInMemoryVariableStorage;

    public VOLineController BoyVOLineController, GirlVOLineController;

    public float boyVoiceLine, girlVoiceLine;

    //public UnityEvent NPCFinishedTalking;



    [YarnCommand("boy_talk")]                           //yarn command retrieving required voiceline number from YARN and setting clip and initiating Audio in VOLineController
    public void BoyToTalk()
    {
        //Debug.LogError("Boy talking...");
        yarnInMemoryVariableStorage.TryGetValue("$boyVoiceLine", out boyVoiceLine);
        //Debug.LogError(boyVoiceLine);
        BoyVOLineController.voiceLineToPlay = boyVoiceLine;
        BoyVOLineController.UpdateLineAndPlay();

    }

    [YarnCommand("girl_talk")]                          //yarn command retrieving required voiceline number from YARN and setting clip and initiating Audio in VOLineController
    public void GirlToTalk()
    {
        //Debug.LogError("Girl talking...");
        yarnInMemoryVariableStorage.TryGetValue("$girlVoiceLine", out girlVoiceLine);
        //Debug.LogError(girlVoiceLine);
        GirlVOLineController.voiceLineToPlay = girlVoiceLine;
        GirlVOLineController.UpdateLineAndPlay();

    }


    [YarnCommand("boy_NPC_finish_talking_wait")]        //custom YARN wait based on length of current voice line plus half a second
    public IEnumerator BoyNPCFinishTalking()
    {
        //Debug.Log("Coroutine started...");
        yield return new WaitForSeconds((BoyVOLineController.voiceLineClipLength) + 0.5f);
        //Debug.Log("Coroutine finished!!");
    }

    [YarnCommand("girl_NPC_finish_talking_wait")]       //custom YARN wait based on length of current voice line plus half a second
    public IEnumerator GirlNPCFinishTalking()
    {
        //Debug.Log("Coroutine started...");
        yield return new WaitForSeconds((GirlVOLineController.voiceLineClipLength) + 0.5f);
        //Debug.Log("Coroutine finished!!");
    }




}
