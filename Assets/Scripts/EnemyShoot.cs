using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {
	[SerializeField]
	GameObject projectile;

	[SerializeField]
	bool aimed;

	[SerializeField]
	float range = 25f;

	float bulletSpeed;
	float timeBetweenShots;

	Vector3 faceDir;
	private Rigidbody2D rigidbody;
	private float timestamp;
	Transform player;
	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody2D>();
		Swap_Projectile (projectile);
		player = GameObject.Find ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		faceDir = (player.transform.position- transform.position);
		faceDir.z = 0f;
		faceDir = faceDir.normalized;
		//Debug.Log (timestamp);
		if ((Time.time >= timestamp) && (player.transform.position- transform.position).magnitude <= range) {
			//Debug.Log ("fire");
			if (aimed) {
				Fire ();
			} else {
				Blind_fire();
			}	
			timestamp = Time.time + timeBetweenShots;
		}

	}

    

	void Fire(){
        
		Vector3 bulletDir = Quaternion.Euler (0,0,0) * faceDir; // 
		GameObject clone;
		clone = Instantiate(projectile, transform.position + faceDir*5 , Quaternion.Euler (0,0,transform.eulerAngles.z )) as GameObject;
		Vector3 bulletVelocity =  new Vector3 (rigidbody.velocity.x, rigidbody.velocity.y, 0f) + (bulletDir * bulletSpeed); //
		clone.GetComponent<Rigidbody2D>().velocity = bulletVelocity;
		Destroy (clone, 25f);
	}
	void Swap_Projectile(GameObject new_shot){
		//recoil = 0f;
		projectile = new_shot;
		attributes attr =  new_shot.GetComponent<attributes>();
		bulletSpeed = attr.bulletSpeed;
		timeBetweenShots = attr.timeBetweenShots;
	}
	void Blind_fire (){
		float rand1 = Random.Range (-1f,1f);
		float rand2= Random.Range (-1f,1f);
		Vector3 bulletDir = new Vector3(rand1,rand2,0f);
		bulletDir = bulletDir.normalized;

		//Debug.Log(bulletDir );
		//Debug.Log (Mathf.Atan (bulletDir.y / bulletDir.x) * Mathf.Rad2Deg);
		GameObject clone;
		clone = Instantiate(projectile, transform.position + bulletDir*5 , Quaternion.Euler(0,0, Mathf.Atan(bulletDir.y/bulletDir.x)*Mathf.Rad2Deg + 90f)) as GameObject;
		Vector3 bulletVelocity =  new Vector3 (rigidbody.velocity.x, rigidbody.velocity.y, 0f) + (bulletDir * bulletSpeed); //
		clone.GetComponent<Rigidbody2D>().velocity = bulletVelocity;
		Destroy (clone, 25f);
	}
}
