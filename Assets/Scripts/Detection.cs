using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Detection : MonoBehaviour
{
  private Monster monster;

  void Start()
  {
    monster = GetComponentInParent<Monster>();
  }

  private void CheckWall (Collider player, bool enter = true) {
    if (player.CompareTag("Player")) {
      RaycastHit hit;
      Vector3 positionToPlayer = -(monster.gameObject.transform.position - player.gameObject.transform.position).normalized;
      Debug.DrawLine(monster.gameObject.transform.position, positionToPlayer, Color.red);
      Ray ray = new Ray(player.gameObject.transform.position, monster.gameObject.transform.position);
      if (Physics.Raycast(ray, out hit)) {
        Debug.Log(hit.collider.tag);
        if (hit.collider != null && hit.collider.tag == "Wall") {
          Debug.Log(hit.collider.tag);
          //GetComponentInParent<Monster>().Target = (enter ? player.gameObject : null);
        }
      }
    }
  }

  private void OnTriggerEnter (Collider other) {
    CheckWall(other);
  }

  private void OnTriggerStay (Collider other) {
    CheckWall(other);
  }

  private void OnTriggerExit (Collider other) {
    CheckWall(other, false);
    /*if (other.CompareTag("Player")) {
      GetComponentInParent<Monster>().Target = null;
    }*/
  }
}
