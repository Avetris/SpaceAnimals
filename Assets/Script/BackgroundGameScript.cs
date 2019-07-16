using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class BackgroundGameScript : MonoBehaviour {

	// SCRIPT FOR ADJUST RESOLUTION VARIABLES
	float originalWidth = 1280;  // define here the original resolution
	float originalHeight = 728; // you used to create the GUI contents 
	private Vector3 scale;
	private GameObject meteorito1, meteorito2, meteorito3, meteorito4, agujeroNegro, ship1, ship2, ship3, boss, bossFinal;
	public Camera cm;
	int time, timeAux;
	Texture2D inTouch, fuego;

	GUISkin[] gatos, perros;
	GUISkin unlock;

	Font font;
	GameObject animal, fondo1, fondo2, fondo3;

	GUIText puntuation;

	bool parado, mostrar, sonar, rapido, masRapido;

	bool pj;

	int fontSize = 200;

	admobScript admob;

	// Use this for initialization
	void Start () {
		puntuation = GameObject.Find ("Puntuation").GetComponent<GUIText> ();
		try{
			admob = (admobScript) GameObject.Find ("ServiciosGoogle").GetComponent<admobScript>();
			admob.ShowBanner();
		}catch(System.Exception e){
		}
		puntuation.text = " ";
		puntuation.pixelOffset = cm.ViewportToWorldPoint (new Vector2 (0.1f, 0.4f));
		puntuation.fontSize = Mathf.Min (Mathf.FloorToInt (Screen.width * fontSize / 1000), Mathf.FloorToInt (Screen.height * fontSize / 1000));
		sonar = true;
		rapido = true;
		masRapido = true;
		cogerTodo ();
		obtenerAnimal ();
		parado = true;
		mostrar = true;
		pj = false;
		timeAux = 0;
		time = 0;
		inTouch = Resources.Load<Texture2D> ("Texture/clickTouch");
		fuego = Resources.Load<Texture2D> ("Texture/fuego");
		animal = (GameObject)Instantiate (animal, cm.ViewportToWorldPoint(new Vector3 (0.1f, 0.5f, 10)), Quaternion.identity);
		animal.GetComponent<PlayerMovementScript> ().enabled = false;
		InvokeRepeating ("aumentarTiempo", 0f, 1f);
		InvokeRepeating ("generarObjetos", 0f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		if (!parado) {
			puntuation.text = time.ToString ();
		}
		pulsarPantalla ();
		desbloquear ();
	}

	void obtenerAnimal(){
		if (PlayerPrefs.GetString ("Animal") == "cat5Skin") {
			animal = Resources.Load<GameObject> ("Prefab/Cat5");
		} else if (PlayerPrefs.GetString ("Animal") == "dog5Skin") {
			animal = Resources.Load<GameObject> ("Prefab/Dog5");
		} else if (PlayerPrefs.GetString ("Animal") == "dog1Skin") {
			animal = Resources.Load<GameObject> ("Prefab/Dog1");
		} else if (PlayerPrefs.GetString ("Animal") == "cat2Skin") {
			animal = Resources.Load<GameObject> ("Prefab/Cat2");
		} else if (PlayerPrefs.GetString ("Animal") == "cat3Skin") {
			animal = Resources.Load<GameObject> ("Prefab/Cat3");
		} else if (PlayerPrefs.GetString ("Animal") == "dog2Skin") {
			animal = Resources.Load<GameObject> ("Prefab/Dog2");
		} else if (PlayerPrefs.GetString ("Animal") == "dog3Skin") {
			animal = Resources.Load<GameObject> ("Prefab/Dog3");
		} else if (PlayerPrefs.GetString ("Animal") == "cat4Skin") {
			animal = Resources.Load<GameObject> ("Prefab/Cat4");
		} else if (PlayerPrefs.GetString ("Animal") == "dog4Skin") {
			animal = Resources.Load<GameObject> ("Prefab/Dog4");
		} else {
			animal = Resources.Load<GameObject> ("Prefab/Cat1");
		}
	}

	void pulsarPantalla(){
		if (parado && mostrar) {
			if (Input.GetKeyDown (KeyCode.Space) || Input.touchCount > 0) {
				parado = false;
				mostrar = false;
				if(sonar){
					sonar = false;
					if(PlayerPrefs.GetString("Music") != "off"){
						GetComponent<AudioSource>().Play ();
					}
				}
				animal.GetComponent<PlayerMovementScript> ().enabled = true;
				animal.transform.Translate(Vector2.up*Time.deltaTime*0.8f);
			}
		}
	}

	void aumentarTiempo(){
		if (!parado) {
			time++;
		}
	}

	void cogerTodo(){
		font = Resources.Load<Font> ("Labtop Down Under");
		meteorito1 = Resources.Load<GameObject>("Prefab/Meteorito1"); 
		meteorito2 = Resources.Load<GameObject>("Prefab/Meteorito2"); 
		meteorito3 = Resources.Load<GameObject>("Prefab/Meteorito3"); 
		meteorito4 = Resources.Load<GameObject>("Prefab/Meteorito4"); 
		agujeroNegro = Resources.Load<GameObject>("Prefab/AgujeroNegro");
		ship1 = Resources.Load<GameObject>("Prefab/Ship1");
		ship2 = Resources.Load<GameObject>("Prefab/Ship2");
		ship3 = Resources.Load<GameObject>("Prefab/Ship3");
		boss = Resources.Load<GameObject>("Prefab/Boss");
		cm = FindObjectOfType<Camera>();
		gatos = new GUISkin[4];
		perros = new GUISkin[4];
		gatos [0] = Resources.Load<GUISkin> ("GUISkin/changePj/animals/cat2Skin");
		gatos [1] = Resources.Load<GUISkin> ("GUISkin/changePj/animals/cat3Skin");
		gatos [2] = Resources.Load<GUISkin> ("GUISkin/changePj/animals/cat4Skin");
		gatos [3] = Resources.Load<GUISkin> ("GUISkin/changePj/animals/cat5Skin");
		perros [0] = Resources.Load<GUISkin> ("GUISkin/changePj/animals/dog2Skin");
		perros [1] = Resources.Load<GUISkin> ("GUISkin/changePj/animals/dog3Skin");
		perros [2] = Resources.Load<GUISkin> ("GUISkin/changePj/animals/dog4Skin");
		perros [3] = Resources.Load<GUISkin> ("GUISkin/changePj/animals/dog5Skin");
	}

	void desbloquear(){
		if (time == 60) {
			if (PlayerPrefs.GetString ("tipo") == "perros") {
				if(PlayerPrefs.GetString("dog2") != "available"){
					unlock = perros[0];
					pj = true;
					PlayerPrefs.SetString ("dog2", "available");
				}
			} else { 
				if(PlayerPrefs.GetString("cat2") != "available"){
					pj = true;
					PlayerPrefs.SetString ("cat2", "available");
					unlock = gatos[0];
				}
			}
			timeAux = time;
		}else if(time == 300){
			if (PlayerPrefs.GetString ("tipo") == "perros") {
				if(PlayerPrefs.GetString("dog3") != "available"){
					pj = true;
					PlayerPrefs.SetString ("dog3", "available");
					unlock = perros[1];
				}
			} else { 
				if(PlayerPrefs.GetString("cat3") != "available"){
					pj = true;
					PlayerPrefs.SetString ("cat3", "available");
					unlock = gatos[1];
				}
			}
			timeAux = time;
		}else if(time == 600){
			if (PlayerPrefs.GetString ("tipo") == "perros") {
				if(PlayerPrefs.GetString("dog4") != "available"){
					pj = true;
					PlayerPrefs.SetString ("dog4", "available");
					unlock = perros[2];
				}
			} else { 
				if(PlayerPrefs.GetString("cat4") != "available"){
					pj = true;
					PlayerPrefs.SetString ("cat4", "available");
					unlock = gatos[2];
				}
			}
			timeAux = time;
		}else if(time > 900){
			if(bossFinal == null){
				if (PlayerPrefs.GetString ("tipo") == "perros") {
					if(PlayerPrefs.GetString("dog5") != "available"){
						pj = true;
						PlayerPrefs.SetString ("dog5", "available");
						unlock = perros[3];
					}
				} else { 
					if(PlayerPrefs.GetString("cat5") != "available"){
						pj = true;
						PlayerPrefs.SetString ("cat5", "available");
						unlock = gatos[3];
					}
				}
			}
			timeAux = time;
		}
	}

	void generarObjetos(){
		if (!parado) {
			if(rapido){
				if(masRapido){
					int rd1 = 0;
					if(time > 30 && time < 60){
						rd1 = Random.Range (0,3);
					}else if (time > 60 && time < 300){
						rd1 = Random.Range (0,2);
					}else if (time > 300 && time < 900){
						rd1 = Random.Range (0,4);
					}else if (time > 900){
						rd1 = Random.Range (0,4);
					}
		 			if (time < 30) {
						generarMeteoritos();
						rapido = false;
						masRapido = false;
					} else if (time < 60) {
						rapido = false;
						masRapido = false;
						if(rd1 == 1){
							generarMeteoritos();
						}else{
							Instantiate (agujeroNegro, cm.ViewportToWorldPoint(new Vector3 (1.2f, Random.Range (0.1f, 0.9f), 10)), Quaternion.identity);
						}
					} else if (time < 300) {
						masRapido = false;
						if(rd1 == 1){
							generarMeteoritos();
						}else{
							Instantiate (agujeroNegro, cm.ViewportToWorldPoint(new Vector3 (1.2f, Random.Range (0.1f, 0.9f), 10)), Quaternion.identity);
						}
					} else if (time < 600) {
						masRapido = false;
						if(rd1 == 1){
							generarMeteoritos();
						}else if(rd1 == 2){
							Instantiate (agujeroNegro, cm.ViewportToWorldPoint(new Vector3 (1.2f, Random.Range (0.1f, 0.9f), 10)), Quaternion.identity);
						}else{
							generarNaves();
						}
					} else if (time<900) {
						if(rd1 == 1){
							generarMeteoritos();
						}else if(rd1 == 2){
							Instantiate (agujeroNegro, cm.ViewportToWorldPoint(new Vector3 (1.2f, Random.Range (0.1f, 0.9f), 10)), Quaternion.identity);
						}else{
							generarNaves();
						}
					}else if(time < 905){
						if(bossFinal == null){
							bossFinal = (GameObject) Instantiate (boss,cm.ViewportToWorldPoint(new Vector3 (1.2f, 0.5f, 10)), Quaternion.identity);
						}
					}else{
						if(bossFinal == null){
							if(rd1 == 1){
								generarMeteoritos();
							}else if(rd1 == 2){
								Instantiate (agujeroNegro, cm.ViewportToWorldPoint(new Vector3 (1.2f, Random.Range (0.1f, 0.9f), 10)), Quaternion.identity);
							}else if(rd1 == 3){
								generarNaves();
							}else{
								bossFinal = (GameObject) Instantiate (boss,cm.ViewportToWorldPoint(new Vector3 (1.2f, 0.5f, 10)), Quaternion.identity);
							}
						}
					}
				}else{
					masRapido = true;
				}
			}else{
				rapido = true;
			}
		}
	}

	void generarMeteoritos(){
		int rd1 = Random.Range (0, 4);
		if (rd1 == 1) {
			Instantiate (meteorito1, cm.ViewportToWorldPoint(new Vector3 (1.2f, Random.Range (0.1f, 0.9f), 10)), Quaternion.identity);
		} else if( rd1 == 2) {
			Instantiate (meteorito2, cm.ViewportToWorldPoint(new Vector3 (1.2f, Random.Range (0.1f, 0.9f), 10)), Quaternion.identity);
		} else if( rd1 == 3) {
			Instantiate (meteorito3, cm.ViewportToWorldPoint(new Vector3 (1.2f, Random.Range (0.1f, 0.9f), 10)), Quaternion.identity);
		} else {
			Instantiate (meteorito4, cm.ViewportToWorldPoint(new Vector3 (1.2f, Random.Range (0.1f, 0.9f), 10)), Quaternion.identity);
		}
	}
	void generarNaves(){
		int rd1 = Random.Range (0, 3);
		if (rd1 == 1) {
			Instantiate (ship1, cm.ViewportToWorldPoint(new Vector3 (1.2f, Random.Range (0.1f, 0.9f), 10)), Quaternion.identity);
		} else if( rd1 == 2) {
			Instantiate (ship2, cm.ViewportToWorldPoint(new Vector3 (1.2f, Random.Range (0.1f, 0.9f), 10)), Quaternion.identity);
		} else if( rd1 == 3) {
			Instantiate (ship3, cm.ViewportToWorldPoint(new Vector3 (1.2f, Random.Range (0.1f, 0.9f), 10)), Quaternion.identity);
		} 
	}

	void OnGUI(){
		scale.x = Screen.width/originalWidth; // calculate hor scale
		scale.y = Screen.height/originalHeight; // calculate vert scale
		scale.z = 1;
	//	var svMat = GUI.matrix; // save current matrix
		// substitute matrix - only scale is altered from standard

		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
		// draw your GUI controls here:
		//...
		GUI.DrawTexture (new Rect (0, 628, 300, 200), fuego);
		GUI.DrawTexture (new Rect (300, 628, 300, 200), fuego);
		GUI.DrawTexture (new Rect (600, 628, 300, 200), fuego);
		GUI.DrawTexture (new Rect (900, 628, 380, 200), fuego);

		if (mostrar) {
			GUI.DrawTexture (new Rect (290, 380, 700, 300), inTouch);
		}
		if (pj) {
			if(time < timeAux +3){
				GUI.Label (new Rect (40, 50, 100, 100), "UNLOCK", unlock.label);
			}else{
				pj = false;
			}
		}
	}

	public void finish(){
		PlayerPrefs.SetString("Puntuacion", time.ToString());
		int pnt = 0;
		int.TryParse (PlayerPrefs.GetString ("PuntuacionMax"),out pnt);
		if(time > pnt){
			PlayerPrefs.SetString("PuntuacionMax", time.ToString());
		}
		try{
			if(Social.localUser.authenticated){
				Social.ReportScore(pnt, "CgkIvdPY9ekMEAIQAQ", (bool success)=>{
					if(success){
						Debug.Log("CORRECTO");
					}else{							
						Debug.Log("NO CORRECTO");
					}
				});
			}
		}catch(AndroidJavaException e){}
			DontDestroyOnLoad (admob.gameObject);
			Application.LoadLevel("GameOverScene");
	}
}
