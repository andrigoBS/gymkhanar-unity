using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DataHelpers;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestionController : MonoBehaviour
{
    private static int OPTIONS_LENGTH = 3;
    [SerializeField] private TextMeshProUGUI text = null;
    [SerializeField] private TextMeshProUGUI[] optionsTexts = new TextMeshProUGUI[OPTIONS_LENGTH];
    [SerializeField] private Button[] optionsButtons = new Button[OPTIONS_LENGTH];
    
    private bool[] addedCorrections = new bool[OPTIONS_LENGTH];

    void Awake()
    {
        Question question = DataHelper.getInstance().getQuestion();

        text.text = question.text;

        for (int i = 0; i < OPTIONS_LENGTH; i++)
        {
            int index = i;
            addedCorrections[index] = false;
            QuestionOption option = question.options[index];
            optionsTexts[index].text = option.text;
            if (option.isCorrect)
            {
                optionsButtons[index].onClick.AddListener(() =>
                {
                    optionsTexts[index].text += "\nJustificativa: " + option.correction;
                    addedCorrections[index] = true;
                    StartCoroutine(LoadLevelAfterDelay(2));
                });
            }
            else 
            {
                optionsButtons[index].onClick.AddListener(() => {
                    if (!addedCorrections[index])
                    {
                        optionsTexts[index].text += "\nJustificativa: " + option.correction;
                        optionsTexts[index].color = new Color32(255, 0, 0, 255);
                        addedCorrections[index] = true;
                    } 
                });
            }
        }
    }
    
    private IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Main");
    }
}
