using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoogleService : MonoBehaviour  
{
    public PictureFactory pictureFactory;
    public Text buttonText;
    private const string API_KEY = "AIzaSyCBf4tiA4-BWo-fwm7ff3KKubmWe5Rt6_o";

    public void GetPicture()
    {
        StartCoroutine (PictureRoutine());
    }

    IEnumerator PictureRoutine()
    {
        buttonText.transform.parent.gameObject.SetActive(false);
        string query = buttonText.text;
        query = WWW.EscapeURL(query + "Art");
        pictureFactory.DeleteOldPicures();
        Vector3 cameraForward = Camera.main.transform.forward;

        int rowNum = 1;
        for(int i = 1; i < 10; i+= 10)
        {
            string url = "https://www.googleapis.com/customsearch/v1?q" + query +
                "&cx=000004262842818947951%3Abejwegtk9ia&filter=1&num=10&searchType=image&start=" + i + "&fields=items%2Flink&key=" + API_KEY;
            WWW www = new WWW(url);
            yield return www;
            pictureFactory.CreateImages(ParseResponse(www.text),rowNum,cameraForward);
            rowNum++;
        }

        yield return new WaitForSeconds(5f);
        buttonText.transform.parent.gameObject.SetActive(true);
    }

    List<string> ParseResponse(string text)
    {
        List<string> urlList = new List<string>();
        string[] urls = text.Split('\n');
        foreach (string line in urls)
        {
            if (line.Contains ("Link"))
                {
                    string url = line.Substring(12, line.Length - 13);
                    if(url.Contains (".jpg") || url.Contains (".png"))
                    {
                        urlList.Add(url);
                    }
                }
        }
        return urlList;
    }
}
