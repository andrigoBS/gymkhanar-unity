using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using UnityEngine;
using TMPro;

public class LoadScene : MonoBehaviour
{
    public static LoadScene Instance { get; private set; }
    
    [SerializeField] private TextMeshProUGUI text = null;
    
    private string deeplinkURL;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;                
            Application.deepLinkActivated += onDeepLinkActivated;
            if (!String.IsNullOrEmpty(Application.absoluteURL))
            {
                onDeepLinkActivated(Application.absoluteURL);
            }
            else deeplinkURL = "[none]";
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
 
    private void onDeepLinkActivated(string url)
    {
        deeplinkURL = url;
        string[] parameters = url.Split("?"[0])[1].Split(";"[0]);

		string token = parameters[0];
        string scene = parameters[1];
        
        PlayerPrefs.SetString("TOKEN", token);

        try 
        {
            SceneManager.LoadScene(scene);
        }
        catch (Exception e)
        {
            text.text = "Não foi possivel carregar a cena!!!! :( \n\n"+e;
        }
    }
}
