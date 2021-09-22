using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Vuforia;

public class MixedRealityServiceConnectionManager : MonoBehaviour
{
    //public TextMeshPro debug;
    private Dictionary<string, string> informationDatabase;
    private Dictionary<string, Texture2D> markerImages;
    private string serverAddress = "http://192.168.40.100/";

    private void Start()
    {
        StartCoroutine(GetInformationDatabase());
    }

    public Dictionary<string, Texture2D> getMarkerImages()
    {
        return markerImages;
    }

    public Dictionary<string, string> getInformationDatabase()
    {
        return informationDatabase;
    }

    IEnumerator GetInformationDatabase()
    {
        using (UnityWebRequest uwr = UnityWebRequest.Get(serverAddress + "information-database.json"))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                //debug.SetText(uwr.error);
            }
            else
            {
                // Show results as text
                //debug.SetText(uwr.downloadHandler.text);
                informationDatabase = JsonConvert.DeserializeObject<Dictionary<string, string>>(uwr.downloadHandler.text);
                //debug.SetText(informationDatabase.ContainsKey("marker2").ToString());

                StartCoroutine(DownloadMarkerImages());
            }
        }
    }

    private IEnumerator DownloadMarkerImages()
    {
        markerImages = new Dictionary<string, Texture2D>();
        foreach (var marker in informationDatabase.Keys)
        {
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(serverAddress + marker + ".jpg"))
            {
                yield return uwr.SendWebRequest();

                if (uwr.result != UnityWebRequest.Result.Success)
                {
                    //debug.SetText(uwr.error);
                }
                else
                {
                    // Get downloaded texture once the web request completes
                    var texture = DownloadHandlerTexture.GetContent(uwr);

                    markerImages.Add(marker, texture);
                    //debug.SetText("Image downloaded " + uwr);
                    CreateImageTargetFromTexture(marker, texture);
                }
            }
        }
    }

    private void CreateImageTargetFromTexture(string markerName, Texture2D image)
    {
        var mTarget = VuforiaBehaviour.Instance.ObserverFactory.CreateImageTarget(image, 0.1f, markerName);

        // Add the DefaultObserverEventHandler to the newly created game object
        mTarget.gameObject.AddComponent<DefaultObserverEventHandler>();

        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = mTarget.transform.position;
        cube.transform.parent = mTarget.transform;
    }
}
