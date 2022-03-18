using System;
using System.Collections;
using System.Collections.Generic;
using InternetConnectionTest.Script;
using UnityEngine;
using UnityEngine.UI;

public class InternetTestController : MonoBehaviour
{
    public Text FeedbackText;
    //public Button ButtonTestInternet;

    public string pingResultString;

    private ConnectionTester _connectionTester;

    public void CheckConnection()
    {
        _connectionTester = ConnectionTester
           .GetInstance(gameObject)
           .ipToTest("8.8.8.8");

            ShowFeedback("Starting test");
            _connectionTester.TestInternet(hasInternet =>
            {
                ShowFeedback($"Has internet connection: {hasInternet}");
                FeedbackText.text += _connectionTester.pingResultString;
            }
            );
        
    }

    void ShowFeedback(string text)
    {
        FeedbackText.text += text;
    }

}
