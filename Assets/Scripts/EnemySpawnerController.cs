using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawnerController : MonoBehaviour
{
  public GameObject enemyPrefab;

  public bool stopTurn = false;

  public int waves = 0;

  internal ArrayList enemys = new ArrayList();

  public DejavuController dejavuController;


  private void spawn(){
    int i = 0;
    foreach (Transform child in transform) {
      GameObject newEnemy = Instantiate(enemyPrefab, child.position, Quaternion.identity);
      enemys.Add(newEnemy);
      Target target = newEnemy.GetComponent("Target") as Target;
      target.dieCallBack = new UnityEvent();
      target.dieCallBack.AddListener(() => {
        enemys.Remove(newEnemy);  
      });
      i++;
    }
  }

  private void Update() {
    if(enemys.Count == 0 && waves > 0){
      spawn();
      waves--;
    }
  }

  internal void applyBuffs(float buffMultiplier, int buffType, bool applyToEnemies) {
    Debug.Log("Buff mult" + buffMultiplier);
    Debug.Log("Buff type" + buffType);
    Debug.Log("Apply enemies" + applyToEnemies);
  }
}
