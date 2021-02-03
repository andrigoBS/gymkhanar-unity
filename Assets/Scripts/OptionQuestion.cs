using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionQuestion : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] texts = new TextMeshProUGUI[3];
    
    private int correctOptionIndex = 1;
    private string[] justifications = new string[]
    {
        "errado porque nao ta certo bla bla bla bla bla", 
        "", 
        "errado porque sim"
    };

    public void onClickOption(int optionIndex) {
        QuestionMenu questionScript = transform.parent.gameObject.GetComponent<QuestionMenu>();
        if (correctOptionIndex == optionIndex) {
            questionScript.correctAnswer();
        } else
        {
            texts[optionIndex].text = texts[optionIndex].text + ".\nJustificativa:" + justifications[optionIndex];
            texts[optionIndex].color = new Color32(255, 0, 0, 255);
            Debug.Log(texts[optionIndex].text);
        }
    }
}
