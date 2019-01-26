using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour {

  public GameObject Target;

  private NavMeshAgent _navMeshAgent;

  private void Awake () {
    _navMeshAgent = GetComponent<NavMeshAgent>();
  }

  // Start is called before the first frame update
  void Start() {
    _navMeshAgent.SetDestination(Target.transform.position);
  }

  // Update is called once per frame
  void Update()
  {

  }
}
