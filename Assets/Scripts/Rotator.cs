using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour 
{
	[SerializeField] private float speed=100;

	private void Update ()
    {
        transform.Rotate(Vector3.up * (speed * Time.deltaTime));
	}
}
