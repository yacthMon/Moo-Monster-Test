using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class TaskCard : ScriptableObject
{
  [SerializeField]
  private int manaCost = 1;
  [SerializeField]
  private string cardTitle = "";
  
  public Character character {private set; get;}

  public abstract void Action();

  public bool isAvailable(){
    return false;
  }

  public void Use(){
    if(!PlayerController.instance.IsManaAvailable(manaCost)) return;
    PlayerController.instance.UseMana(manaCost);
    this.Action();
  }

  public string GetCardTitle(){
    return this.cardTitle;
  }

  public int GetCardManaCost(){
    return this.manaCost;
  }

  public void SetCharacter(Character character){
    this.character = character;
  }

}
