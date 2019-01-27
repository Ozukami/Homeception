using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour {

  public GameObject Target;

  private NavMeshAgent _navMeshAgent;
  private Animator _feetAnimator;

  private static readonly int IsWalking = Animator.StringToHash("isWalking");

  private void Awake () {
    _feetAnimator = transform.Find("Feet").GetComponent<Animator>();
    _navMeshAgent = GetComponent<NavMeshAgent>();
  }

  // Start is called before the first frame update
  void Start() {

  }

  // Update is called once per frame
  void Update()
  {
    if (Target) {
      _navMeshAgent.SetDestination(Target.transform.position);
    }

    _feetAnimator.SetBool(IsWalking, _navMeshAgent.hasPath);
  }
}
