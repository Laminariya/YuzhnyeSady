using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [HideInInspector] public StartPanel startPanel;
    [HideInInspector] public GenplanPanel genplanPanel;
    [HideInInspector] public GalereyaPanel galereyaPanel;
    [HideInInspector] public VideoPanel videoPanel;
    [HideInInspector] public SerializeJson serializeJson;
    [HideInInspector] public CreateMyData createMyData;
    [HideInInspector] public CreateImagePNG createImagePng;
    
    
    [HideInInspector] public MyData MyData;
    
    public GameObject LoadPanel;
    public TMP_Text InfoStartPanel;
    
    
    void Start()
    {
        LoadPanel.SetActive(true);
        var loadJson = LoadJson();
    }
    
    private async Task LoadJson()
    {
        serializeJson = FindObjectOfType<SerializeJson>();
        await serializeJson.Init(this);
        StartCoroutine(StartGame());
    }
    
    IEnumerator StartGame()
    {
        
        // foreach (var floor in serializeJson.JsonData.floors)
        // {
        //     Debug.Log(floor.plan);
        // }

        startPanel = FindObjectOfType<StartPanel>(true);
        genplanPanel = FindObjectOfType<GenplanPanel>(true);
        galereyaPanel = FindObjectOfType<GalereyaPanel>(true);
        videoPanel = FindObjectOfType<VideoPanel>(true);
        serializeJson = FindObjectOfType<SerializeJson>(true);
        createMyData = FindObjectOfType<CreateMyData>(true);
        createImagePng = FindObjectOfType<CreateImagePNG>(true);
        
        startPanel.Init(this);
        genplanPanel.Init(this);
        galereyaPanel.Init(this);
        videoPanel.Init(this);
        createMyData.Init(this);

        //yield return StartCoroutine(createMyData.Init());
        yield return StartCoroutine(createImagePng.Init(this));
        //yield return StartCoroutine(createMyData.CreateSprites());
        
        
        LoadPanel.SetActive(false);
        MessageOnDemo();
        yield return null;

        // foreach (var builder in MyData.Builders)
        // {
        //     foreach (var mySection in builder.Sections)
        //     {
        //         Debug.Log(builder.Number + " " + mySection.Number + " " + mySection.Flats.Count);
        //     }
        // }
        
    }
    
    public string GetSplitPrice(int price)
    {
        string result = price.ToString();
        int count = result.Length;

        if (count > 3)
            result = result.Insert(result.Length - 3, " ");
        if(count > 6)
            result = result.Insert(result.Length - 7, " ");
        if(count > 9)
            result = result.Insert(result.Length - 11, " ");
        return result;
    }

    public string GetShortPrice(int price)
    {
        string p = (price / 1000000f).ToString();
        if(p.Length>=4)
            p = p.Substring(0, 4);
        return p;
    }

    public void MessageOnHouse(int house, int porch, bool isOn = true)
    {
        //Debug.Log(house+" " + porch);
        //HH02PP0300000000
        string str = house.ToString("X");
        if(str.Length==1) str = "0" + str;
        str += "02";
        string por = porch.ToString("X");
        if(por.Length==1) por = "0" + por;
        str += por;
        if (isOn) str += "0300000000";
        else str += "0000000000";
        Debug.Log("Mess House");
        //sendComPort.AddMessage(str);
    }

    public void MessageOnFlat(int house, int porch, int flat, bool isOn = true)
    {
        //HH01FFFF03000000
        string str = house.ToString("X");
        if(str.Length==1) str = "0" + str;
        str += "01";
        string f = flat.ToString("X");
        if (f.Length == 1) f = "000" + f;
        else if (f.Length == 2) f = "00" + f;
        else if (f.Length == 3) f = "0" + f;
        if (isOn) f += "03000000";
        else f += "00000000";
        str += f;
        Debug.Log("Mess Flat");
        //sendComPort.AddMessage(str);
    }

    public void MessageOnFloor(int house, int porch, int floor)
    {
        //HH03SSXX03000000
        string str = house.ToString("X");
        if(str.Length==1) str = "0" + str;
        str += "03";
        string f = floor.ToString("X");
        if (f.Length == 1) f = "0" + f;
        str += f;
        string s = porch.ToString("X");
        if (s.Length == 1) s = "0" + s;
        str += s + "03000000";
        Debug.Log("Mess Floor");
        //sendComPort.AddMessage(str);
    }

    public void MessageOffAllLight()
    {
        Debug.Log("Mess OffAll");
        //sendComPort.AddMessage("007F060100000000"); //Погасить всё!!!
    }

    public void MessageOnDemo()
    {
        Debug.Log("Mess Demo");
        //sendComPort.AddMessage("0064010000000000"); //Включить демо!
    }
    
}
