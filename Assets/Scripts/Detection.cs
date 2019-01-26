using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Detection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

  private void OnTriggerEnter (Collider other) {
    if (other.CompareTag("Player")) {
      GetComponentInParent<Monster>().Target = other.gameObject;
    }
  }

  private void OnTriggerExit (Collider other) {
    if (other.CompareTag("Player")) {
      GetComponentInParent<Monster>().Target = null;
    }
  }
}
