using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	[SerializeField]private Transform target;
	private Vector3 smoothMovement;
	[SerializeField] private float movementSpeed;
	[SerializeField] private float offset;
	[SerializeField] private Player _player;

	void Update() {

		offset = SetOffset();
		smoothMovement = Vector3.Lerp(transform.position, target.position + Vector3.right * offset, movementSpeed);
		transform.position = smoothMovement;
	}

	float SetOffset() {

		if (_player.point == PlayerPoint.CENTER) {

			return 0;
		} else if (_player.point == PlayerPoint.LEFT) {

			return 0.5f;
		} else {

			return -0.5f;
		}
	}
}