using UnityEngine;
using System.Collections;

public class ShipMovementScript : MonoBehaviour {
	bool up;
	bool stop;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("Elegir", 0, 2);
	
	}

	void Elegir(){
		int numero = Random.Range (0, 3);
		if (numero == 1) {
			stop = false;
			up = true;
		} else if (numero == 2) {
			stop = false;
			up = false;
		} else {
			stop = true;
		}
	}

	// Update is called once per frame
	void Update () {
		this.transform.Translate (-Vector2.right * Time.deltaTime);
		if (transform.position.x < -5) {
			Destroy (this.gameObject);
		}if (!stop) {
			if (up) {
				this.transform.Translate (Vector2.up * Time.deltaTime);
			} else {
				this.transform.Translate (-Vector2.up * Time.deltaTime);
			}
		}
		if (transform.position.y <= -1.5f) {
			up = true;
		}
		if (transform.position.y >= 1.7f) {
			up = false;
		}
	}
}
