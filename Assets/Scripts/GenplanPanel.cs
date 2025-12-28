using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenplanPanel : MonoBehaviour
{
    private GameManager _manager;

    public Button b_Menu;
    public Button b_LightLife;
    public Button b_LightKomercial;
    public Button b_LightSocial;

    public Button b_Korpus_1;
    public Button b_Korpus_2;
    public Button b_Korpus_4_1;

    public Sprite OffLight;
    public Sprite OnLight;
    
    public void Init(GameManager manager)
    {
        _manager = manager;
        
        b_Menu.onClick.AddListener(OnMenu);
        b_LightLife.onClick.AddListener(OnLightLife);
        b_LightKomercial.onClick.AddListener(OnLightKomercial);
        b_LightSocial.onClick.AddListener(OnLightSocial);
        b_Korpus_1.onClick.AddListener(OnKorpus1);
        b_Korpus_2.onClick.AddListener(OnKorpus2);
        b_Korpus_4_1.onClick.AddListener(OnKorpus4_1);
        
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnMenu()
    {
        Hide();
    }

    private void OnLightLife()
    {
        
    }

    private void OnLightKomercial()
    {
        
    }

    private void OnLightSocial()
    {
        
    }

    private void OnKorpus1()
    {
        
    }

    private void OnKorpus2()
    {
        
    }

    private void OnKorpus4_1()
    {
        
    }
}
