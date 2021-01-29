using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;

public class TeamMenu : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI teamInputField = null;
    [SerializeField] private TextMeshProUGUI friendsInputField = null;

    [SerializeField] private string nextScene = "";
    [SerializeField] private string tokenPrefabName = "";

    private string token = "";

    void Start(){
        token = PlayerPrefs.GetString(tokenPrefabName);

        StartCoroutine(getRequest("http://localhost:5000/team"));
    }

    public IEnumerator getRequest(string url){
        UnityWebRequest uwr = UnityWebRequest.Get(url+"?token="+token);

        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError){
            Debug.Log("Error While Sending: " + uwr.error);
        } else {
            ResponseTeamJson response = JsonUtility.FromJson<ResponseTeamJson>(uwr.downloadHandler.text);
            if(string.IsNullOrEmpty(response.error)){
                Debug.Log("Name: " + response.name);

                teamInputField.text = "Nome da equipe: "+response.name;

                friendsInputField.text = "Equipe:\n";
                foreach (ResponseFriendsJson friend in response.friends){
                    friendsInputField.text += friend.name + " - " + friend.status + "\n";
                }
            }else{
                Debug.Log("Server Reponse Error: " + response.error);
            }
        }
    }

    public void changeScene(){
        SceneManager.LoadScene(nextScene);
    }

    public void quitGame(){
        Application.Quit();
    }

    [System.Serializable]
    public class ResponseTeamJson{
        public string error;
        public string name;
        public ResponseFriendsJson[] friends;
    }

    [System.Serializable]
    public class ResponseFriendsJson{
            public string name;
            public string status;
    }
}
