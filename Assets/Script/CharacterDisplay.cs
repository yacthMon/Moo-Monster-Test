using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDisplay : MonoBehaviour
{
  [SerializeField]
  private int spriteNumber = 0;
  private int maxSpriteAvailable = 5;
  private SpriteRenderer spriteRenderer;
  
  void Start()
  {
    spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    LoadSprite(spriteNumber);
  }
  
  void LoadSprite(int spriteNumber)
  {
    Sprite sprite = Resources.LoadAll<Sprite>(@"Sprites/Characters/")[spriteNumber];
    if(ReferenceEquals(sprite, null)) {
      // Load default
      Debug.Log("Load default");
      sprite = Resources.LoadAll<Sprite>(@"Sprites/Characters/moo")[0];
    }
    spriteRenderer.sprite = sprite;
  }
  
  public void ChangeCharacter(int characterNumber) {
    this.LoadSprite(characterNumber);
  }

  [ContextMenu("Next Character")]
  public void NextCharacter(){
    spriteNumber = ++this.spriteNumber % maxSpriteAvailable;
    this.ChangeCharacter(spriteNumber);
  }

  [ContextMenu("Previous Character")]
  public void PreviousCharacter(){
    spriteNumber = this.spriteNumber == 0 ? maxSpriteAvailable-1 : --this.spriteNumber % maxSpriteAvailable;
    this.ChangeCharacter(spriteNumber);
  }
 
}
