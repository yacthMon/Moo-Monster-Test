using UnityEngine;

[CreateAssetMenu(fileName = "New Critical Card", menuName = "Card/Target/Critical", order = 0)]
public class CriticalOnTarget : TaskCard
{
  [SerializeField]
  int criticalRate;
  
  public override void Action(){
    int damageToDeal = this.character.GetATK() * criticalRate;
    this.character.GetTarget()?.ReceiveDamage(damageToDeal);
  }
}
