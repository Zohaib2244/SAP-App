using UnityEngine;
using TMPro;
using Scripts.Resources;
using System;
using Scripts.UI.Screens;

public class SigninScreen_1 : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private TMP_InputField ageInputField;
    [SerializeField] private AdvancedDropdown genderDropdown;
    private Gender gender;

    void Start()
    {
        //add value from HelloEnum to the dropdown
        string[] names = Enum.GetNames(typeof(Gender));
        genderDropdown.AddOptions(names);
        genderDropdown.onChangedValue += OnValueChanged;
    }
    void OnEnable()
    {
        ResetEverything();
    }
    public void OnNextButtonClicked()
    {
        if(!ValideName()) return;
        if(!ValideAge()) return;
        UserSignInScreen.Instance.newUserData.AddSignIn1Data(nameInputField.text, int.Parse(ageInputField.text), gender);
        UserSignInScreen.Instance.ShowScreen2();
    }
    private bool ValideName()
    {
        if (string.IsNullOrEmpty(nameInputField.text))
        {
            Alert.Instance.ShowAlert("Name is Invalid", false);
            return false;
        }
        return true;
    }
    private bool ValideAge()
    {
        if (string.IsNullOrEmpty(ageInputField.text) || int.Parse(ageInputField.text) < 0)
        {
            Alert.Instance.ShowAlert("Age is Invalid", false);
            return false;
        }
        return true;
    }
    private void OnValueChanged(int index)
    {
        gender = (Gender)index;
    }
    void ResetEverything()
    {
        nameInputField.text = "";
        ageInputField.text = "";
        genderDropdown.SetDefaultText();
    }
}