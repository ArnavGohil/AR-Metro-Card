using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class OnClick : MonoBehaviour
{
    public Animator cubeAni;

    String btnName;
    
    // Start is called before the first frame update
    void Start()
    {
        cubeAni.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //To check Button is pressed and pressed only once. as Update function runs on every frame..
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began  )
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray , out hit))
            {
                btnName = hit.transform.name;
                //Api Call with btnName;
                switch (btnName)
                {
                    case "Green" : cubeAni.Play("anim");
                        break;
                    
                    case "Violet" : cubeAni.Play("none");
                        break;
                }
            }
        }
    }
}
