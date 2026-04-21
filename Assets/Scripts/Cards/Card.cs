using UnityEngine;

// enable creating card from Card template, then set the values in inspector
// menuName - object name in create menu in Project window
// fileName - default created object name
[CreateAssetMenu(fileName = "New Card", menuName = "Card")]

// Scriptable Objects - data container
// ScriptableObject - template used to store all the data
public class Card : ScriptableObject
{
  // new - hide name of parent class
  // public new string name;

  public string cardName;
  public string description;

  // image
  public Sprite artwork;

  public int manaCost;
  public int attack;
  public int health;

  public CardType cardType;

  // spell effect
  public CardEffectType effectType;
  public int effectValue;

  // minion battlecry
  // read directly from card
  // trigger in BattleResolver.ResolveMinionSummon() & ResolveMinionSummonFromAI()
  public CardEffectType battlecryEffectType = CardEffectType.None;
  public int battlecryValue = 0;

  // minion deathrattle
  // bind in BoardManager.SummonPlayerMinion(), saved in minion
  // trigger in Minion.Die()
  public CardEffectType deathrattleEffectType = CardEffectType.None;
  public int deathrattleValue = 0;
}

public enum CardType
{
  Minion,
  Spell
}

public enum CardEffectType
{
  None,

  // to enemy hero target
  Damage,

  DrawCard,
}
