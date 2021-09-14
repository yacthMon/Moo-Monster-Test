using UnityEngine;

[CreateAssetMenu(fileName = "New Aoe Attack Card", menuName = "Card/Aoe/AoeAttack", order = 0)]
public class AoeAttack : TaskCard
{
  [SerializeField]
  float damagePercent;
  
  public override void Action(){
    int damageToDeal = (int)(this.character.GetATK() * damagePercent);
    Character[] enemyCharacters = EnemyController.instance.GetAllCharacter();
    foreach(Character character in enemyCharacters){
      character.ReceiveDamage(damageToDeal);
    }
  }
}
