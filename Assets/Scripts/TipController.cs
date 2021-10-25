using UnityEngine;
using UnityEngine.SceneManagement;
using DataHelpers;
using TMPro;

public class TipController : MonoBehaviour
{
    void Start()
    {
        Tip tip = DataHelper.getInstance().getTip();

        setTextInput(0, tip.title);
        setTextInput(1, tip.subtitle);
        setTextInput(2, tip.body);
    }
    public void onOkClick()
    {
        SceneManager.LoadScene("Main");
    }

    private void setTextInput(int index, string text)
    {
        transform.GetChild(index).GetComponent<TextMeshProUGUI>().text = text;
    }
}
