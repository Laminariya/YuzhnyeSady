using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoPanel : MonoBehaviour
{
    private GameManager _manager;

    public Button b_Back;

    public void Init(GameManager manager)
    {
        _manager = manager;
        
        b_Back.onClick.AddListener(OnBack);

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

    private void OnBack()
    {
        Hide();
    }
}
