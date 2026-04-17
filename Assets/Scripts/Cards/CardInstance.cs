public class CardInstance
{
  public Card data;
  public int currentCost;

  public CardInstance(Card data)
  {
    this.data = data;

    // initiate to original cost
    this.currentCost = data.manaCost;
  }
}
