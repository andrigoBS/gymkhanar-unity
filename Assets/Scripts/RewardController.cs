using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RewardController : MonoBehaviour
{
    private bool finishAnimation = false;
    void Start()
    {
        StartCoroutine(finishAfterDelay(16));
    }
    
    void Update()
    {
        if (finishAnimation || Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Main");
        }
    }
    
    private IEnumerator finishAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        finishAnimation = true;
    }
}
