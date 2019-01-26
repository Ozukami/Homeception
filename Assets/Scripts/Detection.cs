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

  private void CheckWall (Collider player) {
    if (!player.CompareTag("Player")) return;
    Vector3 positionToPlayer = -(monster.gameObject.transform.position - player.gameObject.transform.position).normalized;
    if (!Physics.Raycast(transform.position, positionToPlayer, out RaycastHit raycastHit)) return;
    if (!raycastHit.collider.CompareTag("Player")) return;
    monster.Target = raycastHit.collider.gameObject;
  }

  private void OnTriggerEnter (Collider other) {
    CheckWall(other);
  }

  private void OnTriggerStay (Collider other) {
    CheckWall(other);
  }

  private void OnTriggerExit (Collider other) {
    if (other.CompareTag("Player")) {
      GetComponentInParent<Monster>().Target = null;
    }
  }
}
