using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace DataHelpers
{
    [System.Serializable]
    public class Level
    {
        public static Level fromJSON(string json)
        {
            Level level = JsonUtility.FromJson<Level>(json);
            level.sceneQueueSequence = new Queue<string>(level.sceneSequence);
            return level;
        }
        
        public string error;
        public int id;
        public string levelName;
        public string[] sceneSequence;
        public int tipId;
        public int questionId;
        public int trackingId;
        
        private Queue<string> sceneQueueSequence;

        public string getNextScene()
        {
            return sceneQueueSequence.Dequeue();
        }

        public bool hasNextLevel()
        {
            return sceneQueueSequence.Count != 0;
        }
    }
}