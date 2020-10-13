using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FirstScript : MonoBehaviour
{
    List<string> passwords = new List<string>();
    public TextMeshPro tmPro;
    public Slider slider;

    private void Start()
    {
        slider.onValueChanged.AddListener(delegate { SliderChanged(); });
        Screen.orientation = ScreenOrientation.LandscapeRight;
        slider.minValue = 8;
        slider.maxValue = 128;
    }

    void SliderChanged() // Ändrar tmPro till lösenordet som randomiseras fram när man flyttar slidern vid varje förflyttning
    {
        string password = "";
        int passwordLength = (int)slider.value;

        for (int i = 0; i < passwordLength; i++)
        {
            char c = (char)(Random.Range(33, 126));
            password = password + c;
        }

        //passwords.Add(password);
        tmPro.SetText("New password: " + password);
    }

    void Update()
    {
        RandomizePassword((int)slider.value);  // Gör så att den randomiserar fram och visar ett nytt lösenord varje gång slidern flyttas
        ShowPasswords();
        TheDevilSpeaksFromTheVoid();
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

            tmPro.SetText("Cleared all passwords.");
        }
    }

    void TheDevilSpeaksFromTheVoid()
    {
        if (Input.GetKeyDown(KeyCode.Space) && passwords.Count > 0)
        {
            int randomInt = Random.Range(0, passwords.Count);
            tmPro.SetText("The Devil says: " + passwords[randomInt]);
        }

        if (Input.GetKeyDown(KeyCode.Space) && passwords.Count == 0)
        {
            tmPro.SetText("The Devil is silent...");
        }
    }
    
    void ShowPasswords()
    {
        string tmp = "";
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Man plockar ut det som ligger i passwords och lägger det i password (som är tillfällig för loopen)
            // För varje grej vi plockar ut ur passwords och lägger i password så gör vi detta (Debug.Log just nu).
            // Du fattar.. 
            if (passwords.Count == 0)
            {
                tmPro.SetText("You have no saved passwords.");
            }

            foreach (string password in passwords)
            {
                tmp = tmp + password + "\n";
                tmPro.SetText("These are your saved passwords: \n" + tmp);
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
            tmPro.SetText("New password: " + password);
        }
    }

    private void OnMouseDown() // PurpleSquare
    {
        string password = "";
        int passwordLength = (int)slider.value;

        for (int i = 0; i < passwordLength; i++)
        {
            char c = (char)(Random.Range(33, 126));
            password = password + c;
        }

        passwords.Add(password);
        tmPro.SetText("New password: " + password);
    }

    void RandomTouchPassword()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            // Sätt den på slidern så det fungerar på mobil?
        }
    }

    public void CopyToClipboard()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            string tmp = "";

            foreach (string password in passwords)
            {
                tmp = tmp + password + "\n";
                tmPro.SetText("Copied to Clipboard");
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
