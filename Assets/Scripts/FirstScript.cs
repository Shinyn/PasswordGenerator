using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FirstScript : MonoBehaviour
{

    // TO-DO - Fixa slider eller input för att bestämma lösenordets storlek

    List<string> passwords = new List<string>();
    public TextMeshPro tmPro;


    void Update()
    {
        RandomizePassword();
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
                tmPro.SetText("These are your saved passwords: \n" + tmp); // fixa scroll
            }
        }
    }

    void RandomizePassword() // Nästa steg är att låta användaren bestämma längd på password
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            string password = "";

            for (int i = 0; i < 15; i++) // Skulle kunna göra så att den randomiserar fram unika symboler / inte 2 av samma i rad osv
            {
                char c = (char)(Random.Range(33, 126));  
                password = password + c;
            }

            passwords.Add(password);
            tmPro.SetText("New password: " + password);
        }
    }


    public string GetSomeString()
    {
        return "This is comming from a function";
    }

    public void CopyToClipboard()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            string tmp = "";

            foreach (string password in passwords)
            {
                tmp = tmp + password + "\n";
                tmPro.SetText("Copied to Clipboard"); // fixa scroll
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
