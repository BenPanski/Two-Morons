using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EffectType
{
    IncreaseHealth,
    IncreaseSpeed,
    IncreaseDamage,
    IncreaseDefense,
    // Add more effect types as needed
}
public enum Rarity
{
    Legendary,
    Epic,
    Rare,
    Common,
    // Add more effect types as needed
}
[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrades/Upgrade")]
public class Upgrade : ScriptableObject
{
    public string upgradeName;
    public Image icon;
    public Rarity rarity;
    public EffectType effectType;
    public int effectValue; // The value associated with the effect (e.g., amount of health increase)

    // You can add more properties here as needed
}
