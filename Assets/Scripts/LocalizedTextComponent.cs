using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LocalizedTextComponent : MonoBehaviour
{
    [SerializeField] private string tableReference; //name of accessed table
    [SerializeField] private string localizationKey; //key to access translation

    private LocalizedString localizedString;
    private Text textComponent;

    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponent<Text>();
        localizedString = new LocalizedString { TableReference = tableReference, TableEntryReference = localizationKey };

        LocalizationSettings.SelectedLocaleChanged += UpdateText;

        //var frenchLocale = LocalizationSettings.AvailableLocales.GetLocale("fr");
        //LocalizationSettings.SelectedLocale = frenchLocale;
    }

    void UpdateText(Locale locale)
    {
        textComponent.text = localizedString.GetLocalizedString(); //sets the text to translated string
        //textComponent = GetComponent<Text>();
    }

    private void OnEnable()
    {
        
    }

    private void OnDestroy()
    {
        LocalizationSettings.SelectedLocaleChanged -= UpdateText;
    }
}
