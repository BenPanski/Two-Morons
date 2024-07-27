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

    public void OnUpgradeSelection(List<Upgrade> upgradesFromPool,int PlayerNumber)
    {
        for (int i = 0; i < upgradesFromPool.Count - 1; i++)
        {
            UpgradeTexts[i].text = upgradesFromPool[i].name;
            UpgradeIcons[i] = upgradesFromPool[i].icon;
        }
        PlayerNumberText.text = "Player " + PlayerNumber;
        UpgradeSelectionScreen.SetActive(true);
    }
    public void ChooseUpgrade(int UpgradeIndex) 
    {
       GameManager.Instance.ManageChosenUpgrade(UpgradeIndex);
       
    }

    public void CloseUpgradeSelection()
    {
        UpgradeSelectionScreen.SetActive(false);
    }
}
