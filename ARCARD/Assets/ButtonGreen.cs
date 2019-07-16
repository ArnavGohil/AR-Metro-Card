using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ButtonGreen : MonoBehaviour , IVirtualButtonEventHandler
{

    public GameObject green;

    public Animator cubeAni;
    
    // Start is called before the first frame update
    void Start()
    {
     green = GameObject.Find("Green_Line"); 
     green.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
     cubeAni.GetComponent<Animator>();
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        cubeAni.Play("anim");
        Debug.Log("Button is Clicked");
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        cubeAni.Play("none");
        Debug.Log("Button is Released");


    }
}
