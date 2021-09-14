using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  public static EnemyController instance;
  [SerializeField]
  Character[] characters;

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
  }

  public Character[] GetAllCharacter(){
    return this.characters;
  }

  public Character GetRandomCharacter(){
    return this.characters[Random.Range(0, this.characters.Length)];
  }
}
