using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Rigidbody2D _rigidBody = null;
	public void Awake()
	{
		_rigidBody = GetComponent<Rigidbody2D>();

	}
	public void FixedUpdate()
	{
		if (Input.GetKey(KeyCode.A))
			transform.Translate( 0.05f * (Vector3.right));
		else if (Input.GetKey(KeyCode.S))
			transform.Translate(0.05f * (Vector3.left));
	}
}