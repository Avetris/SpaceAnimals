using UnityEngine;
using System.Collections;

public class SetttingScript : MonoBehaviour {

	// SCRIPT FOR ADJUST RESOLUTION VARIABLES
	float originalWidth = 1280;  // define here the original resolution
	float originalHeight = 728; // you used to create the GUI contents 
	private Vector3 scale;
	
	GUISkin mainMenuButtonSkin, tittleLabelSkin, effectsSkin, musicSkin, eliminateAdsSkin, musicVol, effectVol, ads;
	Texture2D fondo;

	bool back;
	
	// Use this for initialization
	void Start () {
		try{
			admobScript admob = (admobScript) GameObject.Find("ServiciosGoogle").GetComponent<admobScript>();
			admob.HideBanner();
		}catch(System.Exception e){}
		back = false;
		fondo = Resources.Load<Texture2D> ("Texture/Background/fondo" + Random.Range (0, 9));
		cogerTexturas ();
		if (PlayerPrefs.GetString ("Music") == "off") {
			musicVol = musicSkin;
		} else {
			musicVol = effectsSkin;
		}
		if (PlayerPrefs.GetString ("Effect") == "off") {
			effectVol = musicSkin;
		} else {
			effectVol = effectsSkin;
		}
		if(PlayerPrefs.GetString("Publicity") == "eliminate"){
			ads = musicSkin;
		} else {
			ads = effectsSkin;
		}

	}
	
	// Update is called once per frame
	void Update () {
		input ();
		StartCoroutine (comprobarBoton ());
	}
	
	IEnumerator comprobarBoton (){
		if (back) {
			back = false;
			if (PlayerPrefs.GetString ("Effect") == "off") {
				yield return new  WaitForSeconds (0);
			} else {
				GetComponent<AudioSource> ().Play ();
				yield return new  WaitForSeconds (1);
			}
			Application.LoadLevel("MainMenuScene");
		}
	}
	
	void cogerTexturas(){
		try{
			mainMenuButtonSkin = Resources.Load<GUISkin>("GUISkin/mainMenuButtonSkin");
			tittleLabelSkin = Resources.Load<GUISkin> ("GUISkin/settingLabelSkin");
			musicSkin = Resources.Load<GUISkin> ("GUISkin/musicSkin");
			effectsSkin = Resources.Load<GUISkin> ("GUISkin/effectsSkin");
			eliminateAdsSkin = Resources.Load<GUISkin> ("GUISkin/eliminateAdsButtonSkin");
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
		GUI.Label (new Rect (370, 50, 540, 150), "", tittleLabelSkin.label);
		GUI.Label (new Rect (420, 250, 200, 100), "", musicSkin.label);
		GUI.Label (new Rect (420, 410, 200, 100), "", effectsSkin.label);
		//GUI.Label (new Rect (420, 570, 150, 100), "", eliminateAdsSkin.label);
		//GUI.backgroundColor = Color.clear;
		if (GUI.Button (new Rect (660, 250, 200, 100), "", musicVol.button)) {
			if(musicVol.name == "effectsSkin"){
				musicVol = musicSkin;
				PlayerPrefs.SetString("Music", "off");
			}else{
				musicVol = effectsSkin;
				PlayerPrefs.SetString("Music", "on");
			}
		}
		if (GUI.Button (new Rect (660, 410, 200, 100), "", effectVol.button)) {
			if(effectVol.name == "effectsSkin"){
				effectVol = musicSkin;
				PlayerPrefs.SetString("Effect", "off");
			}else{
				effectVol = effectsSkin;
				PlayerPrefs.SetString("Effect", "on");
			}
		}
		/*if (GUI.Button (new Rect (660, 570, 200, 100), "", ads.button)) {
			if(ads.name == "effectsSkin"){
				//TODO
				ads = musicSkin;
				//PlayerPrefs.SetString("Publicity", "eliminate");
			}
		}*/
		if (GUI.Button (new Rect (1000, 500, 150, 145), "", mainMenuButtonSkin.button)) {
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
