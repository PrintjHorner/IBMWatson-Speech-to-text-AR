using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicBehavoir : MonoBehaviour
{
    public Renderer quadRenderer;
    private Vector3 desieredPosition;


    void Start()
    {
        transform.LookAt(Camera.main.transform);
        Vector3 desiredAngle = new Vector3(0, transform.localEulerAngles.y, 0);
        transform.rotation = Quaternion.Euler(desieredPosition);

        //force into air
        desieredPosition = transform.localPosition;
        transform.localPosition = new Vector3(0, 20, 0);
    }

    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.position, desieredPosition, Time.deltaTime * 4f);

    }

    public void LoadImage (string url)
    {
        StartCoroutine(LoadImageFronURL(url));
    }

    IEnumerator LoadImageFronURL(string url)
    {
        WWW www = new WWW (url);
        yield return www;
        quadRenderer.material.mainTexture = www.texture; //set quad render to www result
    }


}
