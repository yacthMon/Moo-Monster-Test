using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaDisplay : MonoBehaviour
{

  void Start()
  {
    Text text = GetComponent<Text>();
    PlayerController.instance.OnManaUpdateHanlder += (int currentMana)=>{
      text.text = "Mana : " + currentMana;
    };
  }
}
