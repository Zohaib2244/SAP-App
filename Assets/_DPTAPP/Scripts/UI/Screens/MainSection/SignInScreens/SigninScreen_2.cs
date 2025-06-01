using UnityEngine;
using TMPro;
using Scripts.Resources;
using Scripts.UI.Screens;
using Unity.VisualScripting;
using System.Linq;

public class SigninScreen_2 : MonoBehaviour
{
    [SerializeField] private TMP_InputField heightInputField;
    [SerializeField] private TMP_InputField weightInputField;
    [SerializeField] private AdvancedDropdown bodyTypeDropdown;
    private BodyTypes bodyType;
    void OnEnable()
    {
        ResetEverything();
    }
    void Start()
    {
        string[] names = System.Enum.GetNames(typeof(BodyTypes));
        //remove the type None
        names = names.Skip(1).ToArray();
        bodyTypeDropdown.AddOptions(names);
        bodyTypeDropdown.onChangedValue += OnValueChanged;
    }
    public void OnNextButtonClicked()
    {
        if (!ValideHeight()) return;
        if (!ValideWeight()) return;
        UserSignInScreen.Instance.newUserData.AddSignIn2Data(float.Parse(heightInputField.text), float.Parse(weightInputField.text), bodyType);
        UserSignInScreen.Instance.ShowScreen3();
    }
    private bool ValideHeight()
    {
        if (string.IsNullOrEmpty(heightInputField.text) || float.Parse(heightInputField.text) < 0)
        {
            Alert.Instance.ShowAlert("Height is Invalid", false);
            return false;
        }
        return true;
    }
    private bool ValideWeight()
    {
        if (string.IsNullOrEmpty(weightInputField.text) || float.Parse(weightInputField.text) < 0)
        {
            Alert.Instance.ShowAlert("Weight is Invalid", false);
            return false;
        }
        return true;
    }
    private void OnValueChanged(int index)
    {
       switch (index)
       {
        case 0:
            bodyType = BodyTypes.Thin;
            break;
        case 1:
            bodyType = BodyTypes.Normal;
            break;
        case 2:
            bodyType = BodyTypes.Fat;
            break;
        default:
            bodyType = BodyTypes.Fat;
            break;
       }
    }
    void ResetEverything()
    {
        heightInputField.text = "";
        weightInputField.text = "";
        bodyTypeDropdown.SetDefaultText();
    }
}