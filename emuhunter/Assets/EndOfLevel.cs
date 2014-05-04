using UnityEngine;
using System.Collections;

public class EndOfLevel : MonoBehaviour {

	private bool _triggered;
	private GenerateEnvironment _generateEnvironment;
	private RailsMovement _railsMovement;
	private PlayerBehavior _player;
	private GameState _gameState;
	private bool _nextLevel = false;
	
	// Use this for initialization
	void Start () {
		_triggered = false;

		var scripts = GameObject.FindGameObjectWithTag("GlobalScripts");
		if (scripts) {
			_gameState = scripts.GetComponent<GameState>();
			_generateEnvironment = scripts.GetComponent<GenerateEnvironment>();
		}
		scripts = GameObject.FindGameObjectWithTag ("Player");
		if (scripts) {
			_player = scripts.GetComponent<PlayerBehavior>();
		}
	}
	
	void OnTriggerEnter(Collider collision) {
		if (!_triggered && collision.gameObject.GetComponent<PlayerBehavior> ()) {
			_triggered = true;
			_nextLevel = true;
		}
	}

	void OnGUI() {
		if (_nextLevel && _player) {
			Time.timeScale = 0.0f;

			GUI.skin.label.fontSize = 72;
			GUI.skin.button.fontSize = 72;
			GUI.Label(new Rect((Screen.width / 2) - 150, 0, Screen.width, Screen.height), "Level Complete!");
			if (GUI.Button(new Rect ((Screen.width / 2) - 300, Screen.height - 300, 600, 75), "Next Level")){
				_nextLevel = false;
				Debug.Log ("Trying to start new level...");
				GameState.LevelLimit = (int)((float)GameState.LevelLimit * 1.1f);
				GenerateEnvironment.LevelLimit = GameState.LevelLimit;
				Application.LoadLevel(Application.loadedLevel);
				Time.timeScale = 1.0f;
				_player.health = 100;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
