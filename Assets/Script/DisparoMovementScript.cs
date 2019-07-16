using UnityEngine;
using System.Collections;

public class DisparoMovementScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector2.left * Time.deltaTime*4);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			GameObject.FindGameObjectWithTag ("Background").GetComponent<BackgroundGameScript> ().finish ();
		}
	}
}
