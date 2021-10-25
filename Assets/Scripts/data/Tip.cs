using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace DataHelpers
{
    [System.Serializable]
    public class Tip
    {
        public static Tip fromJSON(string json)
        {
            return JsonUtility.FromJson<Tip>(json);
        }

        public string error;
        public int id;
        public string title;
        public string subtitle;
        public string body;
    }
}
