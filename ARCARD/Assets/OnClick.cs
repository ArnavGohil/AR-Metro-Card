using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class OnClick : MonoBehaviour
{
    public Animator cubeAni;

    public GameObject tile;

    public GameObject station;
    public GameObject first;
    public GameObject last;
    public GameObject parking;
    public GameObject tourist;

    String btnName;
    
    // Start is called before the first frame update
    void Start()
    {
        tile = GameObject.Find("InfoTile");
        station = GameObject.Find("Station");
        first = GameObject.Find("First");
        last = GameObject.Find("Last");
        parking = GameObject.Find("Parking");
        tourist = GameObject.Find("Tourist");
        cubeAni.GetComponent<Animator>();
        tile.SetActive(false);
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
                if (btnName.Equals("Green"))
                {
                    cubeAni.Play("anim");
                }
                else if (btnName.Equals("Violet"))
                {
                    cubeAni.Play("none");
                }
                else
                {
                    tile.SetActive(true);
                }
            }
        }
    }
}
