using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalereyaPanel : MonoBehaviour
{
    private GameManager _manager;

    public GameObject ImagePanel;
    public Button b_Architectura;
    public Button b_Blagoustriystvo;
    public Button b_Back;

    public Button b_Left;
    public Button b_Right;
    public Button b_ImageBack;
    
    public List<Sprite> Architectura = new List<Sprite>();
    public List<Sprite> Blagoustroystvo = new List<Sprite>();
    
    private List<Sprite> _currentList = new List<Sprite>();
    private Image _image;
    private int _currentImage;
    
    public void Init(GameManager manager)
    {
        _manager = manager;
        
        b_Architectura.onClick.AddListener(OnArchitecture);
        b_Blagoustriystvo.onClick.AddListener(OnBlagoustriystvo);
        b_Back.onClick.AddListener(OnBack);
        b_Left.onClick.AddListener(OnLeft);
        b_Right.onClick.AddListener(OnRight);
        b_ImageBack.onClick.AddListener(OnImageBack);
        _image = ImagePanel.GetComponent<Image>();
        
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        ImagePanel.SetActive(false);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        ImagePanel.SetActive(false);
    }

    private void OnArchitecture()
    {
        ImagePanel.SetActive(true);
        _currentList = new List<Sprite>(Architectura);
        _currentImage = 0;
        OnLeft();
    }

    private void OnBlagoustriystvo()
    {
        ImagePanel.SetActive(true);
        _currentList = new List<Sprite>(Blagoustroystvo);
        _currentImage = 0;
        OnLeft();
    }

    private void OnBack()
    {
        Hide();
    }

    private void OnLeft()
    {
        _currentImage--;
        if (_currentImage <= 0)
            _currentImage = 0;
        _image.sprite = _currentList[_currentImage];
    }

    private void OnRight()
    {
        _currentImage++;
        if (_currentImage >= _currentList.Count)
            _currentImage = _currentList.Count - 1;
        _image.sprite = _currentList[_currentImage];
    }

    private void OnImageBack()
    {
        ImagePanel.SetActive(false);
    }
}
