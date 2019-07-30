using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
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
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
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
                    string json1 = GET(btnName);
                    MyClass fld = MyClass.CreateFromJson(json1.Substring(3, json1.Length - 5));
                    TextMesh sta1 = (TextMesh) station.GetComponent(typeof(TextMesh));
                    TextMesh sta2 = (TextMesh) first.GetComponent(typeof(TextMesh));
                    TextMesh sta3 = (TextMesh) last.GetComponent(typeof(TextMesh));
                    TextMesh sta4 = (TextMesh) parking.GetComponent(typeof(TextMesh));
                    TextMesh sta5 = (TextMesh) tourist.GetComponent(typeof(TextMesh));
                    sta1.text = fld.Name;
                    sta2.text = fld.FirstTrain;
                    sta3.text = fld.LastTrain;
                    sta4.text = fld.Parking;
                    sta5.text = fld.ImportantSpots;
                }
            }
        }
    }


    [Serializable]
    public class MyClass
    {
        public string _id;

        public string Name;

        public string FirstTrain;

        public string LastTrain;

        public string Parking;

        public string ImportantSpots;

        public static MyClass CreateFromJson(string json)
        {
            return JsonUtility.FromJson<MyClass>(json);
        }
    }


    string GET(string name)
    {
        String url = "https://stations-9f55.restdb.io/rest/stations?q={\"Name\":\"" + name +
                     "\"}&apikey=730f7ba7d051e92ec574d65255bf8790d6df4";

        HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);

        try
        {
            WebResponse response = request.GetResponse();
            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                return reader.ReadToEnd();
            }
        }
        catch (WebException ex)
        {
            WebResponse errorResponse = ex.Response;
            using (Stream responseStream = errorResponse.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
                String errorText = reader.ReadToEnd();
                return errorText;
            }

            throw;
        }
    }
}