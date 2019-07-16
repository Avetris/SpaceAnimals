using UnityEngine;
using System.Collections;

public class ChangePjScript : MonoBehaviour {
	const int MAX =5;
	// SCRIPT FOR ADJUST RESOLUTION VARIABLES
	float originalWidth = 1280;  // define here the original resolution
	float originalHeight = 728; // you used to create the GUI contents 
	private Vector3 scale;

	GUISkin[] gatos, perros;
	bool[] gatosDisp, perrosDisp;
	GUISkin fijado;
	int pos;
	string seleccion;
	GUISkin flechaMenosOnSkin, flechaMenosOffSkin, flechaMenosSkin, flechaMasOnSkin, flechaMasOffSkin, flechaMasSkin, catOnSkin, catOffSkin, catSkin, dogOnSkin, dogOffSkin, dogSkin, cancelSkin, acceptSkin, changePjSkin;
	// Use this for initialization
	Texture2D fondo;

	AudioClip aud;

	bool back;

	void Start () {
		try{
			admobScript admob = (admobScript) GameObject.Find("ServiciosGoogle").GetComponent<admobScript>();
			admob.HideBanner();
		}catch(System.Exception e){}
		back = false;
		pos = 0;
		seleccion = "gatos";
		fondo = Resources.Load<Texture2D> ("Texture/Background/fondo" + Random.Range (0, 9));
		cogerSkins ();
		cogerSprite ();
		comprobarTipo ();
		aud = Resources.Load<AudioClip> ("Sounds/buttomSound");
	}

	void cogerSkins(){
		flechaMenosOnSkin = Resources.Load<GUISkin> ("GUISkin/changePj/flechaMenosOnSkin");
		flechaMenosOffSkin = Resources.Load<GUISkin> ("GUISkin/changePj/flechaMenosOffSkin");
		flechaMasOnSkin = Resources.Load<GUISkin> ("GUISkin/changePj/flechaMasOnSkin");
		flechaMasOffSkin = Resources.Load<GUISkin> ("GUISkin/changePj/flechaMasOffSkin");
		catOnSkin = Resources.Load<GUISkin> ("GUISkin/changePj/catOnSkin");
		catOffSkin = Resources.Load<GUISkin> ("GUISkin/changePj/catOffSkin");
		dogOnSkin = Resources.Load<GUISkin> ("GUISkin/changePj/dogOnSkin");
		dogOffSkin = Resources.Load<GUISkin> ("GUISkin/changePj/dogOffSkin");
		cancelSkin = Resources.Load<GUISkin> ("GUISkin/changePj/cancelSkin");
		acceptSkin = Resources.Load<GUISkin> ("GUISkin/changePj/acceptSkin");
		changePjSkin = Resources.Load<GUISkin> ("GUISkin/changePj/tittleSkin");
	}

	void comprobarTipo(){
		if (PlayerPrefs.GetString ("tipo") == "perros") {
			catSkin = catOffSkin;
			dogSkin = dogOnSkin;
			seleccion = "perros";
			if (PlayerPrefs.GetInt ("numeroAnimal") > 0) {
				fijado = perros [PlayerPrefs.GetInt ("numeroAnimal")];
			} else {
				fijado = perros [0];
			}
		} else {
			catSkin = catOnSkin;
			dogSkin = dogOffSkin;
			seleccion = "gatos";
			if (PlayerPrefs.GetInt ("numeroAnimal") > 0) {
				fijado = gatos [PlayerPrefs.GetInt ("numeroAnimal")];
			} else {
				fijado = gatos [0];
			}		
		}
		if (pos == 0 && pos != MAX - 1) {
			flechaMasSkin = flechaMasOnSkin;
			flechaMenosSkin = flechaMenosOffSkin;
		} else if (pos == 0 && pos == MAX - 1) {
			flechaMasSkin = flechaMasOffSkin;
			flechaMenosSkin = flechaMenosOffSkin;
		} else if (pos == MAX - 1) {
			flechaMasSkin = flechaMasOffSkin;
			flechaMenosSkin = flechaMenosOnSkin;
		} else {
			flechaMasSkin = flechaMasOnSkin;
			flechaMenosSkin = flechaMenosOnSkin;
		}
	}

	void cogerSprite(){
		gatos = new GUISkin[MAX];
		perros = new GUISkin[MAX];
		gatosDisp = new bool[MAX];
		perrosDisp = new bool[MAX];
		gatosDisp [0] = true; 
		perrosDisp [0] = true;
		gatos [0] = Resources.Load<GUISkin> ("GUISkin/changePj/animals/cat1Skin");
		if (PlayerPrefs.GetString ("cat2") == "available") {
			gatosDisp [1] = true;
			gatos [1] = Resources.Load<GUISkin> ("GUISkin/changePj/animals/cat2Skin");
		} else {
			gatos [1] = Resources.Load<GUISkin> ("GUISkin/changePj/animals/cat2DisSkin");
			gatosDisp [1] = false;
		}
		if (PlayerPrefs.GetString ("cat3") == "available") {
			gatosDisp [2] = true;
			gatos [2] = Resources.Load<GUISkin> ("GUISkin/changePj/animals/cat3Skin");
		} else {
			gatos [2] = Resources.Load<GUISkin> ("GUISkin/changePj/animals/cat3DisSkin");
			gatosDisp [2] = false;
		}
		if (PlayerPrefs.GetString ("cat4") == "available") {
			gatosDisp [3] = true;
			gatos [3] = Resources.Load<GUISkin> ("GUISkin/changePj/animals/cat4Skin");
		} else {
			gatos [3] = Resources.Load<GUISkin> ("GUISkin/changePj/animals/cat4DisSkin");
			gatosDisp [3] = false;
		}
		if (PlayerPrefs.GetString ("cat5") == "available") {
			gatosDisp [4] = true;
			gatos [4] = Resources.Load<GUISkin> ("GUISkin/changePj/animals/cat5Skin");
		} else {
			gatos [4] = Resources.Load<GUISkin> ("GUISkin/changePj/animals/cat5DisSkin");
			gatosDisp [4] = false;
		}
			
		perros [0] = Resources.Load<GUISkin> ("GUISkin/changePj/animals/dog1Skin");
		if (PlayerPrefs.GetString ("dog2") == "available") {
			perrosDisp [1] = true;
			perros [1] = Resources.Load<GUISkin> ("GUISkin/changePj/animals/dog2Skin");
		} else {
			perros [1] = Resources.Load<GUISkin> ("GUISkin/changePj/animals/dog2DisSkin");
			perrosDisp [1] = false;
		}
		if (PlayerPrefs.GetString ("dog3") == "available") {
			perrosDisp [2] = true;
			perros [2] = Resources.Load<GUISkin> ("GUISkin/changePj/animals/dog3Skin");
		} else {
			perros [2] = Resources.Load<GUISkin> ("GUISkin/changePj/animals/dog3DisSkin");
			perrosDisp [2] = false;
		}
		if (PlayerPrefs.GetString ("dog4") == "available") {
			perrosDisp [3] = true;
			perros [3] = Resources.Load<GUISkin> ("GUISkin/changePj/animals/dog4Skin");
		} else {
			perros [3] = Resources.Load<GUISkin> ("GUISkin/changePj/animals/dog4DisSkin");
			perrosDisp [3] = false;
		}
		if (PlayerPrefs.GetString ("dog5") == "available") {
			perrosDisp [4] = true;
			perros [4] = Resources.Load<GUISkin> ("GUISkin/changePj/animals/dog5Skin");
		} else {
			perrosDisp [4] = false;
			perros [4] = Resources.Load<GUISkin> ("GUISkin/changePj/animals/dog5DisSkin");
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
				GetComponent<AudioSource>().clip = aud;
				GetComponent<AudioSource> ().Play ();
				yield return new  WaitForSeconds (1);
			}
			Application.LoadLevel("MainMenuScene");
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
		GUI.Label (new Rect (320, 50, 640, 100), "", changePjSkin.label);
		GUI.Label (new Rect (560, 325, 160, 200),"", fijado.label);
		if(GUI.Button(new Rect(240, 325, 160, 200), "", flechaMenosSkin.button)){
			if(pos > 0){
				if (PlayerPrefs.GetString ("Effect") != "off") {
					GetComponent<AudioSource> ().Play ();
				}
				pos--;
				if(seleccion == "perros"){
					fijado = perros[pos];
				}else{
					fijado = gatos[pos];
				}
				if(pos == 0){
					flechaMenosSkin = flechaMenosOffSkin;
				}
				flechaMasSkin = flechaMasOnSkin;
			}
		}
		if(GUI.Button(new Rect(880, 325, 160, 200), "", flechaMasSkin.button)){
			if(pos < MAX-1){
				if (PlayerPrefs.GetString ("Effect") != "off") {
					GetComponent<AudioSource> ().Play ();
				}
				pos++;
				if(seleccion == "perros"){
					fijado = perros[pos];
				}else{
					fijado = gatos[pos];
				}
				if(pos == MAX-1){
					flechaMasSkin = flechaMasOffSkin;
				}
				flechaMenosSkin = flechaMenosOnSkin;
			}
		}
		if (GUI.Button (new Rect (490, 200, 150, 100), "", catSkin.button)) {
			if(seleccion == "perros"){
				if (PlayerPrefs.GetString ("Effect") != "off") {
					GetComponent<AudioSource> ().Play ();
				}
				catSkin = catOnSkin;
				dogSkin = dogOffSkin;
				seleccion = "gatos";
				pos = 0;
				fijado = gatos[pos];
				flechaMasSkin = flechaMasOnSkin;
				flechaMenosSkin = flechaMenosOffSkin;
			}
		}
		if (GUI.Button (new Rect (640, 200, 150, 100), "", dogSkin.button)) {
			if(seleccion != "perros"){
				if (PlayerPrefs.GetString ("Effect") != "off") {
					GetComponent<AudioSource> ().Play ();
				}
				dogSkin = dogOnSkin;
				catSkin = catOffSkin;
				seleccion = "perros";
				pos = 0;
				fijado = perros[pos];
				flechaMasSkin = flechaMasOnSkin;
				flechaMenosSkin = flechaMenosOffSkin;
			}
		}
		if (GUI.Button (new Rect (340, 550, 300, 100), "", cancelSkin.button)) {
			back = true;
		}
		if (GUI.Button (new Rect (640, 550, 300, 100), "", acceptSkin.button)) {
			if(seleccion == "perros"){
				if(perrosDisp[pos]){
					PlayerPrefs.SetString("Animal", fijado.name);
					PlayerPrefs.SetString ("tipo", seleccion);
					back = true;
				}
			}else{
				if(gatosDisp[pos]){
					PlayerPrefs.SetString("Animal", fijado.name);
					PlayerPrefs.SetString ("tipo", seleccion);					
					back = true;
				}
			}
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
