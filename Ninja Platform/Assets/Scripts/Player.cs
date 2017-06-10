using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Animator anim;
	[SerializeField] private float speed = 1f;
	[SerializeField] private float jumpForce = 1f;
	[SerializeField] private float slideSpeed = 1f;
	private bool touchingFloor = true;
	private Collider2D playerCollider;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		playerCollider = GetComponent<Collider2D> ();
		float originalSlideSpeed = slideSpeed;

	}
	
	// Update is called once per frame
	void Update () {

		transform.localRotation = new Quaternion();
		if (Input.GetKey (KeyCode.W) && Input.GetKey(KeyCode.D) && touchingFloor==true) {
			if (transform.localScale.x < 0) {
				transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
			}
			anim.Play ("Jump");
			transform.Translate (Vector2.up*Time.deltaTime*jumpForce);
			transform.Translate (Vector2.right*Time.deltaTime*speed);
		}

		else if (Input.GetKey (KeyCode.W) && Input.GetKey(KeyCode.A) && touchingFloor==true) {
			if (transform.localScale.x > 0) {
				transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
			}
			anim.Play ("Jump");	
			transform.Translate (Vector2.up*Time.deltaTime*jumpForce);
			transform.Translate (Vector2.right*Time.deltaTime*speed);
		}

		else if (Input.GetKey (KeyCode.W) && touchingFloor==true) {
			anim.Play ("Jump");
			transform.Translate (Vector2.up*Time.deltaTime*jumpForce);
		}

		else if (Input.GetKey (KeyCode.S) && Input.GetKey(KeyCode.A)) {
			if (transform.localScale.x > 0) {
				transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
			}
			anim.Play ("Slide");	
			transform.Translate (Vector2.down*Time.deltaTime*speed);
			transform.Translate (Vector2.right*Time.deltaTime*slideSpeed);
		}

		else if (Input.GetKey (KeyCode.S) && Input.GetKey(KeyCode.D)) {
			if (transform.localScale.x < 0) {
				transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
			}
			anim.Play ("Slide");	
			transform.Translate (Vector2.down*Time.deltaTime*speed);
			transform.Translate (Vector2.right*Time.deltaTime*slideSpeed);
		}


		else if (Input.GetKey (KeyCode.D)) {
			if (transform.localScale.x < 0) {
				transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
			}
			anim.Play ("Run");
			transform.Translate (Vector2.right*Time.deltaTime*speed);
		}

		else if (Input.GetKey (KeyCode.A)) {
			
			if (transform.localScale.x > 0) {
				transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

			}
			anim.Play ("Run");
			transform.Translate (Vector2.right * Time.deltaTime * speed);
		}
	}
		

}
