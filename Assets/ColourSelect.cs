using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourSelect : MonoBehaviour
{
    public GameObject PositiveButton, NegativeButton;

    // Start is called before the first frame update
    void Start()
    {
        NeutralSelect();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PositiveSelect()
    {
        PositiveButton.SetActive(true);
        NegativeButton.SetActive(false);
    }

    public void NeutralSelect()
    {
        PositiveButton.SetActive(false);
        NegativeButton.SetActive(false);
    }

    public void NegativeSelect()
    {
        PositiveButton.SetActive(false);
        NegativeButton.SetActive(true);
    }
}
