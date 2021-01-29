using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;

public class MainMenu : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI nameInputField = null;
    [SerializeField] private TextMeshProUGUI codeInputField = null;

    [SerializeField] private string nextScene = "";

    public void changeScene(){
        RequestMenuJson request = new RequestMenuJson();
        request.email = nameInputField.text;
        request.code = codeInputField.text;

        if(isValid(request)){
            StartCoroutine(getRequest("http://localhost:5000/users", request));
        }
    }

    /*
    public IEnumerator postRequest(string url, RequestMenuJson request){
        var uwr = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(JsonUtility.ToJson(request));
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError){
            Debug.Log("Error While Sending: " + uwr.error);
        } else {
            ResponseMenuJson response = JsonUtility.FromJson<ResponseMenuJson>(uwr.downloadHandler.text);
            if(string.IsNullOrEmpty(response.error)){
                Debug.Log("Token: " + response.token);

                saveAndChange(response);
            }else{
                Debug.Log("Server Reponse Error: " + response.error);
            }
        }
    }
    */

    public IEnumerator getRequest(string url, RequestMenuJson request){
        UnityWebRequest uwr = UnityWebRequest.Get(url+"?email="+request.email+"&code="+request.code);

        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError){
            Debug.Log("Error While Sending: " + uwr.error);
        } else {
            ResponseMenuJson response = JsonUtility.FromJson<ResponseMenuJson>(uwr.downloadHandler.text);
            if(string.IsNullOrEmpty(response.error) && response.validCode){
                saveAndChange(response);
            }else{
                Debug.Log("Server Reponse Error: " + response.error);
            }
        }
    }

    private void saveAndChange(ResponseMenuJson response){
        

    }

    public void quitGame(){
        Application.Quit();
    }

    private bool isValid(RequestMenuJson request){
        return !string.IsNullOrEmpty(request.email) && !string.IsNullOrEmpty(request.code);
    }

    public class RequestMenuJson{
        public string email;
        public string code;
    }

    public class ResponseMenuJson{
        public bool haveRegistration;
        public bool validCode;
        public string error;
    }
}
