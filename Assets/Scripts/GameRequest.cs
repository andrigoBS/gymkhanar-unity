using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Wikitude;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

public class GameRequest : MonoBehaviour{
    private string token = "004";
    private string baseUrl = "http://localhost:5000/";
    private ImageTrackable imageTrackable;
    private string reward = "Sphere";

    private bool tracked = false;

    void Start(){
        StartCoroutine(getRequest());
    }

    private void createLevel(ResponseLevel response){
        StartCoroutine(importObject(response.asset));

        ImageTracker imageTracker = gameObject.AddComponent<ImageTracker>();
        imageTracker.TargetSourceType = TargetSourceType.TargetCollectionResource;
        imageTracker.TargetCollectionResource = new TargetCollectionResource();
        imageTracker.TargetCollectionResource.UseCustomURL = true;
        imageTracker.TargetCollectionResource.TargetPath = response.tracking.url;
    }

    IEnumerator importObject(ResponseLevelFile responseObject){
        string writePath = Application.dataPath + "/Resources/" + responseObject.name + "." + responseObject.type;

        if(!System.IO.File.Exists(@""+writePath)){
            Debug.Log("não existe");
            UnityWebRequest uwr = UnityWebRequest.Get(responseObject.url);

            //Send the request then wait here until it returns
            yield return uwr.SendWebRequest();

            if (uwr.result == UnityWebRequest.Result.ConnectionError){
                Debug.Log("Error While Sending: " + uwr.error);
            } else {
                System.IO.File.WriteAllBytes(writePath, uwr.downloadHandler.data);
    
                Debug.Log("Wrote to path");
            }

            AssetDatabase.Refresh();
        }

        GameObject trackableObject = new GameObject("TrackableObject");
        imageTrackable = trackableObject.AddComponent<ImageTrackable>();
        trackableObject.transform.SetParent(transform, false);
        imageTrackable.Drawable = (GameObject)Resources.Load(responseObject.name);

        imageTrackable.OnImageRecognized.AddListener((eventData) => { 
            tracked = true;
        });

        imageTrackable.OnImageLost.AddListener((eventData) => { 
            tracked = false;
        });
    }

    public IEnumerator getRequest(){
        UnityWebRequest uwr = UnityWebRequest.Get(baseUrl+"level?token="+token);

        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError){
            Debug.Log("Error While Sending: " + uwr.error);
        } else {
            ResponseLevel response = JsonUtility.FromJson<ResponseLevel>(uwr.downloadHandler.text);
            if(string.IsNullOrEmpty(response.error)){
                createLevel(response);
            }else{
                Debug.Log("Server Reponse Error: " + response.error);
            }
        }
    }

    [System.Serializable]
    public class ResponseLevel{
        public string error;
        public int levelNumber;
        public ResponseLevelFile previewImage;
        public ResponseLevelFile tracking;
        public ResponseLevelFile asset;
    }
    [System.Serializable]
    public class ResponseLevelFile{
        public string url;
        public string name;
        public string type;
    }

    public void destroyChildren(){
        Destroy(gameObject.GetComponent<ImageTracker>());
        Destroy(gameObject.transform.GetChild(0).gameObject);
    }
    
    public bool isTracked(){
        return tracked;
    }

    public void activeReward(){
        GameObject rewardObject = Instantiate((GameObject)Resources.Load(reward));
        rewardObject.transform.parent = gameObject.transform;
        StartCoroutine(LoadLevelAfterDelay(2, rewardObject));
    }
    
    private IEnumerator LoadLevelAfterDelay(float delay, GameObject gameObject)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
        SceneManager.LoadScene("Game");
    }
}
