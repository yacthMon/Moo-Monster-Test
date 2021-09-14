using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : MonoBehaviour
{
  enum Target {
    player,
    enemy
  }
  [SerializeField]
  Target targetFocus;

  
  Character character;
  float atkSpeed;

  List<Character> characterTargetList;

  Character currentTarget;

  void Start()
  {
    character = GetComponent<Character>();
    atkSpeed = character.GetATKSpeed();
    if(targetFocus == Target.enemy){
      characterTargetList = new List<Character>(EnemyController.instance.GetAllCharacter());
    } else if(targetFocus == Target.player){
      characterTargetList = new List<Character>(PlayerController.instance.GetAllCharacter());
    }
    Invoke("TargetFromTargetList", atkSpeed);
  }

  public void TargetFromTargetList(){
    if(this.character.isDead) return;
    this.currentTarget = characterTargetList[Random.Range(0, characterTargetList.Count)];
    this.currentTarget.ReceiveDamage(character.GetATK());
    if(this.currentTarget.isDead){
      characterTargetList.Remove(this.currentTarget);
      this.currentTarget = null;
      if(characterTargetList.Count > 0) {
        this.currentTarget = characterTargetList[Random.Range(0, characterTargetList.Count)];
      }
    }
    if(!ReferenceEquals(this.currentTarget, null)){
      Invoke("TargetFromTargetList", this.character.GetATKSpeed());
    }
  }

  public Character GetTarget(){
    return this.currentTarget;
  }  
  
}
