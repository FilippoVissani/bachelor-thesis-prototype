using Microsoft.MixedReality.Toolkit.UI;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using Vuforia;

public class MixedRealityClientController : MonoBehaviour
{
    public HologramManager hologramManager;
    private State currentState;
    private Dictionary<string, string> informationDatabase;
    private Dictionary<string, Texture2D> markerImages;
    private string serverAddress = "http://192.168.40.100:8080/";

    private void Start()
    {
        currentState = State.Downloading;
        StartCoroutine(GetInformationDatabase());
    }

    private void Update()
    {
        if (currentState == State.InformationReady)
        {
            foreach (var item in markerImages)
            {
                CreateImageTargetFromTexture(item.Key, item.Value);
            }

            currentState = State.MarkersCreated;
        }

        if (currentState == State.MarkersCreated)
        {
            foreach (var item in informationDatabase)
            {
                hologramManager.addHologram(item.Key, item.Value);
            }
            currentState = State.Ended;
        }
    }

    IEnumerator GetInformationDatabase()
    {
        using (UnityWebRequest uwr = UnityWebRequest.Get(serverAddress + "json/information-database.json"))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result == UnityWebRequest.Result.Success)
            {
                informationDatabase = JsonConvert.DeserializeObject<Dictionary<string, string>>(uwr.downloadHandler.text);
                StartCoroutine(DownloadMarkerImages());
            }
        }
    }

    private IEnumerator DownloadMarkerImages()
    {
        markerImages = new Dictionary<string, Texture2D>();
        foreach (var marker in informationDatabase.Keys)
        {
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(serverAddress + "markers/" + marker + ".jpg"))
            {
                yield return uwr.SendWebRequest();

                if (uwr.result == UnityWebRequest.Result.Success)
                {
                    var texture = DownloadHandlerTexture.GetContent(uwr);
                    markerImages.Add(marker, texture);
                }
            }
        }
        currentState = State.InformationReady;
    }

    private void CreateImageTargetFromTexture(string markerName, Texture2D image)
    {
        var mTarget = VuforiaBehaviour.Instance.ObserverFactory.CreateImageTarget(image, 0.1f, markerName);

        mTarget.gameObject.AddComponent<DefaultObserverEventHandler>();
    }
}

enum State
{
    Downloading,
    InformationReady,
    MarkersCreated,
    HologramManagerUpdated,
    Ended
}
