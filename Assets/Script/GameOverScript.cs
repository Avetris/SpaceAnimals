using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class GameOverScript : MonoBehaviour {

	// SCRIPT FOR ADJUST RESOLUTION VARIABLES
	float originalWidth = 1280;  // define here the original resolution
	float originalHeight = 728; // you used to create the GUI contents 
	private Vector3 scale;
	
	GUISkin puntuationSkin, animal, replayButtonSkin, mainMenuButtonSkin, tittleLabelSkin;
	Texture2D fondo;
	GUIStyle style;

	string puntuation ;
	bool back, replay;

	// Use this for initialization
	void Start () {
		try{
			admobScript admob = (admobScript) GameObject.Find("ServiciosGoogle").GetComponent<admobScript>();
			admob.HideBanner();
			admob.ShowInterstitial();
		}catch(System.Exception e){
		}
		puntuation = PlayerPrefs.GetString("Puntuacion");
		back = false;
		replay = false;
		style = new GUIStyle ();
		style.font = Resources.Load<Font> ("Labtop Down Under");
		style.fontStyle = FontStyle.Bold;
		style.fontSize = Mathf.Min (Mathf.FloorToInt (Screen.width * 250 / 1000), Mathf.FloorToInt (Screen.height * 250 / 1000));
		style.normal.textColor = Color.green;
		fondo = Resources.Load<Texture2D> ("Texture/Background/fondo" + Random.Range (0, 9));
		cogerTexturas ();
	}
	
	// Update is called once per frame
	void Update () {
		input ();
		StartCoroutine (comprobarBoton ());
	}
	
	IEnumerator comprobarBoton (){
		if (replay) {
			replay = false;
			if(PlayerPrefs.GetString("Effect") == "off"){
				yield return new  WaitForSeconds(0);
			}else{
				GetComponent<AudioSource>().Play ();
				yield return new  WaitForSeconds(1);
			}
			Application.LoadLevel("GameScene");
		} else if (back) {
			back = false;
			if(PlayerPrefs.GetString("Effect") == "off"){
				yield return new  WaitForSeconds(0);
			}else{
				GetComponent<AudioSource>().Play ();
				yield return new  WaitForSeconds(1);
			}
			Application.LoadLevel("MainMenuScene");
		} 
	}
	
	void cogerTexturas(){
		try{
			replayButtonSkin = Resources.Load<GUISkin>("GUISkin/replayButtonSkin");
			mainMenuButtonSkin = Resources.Load<GUISkin>("GUISkin/mainMenuButtonSkin");
			tittleLabelSkin = Resources.Load<GUISkin> ("GUISkin/gameOverTittleLabelSkin");
			puntuationSkin = Resources.Load<GUISkin> ("GUISkin/puntuationSkin");
			if(PlayerPrefs.GetString("Animal") ==""){
				animal = Resources.Load<GUISkin> ("GUISkin/changePj/animals/cat1Skin");
			}else{
				animal = Resources.Load<GUISkin> ("GUISkin/changePj/animals/" + PlayerPrefs.GetString("Animal"));
			}
		}catch(UnityException e){
		}
	}
	
	void OnGUI(){
		scale.x = Screen.width / originalWidth; // calculate hor scale
		scale.y = Screen.height / originalHeight; // calculate vert scale
		scale.z = 1;
		var svMat = GUI.matrix; // save current matrix
		// substitute matrix - only scale is altered from standard
		GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, scale);
		// draw your GUI controls here:
		//...
		GUI.DrawTexture (new Rect (0, 0, 1280, 728), fondo);
		GUI.Label (new Rect (300, 300, 160, 160), "", animal.label);
		GUI.Label (new Rect (320, 50, 640, 200), "", tittleLabelSkin.label);
		GUI.Label (new Rect (600, 300, 0, 0), puntuation, style);
		//GUI.backgroundColor = Color.clear;
		if (GUI.Button (new Rect (256, 515, 256, 150), "", replayButtonSkin.button)) {
			replay = true;
		}
		if (GUI.Button (new Rect (768, 515, 256, 150), "", mainMenuButtonSkin.button)) {
			back = true;
		}
	}

	void input(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevel("MainMenuScene");
		}
		if(Input.GetKeyDown (KeyCode.Menu)) {
			Application.Quit();
		}
	}
}
