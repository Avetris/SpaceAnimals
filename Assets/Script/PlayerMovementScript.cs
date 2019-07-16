using UnityEngine;
using System.Collections;

public class PlayerMovementScript : MonoBehaviour {
	bool parado, finished, gravedadInversa;
	// Use this for initialization
	void Start () {
		finished = false;
		parado = false;
		gravedadInversa = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!parado) {
			this.GetComponent<Rigidbody2D>().isKinematic = false;
			pulsarBoton ();
		}
		if(this.transform.position.y <= -1.6f || this.transform.position.y >= 1.9f){
			finished = true;
			GameObject.FindGameObjectWithTag ("Background").GetComponent<BackgroundGameScript> ().finish ();
		}
	}

	void pulsarBoton(){
		if (!finished) {
			if(!gravedadInversa){
				if (Input.GetKeyDown (KeyCode.Space)) {
					transform.Translate (Vector2.up * 0.7f);
				}
				foreach (Touch touch in Input.touches) {
					if (touch.phase == TouchPhase.Began) {
						transform.Translate (Vector2.up * 0.7f);
					}
				}
			}else{
				if (Input.GetKeyDown (KeyCode.Space)) {
					transform.Translate (-Vector2.up * 0.7f);
				}
				foreach (Touch touch in Input.touches) {
					if (touch.phase == TouchPhase.Began) {
						transform.Translate (-Vector2.up * 0.7f);
					}
				}
			}
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Meteorito") {
			finished = true;
			GameObject.FindGameObjectWithTag ("Background").GetComponent<BackgroundGameScript> ().finish ();
		} else if (other.tag == "AgujeroNegro") {
			if (other.GetType ().Name == "CircleCollider2D") {
				finished = true;
				GameObject.FindGameObjectWithTag ("Background").GetComponent<BackgroundGameScript> ().finish ();
			}
		} else if (other.tag == "Boss") {
			finished = true;
			GameObject.FindGameObjectWithTag ("Background").GetComponent<BackgroundGameScript> ().finish ();
		} else if (other.tag == "Ship") {
			finished = true;
			GameObject.FindGameObjectWithTag ("Background").GetComponent<BackgroundGameScript> ().finish ();

		}
	}
	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "AgujeroNegro") {
			if (other.GetType ().Name == "BoxCollider2D") {
				if (other.transform.position.y > transform.position.y) {
					gravedadInversa = true;
					transform.Translate (Vector2.up * Time.deltaTime * 2.3f);
				} else {
					transform.Translate (-Vector2.up * Time.deltaTime * 0.8f);
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "AgujeroNegro") {
			if (other.GetType ().Name == "BoxCollider2D") {
				if (other.transform.position.y > transform.position.y) {
					gravedadInversa = false;
				}
			}
		}
	}
}
