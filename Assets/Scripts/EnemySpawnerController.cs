using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawnerController : MonoBehaviour {
  public GameObject enemyPrefab;

  public bool stopTurn = false;

  public int waves = 0;

  internal ArrayList enemys = new ArrayList();

  public DejavuController dejavuController;

  public FirstPersonController playerController;

  public Target playerTarget;

  public Weapon playerWeapon;


  private void spawn() {
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
    if (enemys.Count == 0 && waves > 0) {
      spawn();
      waves--;
    }
  }

  internal void applyBuffs(float buffMultiplier, int buffType, bool applyToEnemies) {

    // Health attack speed
    switch (buffType) {
      case 0: {// Health
          float newHealth = playerTarget.maxHealth + playerTarget.maxHealth * buffMultiplier;
          float deltaHealth = newHealth - playerTarget.maxHealth;
          playerTarget.maxHealth = newHealth;
          playerTarget.health += deltaHealth;
          playerTarget.updateMaxHealth.Invoke(newHealth);
          playerTarget.updateHealthBar.Invoke(playerTarget.health);

          if (applyToEnemies) {
            Target target = enemyPrefab.GetComponent<Target>();
            target.maxHealth *= buffMultiplier + 1;
            target.health = target.maxHealth;
          }
          break;
        }
      case 1: { // Attack
          playerWeapon.damage *= buffMultiplier + 1;
          if (applyToEnemies) {
            EnemyController controller = enemyPrefab.GetComponent<EnemyController>();
            controller.damage *= buffMultiplier + 1;
          }
          break;
        }
      case 2:{ // Speed
          playerController.MoveSpeed *= buffMultiplier + 1;
          playerController.SprintSpeed *= buffMultiplier + 1;

          if(applyToEnemies){
            EnemyController controller = enemyPrefab.GetComponent<EnemyController>();
            controller.speed *= buffMultiplier + 1;
          }
          break;
        }
      default: { break; }
    }
    Debug.Log("Buff mult" + buffMultiplier);
    Debug.Log("Buff type" + buffType);
    Debug.Log("Apply enemies" + applyToEnemies);
  }
}
