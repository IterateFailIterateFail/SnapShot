	using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

	private Rigidbody2D rigidbody;
	public float friction= 0.85f;
	public float acceleration = 1f;
	float bulletSpeed;
	float timeBetweenShots;
	private float timestamp;
	float recoil;
	float recoilBuild;
	float recoil_max = 45f;
	float recoilFall;
	public float maxSpeed = 10f;
	bool singleShot;
    bool explosive;

    public int score = 0;
	Vector3 mousePos;
	Vector3 mouseDir;
	//public float turningspeed = 1f; // degrees per second
	GameObject mouse;
	GameObject projectile;
	UnityEngine.UI.Text score_box;

	void Start () {
		rigidbody = GetComponent<Rigidbody2D> ();
		mouse = GameObject.Find ("centre");
		Swap_Projectile (Resources.Load ("NormalBullet") as GameObject);
		score_box = GameObject.Find ("Score").GetComponent<UnityEngine.UI.Text>();
	}

	void Update () {
		
		// basic movement
		Vector2 velocity = rigidbody.velocity;
		float moveHorizontal = Input.GetAxis("Horizontal");
		velocity.x += moveHorizontal *acceleration;
		float moveVertical = Input.GetAxis("Vertical");
		velocity.y += moveVertical *acceleration;
		velocity.x *= friction;
		
		velocity.y *= friction;
		velocity.x = Mathf.Clamp(velocity.x,  -maxSpeed, maxSpeed);
		velocity.y = Mathf.Clamp(velocity.y,  -maxSpeed, maxSpeed);
		rigidbody.velocity = velocity;
		Direction ();
		//rotate

		UnityEngine.Debug.DrawLine(transform.position,mouse.transform.position, Color.magenta);

		transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);       // this works for instant turning
		//float step = turningspeed * Time.deltaTime;
		//transform.rotation = Quaternion.RotateTowards (transform.rotation,mouse.transform.rotation, step);
		//Debug.Log(recoil);
		//Debug.Log (transform.rotation.z);

		//Shooty-Bang Bangs
		//Debug.Log(Time.time);
		if ((Time.time >= timestamp) && Input.GetMouseButton (0) && !singleShot) {
			//Debug.Log ("fire");
			Fire ();
			timestamp = Time.time + timeBetweenShots;

		} else if (Input.GetMouseButtonDown(0) && singleShot && (Time.time >= timestamp)) {			
			Fire ();
			timestamp = Time.time + timeBetweenShots;

		}
		if (Input.GetKeyDown(KeyCode.Alpha1))Swap_Projectile (Resources.Load ("NormalBullet") as GameObject);
		else if (Input.GetKeyDown(KeyCode.Alpha2)) Swap_Projectile(Resources.Load("PowerBullet") as GameObject);
        else if (Input.GetKeyDown(KeyCode.Alpha3)) Swap_Projectile(Resources.Load("Nade") as GameObject);

        if (recoil > 0f) {
			recoil -= recoilFall;
		//	Debug.Log("Reduced By:");
			//Debug.Log (recoilFall);
		}


		UpdateScore();
	}
		
	void Fire(){
		
		float rand = Random.Range (-recoil,recoil);
		Vector3 bulletDir = Quaternion.Euler (0,0,rand) * mouseDir; // 
		GameObject clone;
		clone = Instantiate(projectile, transform.position + mouseDir*5 , Quaternion.Euler (0,0,transform.eulerAngles.z + rand)) as GameObject;
		Vector3 bulletVelocity =  new Vector3 (rigidbody.velocity.x, rigidbody.velocity.y, 0f) + (bulletDir * bulletSpeed); //
        clone.GetComponent<Rigidbody2D>().velocity = bulletVelocity;

		recoil += recoilBuild;
		//Debug.Log (recoilBuild);
		if (recoil > recoil_max)
			recoil = recoil_max;
        //Debug.Log ("fired");
        if (explosive){

        }
		Destroy (clone, 2f);


	}
	void Direction(){
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mouseDir = (mousePos- transform.position);
		mouseDir.z = 0f;
		mouseDir = mouseDir.normalized;
	}

	void Swap_Projectile(GameObject new_shot){
		//recoil = 0f;
		projectile = new_shot;
		attributes attr =  new_shot.GetComponent<attributes>();
		bulletSpeed = attr.bulletSpeed;
		timeBetweenShots = attr.timeBetweenShots;
		recoilBuild = attr.recoilBuild;
		recoilFall = attr.recoilFall;
		singleShot = attr.singleShot;
        explosive = attr.explosive;
	}
	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Enemy_Bullet"|| other.gameObject.tag == "Enemy") {
			//Debug.Log (other.gameObject.name);
			Destroy (other.gameObject);
			Destroy (this.gameObject);
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
	void UpdateScore (){
		score_box.text= "Score: " + score;
		//Debug.Log (score_box.text);
	}
}

