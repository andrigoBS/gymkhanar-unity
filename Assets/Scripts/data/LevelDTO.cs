using System;

namespace DataHelpers
{
    [System.Serializable]
    public class LevelDTO
    {
        public string error;
        public int levelNumber;
        public LevelFileDTO previewImage;
        public LevelFileDTO tracking;
        public LevelFileDTO asset;
    }
    
    [System.Serializable]
    public class LevelFileDTO
    {
        public string url;
        public string name;
        public string type;
    }
}