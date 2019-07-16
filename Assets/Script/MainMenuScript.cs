using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class MainMenuScript : MonoBehaviour {

	// SCRIPT FOR ADJUST RESOLUTION VARIABLES
	float originalWidth = 1280;  // define here the original resolution
	float originalHeight = 728; // you used to create the GUI contents 
	private Vector3 scale;

	GUISkin animal, playButtonSkin, scoreButtonSkin, settingsButtonSkin, changePjButtonSkin, tittleLabelSkin;
	Texture2D fondo;

	bool score, change, settings, game;
	// Use this for initialization

	
	void Start () {
		try{
			PlayGamesPlatform.Activate();

			if(!Social.localUser.authenticated){
				Social.localUser.Authenticate ((bool success) => {
					if(success){
						Debug.Log("SUCCEEDED");
					}else{
						Debug.Log("NOT SUCCEEDED");
					}
				});
			}
		}catch(InvalidComObjectException e){}
		try{
			admobScript admob = (admobScript) GameObject.Find("ServiciosGoogle").GetComponent<admobScript>();
			admob.HideBanner();
		}catch(System.Exception e){}
		score = false;
		change = false;
		settings = false;
		game = false;
		fondo = Resources.Load<Texture2D> ("Texture/Background/fondo" + Random.Range (0, 9));
		cogerTexturas ();
		GameObject.Find("ServiciosGoogle").GetComponent<admobScript>().HideBanner();
	}

	// Update is called once per frame
	void Update () {
		input ();
		StartCoroutine (comprobarBoton ());
	}
	
	IEnumerator comprobarBoton (){
		if (score) {
			score = false;
			if(PlayerPrefs.GetString("Effect") == "off"){
				yield return new  WaitForSeconds(0);
			}else{
				GetComponent<AudioSource>().Play ();
				yield return new  WaitForSeconds(1);
			}
			try{
				int pnt = 0;
				int.TryParse (PlayerPrefs.GetString("PuntuacionMax"), out pnt);
				if(pnt > 0){
					Social.ReportScore(pnt, "CgkIvdPY9ekMEAIQAQ", (bool success)=>{
						if(success){
							Debug.Log("CORRECTO");
						}else{							
							Debug.Log("NO CORRECTO");
						}
					});
				}
				((PlayGamesPlatform)Social.Active).ShowLeaderboardUI("CgkIvdPY9ekMEAIQAQ");
			}catch(AndroidJavaException e){
				print (e);
			}
		} else if (change) {
			change = false;
			if(PlayerPrefs.GetString("Effect") == "off"){
				yield return new  WaitForSeconds(0);
			}else{
				GetComponent<AudioSource>().Play ();
				yield return new  WaitForSeconds(1);
			}
			Application.LoadLevel("ChangePjScene");
		} else if (settings) {
			settings = false;
			if(PlayerPrefs.GetString("Effect") == "off"){
				yield return new  WaitForSeconds(0);
			}else{
				GetComponent<AudioSource>().Play ();
				yield return new  WaitForSeconds(1);
			}
			Application.LoadLevel("SettingScene");
		} else if (game) {
			game = false;
			if(PlayerPrefs.GetString("Effect") == "off"){
				yield return new  WaitForSeconds(0);
			}else{
				GetComponent<AudioSource>().Play ();
				yield return new  WaitForSeconds(1);
			}
			DontDestroyOnLoad(GameObject.Find("ServiciosGoogle"));
			GameObject.Find("ServiciosGoogle").GetComponent<admobScript>().ShowBanner();
			Application.LoadLevel("GameScene");
		}
	}
	void cogerTexturas(){
		try{
			playButtonSkin = Resources.Load<GUISkin>("GUISkin/playButtonSkin");
			settingsButtonSkin = Resources.Load<GUISkin>("GUISkin/settingButtonSkin");
			changePjButtonSkin = Resources.Load<GUISkin>("GUISkin/changePjButtonSkin");
			scoreButtonSkin = Resources.Load<GUISkin>("GUISkin/scoreButtonSkin");
			tittleLabelSkin = Resources.Load<GUISkin> ("GUISkin/tittleLabelSkin");
			if(PlayerPrefs.GetString("Animal") ==""){
				animal = Resources.Load<GUISkin> ("GUISkin/changePj/animals/cat1Skin");
			}else{
				animal = Resources.Load<GUISkin> ("GUISkin/changePj/animals/" + PlayerPrefs.GetString("Animal"));
			}
		}catch(UnityException e){
		}
	}

	void OnGUI(){
		scale.x = Screen.width/originalWidth; // calculate hor scale
		scale.y = Screen.height/originalHeight; // calculate vert scale
		scale.z = 1;
		var svMat = GUI.matrix; // save current matrix
		// substitute matrix - only scale is altered from standard
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
		// draw your GUI controls here:
		//...
		GUI.DrawTexture (new Rect (0, 0, 1280, 728), fondo);
		GUI.Label (new Rect (560, 325, 160, 200),"", animal.label);
		GUI.Label (new Rect (320, 50, 640, 200), "", tittleLabelSkin.label);
		//GUI.backgroundColor = Color.clear;
		if(GUI.Button(new Rect(256, 305, 256, 150),"", playButtonSkin.button)){
			game = true;
		}
		if(GUI.Button(new Rect(768, 305, 256, 150),"", changePjButtonSkin.button)){
			change = true;
		}
		if(GUI.Button(new Rect(256, 515, 256, 150),"", scoreButtonSkin.button)){
			score = true;
		}
		if(GUI.Button(new Rect(768, 515, 256, 150),"", settingsButtonSkin.button)){
			settings = true;
		}
	}
	void input(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
		if(Input.GetKeyDown (KeyCode.Menu)) {
			Application.Quit();
		}
	}

	void OnAuthCB(bool result)
	{
		Debug.Log("GPGUI: Got Login Response: " + result);
	}

	public void OnSubmitScore(bool result)
	{
		Debug.Log("GPGUI: OnSubmitScore: " + result);
	}
}
