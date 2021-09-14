using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public delegate void OnManaRegenerate(int currentMana);
  public delegate void OnManaUpdate(int currentMana);
  public OnManaRegenerate OnManaRegenerateHanlder;
  public OnManaUpdate OnManaUpdateHanlder;
  


  public static PlayerController instance;
  [SerializeField]
  private int mana = 0;
  [SerializeField]
  private int maxMana = 10;
  [SerializeField]
  private float manaRegendSpeed = 1;
  [SerializeField]
  private Character[] playerCharacters;

  private void Awake()
  {
    if (!instance)
    {
      instance = this;
    }
    else if (instance != this)
    {
      Destroy(instance);
      instance = this;
    }
  }

  void Start()
  {
    StartManaRegenerate();
  }

  public void StopManaRegenerate() {
    CancelInvoke("RegenerateMana");
  }

  public void StartManaRegenerate() {
    InvokeRepeating("RegenerateMana", manaRegendSpeed, manaRegendSpeed);
  }

  private void UpdateMana(int value){
    if(value == 0 ) return;
    mana += value;
    OnManaUpdateHanlder?.Invoke(this.mana);
  }
  
  void RegenerateMana(){
    UpdateMana(mana == maxMana ? 0 : 1);
    OnManaRegenerateHanlder?.Invoke(this.mana);
  }

  public bool IsManaAvailable(int cost){
    return this.mana >= cost;
  }

  public bool UseMana(int cost){
    if(this.mana - cost >= 0){
      UpdateMana(-cost);
      return true;
    } else {
      return false;
    }
  }

  public int GetMana(){
    return this.mana;
  }

  public Character GetRandomPlayerCharacter(){
    int characterCount = this.playerCharacters.Length;
    return characterCount > 0 ? this.playerCharacters[Random.Range(0, characterCount)] : null;
  }

  public Character[] GetAllCharacter(){
    return this.playerCharacters;
  }
  
}
