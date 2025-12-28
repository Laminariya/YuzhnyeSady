using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CreateMyData : MonoBehaviour
{

    private GameManager _manager;
    public MyData MyData;
    
    private List<string> _numberBuilder = new List<string>();
    
    public void Init(GameManager manager)
    {
        _manager = manager;
        CreateData();
    }

    private void CreateData()
    {
        MyData = new MyData();

        foreach (var floor in _manager.serializeJson.JsonData.floors)
        {
            foreach (var flat in floor.flat_set)
            {
                string[] split1 = flat.article.Split("-");
                if (!_numberBuilder.Contains(split1[1]))
                {
                    _numberBuilder.Add(split1[1]);
                }
            }
        }

        foreach (var numberBuilder in _numberBuilder)
        {
            MyBuilder myBuilder = new MyBuilder();
            string[] split = numberBuilder.Split(".");
            string number = split[0];
            if (split.Length == 3)
                number = split[0] + "." + split[1];

            bool isCreate = false;
            foreach (var builder in MyData.Builders)
            {
                if (builder.Number == number)
                {
                    isCreate = true;
                    myBuilder = builder;
                }
            }
            
            if(!isCreate)
                MyData.Builders.Add(myBuilder);
            
            myBuilder.Number = number;
            myBuilder.FullNumber = numberBuilder;
            MySection section = new MySection();
            section.Number = int.Parse(split[^1]);
            myBuilder.Sections.Add(section);
            
            foreach (var floor in _manager.serializeJson.JsonData.floors)
            {
                foreach (var flat in floor.flat_set)
                {
                    string[] split1 = flat.article.Split("-");
                    if (numberBuilder == split1[1])
                    {
                        MyFlat myFlat = new MyFlat(flat, floor, number);
                        section.Flats.Add(myFlat);
                    }
                }
            }
        }
        
        
        _manager.MyData = MyData;
    }

}

[Serializable]
public class MyData
{
    public List<MyBuilder> Builders = new List<MyBuilder>();
}

[Serializable]
public class MyBuilder
{
    public string Number;
    public string FullNumber;
    public List<MySection> Sections = new List<MySection>();
}

[Serializable]
public class MySection
{
    public int Number;
    public List<MyFlat> Flats = new List<MyFlat>();
}

[Serializable]
public class MyFlat
{
    public string Korpus;
    public int Number;
    public int Floor;
    public int Price;
    public float Area;
    public int Rooms;
    public int MaxFloor;
    public bool IsStudio;
    public string UrlFloor;
    public string UrlFlat;
    public string PathFloor;
    public string PathFlat;
    public Sprite FlatSprite;
    public Sprite FloorSprite;
    public int SendKorpus;

    public MyFlat(Flat flat, Floor floor, string korpus)
    {
        Korpus = korpus;
        string[] split = korpus.Split(".");
        SendKorpus = int.Parse(split[0]);
        string[] split1 = flat.article.Split("-");
        Number = int.Parse(split1[2]);
        Floor = floor.number;
        Price = flat.price;
        Area = flat.area;
        Rooms = flat.room;
        IsStudio = flat.studio;
        MaxFloor = floor.total_floors;
        UrlFloor = floor.plan;
        UrlFlat = flat.big_layout;
        PathFlat = Directory.GetCurrentDirectory() + "//Plans//PlanFlats//" + flat.id;
        PathFloor = Directory.GetCurrentDirectory() + "//Plans//PlanFloors//" + floor.id;
    }

}