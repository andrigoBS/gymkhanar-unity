using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using UnityEngine;
using TMPro;
using DataHelpers;

public class LoadSceneController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text = null;

	private static DataHelper dataHelper = null;
    
    void Start()
    {
        if (dataHelper == null)
        {               
            Application.deepLinkActivated += onDeepLinkActivated;
            if (!String.IsNullOrEmpty(Application.absoluteURL))
            {
                onDeepLinkActivated(Application.absoluteURL);
            }else{
				text.text = "Não foi possivel carregar a cena!!!! :( \nToken não informado";
			}
            DontDestroyOnLoad(gameObject);
        }
        else
        {
			loadScene();
            Destroy(gameObject);
        }
		Debug.Log(text.text);
    }
 
    private void onDeepLinkActivated(string url)
    {
		dataHelper = DataHelper.getInstance();
		dataHelper.setToken(url.Split("?token="[0])[1]);
		loadScene();
    }

	private void loadScene()
	{		
		try 
        {
	       	SceneManager.LoadScene(dataHelper.getLevel().getNextScene());
        }
        catch (Exception e)
        {
            text.text = "Não foi possivel carregar a cena!!!! :( \n\n"+e;
        }
	}
}
