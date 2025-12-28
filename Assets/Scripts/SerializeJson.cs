using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

public class SerializeJson : MonoBehaviour
{
    
    [HideInInspector] public JsonData JsonData;
    private string _nameJson = "note1.txt";
    private HttpClient Client = new HttpClient();
    private string _json;
    private GameManager _manager;
    
    //ссылка на json
    private string _url = "https://a101.ru/api/floors_masterplan_redesign/?project=yujnie-sadi";
    
    public async Task Init(GameManager manager)
    {
        _manager = manager;
        await LoadJSON();
    }
    
    public void  LoadJsonFile()
    {
        _json = Resources.Load<TextAsset>("note1").text;

        if (_json != "")
        {
            //GetComponent<GameManager>().Json = JsonUtility.FromJson<JsonData>(_json);
        }
        Debug.Log("Json загружен");
    }

    private async Task LoadJSON()
    {
        var uri = new Uri(_url);
        var result = await Client.GetAsync(uri);
        string str = await result.Content.ReadAsStringAsync();
        
        //Debug.Log("JSON " + str);
        str = "{\"floors\":" + str + "}";
        JsonData = JsonUtility.FromJson<JsonData>(str);

    }

}

[Serializable]
public class JsonData
{
    public List<Floor> floors = new List<Floor>();
}

[Serializable]
public class Floor
{
    public int id;
    public bool is_active;
    public int number;
    public string plan;
    public string plan_image;
    public string plan_image_450;
    public string hover;
    public int gp_roza;
    public int image_width;
    public int image_height;
    public int count;
    public int studio__min_price;
    public int room_1__min_price;
    public int room_2__min_price;
    public int room_3__min_price;
    public int room_4__min_price;
    public int room_4plus__min_price;
    public int studio__count;
    public int room_1__count;
    public int room_2__count;
    public int room_3__count;
    public int room_4__count;
    public int room_4plus__count;
    public List<Flat> flat_set = new List<Flat>();
    public string building_class;
    public int total_floors;
    public string gp_pin_pos_x;
    public string gp_pin_pos_y;
    public string discount;
    public bool has_discount;
    public int architectural_layout_id;
}

[Serializable]
public class Flat
{
    public int id;
    public string article;
    public int room;
    public float area;
    public int price;
    public int actual_price;
    public int ppm;
    public int actual_ppm;
    public string small_layout;
    public string big_layout;
    public string hover;
    public bool is_two_floor_flat;
    public int number;
    public bool design;
    public bool whitebox;
    public int furnishing;
    public string realty_class;
    public bool balcony;
    public bool master_bedroom;
    public bool two_bathrooms;
    public bool two_windows_room;
    public bool bathroom_window;
    public bool isolated_kitchen;
    public bool storage_dressing_room;
    public bool terrace;
    public bool hightflat;
    public bool increased_ceiling_height;
    public bool studio;
    public bool euro;
    public List<string> room_type = new List<string>();
    public bool view_apartment;
    public int smart_flat;
    public int payment;
    public bool status;
    public int discount;
    public bool has_discount;
}