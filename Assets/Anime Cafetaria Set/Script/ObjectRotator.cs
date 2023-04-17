using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour {

	public Vector3 rotationAxis;

	void Update(){
		transform.Rotate(rotationAxis * Time.deltaTime);
	}
}
