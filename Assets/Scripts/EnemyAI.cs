using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {


	Transform player;
	public float speed = 0.2f;
	public float health = 5f;
	float damage;
	PlayerMovement playerScript;
	SpriteRenderer sprite;

	void Start () {
		player = GameObject.Find ("Player").transform;
		playerScript = player.GetComponent<PlayerMovement> ();
		sprite = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards (transform.position, player.position, speed);
		transform.rotation = Quaternion.LookRotation(Vector3.forward, player.position - transform.position);       // this works for instant turning

	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Bullet") {
			//Debug.Log (other.gameObject.name);
			damage = other.gameObject.GetComponent<attributes>().damage;
			health-= damage;
			sprite.color = Color.magenta;
			Destroy (other.gameObject);
			if (health <= 0) {
				Destroy (this.gameObject);
				playerScript.score++;
			}
		} 
	}

}
