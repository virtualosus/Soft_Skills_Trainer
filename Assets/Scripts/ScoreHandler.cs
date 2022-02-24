using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.WitAi;
using Facebook.WitAi.Lib;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    //public Text debugText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    ///// <summary>
    ///// Directly processes a command result getting the slots with WitResult utilities
    ///// </summary>
    ///// <param name="commandResult">Result data from Wit.ai activation to be processed</param>
    //public void UpdateScore(WitResponseNode commandResult)
    //{
    //    string score = commandResult.GetFirstEntityValue("comment:comment");
    //    Debug.LogError("Wit1 command heard.");
    //}

    public void QuestionAsked()
    {
        Debug.Log("Question asked");
    }


    public void NPCReferenced()
    {
        Debug.Log("NPC referred to.");
    }



}
