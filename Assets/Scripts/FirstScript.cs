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

    // Appvision: Den ultimata randomiseraren. Randomisera ALLT!... eller ultimata inspirationen för kreativa personer. :D

    // Lägg till fler alternativ till alla. Importera bibliotek som täcker varje kategori.
    string[] materials = {"Plastic", "Wood", "Metal", "Edible", "Organic", "Paper", "Concrete", "Sand", "Ceramic", "Textile"};
    string[] scale = {"Giant", "Mini", "Pocket", "Portable", "Wearable", "Inhabitable"};
    string[] device = {"Robot", "Vehicle", "Computer", "Game", "Tool", "Art"};
    string[] action = {"Flying", "Random", "Self-Build", "Underwater", "Stealth", "Disposable"};
    string[] consumer = {"Family", "Personal", "Office", "Home", "Industrial", "Public"};
    string[] poweredBy = {"Manual", "Electric", "Solar", "Wind", "Water", "Clockwork", "Gas", "Animal"};

    void InspirationGenerator()
    {
        int theMaterial = Random.Range(1, materials.Length);
        int theScale = Random.Range(1, scale.Length);
        int theDevice = Random.Range(1, device.Length);
        int theAction = Random.Range(1, action.Length);
        int theConsumer = Random.Range(1, consumer.Length);
        int thePoweredBy = Random.Range(1, poweredBy.Length);

        string invention = scale[theScale] + ", " + materials[theMaterial] + ", " + action[theAction] + ", " +
              poweredBy[thePoweredBy] + ", " +  device[theDevice] + " for the " + consumer[theConsumer];

        Debug.Log("Create a " + invention); // create a: giant, plastic, flying, solar robot for the family
    }

    private void Start()
    {
        
        InspirationGenerator();
        slider.onValueChanged.AddListener(delegate { SliderChanged(); });
        Screen.orientation = ScreenOrientation.Portrait;
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
        passwords.Clear();
        passwords.Add(password);
        tmProUI.SetText("New password: \n\n" + password);
        charCounter.SetText("Number of characters: " + slider.value);
    }

    void cooldown()
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

    void Update()
    {
        cooldown();
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

    void TheDevilSpeaksFromTheVoid() // Error - index out of bound & gör så att Button trycks ner efter nån kombo
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
