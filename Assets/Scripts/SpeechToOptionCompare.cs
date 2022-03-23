using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using System.Linq;
using Oculus.Voice.Demo.UIShapesDemo;



public class SpeechToOptionCompare : MonoBehaviour
{
    public YarnProject myYarnProject;

    public InteractionHandler InteractionHandler;

    public LearningResponse LearningResponse;

    public OptionController OptionController;

    public string currentLine;
    
    public bool requestRetry;

    public int ratingOne, ratingTwo, ratingThree;

    public int optionCounter = 0;

    private List<string> allLinesIDList = new List<string>();

    private List<string> allOptionsIDList = new List<string>();
    private List<string> optionOneIDList = new List<string>();
    private List<string> optionTwoIDList = new List<string>();
    private List<string> optionThreeIDList = new List<string>();


    private List<string> allOptionsText = new List<string>();
    public List<string> optionOneText = new List<string>();
    public List<string> optionTwoText = new List<string>();
    public List<string> optionThreeText = new List<string>();




    public void Awake()
    {
        GetLineIDs();
        SortLineIDs();
        GetLinesFromIDs();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
            if (tempString.Contains("Option"))
            {
                allOptionsIDList.Add(tempString);
            }
            if (tempString.Contains("OptionOne"))
            {
                optionOneIDList.Add(tempString);
            }
            if (tempString.Contains("OptionTwo"))
            {
                optionTwoIDList.Add(tempString);
            }
            if (tempString.Contains("OptionThree"))
            {
                optionThreeIDList.Add(tempString);
            }
        }
    }

    public void GetLinesFromIDs()                                           //pulls all of the text for each line ID out of YARN project and lists
    {
        foreach (string tempString in allOptionsIDList)
        {
            var text = myYarnProject.GetLocalization("En").GetLocalizedString(tempString);
            allOptionsText.Add(text);
        }

        foreach (string tempString in optionOneIDList)
        {
            var text = myYarnProject.GetLocalization("En").GetLocalizedString(tempString);
            optionOneText.Add(text);
        }
        foreach (string tempString in optionTwoIDList)
        {
            var text = myYarnProject.GetLocalization("En").GetLocalizedString(tempString);
            optionTwoText.Add(text);
        }
        foreach (string tempString in optionThreeIDList)
        {
            var text = myYarnProject.GetLocalization("En").GetLocalizedString(tempString);
            optionThreeText.Add(text);
        }
    }

    public void LineComparison()
    {
        string CurrentOptionOne = optionOneText[optionCounter];
        string CurrentOptionTwo = optionTwoText[optionCounter];
        string CurrentOptionThree = optionThreeText[optionCounter];

        ratingOne = GetDamerauLevenshteinDistance(CurrentOptionOne, currentLine);
        ratingTwo = GetDamerauLevenshteinDistance(CurrentOptionTwo, currentLine);
        ratingThree = GetDamerauLevenshteinDistance(CurrentOptionThree, currentLine);

        Debug.LogError("Option one rating: " + ratingOne + " Option two rating: " + ratingTwo + " Option three rating: " + ratingThree);

        Debug.LogError(Mathf.Min(ratingOne, ratingTwo, ratingThree));

        if(Mathf.Min(ratingOne, ratingTwo, ratingThree) == ratingOne)
        {
            if(ratingOne < 15)
            {
                OptionController.OptionOneSelect();
                LearningResponse.optionSelected = 1;
                optionCounter++;
            }
            else
            {
                requestRetry = true;
                StartCoroutine(InteractionHandler.NothingHeardRetry());
            }

        }
        if (Mathf.Min(ratingOne, ratingTwo, ratingThree) == ratingTwo)
        {
            if (ratingTwo < 15)
            {
                OptionController.OptionTwoSelect();
                LearningResponse.optionSelected = 2;
                optionCounter++;
            }
            else
            {
                requestRetry = true;
                StartCoroutine(InteractionHandler.NothingHeardRetry());
            }
        }
        if (Mathf.Min(ratingOne, ratingTwo, ratingThree) == ratingThree)
        {
            if (ratingThree < 15)
            {
                OptionController.OptionThreeSelect();
                LearningResponse.optionSelected = 3;
                optionCounter++;
            }
            else
            {
                requestRetry = true;
                StartCoroutine(InteractionHandler.NothingHeardRetry());
            }
        }
        
    }


    public static int GetDamerauLevenshteinDistance(string s, string t)
    {
        //if (string.IsNullOrEmpty(s))
        //{
        //    throw new ArgumentNullException(s, "String Cannot Be Null Or Empty");
        //}

        //if (string.IsNullOrEmpty(t))
        //{
        //    throw new ArgumentNullException(t, "String Cannot Be Null Or Empty");
        //}

        int n = s.Length; // length of s
        int m = t.Length; // length of t

        if (n == 0)
        {
            return m;
        }

        if (m == 0)
        {
            return n;
        }

        int[] p = new int[n + 1]; //'previous' cost array, horizontally
        int[] d = new int[n + 1]; // cost array, horizontally

        // indexes into strings s and t
        int i; // iterates through s
        int j; // iterates through t

        for (i = 0; i <= n; i++)
        {
            p[i] = i;
        }

        for (j = 1; j <= m; j++)
        {
            char tJ = t[j - 1]; // jth character of t
            d[0] = j;

            for (i = 1; i <= n; i++)
            {
                int cost = s[i - 1] == tJ ? 0 : 1; // cost
                                                   // minimum of cell to the left+1, to the top+1, diagonally left and up +cost                
                d[i] = Mathf.Min(Mathf.Min(d[i - 1] + 1, p[i] + 1), p[i - 1] + cost);
            }

            // copy current distance counts to 'previous row' distance counts
            int[] dPlaceholder = p; //placeholder to assist in swapping p and d
            p = d;
            d = dPlaceholder;
            
        }

        // our last action in the above loop was to switch d and p, so p now 
        // actually has the most recent cost counts
        return p[n];
        
    }
}
