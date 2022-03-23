using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using System.Linq;

public class LearningResponse : MonoBehaviour
{

    public CharacterYarnLineHandler Responder;

    public YarnProject myYarnProject;

    public int responseCounter = 0;

    public int optionSelected;

    private List<string> allLinesIDList = new List<string>();

    private List<string> allResponsesIDList = new List<string>();
    private List<string> responseOneIDList = new List<string>();
    private List<string> responseTwoIDList = new List<string>();
    private List<string> responseThreeIDList = new List<string>();


    private List<string> allResponsesText = new List<string>();
    public List<string> responseOneText = new List<string>();
    public List<string> responseTwoText = new List<string>();
    public List<string> responseThreeText = new List<string>();




    public void Awake()
    {
        GetLineIDs();
        SortLineIDs();
        GetLinesFromIDs();
    }

    public void GetLineIDs()                                                //finds all of the line IDs in the referenced YARN project and lists
    {
        var lineID = myYarnProject.GetLocalization("En").GetLineIDs();
        allLinesIDList = lineID.ToList();
    }

    public void SortLineIDs()                                               //sorts all of the IDs based on string segments found in the YARN line ID and lists
    {
        foreach (string tempString in allLinesIDList)
        {
            if (tempString.Contains("Response"))
            {
                allResponsesIDList.Add(tempString);
            }
            if (tempString.Contains("ResponseOne"))
            {
                responseOneIDList.Add(tempString);
            }
            if (tempString.Contains("ResponseTwo"))
            {
                responseTwoIDList.Add(tempString);
            }
            if (tempString.Contains("ResponseThree"))
            {
                responseThreeIDList.Add(tempString);
            }
        }
    }

    public void GetLinesFromIDs()                                           //pulls all of the text for each line ID out of YARN project and lists
    {
        foreach (string tempString in allResponsesIDList)
        {
            var text = myYarnProject.GetLocalization("En").GetLocalizedString(tempString);
            allResponsesText.Add(text);
        }

        foreach (string tempString in responseOneIDList)
        {
            var text = myYarnProject.GetLocalization("En").GetLocalizedString(tempString);
            responseOneText.Add(text);
        }
        foreach (string tempString in responseTwoIDList)
        {
            var text = myYarnProject.GetLocalization("En").GetLocalizedString(tempString);
            responseTwoText.Add(text);
        }
        foreach (string tempString in responseThreeIDList)
        {
            var text = myYarnProject.GetLocalization("En").GetLocalizedString(tempString);
            responseThreeText.Add(text);
        }
    }

    public void SetupLearningResponse()
    {
        if(optionSelected == 1)
        {
            Responder.learningResponseActivate = true;
            Responder.learningResponseLine = responseOneText[responseCounter];
            responseCounter++;
            optionSelected = 0;
        }
        if (optionSelected == 2)
        {
            Responder.learningResponseActivate = true;
            Responder.learningResponseLine = responseTwoText[responseCounter];
            responseCounter++;
            optionSelected = 0;
        }
        if (optionSelected == 3)
        {
            Responder.learningResponseActivate = true;
            Responder.learningResponseLine = responseThreeText[responseCounter];
            responseCounter++;
            optionSelected = 0;
        }
    }
}
