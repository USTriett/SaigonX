using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoginHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _accountBox;

    [SerializeField]
    private GameObject _passwordBox;

    [SerializeField]
    private GameObject _errorBox;

    [SerializeField]
    private GameObject _loadingCanvas;

    class PlayerAccount
    {
        public string username;
        public string password;
    }

    class LoginResponse
    {
        public string message;
        public string acessToken;
    }

    private PlayerAccount _player;
    private LoginResponse _loginResponse;

    public Action LoginFailed { get; private set; }
    public Action LoginSuccess { get; private set; }

    private void OnEnable()
    {
        _errorBox.SetActive(false);
    }

    public void OnLoginButtonClick()
    {
        string username = _accountBox.GetComponent<TMP_InputField>().text;
        if (username == "")
        {
            _accountBox.GetComponent<Outline>().effectColor = Color.red;
            _errorBox.GetComponent<TextMeshProUGUI>().text = "Vui lòng nhập tài khoản";

            return;
        }

        string password = _passwordBox.GetComponent<TMP_InputField>().text;
        //Create data for post
        if (password == "")
        {
            _passwordBox.GetComponent<Outline>().effectColor = Color.red;
            _errorBox.SetActive(true);
            _errorBox.GetComponent<TextMeshProUGUI>().text = "Vui lòng nhập mật khẩu";
            return;
        }
        _player = new PlayerAccount { username = username, password = password };

        WWWForm form = new WWWForm();
        form.AddField("username", _player.username);
        form.AddField("password", _player.password);
        StartCoroutine(Login(form));
    }

    IEnumerator Login(WWWForm data)
    {
        using (UnityWebRequest loginRequest = UnityWebRequest.Post(Route.LOGIN, data))
        {
            yield return loginRequest.SendWebRequest();
            _loadingCanvas.SetActive(true);
            if (loginRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonData = loginRequest.downloadHandler.text;
                Debug.Log("message: " + jsonData);
                string accessToken = JsonUtility.FromJson<LoginResponse>(jsonData).acessToken;
                PlayerPrefs.SetString("accessToken", accessToken);
                PlayerPrefs.Save();
            }
            else
            {
                _errorBox.SetActive(true);
                _errorBox.GetComponent<TextMeshProUGUI>().text =
                    "Tài khoản hoặc mật khẩu không đúng";
                Debug.Log("Login Error: " + loginRequest.error);
            }
            _loadingCanvas.SetActive(false);
        }
    }
}
