using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] private GameObject UpgradeSelectionScreen;
    [SerializeField] private Image[] UpgradeIcons;
    [SerializeField] private TMP_Text[] UpgradeTexts;
    [SerializeField] private TMP_Text PlayerNumberText;

    private void OnEnable()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void OnUpgradeSelection(Image[] upgradeIcons, string[] upgradeText, int playerNumber)
    {
        for (int i = 0; i < upgradeIcons.Length-1; i++)
        {
            upgradeText[i] = upgradeText[i];
            UpgradeIcons[i] = upgradeIcons[i];
        }
        PlayerNumberText.text = "Player " + playerNumber;
        UpgradeSelectionScreen.SetActive(true);
    }
}
