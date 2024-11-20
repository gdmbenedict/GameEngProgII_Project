using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Language
{
    public string lang;
    public string title;
    public string play;
    public string quit;
    public string options;
    public string credits;
}

public class LanguageData
{
    public Language[] languages;
}

public class Reader : MonoBehaviour
{
    public TextAsset jsonFile;
    public string selectedLanguage;
    private LanguageData languageData;

    public Text titleText;
    public Text playText;
    public Text quitText;
    public Text optionsText;
    public Text creditsText;

    private string currentLanguage;

    private void Start()
    {
        languageData = JsonUtility.FromJson<LanguageData>(jsonFile.text);

        currentLanguage = "en";
    }

    public void SetLanguage(string newLanguage)
    {
        foreach (Language lang in languageData.languages)
        {
            if (lang.lang.ToLower() == newLanguage.ToLower())
            {
                titleText.text = lang.title;
                playText.text = lang.play;
                optionsText.text = lang.options;
                quitText.text = lang.quit;
                creditsText.text = lang.credits;
                return;
            }
        }
    }

    private void Update()
    {
        if (selectedLanguage != currentLanguage)
        {
            currentLanguage = selectedLanguage;
            SetLanguage(currentLanguage);
        }
    }
}
