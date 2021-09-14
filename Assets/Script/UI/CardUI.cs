using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
  Button button;
  TaskCard card;
  [SerializeField]
  Text characterTxt;
  [SerializeField]
  Text manaCostTxt;
  [SerializeField]
  Text cardTitleTxt;
  
  void Start()
  {
    button = GetComponent<Button>();
    button.interactable = false;
    this.UpdateButton(PlayerController.instance.GetMana());
    PlayerController.instance.OnManaUpdateHanlder += UpdateButton;
    button.onClick.AddListener(UseCard);
  }

  void UpdateButton(int currentMana){
    if(ReferenceEquals(this.card, null)) return;
    button.interactable = PlayerController.instance.IsManaAvailable(this.card.GetCardManaCost());
  }
  
  public void SetCard(TaskCard card){
    card.Mount();
    this.card = card;
    manaCostTxt.text = card.GetCardManaCost()+"";
    cardTitleTxt.text = card.GetCardTitle();
    characterTxt.text = card.character.GetCharacterName();
  }

  public void UseCard(){
    button.interactable = false;
    this.card.Use();
    PlayerController.instance.OnManaUpdateHanlder -= UpdateButton;
    Destroy(this.gameObject);
  }

  public string GetCardTitle(){
    return this.card.GetCardTitle();
  }

  public TaskCard.CardType GetCardType(){
    return this.card.type;
  }

}
