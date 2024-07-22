using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    // Start is called before the first frame update
    private TextMeshProUGUI _textMeshProUGUI;
    public string myColor = "";

    void Start()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    public void MakeNormal()
    {
        _textMeshProUGUI.color = Color.black;
        _textMeshProUGUI.fontStyle = FontStyles.Normal;
    }

    public void MakeColor()
    {
        _textMeshProUGUI.color = ColorUtility.TryParseHtmlString(myColor, out Color color)
            ? color
            : Color.white;
        _textMeshProUGUI.fontStyle = FontStyles.Bold | FontStyles.Underline;
    }
}
