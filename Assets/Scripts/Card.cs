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
}
