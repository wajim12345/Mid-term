using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class passwordInput : MonoBehaviour
{
    [SerializeField] TMP_Text hintText;
    [SerializeField] TMP_Text showText;
    string validPassword = "";
    private string enteredPassword = "";
    public UnityEvent OnValidPassword;

    private void Start()
    {
        GenerateRandomPassword();
    }

    public void OnButtonPressed(string number)
    {
        if (enteredPassword.Length < 4 || enteredPassword == null)
        {
            enteredPassword += number;
            showText.text = enteredPassword;
        }

        if (enteredPassword.Length == 4)
        {
            CheckPassword();
        }

    }
    private void CheckPassword()
    {
        if (enteredPassword == validPassword)
        {
            showText.text = "Unlocked";
            OnValidPassword?.Invoke();
        }
        else
        {
            showText.text = "Invalid password";
            enteredPassword = null;
        }
    }
    private void GenerateRandomPassword()
    {

        while (validPassword.Length < 4)
        {
            int randomNumber = Random.Range(1, 10);
            validPassword += randomNumber.ToString();
        }
        hintText.text = validPassword;



    }
}
