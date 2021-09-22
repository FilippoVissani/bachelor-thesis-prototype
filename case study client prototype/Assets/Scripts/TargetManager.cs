using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using Vuforia;

public class TargetManager : MonoBehaviour
{
    public TextMeshPro debug;
    private Texture2D imageFromWeb;
    private string serverAddress = "http://192.168.40.100/";
    void Start()
    {
        VuforiaApplication.Instance.OnVuforiaInitialized += OnVuforiaInitialized;
    }

    void OnVuforiaInitialized(VuforiaInitError error)
    {
        StartCoroutine(RetrieveTextureFromWeb());
    }

    IEnumerator RetrieveTextureFromWeb()
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(serverAddress + "marker1.jpg"))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                debug.SetText(uwr.error);
            }
            else
            {
                // Get downloaded texture once the web request completes
                var texture = DownloadHandlerTexture.GetContent(uwr);

                imageFromWeb = texture;
                debug.SetText("Image downloaded " + uwr);

                CreateImageTargetFromDownloadedTexture();
            }
        }
    }

    void CreateImageTargetFromDownloadedTexture()
    {
        var mTarget = VuforiaBehaviour.Instance.ObserverFactory.CreateImageTarget(imageFromWeb,0.1f,"marker1");

        // Add the DefaultObserverEventHandler to the newly created game object
        mTarget.gameObject.AddComponent<DefaultObserverEventHandler>();

        debug.SetText("Target created and active" + mTarget);

        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = mTarget.transform.position;
        cube.transform.parent = mTarget.transform;
    }
}
