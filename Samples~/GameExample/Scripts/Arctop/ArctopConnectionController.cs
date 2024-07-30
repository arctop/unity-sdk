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
    [SerializeField] private GameObject m_SplashPanel;
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
            clearTextTimer = 5f;
            m_MessagePanel.text = "Checking User's status";
            m_ArctopClient.CheckUserLoggedIn();
        }
        else
        {
            clearTextTimer = 10000000f;
            m_MessagePanel.text = "SDK Init Failed";
        }
        m_SplashPanel.SetActive(false);
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
            m_ArctopClient.GetUserCalibrationStatus();
        }
        else
        {
            if (m_UserField.text.Length > 0)
            {
                m_MessagePanel.text = "Wrong password or account doesn't exist.";
            }
        }
        clearTextTimer = 3f;
        m_LoginPanel.SetActive(!success);
    }

    public void OnUserCalibrationStatus(ArctopSDK.UserCalibrationStatus status)
    {
        switch (status)
        {
            case ArctopSDK.UserCalibrationStatus.NeedsCalibration:
                clearTextTimer = 30f;
                m_MessagePanel.text = "User is not calibrated. Please use the Arctop app to calibrate.";
                break;
            case ArctopSDK.UserCalibrationStatus.CalibrationDone:
                clearTextTimer = 30f;
                m_MessagePanel.text = "User models are being calibrated. Please check again in a few minutes.";
                break;
            case ArctopSDK.UserCalibrationStatus.ModelsAvailable:
                clearTextTimer = 3f;
                m_MessagePanel.text = "User models available!";
                m_ScanPanel.SetActive(true);
                break;
            case ArctopSDK.UserCalibrationStatus.Blocked:
                clearTextTimer = 100f;
                m_MessagePanel.text = "User interaction is blocked. Please contact support";
                break;
        }
    }

    public void DisablePredictionButton(){
        startPredictionButton.interactable = false;
    }

    public void StartPrediction()
    {
        m_ArctopClient.StartPrediction(ArctopSDK.Predictions.ZONE);
    }
}