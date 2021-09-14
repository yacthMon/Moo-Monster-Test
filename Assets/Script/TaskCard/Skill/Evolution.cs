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
  public override CardType type { get { return CardType.evolution; }}

  public override void Mount(){
    if(this.character.isEvolution){
      Character[] characters = PlayerController.instance.GetAllCharacter();
      foreach(Character character in characters){
        if(character.GetCharacterName() != this.character.GetCharacterName() && !character.isEvolution){
          // set to other chracter that doesn't evolution yet
          this.SetCharacter(character);
          return;
        }
      }
      // no other character available to evolution
      // replace with new card
      CardController.instance.RemoveOnHandCard(this.GetCardTitle(), true);
    }
  }
  
  public override void Action(){
    this.character.Evolution(spriteIndex,atkBonus,atkSpeedBonus);
    CardController.instance.RemoveTaskCardByName(this.GetCardTitle());
  }
}
