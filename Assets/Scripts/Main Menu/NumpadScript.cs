using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using System.Text;
using TMPro;
using UnityEngine;

public class NumpadScript : MonoBehaviour
{
    private StringBuilder inputString = new StringBuilder();
    private int maxStringSize = 14;
    private bool textFieldActive = false;

    private int viewDegreesNoText = 30;
    private int viewDegreesWithText = 60;

    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject keyboard;
    [SerializeField] private RadialView MainMenuRadialView;
    [SerializeField] private NavMenu navMenu;

    private void Start()
    {
        keyboard.SetActive(false);
    }

    private void UpdateInputField()
    {
        inputField.text = inputString.ToString();
    }

    public void AddChar(string c)
    {
        if (inputString.Length <= maxStringSize)
        {
            inputString.Append(c);
            UpdateInputField();

            navMenu.updateButtons();
        }
    }

    public void PopChar()
    {
        if (inputString.Length > 0)
        {
            inputString.Remove(inputString.Length - 1, 1);
            UpdateInputField();

            navMenu.updateButtons();
        }

    }

    public string GetCurrentString()
    {
        return inputString.ToString();
    }

    public void ClearCurrentString()
    {
        inputString.Clear();
        UpdateInputField();
        navMenu.updateButtons();
    }

    public void ToggleText()
    {
        if (!textFieldActive)
        {
            textFieldActive = true;
            keyboard.SetActive(true);

            MainMenuRadialView.MaxViewDegrees = viewDegreesWithText;

            return;
        }

        textFieldActive = false;
        keyboard.SetActive(false);

        MainMenuRadialView.MaxViewDegrees = viewDegreesNoText;
    }
}
