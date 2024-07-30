using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using com.arctop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArctopConnectionController : MonoBehaviour
{
    [SerializeField] private ArctopNativeClient m_ArctopClient;
    [SerializeField] private TMP_InputField m_UserField;
    [SerializeField] private TMP_InputField m_PasswordField;
    [SerializeField] private GameObject m_LoginPanel;
    [SerializeField] private GameObject m_ScanPanel;
    [SerializeField] private TMP_Text m_MessagePanel;
    [SerializeField] private Button startPredictionButton;

    private float clearTextTimer = 0;
    void Update(){
        if (clearTextTimer < 0){
            m_MessagePanel.text = "";
        }

        clearTextTimer -= Time.deltaTime;
    }

    public void SDKInitCallback(bool success)
    {
        if (success)
        {
            m_ArctopClient.CheckUserLoggedIn();
        }
        else
        {
            m_MessagePanel.text = "SDK Init Failed";
        }
    }

    public void OnLoginButtonClicked()
    {
        m_ArctopClient.LoginUser(m_UserField.text , m_PasswordField.text);
        // m_ArctopClient.LoginUser();
    }

    public void OnLoginResponse(bool success)
    {
        Debug.Log($"OnLoginResponse {success}");
        if (success)
        {
            m_MessagePanel.text = "User Logged in!";
        }
        else{
            if (m_UserField.text.Length > 0)
                m_MessagePanel.text = "Wrong password or account doesn't exist.";
        }
        clearTextTimer = 3f;

        m_LoginPanel.SetActive(!success);
        m_ScanPanel.SetActive(success);
    }

    public void DisablePredictionButton(){
        startPredictionButton.interactable = false;
    }

    public void StartPrediction()
    {
        m_ArctopClient.StartPrediction(ArctopSDK.Predictions.ZONE);
    }
}