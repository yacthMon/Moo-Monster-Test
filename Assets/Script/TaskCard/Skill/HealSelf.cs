using UnityEngine;

[CreateAssetMenu(fileName = "New Heal Card", menuName = "Card/Self/Heal", order = 0)]
public class HealSelf : TaskCard
{
  [SerializeField]
  int healPoint;
  
  public override void Action(){
    this.character.RestoreHealth(healPoint);
  }
}
