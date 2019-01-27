using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
  [SerializeField] private Camera _camera;
  [SerializeField] private Transform _bodyPosition;
  [SerializeField] private float _playerAccSpeed = 5f;
  [SerializeField] private float _playerDecSpeed = 0.8f;
  [SerializeField] private float _playerMaxSpeed = 10f;
  [SerializeField] private float _bouncingValue;

  private Animator _bodyAnimator;
  private Animator _legsAnimator;
  private Rigidbody _playerRigidBody;
  private Vector3 _playerMovement;
  private NavMeshAgent _navMeshAgent;

  private bool _canControl = true;
  private bool _isWalking;
  private bool _isTurned;
  private bool _wasTurned;
  private bool _end;
  private bool _hasKey = false;
  private string _turnTo;

  private static readonly int IsWalking = Animator.StringToHash("isWalking");
  private static readonly int IsTurned = Animator.StringToHash("isTurned");
  private static readonly int TurnToFront = Animator.StringToHash("turnToFront");
  private static readonly int TurnToBack = Animator.StringToHash("turnToBack");
  private static readonly int Lantern = Animator.StringToHash("lantern");

  private void Awake() {
    GameObject sprite = transform.Find("Sprite").gameObject;
    _bodyAnimator = sprite.transform.Find("Body").GetComponent<Animator>();
    _legsAnimator = sprite.transform.Find("Legs").GetComponent<Animator>();
    _playerRigidBody = GetComponent<Rigidbody>();
    _navMeshAgent = GetComponent<NavMeshAgent>();
    _playerMovement = Vector3.zero;
  }

  private void Update() {
    var position = transform.position;
    _camera.transform.position = position;

    if (!_canControl) {
      ResetVelocity();
      if (_end && _bodyAnimator.GetCurrentAnimatorStateInfo(0).IsName("BodyLantern")) {
        GameManager.instance.DropLantern();
        GoToBed();
      }
      if (!_navMeshAgent.hasPath || !(_navMeshAgent.remainingDistance < 0.1f)) return;
      _end = true;
      _navMeshAgent.ResetPath();
      _legsAnimator.SetBool(IsWalking, _isWalking = false);
      _bodyAnimator.SetBool(IsWalking, _isWalking);
      _bodyAnimator.SetTrigger(Lantern);
    }
    else {
      _playerMovement = new Vector3(
        (Input.GetAxis("Horizontal") > 0.2f || Input.GetAxis("Horizontal") < -0.2f ? Input.GetAxis("Horizontal") : 0),
        0,
        (Input.GetAxis("Vertical") > 0.2f || Input.GetAxis("Vertical") < -0.2f ? Input.GetAxis("Vertical") : 0)
      );
      MovementAnimation(_playerRigidBody.velocity);
    }
  }

  private void FixedUpdate()
  {
    if (Math.Abs(_playerMovement.x) > 0.02f || Math.Abs(_playerMovement.z) > 0.02f)
      _playerRigidBody.velocity = _playerMovement * _playerAccSpeed;
    else
      _playerRigidBody.velocity *= _playerDecSpeed;
    _playerRigidBody.velocity = Vector3.ClampMagnitude(_playerRigidBody.velocity, _playerMaxSpeed);
  }

  private void ResetVelocity () {
    _playerRigidBody.velocity = Vector3.zero;
    _playerRigidBody.angularVelocity = Vector3.zero;
    _playerMovement = Vector3.zero;
  }

  private void OnCollisionStay(Collision other) {
    if (other.gameObject.CompareTag("Door")) OpenDoor(other.gameObject);
  }

  private void OnTriggerEnter(Collider other) {
    if (other.gameObject.CompareTag("Bed")) GoToLight(other.gameObject);
  }

  private void OnTriggerStay(Collider other) {
    if (other.gameObject.CompareTag("Key")) PickUp(other.gameObject);
    if (other.gameObject.CompareTag("Door")) OpenDoor(other.gameObject);
  }

  #region Actions

  private void GoToLight(GameObject bed) {
    Transform nightLight = bed.transform.Find("Night Light");
    if (!nightLight) return;
    _navMeshAgent.SetDestination(nightLight.gameObject.transform.position);
    _canControl = false;
    _isWalking = true;
  }

  private void GoToBed() {
    _navMeshAgent.SetDestination(GameManager.instance.EndPoint.transform.position);
    _legsAnimator.SetBool(IsWalking, _isWalking = true);
    _bodyAnimator.SetBool(IsWalking, _isWalking);
    _isTurned = false;
  }

  private void OpenDoor(GameObject door) {
    if (!Input.GetKeyDown(KeyCode.E)) return;
    door.GetComponent<Door>().Open(_hasKey);
  }

  private void PickUp(GameObject key) {
    if (!Input.GetKeyDown(KeyCode.E)) return;
    key.SetActive(false);
    key.transform.SetParent(transform.parent.Find("Inventory"));
    _hasKey = true;
  }

  #endregion

  private void MovementAnimation (Vector3 velocity) {
    if (_playerMovement.z > 0f)
      _isTurned = true;
    if (_playerMovement.z < 0f)
      _isTurned = false;

    if (_isTurned != _wasTurned) {
      _bodyAnimator.SetTrigger((_wasTurned) ? TurnToFront : TurnToBack);
      _legsAnimator.SetTrigger((_wasTurned) ? TurnToFront : TurnToBack);
    }
    _wasTurned = _isTurned;
    _bodyAnimator.SetBool(IsWalking, _isWalking = velocity.sqrMagnitude > 0.1f);
    _legsAnimator.SetBool(IsWalking, _isWalking);
    _bodyAnimator.SetBool(IsTurned, _isTurned);
    _legsAnimator.SetBool(IsTurned, _isTurned);

    if (_isWalking)
      _bodyPosition.localPosition = new Vector3(0, 0,
        _bodyPosition.localPosition.z + ((Mathf.Sin(Time.time * 10) > 0) ? +_bouncingValue : -_bouncingValue));
  }

}
