using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
  [SerializeField] private float _playerAccSpeed = 5f;
  [SerializeField] private float _playerDecSpeed = 0.8f;
  [SerializeField] private float _playerMaxSpeed = 10f;
  [SerializeField] private Camera _camera;
  [SerializeField] private Transform _bodyPosition;
  [SerializeField] private float _bouncingValue;

  private Animator[] _animator;
  private Rigidbody _playerRigidBody;
  private Vector3 _playerMovement;
  private NavMeshAgent _navMeshAgent;
  private bool _isWalking;
  private bool _hasKey = false;

  private void Awake() {
    _animator = GetComponentsInChildren<Animator>();
    _playerRigidBody = GetComponent<Rigidbody>();
    _navMeshAgent = GetComponent<NavMeshAgent>();
    _playerMovement = Vector3.zero;
  }

  private void Update()
  {
    _playerMovement = new Vector3(
      (Input.GetAxis("Horizontal") > 0.2f || Input.GetAxis("Horizontal") < -0.2f ? Input.GetAxis("Horizontal") : 0),
      0,
      (Input.GetAxis("Vertical") > 0.2f || Input.GetAxis("Vertical") < -0.2f ? Input.GetAxis("Vertical") : 0)
    );

    var velocity = _playerRigidBody.velocity;
    var position = transform.position;
    Debug.DrawLine(position, position - new Vector3(
                               velocity.x,
                               0,
                               velocity.z
                             ),
      Color.blue);
    _camera.transform.position = position;

    foreach (Animator animator in _animator) {
      _isWalking = velocity.sqrMagnitude > 0.1f;
      animator.SetBool("isWalking", _isWalking);
    }

    if (_isWalking)
      _bodyPosition.localPosition = new Vector3(0, 0, _bodyPosition.localPosition.z + ((Mathf.Sin(Time.time * 10) > 0) ? +_bouncingValue : -_bouncingValue));
  }

  private void FixedUpdate()
  {
    if (Math.Abs(_playerMovement.x) > 0.02f || Math.Abs(_playerMovement.z) > 0.02f)
      _playerRigidBody.velocity = _playerMovement * _playerAccSpeed;
    else
      _playerRigidBody.velocity *= _playerDecSpeed;
    _playerRigidBody.velocity = Vector3.ClampMagnitude(_playerRigidBody.velocity, _playerMaxSpeed);
  }

  private void OnCollisionStay(Collision other) {
    if (other.gameObject.CompareTag("Door")) OpenDoor(other.gameObject);
  }

  private void OnTriggerEnter(Collider other) {
    if (other.gameObject.CompareTag("Bed")) GoToBed(other.gameObject);
  }

  private void OnTriggerStay(Collider other) {
    if (other.gameObject.CompareTag("Key")) PickUp(other.gameObject);
  }

  #region Actions

  private void GoToBed(GameObject bed) {
    Transform nightLight = bed.transform.Find("Night Light");
    if (nightLight)
      _navMeshAgent.SetDestination(nightLight.gameObject.transform.position);
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

}
