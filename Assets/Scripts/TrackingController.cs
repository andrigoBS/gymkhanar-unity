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

public class TrackingController : MonoBehaviour{
     private bool tracked = false;

     void Awake(){
 		Tracking tracking = DataHelper.getInstance().getTracking();

		if(tracking.type == "image")
		{
 			ImageTracker imageTracker = gameObject.AddComponent<ImageTracker>();
 			imageTracker.TargetSourceType = TargetSourceType.TargetCollectionResource;
 			imageTracker.TargetCollectionResource = new TargetCollectionResource();
 			imageTracker.TargetCollectionResource.UseCustomURL = true;
 			imageTracker.TargetCollectionResource.TargetPath = tracking.url;
	 
 			GameObject trackableObject = new GameObject("TrackableObject");
 			ImageTrackable imageTrackable = trackableObject.AddComponent<ImageTrackable>();
 			trackableObject.transform.SetParent(transform, false);

			if(tracking.assetName != null){
				imageTrackable.Drawable = (GameObject)Resources.Load(tracking.assetName);
				imageTrackable.OnImageRecognized.AddListener((eventData) => {
     				tracked = true;
 				});
 				imageTrackable.OnImageLost.AddListener((eventData) => {
     				tracked = false;
 				});
			}else{
				imageTrackable.OnImageRecognized.AddListener((eventData) => {
     				nextScene();
 				});
			}
		}
		else if (tracking.type == "object")
		{
			ObjectTracker objectTracker = gameObject.AddComponent<ObjectTracker>();
			objectTracker.TargetCollectionResource = new TargetCollectionResource();
			objectTracker.TargetCollectionResource.UseCustomURL = true;
			objectTracker.TargetCollectionResource.TargetPath = tracking.url;
	 
			GameObject trackableObject = new GameObject("TrackableObject");
			ObjectTrackable objectTrackable = trackableObject.AddComponent<ObjectTrackable>();
			trackableObject.transform.SetParent(transform, false);

			if(tracking.assetName != null){
				objectTrackable.Drawable = (GameObject)Resources.Load(tracking.assetName);
				objectTrackable.OnObjectRecognized.AddListener((eventData) => {
					tracked = true;
				});
				objectTrackable.OnObjectLost.AddListener((eventData) => {
					tracked = false;
				});
			}else{
				objectTrackable.OnObjectRecognized.AddListener((eventData) => {
					nextScene();
				});
			}
		}
    }

	void Update(){
        if(tracked && Input.GetMouseButtonDown(0)){
			nextScene();
		}
	}

	private void nextScene(){
		SceneManager.LoadScene("Main");
	}
	
    /*
	********** codigo antigo, guardado so pra servir de exemplo 
    public void destroyChildren(){
        Destroy(gameObject.GetComponent<ImageTracker>());
        Destroy(gameObject.transform.GetChild(0).gameObject);
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
	*/
}
