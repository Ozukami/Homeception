using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingSpot : MonoBehaviour
{
  public Sprite completePainting;

  private GameObject _painting;
  private SpriteRenderer _spriteRenderer;
  [SerializeField] private GameObject door;

  private bool IsPaintingLocked => _painting != null;

  private void Awake () {
    _spriteRenderer = GetComponent<SpriteRenderer>();
    if (transform.childCount == 0) return;
    GameObject child = transform.GetChild(0).gameObject;
    if (child.CompareTag("Painting")) _painting = child;
  }

  public void Solve (bool playerHasKey) {
    if (IsPaintingLocked && !playerHasKey) return;
    _spriteRenderer.sprite = completePainting;
    door.GetComponent<Door>().isPaintingLocked = false;
  }
}
