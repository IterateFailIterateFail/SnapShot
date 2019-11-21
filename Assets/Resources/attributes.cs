using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attributes : MonoBehaviour {
	public float bulletSpeed = 100f;
	public float timeBetweenShots = 0.3333f;
	public float recoilBuild = 0.5f;
	public float recoilFall = 0.3f;
	public float damage = 1f;
	public bool singleShot = false;
    public bool explosive = false;
    [SerializeField] 
    GameObject Boom;
    // Vector3 Pos;

    void Start(){
       
    }
    void OnDestroy(){
        
        Rigidbody2D rigidbody;
        
        if (!explosive) return;
        
        rigidbody = GetComponent<Rigidbody2D>();

       
        
        GameObject clone;
        clone = Instantiate(Boom, transform.position)   ) as GameObject;
        
        Debug.Log(clone.transform.position);
        Destroy(clone, 25f);
    }
}
