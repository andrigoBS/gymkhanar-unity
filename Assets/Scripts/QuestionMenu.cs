using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionMenu : MonoBehaviour{
    [SerializeField] private GameObject trackingObject = null;

    private bool onQuestion = false;

    private GameRequest gameScript = null;

    void Start(){
        gameScript = trackingObject.GetComponent<GameRequest>();
    }

    void Update(){
        if(!onQuestion && gameScript.isTracked() && Input.GetMouseButtonDown(0)){
            GameObject questionMenuAssets = (GameObject)Resources.Load("Image");
            GameObject questionAssets = Instantiate(questionMenuAssets);
            questionAssets.transform.SetParent(transform, false);
            onQuestion = true;
            gameScript.destroyChildren();
        }
        
    }

    void correctAnswer(){
        onQuestion = false;
        Destroy(gameObject.transform.GetChild(0).gameObject);
        gameScript.activeReward();
    }

    void errorAnswer(){
        
    }

}
