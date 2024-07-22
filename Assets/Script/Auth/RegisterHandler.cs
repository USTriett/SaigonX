using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RegisterHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _loadingPanel;

    [SerializeField]
    private GameObject _errorBox;

    [SerializeField]
    private GameObject _emailTextBox;

    [SerializeField]
    private GameObject _accountTextBox;

    [SerializeField]
    private GameObject _passwordTextBox;

    [SerializeField]
    private GameObject _repeatPasswordTextBox;

    [SerializeField]
    private GameObject _fullNameTextBox;

    [SerializeField]
    private GameObject _submitButton;

    class RegisterForm
    {
        public string email;
        public string username;
        public string fullName;
        public string password;
    }

    private RegisterForm _registerForm;

    // Start is called before the first frame update
    private void Start()
    {
        _registerForm = new RegisterForm();
    }

    // Update is called once per frame
    void Update()
    {
        _registerForm.password = _passwordTextBox.GetComponent<TMP_InputField>().text;
        _registerForm.username = _accountTextBox.GetComponent<TMP_InputField>().text;
        _registerForm.fullName = _fullNameTextBox.GetComponent<TMP_InputField>().text;
        _registerForm.email = _emailTextBox.GetComponent<TMP_InputField>().text;

        if (!CheckValid())
        {
            _submitButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            _submitButton.GetComponent<Button>().interactable = true;
        }
    }

    IEnumerator Register(string data)
    {
        using (
            UnityWebRequest registerPostRequest = new UnityWebRequest(
                "http://127.0.0.1:8000/api/auth/register",
                "POST"
            )
        )
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(data);
            registerPostRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            registerPostRequest.downloadHandler = new DownloadHandlerBuffer();
            registerPostRequest.SetRequestHeader("Content-Type", "application/json");

            yield return registerPostRequest.SendWebRequest();
            _loadingPanel.SetActive(true);
            if (registerPostRequest.result == UnityWebRequest.Result.Success)
            {
                _loadingPanel.SetActive(false);

                switch (registerPostRequest.responseCode)
                {
                    case 200:

                        break;
                    case 400:
                        _errorBox.GetComponent<TextMeshProUGUI>().text =
                            "Tài khoản đã tồn tại, xin chọn tài khoản khác";
                        break;
                }
            }
            // else if (registerPostRequest.result == UnityWebRequest.Result.ConnectionError)
            // {
            //     Debug.LogError($"Error: {registerPostRequest.error}");
            // }
            else
            {
                _loadingPanel.SetActive(false);
                Debug.LogError("error " + registerPostRequest.error);
                _errorBox.GetComponent<TextMeshProUGUI>().text =
                    "Một lỗi không xác định đã xảy ra vui lòng thử lại";
            }
        }
    }

    public void OnRegisterButtonClick()
    {
        Debug.Log("try register " + JsonUtility.ToJson(_registerForm) + "to " + Route.REGISTER);
        StartCoroutine(Register(JsonUtility.ToJson(_registerForm)));
    }

    private bool CheckValid()
    {
        List<string> fields = GetAllFields();

        if (fields[1].CompareTo(_registerForm.password) != 0)
        {
            _repeatPasswordTextBox.GetComponent<Outline>().effectColor = Color.red;
            _errorBox.GetComponent<TextMeshProUGUI>().text =
                "Mật khẩu nhập lại phải khớp với mật khẩu gốc";
            return false;
        }
        else
        {
            _errorBox.GetComponent<TextMeshProUGUI>().text = "";

            _repeatPasswordTextBox.GetComponent<Outline>().effectColor = new Color(
                r: 0x24 / 255f,
                g: 0x99 / 255f,
                b: 0xCB / 255f
            );
        }

        foreach (string field in fields)
        {
            if (field == "")
            {
                return false;
            }
        }
        return true;
    }

    private List<string> GetAllFields()
    {
        List<string> fields = new List<string>
        {
            _passwordTextBox.GetComponent<TMP_InputField>().text,
            _repeatPasswordTextBox.GetComponent<TMP_InputField>().text,
            _accountTextBox.GetComponent<TMP_InputField>().text,
            _fullNameTextBox.GetComponent<TMP_InputField>().text,
            _emailTextBox.GetComponent<TMP_InputField>().text
        };

        return fields;
    }
}
