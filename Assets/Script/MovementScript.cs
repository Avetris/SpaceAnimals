using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate (-Vector2.right * Time.deltaTime);
		if (transform.position.x < -10) {
			Destroy (this.gameObject);
		}
	}
}
