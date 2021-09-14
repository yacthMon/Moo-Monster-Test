using System;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{

  public static CardController instance;

  [SerializeField]
  List<CardGenerateDetail> generateTaskCardList;
  // List<CardGenerateDetail> taskCardList;
  [SerializeField]
  GameObject card;
  [SerializeField]
  float newCardSpeed = 3f;
  int maxCard = 4;

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
    // Clear child
    foreach (Transform child in transform) {
     GameObject.Destroy(child.gameObject);
    }
    // for(int i=0;i<maxCard;i++){
    //   this.GetNewCard();
    // }
    Invoke("GetNewCard", newCardSpeed);
  }

  public GameObject GetNewCard() {
    if(transform.childCount > maxCard) return null;
    // TaskCard taskCard = taskCardList[Random.Range(0,taskCardList.Count)];
    CardGenerateDetail generateTaskCard = generateTaskCardList[UnityEngine.Random.Range(0,generateTaskCardList.Count)];
    if(generateTaskCard.isOnlyOne){
      foreach(Transform existCard in transform){
        if(existCard.GetComponent<CardUI>().GetCardTitle() == generateTaskCard.taskCard.GetCardTitle()){
          if(!ReferenceEquals(existCard,null)){
            // found an exist card for onlyOne card
            // random a new one
            return GetNewCard();
          }
        }
      }
      
    }
    TaskCard taskCard = generateTaskCard.taskCard;
    GameObject newCard = GameObject.Instantiate(card, this.transform);
    Character playerCharacter = generateTaskCard.specificCharacter ?? PlayerController.instance.GetRandomPlayerCharacter();
    taskCard.SetCharacter(playerCharacter);
    newCard.GetComponent<CardUI>().SetCard(taskCard);
    Invoke("GetNewCard", newCardSpeed);
    return newCard;
  }

  public void RemoveTaskCardByName(string taskCardName){
    CardGenerateDetail toRemove = generateTaskCardList.Find((CardGenerateDetail detailToRemove) => detailToRemove.taskCard.GetCardTitle() == taskCardName);
    generateTaskCardList.Remove(toRemove);
  }

  public void AddTaskCard(TaskCard taskCard, Character specificCharacter = null, bool isOnlyOne = false){
    CardGenerateDetail generateDetail = new CardGenerateDetail(){
      taskCard = taskCard,
      specificCharacter = specificCharacter,
      isOnlyOne = isOnlyOne
    };
    generateTaskCardList.Add(generateDetail);
  }

}

[Serializable]
class CardGenerateDetail {
  public TaskCard taskCard;
  public Character specificCharacter;
  public bool isOnlyOne;
}
