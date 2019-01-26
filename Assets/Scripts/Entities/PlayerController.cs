using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
  [SerializeField] private float _playerAccSpeed = 1f;
  [SerializeField] private float _playerDecSpeed = 0.8f;
	[SerializeField] private float _playerMaxSpeed = 5f;

  private Rigidbody _playerRigidBody;
  private Vector3 _playerMovement;

  private void Awake() {
    _playerRigidBody = GetComponent<Rigidbody>();
    _playerMovement = Vector3.zero;
  }

  private void Update() {
    _playerMovement = new Vector3(
      (Input.GetAxis("Horizontal") > 0.2f || Input.GetAxis("Horizontal") < -0.2f ? Input.GetAxis("Horizontal") : 0),
      0,
      (Input.GetAxis("Vertical") > 0.2f || Input.GetAxis("Vertical") < -0.2f ? Input.GetAxis("Vertical") : 0)
    );

    Debug.DrawLine(transform.position, transform.position - new Vector3(
        _playerRigidBody.velocity.x,
        _playerRigidBody.velocity.y,
        0
      ),
    Color.blue);
  }

  private void FixedUpdate() {

		if (Math.Abs(_playerMovement.x) > 0.02 || Math.Abs(_playerMovement.y) > 0.02)
			_playerRigidBody.velocity = _playerMovement * _playerAccSpeed;
		else
			_playerRigidBody.velocity *= _playerDecSpeed;
		_playerRigidBody.velocity = Vector3.ClampMagnitude(_playerRigidBody.velocity, _playerMaxSpeed);
  }
}
