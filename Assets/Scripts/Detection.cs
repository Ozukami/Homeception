using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Detection : MonoBehaviour
{
  private Monster _monster;

  public void Start()
  {
    _monster = GetComponentInParent<Monster>();
  }

  private void CheckWall (Collider player) {
    if (!player.CompareTag("Player")) return;
    Vector3 positionToPlayer = -(_monster.gameObject.transform.position - player.gameObject.transform.position).normalized;
    transform.LookAt(player.transform);
    if (!Physics.Raycast(transform.position, positionToPlayer, out RaycastHit raycastHit)) return;
    if (!raycastHit.collider.CompareTag("Player")) return;
    _monster.Target = raycastHit.collider.gameObject;
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
