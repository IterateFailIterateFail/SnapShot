using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	[SerializeField]
	GameObject[] spawnee; // thing we are spawning
	Transform player;
	Camera cammera;
	float time = 0;
	float speed = 30f;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player").transform;
		cammera = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		Vector3 pos = transform.position;
		pos.x = player.position.x;
		pos.y = player.position.y - cammera.orthographicSize - 20;
		transform.position = pos;
	}



	// Update is called once per frame
	void Update () {

		Vector3 pos = Position(transform.position);
		transform.position = pos;
		time++;

		if (time % 50 == 0) { //Delay factor
			GameObject clone;
			clone = Instantiate (spawnee[0], transform.position, transform.rotation) as GameObject;

		}
		if (time > 100f && time % 100 == 0) {
			GameObject clone1;
			clone1 = Instantiate (spawnee[1], transform.position, transform.rotation) as GameObject;

		}
	}

	Vector3 Position(Vector3 pos){
		
		float left = player.position.x - cammera.orthographicSize - 20;
		float right = player.position.x + cammera.orthographicSize + 20;
		float up = player.position.y + cammera.orthographicSize + 20;
		float down = player.position.y - cammera.orthographicSize - 20;

		if((pos.y <= down) && (pos.x > left)) { //bot-right -> bot-left
			pos.x -= speed;
			//Debug.Log ("left");
		}else if ((pos.y < up) && (pos.x <= left)) { //bot-right -> top-left
			pos.y += speed;
			//Debug.Log ("up");
		}else if ((pos.y > down) && (pos.x >= right)) { //top-right -> bot-right
			pos.y -= speed;
			//Debug.Log ("dwon");
		}else if ((pos.y >= up) && (pos.x < right)) { //top-left -> top-right
			pos.x += speed;
			//Debug.Log ("right");
		}

		return pos;
	}
}
