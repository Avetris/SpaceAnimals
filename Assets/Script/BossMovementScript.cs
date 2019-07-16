using UnityEngine;
using System.Collections;

public class BossMovementScript : MonoBehaviour {
	GameObject laser;
	bool stop, up, left;
	// Use this for initialization
	void Start () {
		laser = Resources.Load<GameObject> ("Prefab/Laser");
		InvokeRepeating ("Elegir", 0, 2);
		InvokeRepeating ("ElegirAdelante", 0, 2);
		//InvokeRepeating ("Disparar", 0, 1);
		
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

	void ElegirAdelante(){
		if (transform.position.x >= -1) {
			int numero = Random.Range (0, 2);
			if (numero == 1) {
				left = true;
			} else {
				left = false;
			} 
		} else {
			left = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (left) {
			this.transform.Translate (-Vector2.right * Time.deltaTime);
		}
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

	void Disparar(){
		Instantiate (laser, transform.position - new Vector3 (0.5f, 0, 0), Quaternion.identity);
	}
}
