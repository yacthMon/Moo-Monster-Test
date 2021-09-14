using UnityEngine;
using UnityEngine.UI;

public class CharacterLabel : MonoBehaviour
{ 
  [SerializeField]
  GameObject labelPrefab;
  [SerializeField]
  RectTransform CanvasRect;

  void Start()
  {
    // Clear child
    foreach (Transform child in transform) {
     GameObject.Destroy(child.gameObject);
    }
    // Create label for player character
    foreach(Character playerCharacter in PlayerController.instance.GetAllCharacter()){
      Debug.Log("Create player label");
      CreateLabel(playerCharacter);
    }
    // Create label for enemy character
    foreach(Character enemyCharacter in EnemyController.instance.GetAllCharacter() ){
      Debug.Log("Create enemy label");
      CreateLabel(enemyCharacter);
    }
  }

  void CreateLabel(Character character){
    GameObject cloneLabel = GameObject.Instantiate(labelPrefab, transform);
    Vector2 ViewportPosition= Camera.main.WorldToViewportPoint(character.gameObject.transform.position);
    Vector2 WorldObject_ScreenPosition=new Vector2(
    ((ViewportPosition.x*CanvasRect.sizeDelta.x)-(CanvasRect.sizeDelta.x*0.5f)),
    ((ViewportPosition.y*CanvasRect.sizeDelta.y)-(CanvasRect.sizeDelta.y*0.5f)) - 20f);
    cloneLabel.GetComponent<RectTransform>().anchoredPosition = WorldObject_ScreenPosition;
    Text labelTxt = cloneLabel.GetComponent<Text>();
    labelTxt.text = character.GetCharacterName() + " " + character.GetHP() + "/" + character.GetMaxHP();
    character.OnHpUpdateHandler += (int current, int max)=>{
      labelTxt.text = character.GetCharacterName() + " " + current + "/" + max;
    };
  }
}
