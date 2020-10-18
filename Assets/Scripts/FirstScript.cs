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

    private void Start()
    {
        slider.onValueChanged.AddListener(delegate { SliderChanged(); });
        //Screen.orientation = ScreenOrientation.LandscapeRight;
        slider.minValue = 8;
        slider.maxValue = 128;
        Button clipboardButton = copyToClipBoardButton.GetComponent<Button>();
        clipboardButton.onClick.AddListener(CopyToClipBoardBtn);
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

        passwords.Add(password);
        tmProUI.SetText("New password: " + password);
        charCounter.SetText("Number of characters: " + slider.value);
    }

    void Update()
    {
        RandomizePassword((int)slider.value);  // Gör så att den randomiserar fram och visar ett nytt lösenord varje gång slidern flyttas
        ShowPasswords();
        //TheDevilSpeaksFromTheVoid();
        ClearPasswordsFromList();
        CopyToClipboard();
    }

    void ClearPasswordsFromList()
    {
        if (Input.GetKeyDown(KeyCode.T) && passwords.Count > 0)
        {
            for (int i = passwords.Count - 1; i >= 0; i--)
            {
                passwords.RemoveAt(i);
            }

            tmProUI.SetText("Cleared all passwords.");
        }
    }

    void TheDevilSpeaksFromTheVoid()
    {
        if (Input.GetKeyDown(KeyCode.Space) && passwords.Count > 0)
        {
            int randomInt = Random.Range(0, passwords.Count);
            tmProUI.SetText("The Devil says: " + passwords[randomInt]);
        }

        if (Input.GetKeyDown(KeyCode.Space) && passwords.Count == 0)
        {
            tmProUI.SetText("The Devil is silent..." + passwords.Count);
        }
    }
    
    void ShowPasswords()
    {
        string tmp = "";
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Man plockar ut det som ligger i passwords och lägger det i password (som är tillfällig för loopen)
            // För varje grej vi plockar ut ur passwords och lägger i password så gör vi detta (Debug.Log just nu).
            // Du fattar.. man behöver bara ta samma variabeltyp som passwords innehåller
            if (passwords.Count == 0)
            {
                tmProUI.SetText("You have no saved passwords.");
            }

            foreach (string password in passwords)
            {
                tmp = tmp + password + "\n";
                tmProUI.SetText("These are your saved passwords: \n" + tmp);
            }
        }
    }

    // Skulle kunna göra så att den randomiserar fram unika symboler / inte 2 av samma i rad osv
    void RandomizePassword(int passwordLength)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            string password = "";

            for (int i = 0; i < passwordLength; i++)
            {
                char c = (char)(Random.Range(33, 126));  
                password = password + c;
            }

            passwords.Add(password);
            tmProUI.SetText("New password: " + password);
        }
    }

    private void OnMouseDown() // PurpleSquare
    {
        /*
        string password = "";
        int passwordLength = (int)slider.value;

        for (int i = 0; i < passwordLength; i++)
        {
            char c = (char)(Random.Range(33, 126));
            password = password + c;
        }

        passwords.Add(password);
        tmProUI.SetText("New password: " + password);
        */
    }

    public void CopyToClipBoardBtn()
    {
        string tmp = "";
        int number = passwords.Count;
        tmp = passwords[number - 1];
        tmProUI.SetText("Copied to Clipboard");
        tmp.CopyToClipboard();
    }

    public void CopyToClipboard()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            string tmp = "";

            foreach (string password in passwords)
            {
                tmp = tmp + password + "\n";
                tmProUI.SetText("Copied to Clipboard");
            }

            tmp.CopyToClipboard();
        }
    }
}

public static class ClipboardExtension
{
    public static void CopyToClipboard(this string str)
    {
        GUIUtility.systemCopyBuffer = str;
    }
}
