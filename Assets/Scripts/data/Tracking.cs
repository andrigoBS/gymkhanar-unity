using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace DataHelpers
{
    [System.Serializable]
    public class Tracking
    {
        public static Tracking fromJSON(string json)
        {
            return JsonUtility.FromJson<Tracking>(json);
        }

        public string error;
        public int id;
        public string type;
        public string url;
        public string assetName;
    }
}
