using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour
{

    private GameManager _manager;

    public Button b_Genplan;
    public Button b_Galereya;
    public Button b_ChoseFlatOnParameter;
    public Button b_Video;
    
    public void Init(GameManager manager)
    {
        _manager = manager;
        
        b_Galereya.onClick.AddListener(OnGalereya);
        b_Genplan.onClick.AddListener(OnGenplan);
        b_ChoseFlatOnParameter.onClick.AddListener(OnChoseFlatOnParameter);
        b_Video.onClick.AddListener(OnVideo);
        
       Show();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnGenplan()
    {
        _manager.genplanPanel.Show();
    }

    private void OnGalereya()
    {
        _manager.galereyaPanel.Show();
    }

    private void OnChoseFlatOnParameter()
    {
        Debug.Log("OnChoseFlat");
    }

    private void OnVideo()
    {
        _manager.videoPanel.Show();
    }


}
