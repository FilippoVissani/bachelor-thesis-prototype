using UnityEngine;

namespace Microsoft.MixedReality.Toolkit.Examples.Demos
{
    public class LaunchUri : MonoBehaviour
    {
        void Start()
        {
            string uri = "http://192.168.40.100:8080/vital-signs-monitor/saVSMonitor.html";
            UnityEngine.WSA.Launcher.LaunchUri(uri, false);
        }
    }
}