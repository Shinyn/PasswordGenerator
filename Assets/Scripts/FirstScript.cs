using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FirstScript : MonoBehaviour
{
    List<string> passwords = new List<string>();
    public TextMeshProUGUI tmProUI;
    public TextMeshProUGUI charCounter;
    public Slider slider;
    public Button copyToClipBoardButton;
    bool showingMessage = false;
    float messageCooldown = 2f;

    private void Start()
    {
        slider.onValueChanged.AddListener(delegate { SliderChanged(); });
        Screen.orientation = ScreenOrientation.Portrait;
        slider.minValue = 8;
        slider.maxValue = 128;
        Button clipboardButton = copyToClipBoardButton.GetComponent<Button>();
        clipboardButton.onClick.AddListener(CopyToClipBoardBtn);
    }

    void Update()
    {
        toastUptime();
    }

    void SliderChanged() // Ändrar tmProUI till lösenordet som randomiseras fram när man flyttar slidern vid varje förflyttning
    {
        string password = "";
        int passwordLength = (int)slider.value;

        for (int i = 0; i < passwordLength; i++)
        {
            char c = (char)(Random.Range(33, 126));
            password = password + c;
        }
        passwords.Clear();
        passwords.Add(password);
        tmProUI.SetText("New password: \n\n" + password);
        charCounter.SetText("Number of characters: " + slider.value);
    }

    void toastUptime()
    {
        if (showingMessage)
        {
            if (messageCooldown > 0)
            {
                messageCooldown -= Time.deltaTime;
            }
            else
            {
                messageCooldown = 2f;
                showingMessage = false;
                int password = passwords.Count - 1;
                tmProUI.SetText("New password: \n\n" + passwords[password]);
            }
        }
    }

    public void CopyToClipBoardBtn()
    {
        string tmp = "";
        int number = passwords.Count;
        tmp = passwords[number - 1];
        tmProUI.SetText("Copied to Clipboard");
        showingMessage = true;
        tmp.CopyToClipboard();
    }
}

public static class ClipboardExtension
{
    public static void CopyToClipboard(this string str)
    {
        GUIUtility.systemCopyBuffer = str;
    }
}
