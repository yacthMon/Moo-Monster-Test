using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character : MonoBehaviour
{
  public delegate void OnHPUpdate(int current, int max);
  public OnHPUpdate OnHpUpdateHandler;

  SpriteRenderer spriteRenderer;
  Vector3 originalPos;
  
  [SerializeField]
  private int maxHP = 100;
  [SerializeField]
  private int hp = 100;
  [SerializeField]
  private string characterName = "";
  [SerializeField]
  private int atk = 0;
  [SerializeField]
  float atkSpeed = 0;
  
  public TaskCard blueSkill;
  public TaskCard yellowSkill;
  public bool isEvolution = false;

  public bool isDead {private set; get;}

  private AutoAttack autoAttack;


  void Start()
  {
    this.hp = maxHP;
    originalPos = this.transform.position;
    spriteRenderer = GetComponent<SpriteRenderer>();
    autoAttack = GetComponent<AutoAttack>();
  }

  public void ReceiveDamage(int damage) {
    if(damage <= 0 || isDead) return;
    this.hp -= damage;
    if(this.hp <= 0){
      isDead = true;
      Dead();
      this.hp = 0;
    }
    spriteRenderer.DOColor(Color.red, .2f).SetLoops(2, LoopType.Yoyo).OnComplete(()=>{
      spriteRenderer.color = Color.white;
    });
    transform.DOShakePosition(0.2f, 1, 1, 0,false,false).SetLoops(2, LoopType.Yoyo).OnComplete(()=>{
      this.transform.position = originalPos;
    });;
    OnHpUpdateHandler?.Invoke(this.hp, this.maxHP);
  }

  public void RestoreHealth(int point) {
    if(point <= 0 || isDead) return;
    this.hp += point;
    if(this.hp >= this.maxHP){
      this.hp = this.maxHP;
    }
    spriteRenderer.DOColor(Color.green, .2f).SetLoops(2, LoopType.Yoyo).OnComplete(()=>{
      spriteRenderer.color = Color.white;
    });
    OnHpUpdateHandler?.Invoke(this.hp, this.maxHP);
  }

  private void Dead(){
    this.spriteRenderer.DOKill(false);
    this.spriteRenderer.DOColor(new Color(0.17f,0.17f,0.17f,1), .2f);
  }

  public void Evolution(int index, int atkBonus, float atkSpeedBonus){
    this.atk += atkBonus;
    this.atkSpeed -= atkSpeedBonus;
    switch (index)
    {
        case 3: // Evo Blue
          Debug.Log(this.characterName + " Evolution to Blue");
          CardController.instance.AddTaskCard(blueSkill, this);
          break;
        case 4: // Evo Yellow
          Debug.Log(this.characterName + " Evolution to Yellow");
          CardController.instance.AddTaskCard(yellowSkill, this);
          break;
    }
    isEvolution = true;
    GetComponent<CharacterDisplay>().ChangeCharacter(index);
  }

  public int GetATK(){
    return this.atk;
  }

  public int GetMaxHP(){
    return this.maxHP;
  }

  public int GetHP(){
    return this.hp;
  }

  public float GetATKSpeed(){
    return this.atkSpeed;
  }

  public Character GetTarget(){
    return this.autoAttack.GetTarget();
  }

  public string GetCharacterName(){
    return this.characterName;
  }
  
}
