// anything that implements this interface can be a target
public interface ITargetable
{
  void TakeDamage(int damageAmount);
  void Heal(int heaelAmount);
  string GetTargetName();
}
