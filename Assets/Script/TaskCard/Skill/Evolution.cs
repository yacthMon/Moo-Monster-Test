using UnityEngine;

[CreateAssetMenu(fileName = "New Heal Card", menuName = "Card/Self/Evolution", order = 0)]
public class Evolution : TaskCard
{
  [SerializeField]
  int spriteIndex;
  [SerializeField]
  int atkBonus;
  [SerializeField]
  float atkSpeedBonus;
  
  
  public override void Action(){
    this.character.Evolution(spriteIndex,atkBonus,atkSpeedBonus);
    CardController.instance.RemoveTaskCardByName(this.GetCardTitle());
  }
}
