using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoseFlatOnParameterPanel : MonoBehaviour
{

    private GameManager _manager;
    
    public Color ActiveColor;
    public Color NotActiveColor;
    
    public DubleSlider DubleSliderArea;
    public DubleSlider DubleSliderPrice;
    public DubleSlider DubleSliderFloor;
    public Button b_Close;
    public Button b_Reset;
    public Button b_ShowFlat;
    public Button b_St;
    public Button b_1;
    public Button b_2;
    public Button b_3;
    public Button b_4;
    public TMP_Text MinArea;
    public TMP_Text MaxArea;
    public TMP_Text MinPrice;
    public TMP_Text MaxPrice;
    public TMP_Text MinFloor;
    public TMP_Text MaxFloor;

    public Transform ParentPrefabFlat;
    public GameObject PrefabFlat;
    
    public Slider Slider;
    public Scrollbar Scrollbar;
    public ScrollRect ScrollRect;

    private List<FlatPrefab> _flatPrefabs = new List<FlatPrefab>();

    private int _St;
    private int _1;
    private int _2;
    private int _3;
    private int _4;

    private float _minArea;
    private float _maxArea;
    private float _minPrice;
    private float _maxPrice;
    private int _minFloor;
    private int _maxFloor;

    public void Init(GameManager manager)
    {
        _manager = manager;
        b_St.onClick.AddListener(OnSt);
        b_1.onClick.AddListener(On1);
        b_2.onClick.AddListener(On2);
        b_3.onClick.AddListener(On3);
        b_4.onClick.AddListener(On4);
        //b_Close.onClick.AddListener(OnClose);
        b_Reset.onClick.AddListener(OnReset);
        b_ShowFlat.onClick.AddListener(OnShowFlat);
        DubleSliderArea.Action += OnDoubleSliderArea;
        DubleSliderPrice.Action += OnDoubleSliderPrice;
        DubleSliderFloor.Action += OnDoubleSliderFloor;
        Hide();
    }

    public void ShowOnParameters(MyBuilder myBuilding, int rooms)
    {
        Show();
       
        if(rooms==0) OnSt();
        if(rooms==1) On1();
        if(rooms==2) On2();
        if(rooms==3) On3();
        if(rooms==4) On4();

        if (rooms == -1)
        {
            OnSt();
            On1();
            On2();
            On3();
            On4();
        }

        OnShowFlat();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        OnReset();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnClose()
    {
        gameObject.SetActive(false);
        _manager.MessageOffAllLight();
        _manager.MessageOnDemo();
    }

    public void OnReset()
    {
        _St = 0;
        _1 = 1;
        _2 = 2;
        _3 = 3;
        _4 = 4;
      
        b_St.image.color = ActiveColor;
        b_1.image.color = ActiveColor;
        b_2.image.color = ActiveColor;
        b_3.image.color = ActiveColor;
        b_4.image.color = ActiveColor;
        OnSt();
        On1();
        On2();
        On3();
        On4();
        
        DubleSliderArea.LeftSlider.value = 0f;
        DubleSliderArea.RightSlider.value = 1f;
        DubleSliderFloor.LeftSlider.value = 0f;
        DubleSliderFloor.RightSlider.value = 1f;
        DubleSliderPrice.LeftSlider.value = 0f;
        DubleSliderPrice.RightSlider.value = 1f;
        ReloadSliders();
        
        for (int i = 0; i < _flatPrefabs.Count; i++)
        {
            Destroy(_flatPrefabs[i].gameObject);
        }

        _flatPrefabs.Clear();
    }

    private void OnShowFlat()
    {
        _manager.MessageOffAllLight();

        for (int i = 0; i < _flatPrefabs.Count; i++)
        {
            Destroy(_flatPrefabs[i].gameObject);
        }

        _flatPrefabs.Clear();

        foreach (var building in _manager.MyData.Builders)
        {
            foreach (var mySection in building.Sections)
            {
                foreach (var myFlat in mySection.Flats)
                {
                    Debug.Log(myFlat.Price +"  " + myFlat.Area + " " + myFlat.Floor  + " " + myFlat.Rooms);
                
                    if ((myFlat.Rooms == _St || myFlat.Rooms == _1 || myFlat.Rooms == _2
                         || myFlat.Rooms == _3 || myFlat.Rooms == _4)
                        && myFlat.Area <= _maxArea && myFlat.Area >= _minArea &&
                        myFlat.Price <= _maxPrice && myFlat.Price >= _minPrice &&
                        myFlat.Floor <= _maxFloor && myFlat.Floor >= _minFloor)
                    {
                        FlatPrefab flat = Instantiate(PrefabFlat)
                            .GetComponent<FlatPrefab>();
                        flat.Init(myFlat, _manager);
                        _flatPrefabs.Add(flat);
                    }
                }
            }
            
        }

        _flatPrefabs = _flatPrefabs.OrderBy(p => p.PriceValue).ToList();
        foreach (var flatPrefab in _flatPrefabs)
        {
            flatPrefab.transform.parent = ParentPrefabFlat;
        }
    }

    private void OnSt()
    {
        CheckResetButtons();
        if (_St==0)
        {
            b_St.image.color = NotActiveColor;
            _St = -1;
        }
        else
        {
            b_St.image.color = ActiveColor;
            _St = 0;
        }
        CheckAllOffButtons();
        ReloadSliders();
    }

    private void On1()
    {
        CheckResetButtons();
        if (_1==1)
        {
            b_1.image.color = NotActiveColor;
            _1 = -1;
        }
        else
        {
            b_1.image.color = ActiveColor;
            _1 = 1;
        }
        CheckAllOffButtons();
        ReloadSliders();
    }
    
    private void On2()
    {
        CheckResetButtons();
        if (_2==2)
        {
            b_2.image.color = NotActiveColor;
            _2 = -1;
        }
        else
        {
            b_2.image.color = ActiveColor;
            _2 = 2;
        }
        CheckAllOffButtons();
        ReloadSliders();
    }
    
    private void On3()
    {
        CheckResetButtons();
        if (_3==3)
        {
            b_3.image.color = NotActiveColor;
            _3 = -1;
        }
        else
        {
            b_3.image.color = ActiveColor;
            _3 = 3;
        }
        CheckAllOffButtons();
        ReloadSliders();
    }
    
    private void On4()
    {
        CheckResetButtons();
        if (_4==4)
        {
            b_4.image.color = NotActiveColor;
            _4 = -1;
        }
        else
        {
            b_4.image.color = ActiveColor;
            _4 = 4;
        }
        CheckAllOffButtons();
        ReloadSliders();
    }

    private void OnDoubleSliderArea()
    {
        float max = 0;
        float min = int.MaxValue;
        foreach (var building in _manager.MyData.Builders)
        {
            foreach (var mySection in building.Sections)
            {
                foreach (var myFlat in mySection.Flats)
                {
                    if ((myFlat.Rooms == _St || myFlat.Rooms == _1 || myFlat.Rooms == _2
                         || myFlat.Rooms == _3 || myFlat.Rooms == _4)
                        && myFlat.Area > max
                    )
                    {
                        max = myFlat.Area;
                    }
                }
            }
            

            foreach (var mySection in building.Sections)
            {
                foreach (var myFlat in mySection.Flats)
                {
                    if ((myFlat.Rooms == _St || myFlat.Rooms == _1 || myFlat.Rooms == _2
                         || myFlat.Rooms == _3 || myFlat.Rooms == _4)
                        && myFlat.Area < min
                    )
                    {
                        min = myFlat.Area;
                    }
                }
            }
            
        }
        
        if (min > 1000000000) min = 0;

        float _delta = max - min;
        _minArea = min + DubleSliderArea.LeftSlider.value * _delta;
        _maxArea = max - (1 - DubleSliderArea.RightSlider.value) * _delta;
        string min1Str = Math.Round(min, 1).ToString();
        string max1Str = Math.Round(max, 1).ToString();
        string min2Str = Math.Round(_minArea, 1).ToString();
        string max2Str = Math.Round(_maxArea, 1).ToString();
        MinArea.text = min2Str;
        MaxArea.text = max2Str;

        if (min1Str != min2Str || max1Str != max2Str)
        {
            //FilterPointArea.Show(Math.Round(_minArea, 1) + "-" + Math.Round(_maxArea, 1) + "м2");
        }
        else
        {
            //FilterPointArea.Hide();
        }
        //ReloadCountFlat();
        
        //AreaRect.offsetMax+=Vector2.one;
        Canvas.ForceUpdateCanvases();
        //AreaRect.offsetMax-=Vector2.one;
    }

    private void OnDoubleSliderPrice()
    {
        float max = 0;
        float min = int.MaxValue;
        foreach (var building in _manager.MyData.Builders)
        {
            foreach (var mySection in building.Sections)
            {
                foreach (var myFlat in mySection.Flats)
                {
                    if ((myFlat.Rooms == _St || myFlat.Rooms == _1 || myFlat.Rooms == _2
                         || myFlat.Rooms == _3 || myFlat.Rooms == _4)
                        && myFlat.Price > max
                    )
                    {
                        max = myFlat.Price;
                    }
                }
            }
            

            
            foreach (var mySection in building.Sections)
            {
                foreach (var myFlat in mySection.Flats)
                {
                    if ((myFlat.Rooms == _St || myFlat.Rooms == _1 || myFlat.Rooms == _2
                         || myFlat.Rooms == _3 || myFlat.Rooms == _4)
                        && myFlat.Price < min
                    )
                    {
                        min = myFlat.Price;
                    }
                }
            }
           
        }
        
        if (min > 1000000000) min = 0;

        float _delta = max - min;
        _minPrice = min + DubleSliderPrice.LeftSlider.value * _delta;
        _maxPrice = max - (1 - DubleSliderPrice.RightSlider.value) * _delta;
        MinPrice.text = _manager.GetShortPrice((int)_minPrice); //Math.Round(_minPrice, 1).ToString(); //_manager.GetShortPrice()
        MaxPrice.text = _manager.GetShortPrice((int)_maxPrice); //Math.Round(_maxPrice, 1).ToString(); //_manager.GetShortPrice()

        Canvas.ForceUpdateCanvases();
    }
    
    private void OnDoubleSliderFloor()
    {
        float max = 0;
        float min = int.MaxValue;
        foreach (var building in _manager.MyData.Builders)
        {
            foreach (var mySection in building.Sections)
            {
                foreach (var myFlat in mySection.Flats)
                {
                    if ((myFlat.Rooms == _St || myFlat.Rooms == _1 || myFlat.Rooms == _2
                         || myFlat.Rooms == _3 || myFlat.Rooms == _4)
                        && myFlat.Floor > max
                    )
                    {
                        max = myFlat.Floor;
                    }
                }
            }
            

            
            foreach (var mySection in building.Sections)
            {
                foreach (var myFlat in mySection.Flats)
                {
                    if ((myFlat.Rooms == _St || myFlat.Rooms == _1 || myFlat.Rooms == _2
                         || myFlat.Rooms == _3 || myFlat.Rooms == _4)
                        && myFlat.Floor < min
                    )
                    {
                        min = myFlat.Floor;
                    }
                }
            }
           
        }
        
        if (min > 1000000000) min = 0;

        float _delta = max - min;
        _minFloor = (int)(min + DubleSliderFloor.LeftSlider.value * _delta);
        _maxFloor = (int)(max - (1 - DubleSliderFloor.RightSlider.value) * _delta);
        MinFloor.text = _minFloor.ToString();
        MaxFloor.text = _maxFloor.ToString();
        
        Canvas.ForceUpdateCanvases();
    }
    
    public void ReloadSliders()
    {
        DubleSliderArea.Init();
        DubleSliderFloor.Init();
        DubleSliderPrice.Init();
        OnDoubleSliderArea();
        OnDoubleSliderFloor();
        OnDoubleSliderPrice();
    }
    
    private void CheckAllOffButtons()
    {
        if (_St == -1 && _1 == -1 && _2 == -1 && _3 == -1 && _4 == -1)
        {
            _St = 9;
            _1 = 1;
            _2 = 2;
            _3 = 3;
            _4 = 4;
        }
    }
    
    private void CheckResetButtons()
    {
        if (_St != -1 && _1 != -1 && _2 != -1 && _3 != -1 && _4 != -1)
        {
            if (b_St.image.color == ActiveColor && b_1.image.color == ActiveColor && b_2.image.color == ActiveColor &&
                b_3.image.color == ActiveColor && b_4.image.color == ActiveColor ) return;
            _St = -1;
            _1 = -1;
            _2 = -1;
            _3 = -1;
            _4 = -1;
        }
    }

    public void SendMessageOnComPort()
    {
        _manager.MessageOffAllLight();
        foreach (var prefab in _flatPrefabs)
        {
            //prefab.OnSendMessageOnComPort();
        }
    }

    public void OnSliderArea(float value)
    {
        Scrollbar.value = 1f-Slider.value;
    }

    private void Update()
    {
        if(!gameObject.activeSelf) return;
        Slider.value = 1f-Scrollbar.value;
    }
}
