using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController2D : MonoBehaviour
{
  [SerializeField] private float playerAccSpeed = 1f;
  [SerializeField] private float playerDecSpeed = 0.8f;
	[SerializeField] private float playerMaxSpeed = 5f;

  private Rigidbody2D playerRigidBody;
  private Vector2 playerMovement;

  private void Awake() {
    playerRigidBody = GetComponent<Rigidbody2D>();
    playerMovement = Vector2.zero;
  }

  private void Update() {
    playerMovement = new Vector2(
      (Input.GetAxis("Horizontal") > 0.2f || Input.GetAxis("Horizontal") < -0.2f ? Input.GetAxis("Horizontal") : 0),
      (Input.GetAxis("Vertical") > 0.2f || Input.GetAxis("Vertical") < -0.2f ? Input.GetAxis("Vertical") : 0)
    );

    Debug.DrawLine(transform.position, transform.position - new Vector3(
        playerRigidBody.velocity.x,
        playerRigidBody.velocity.y,
        0
      ),
    Color.blue);

    Vector2 positionOnScreen = Camera.main.WorldToViewportPoint (transform.position);
    Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
    float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
    transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
  }

  float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
      return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
  }

  private void FixedUpdate() {

		if (playerMovement.x != 0 || playerMovement.y != 0)
			playerRigidBody.velocity = playerMovement * playerAccSpeed;
		else
			playerRigidBody.velocity *= playerDecSpeed;
		playerRigidBody.velocity = Vector3.ClampMagnitude(playerRigidBody.velocity, playerMaxSpeed);
  }
}
