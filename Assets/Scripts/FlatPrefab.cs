using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FlatPrefab : MonoBehaviour
{

    private MyFlat _myFlat;

    public Image Image;
    public TMP_Text CountRooms;
    public TMP_Text AreaFloorForFloors;
    public TMP_Text Price;
    public int PriceValue;

    private Button _button;
    private GameManager _manager;
    
    public void Init(MyFlat myFlat, GameManager manager)
    {
        _myFlat = myFlat;
        _manager = manager;
        PriceValue = _myFlat.Price;
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
        Image.sprite = _myFlat.FlatSprite;
        CountRooms.text = _myFlat.Rooms + "-комнатная";
        if(_myFlat.Rooms == 0)
            CountRooms.text = "Студия";
        AreaFloorForFloors.text = _myFlat.Area + " м" + _manager.SymvolQuadro + " | этаж " + _myFlat.Floor + " из " + _myFlat.MaxFloor;
        Price.text = _manager.GetSplitPrice(_myFlat.Price) + " " + _manager.SymvolRuble;



        _manager.MessageOnFlat(_myFlat.SendKorpus,1,_myFlat.Number);
    }

    private void OnClick()
    {
        //_manager.cartFlatPanel.Show(_myFlat);
    }
}
