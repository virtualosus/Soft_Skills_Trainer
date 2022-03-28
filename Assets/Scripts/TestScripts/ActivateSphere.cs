//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Oculus.Voice.Demo.UIShapesDemo;

//public class ActivateSphere : MonoBehaviour
//{
//    public InteractionHandler interactionHandler;

//    private bool speechEnabled = false;

//    //// Start is called before the first frame update
//    //void Start()
//    //{
        
//    //}

//    //// Update is called once per frame
//    //void Update()
//    //{
        
//    //}

//    public void OnTriggerEnter(Collider other)
//    {
//        if(other.tag == "Hand" && !speechEnabled)
//        {
//            Debug.Log("Sphere Triggered.");
//            interactionHandler.ToggleActivation();
//            speechEnabled = true;
//        }
//    }

//    public void OnTriggerExit(Collider other)
//    {
//        speechEnabled = false;        
//    }
//}
