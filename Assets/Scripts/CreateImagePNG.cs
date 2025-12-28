using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CreateImagePNG : MonoBehaviour
{

    public enum TypePlane
    {
        floor, plane, furniture
    }
    
    public Image ImageTest;
    public string PlaneFloor = "//Plans//PlanFloors//";
    public string PlaneApartment = "//Plans//PlanFlats//";

    public SVGImage TestSVGImage;
    public RawImage RawImage;

    private GameManager _manager;

    private string _testUrl =
        "https://cdn.a101.ru/mmedia/2024/07/25/upd/big_layout/b33a73d99a0dc40c3a47343edc01ab6fd4dde79d.svg";

    private string _testPath = "G:\\Projects\\YuzhnyeSady\\Plans\\PlanFlats\\999";
    
    private HttpClient _httpClient = new HttpClient();
    
    public IEnumerator Init(GameManager manager)
    {
        Debug.Log("Create ImagePNG");
        _manager = manager;
        
        if(!Directory.Exists(Directory.GetCurrentDirectory()+PlaneFloor))
            CreateFolder(Directory.GetCurrentDirectory()+PlaneFloor);
        if(!Directory.Exists(Directory.GetCurrentDirectory()+PlaneApartment))
            CreateFolder(Directory.GetCurrentDirectory()+PlaneApartment);
        yield return StartCoroutine(CreateSVG());
        //yield return StartCoroutine(CheckPlane());
        yield return null;
    }

    private IEnumerator CreateSVG()
    {
        UnityWebRequest www = UnityWebRequest.Get(_testUrl);
        yield return www.SendWebRequest();
        using (FileStream fstream = new FileStream(_testPath+".txt", FileMode.OpenOrCreate))
        {
            yield return fstream.WriteAsync(www.downloadHandler.data, 0, www.downloadHandler.data.Length);
        }
        try
        {
            string content = File.ReadAllText(_testPath + ".txt");
            CreateSpriteFromSVG(content);
        }
        catch (FileNotFoundException)
        {
            Debug.Log("Файл не найден");
        }
        catch (IOException ex)
        {
            Debug.Log($"Ошибка ввода-вывода: {ex.Message}");
        }
    }

    private void CreateFolder(string url)
    {
        Directory.CreateDirectory(url);
    }

    //Проверка на наличие схем и изменение
    IEnumerator CheckPlane()
    {
        Debug.Log("Checking plane");
        
        int count = 0;
        foreach (var builder in _manager.MyData.Builders)
        {
            foreach (var mySection in builder.Sections)
            {
                count+=mySection.Flats.Count;
            }
            
        }

        int count2 = 0;
        foreach (var builder in _manager.MyData.Builders)
        {
            //Debug.Log(builder.Name);
            foreach (var section in builder.Sections)
            {
                foreach (var myFlat in section.Flats)
                {
                    count2++;
                    _manager.InfoStartPanel.text = count2.ToString()+"/"+count.ToString();
                   
                    //Зашружаем схемы с мебелью
                    if (!File.Exists(myFlat.PathFlat) &&
                        myFlat.UrlFlat != "")
                        yield return StartCoroutine(LoadFileFromUrl(myFlat.UrlFlat,myFlat.PathFlat + ".png"));
                    //Загружаем схему этажа
                    if (!File.Exists(myFlat.PathFloor) &&
                        myFlat.UrlFloor != "")
                        yield return StartCoroutine(LoadFileFromUrl(myFlat.UrlFloor,myFlat.PathFloor + ".png"));
                    yield return null;
                }
                
            }
        }
        Debug.Log("Checking plane END");
    }



    //Загрузили с сервера картинку
    IEnumerator LoadFileFromUrl(string url, string pathPlane)
    {
        if (url.Contains(".png"))
        {
            Debug.Log("LoadFileFromUrl");
            UnityWebRequest www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();

            Debug.Log("Изображение загружено");
            yield return StartCoroutine(CreatePNG(pathPlane, www.downloadHandler.data));
        }
        else
        {
            UnityWebRequest www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();
            using (FileStream fstream = new FileStream(pathPlane+".txt", FileMode.OpenOrCreate))
            {
                yield return fstream.WriteAsync(www.downloadHandler.data, 0, www.downloadHandler.data.Length);
                //CreateSpriteFromSVG(www.downloadHandler.text);
            }
            try
            {
                string content = File.ReadAllText(pathPlane+".txt");
                //CreateSpriteFromSVG(content);
            }
            catch (FileNotFoundException)
            {
                Debug.Log("Файл не найден");
            }
            catch (IOException ex)
            {
                Debug.Log($"Ошибка ввода-вывода: {ex.Message}");
            }
        }
        
    }
    
    private void CreateSpriteFromSVG(string text)
    {
        Debug.Log(text);
        // Загружаем SVG
        var sceneInfo = SVGParser.ImportSVG(new System.IO.StringReader(text));
        
        // Создаем геометрию
        var geometry = VectorUtils.TessellateScene(sceneInfo.Scene, 
            new VectorUtils.TessellationOptions()
            {
                StepDistance = 10.0f,
                MaxCordDeviation = 0.5f,
                MaxTanAngleDeviation = 0.1f,
                SamplingStepSize = 0.01f
            });
        
        Debug.Log("GG "+ geometry.Count);
        
        // Создаем спрайт
        var sprite = VectorUtils.BuildSprite(geometry, 100f, 
            VectorUtils.Alignment.Center, Vector2.zero, 64, true);
        
        // Применяем к объекту
        TestSVGImage.sprite = sprite;
        //StartCoroutine(CreatePngFromSvg());
    }

    IEnumerator CreatePngFromSvg()
    {
        byte[] data = ((Texture2D)RawImage.materialForRendering.mainTexture).EncodeToPNG();
        using (FileStream fstream = new FileStream(_testPath + ".png", FileMode.OpenOrCreate))
        {
            // запись массива байтов в файл
            
            yield return fstream.WriteAsync(data, 0, data.Length);
            Debug.Log("Картинка создана записан в файл");
        }
    }

    // //Загрузили с сервера картинку
    // IEnumerator LoadGifFileFromUrl(string url, string pathPlane) 
    // {
    //     try
    //     {
    //         await DownloadImageAsync(url, pathPlane);
    //         Console.WriteLine("Изображение успешно загружено.");
    //     }
    //     catch (Exception ex)
    //     {
    //         Console.WriteLine($"Ошибка: {ex.Message}");
    //     }
    //     Debug.Log("Изображение загружено");
    //     yield return StartCoroutine(CreatePNG(pathPlane, www.downloadHandler.data));
    //     //}
    // }
    
    //Сохранили картинку у нас
    public IEnumerator CreatePNG(string path, byte[] data)
    {
        // запись в файл
        using (FileStream fstream = new FileStream(path, FileMode.OpenOrCreate))
        {
            // запись массива байтов в файл
            yield return fstream.WriteAsync(data, 0, data.Length);
            Debug.Log("Картинка создана записан в файл");
        }
    }

    public IEnumerator LoadPNG(string urlPlane, Image image)
    {
        string url = "file://" + urlPlane;
        
        using (WWW www = new WWW(url))
        {
            yield return www;
            //Debug.Log(www.texture);
            Texture2D texture2D = www.texture;
            Sprite sprite = Sprite.Create(texture2D, new Rect(0.0f, 0.0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), 100.0f);
            //_sprites.Add(sprite);
            if (image != null)
                image.sprite = sprite;
        }
    }

    public IEnumerator LoadSpritePNG(string urlPlane, Sprite sprite)
    {
        string url = "file://" + urlPlane;
        
        using (WWW www = new WWW(url))
        {
            yield return www;
            Texture2D texture2D = www.texture;
            Sprite _sprite = Sprite.Create(texture2D, new Rect(0.0f, 0.0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), 100.0f);
            sprite = _sprite;
            sprite.name = urlPlane;
        }
    }
    
    public IEnumerator LoadSpriteFlatPNG(MyFlat myObject)
    {
        string url = "file://" + myObject.PathFlat;
        
        using (WWW www = new WWW(url))
        {
            yield return www;
            Texture2D texture2D = www.texture;
            Sprite _sprite = Sprite.Create(texture2D, new Rect(0.0f, 0.0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), 100.0f);
            myObject.FlatSprite = _sprite;
        }
        
        url = "file://" + myObject.PathFloor;
        using (WWW www = new WWW(url))
        {
            yield return www;
            Texture2D texture2D = www.texture;
            Sprite _sprite = Sprite.Create(texture2D, new Rect(0.0f, 0.0f, texture2D.width, texture2D.height),
                new Vector2(0.5f, 0.5f), 100.0f);
            myObject.FloorSprite = _sprite;
        }
        
    }

}
