using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Wikitude;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using DataHelpers;

public class GameRequest : MonoBehaviour{
    private string token;
    private string baseUrl = "http://192.168.3.8:5000";
    private ImageTrackable imageTrackable;
    private string reward = "Sphere";

    private bool tracked = false;

    void Start(){
        token = PlayerPrefs.GetString("TOKEN");
        StartCoroutine(getRequest());
    }

    private void createLevel(LevelDTO response){
        StartCoroutine(importObject(response.asset));

        ImageTracker imageTracker = gameObject.AddComponent<ImageTracker>();
        imageTracker.TargetSourceType = TargetSourceType.TargetCollectionResource;
        imageTracker.TargetCollectionResource = new TargetCollectionResource();
        imageTracker.TargetCollectionResource.UseCustomURL = true;
        imageTracker.TargetCollectionResource.TargetPath = response.tracking.url;
    }

    IEnumerator importObject(LevelFileDTO responseObject){
        string writePath = Application.persistentDataPath + "/" + responseObject.name + "." + responseObject.type;

        Debug.Log(writePath);

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

            //AssetDatabase.Refresh();
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
        UnityWebRequest uwr = UnityWebRequest.Get(baseUrl+"/level?token="+token);

        Debug.Log(baseUrl + "/level?token=" + token);

        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError){
            Debug.Log("Error While Sending: " + uwr.error);
        } else {
            LevelDTO response = JsonUtility.FromJson<LevelDTO>(uwr.downloadHandler.text);
            if(response != null && string.IsNullOrEmpty(response.error)){
                createLevel(response);
            }else{
                Debug.Log("Server Reponse Error: " + JsonUtility.ToJson(response));
            }
        }
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
        SceneManager.LoadScene("imageChestQuest");
    }
}
