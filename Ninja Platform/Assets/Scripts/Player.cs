using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Animator anim;
	private Rigidbody2D rigid;
	private BoxCollider2D col;
	[SerializeField] private float speed = 1f;
	[SerializeField] private float jumpForce = 1f;
	[SerializeField] private float slideSpeed = 1f;
	[SerializeField] private float fallDrag = 100f;
	private bool touchingFloor = false;
	private float airTime = 0f;
	private int jumps = 0;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rigid = GetComponent<Rigidbody2D> ();
		col = GetComponent<BoxCollider2D> ();
		float originalSlideSpeed = slideSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		if (!touchingFloor) {
			airTime += Time.deltaTime;
		}
		transform.localRotation = new Quaternion();
		col.size = new Vector2(1.93f, 4.58f);
		if (jumps > 0) {
			touchingFloor = false;
		}

		if (/*airTime > 3f*/Input.GetKey (KeyCode.Space) && !touchingFloor) {
			Fall ();
		} else if (Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.D) && touchingFloor) {
			if (transform.localScale.x < 0) {
				transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
			}
			JumpRight ();
			jumps += 1;
		} else if (Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.A) && touchingFloor) {
			if (transform.localScale.x > 0) {
				transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
			}
			JumpLeft ();
			jumps += 1;
		} else if (Input.GetKey (KeyCode.W) && touchingFloor && jumps < 2) {
			Jump ();
			jumps += 1;
		} else if (Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.A)) {
			if (transform.localScale.x > 0) {
				transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
			}
			SlideLeft ();
		} else if (Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.D)) {
			if (transform.localScale.x < 0) {
				transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
			}
			SlideRight ();
		} else if (Input.GetKey (KeyCode.D)) {
			if (transform.localScale.x < 0) {
				transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
			}
			if (!touchingFloor) {
				transform.Translate (Vector2.right * Time.deltaTime * speed);
			} else {
				RunRight ();
			}
		} else if (Input.GetKey (KeyCode.A)) {
			
			if (transform.localScale.x > 0) {
				transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

			}
			if (!touchingFloor) {
				transform.Translate (Vector2.right * Time.deltaTime * speed);
			} else {
				RunLeft ();
			}
		}


	}

	void Fall(){
		anim.Play ("Fall");
		col.size = new Vector2(3.87f, col.size.y);
		rigid.drag = fallDrag;
	}

	void JumpRight(){
		anim.Play ("Jump");
		col.size = new Vector2(3.87f, col.size.y);
		rigid.drag = 0;
		rigid.AddForce (Vector2.up*jumpForce);
		rigid.AddForce (Vector2.right*speed);
	}

	void JumpLeft(){
		anim.Play ("Jump");
		col.size = new Vector2(3.87f, col.size.y);
		rigid.drag = 0;
		rigid.AddForce (Vector2.up*jumpForce);
		rigid.AddForce (Vector2.right*speed);
	}

	void Jump(){
		anim.Play ("Jump");
		col.size = new Vector2(3.87f, col.size.y);
		rigid.drag = 0;
		rigid.AddForce (Vector2.up*jumpForce);
	}

	void SlideLeft(){
		anim.Play ("Slide");
		col.size = new Vector2(3.87f, 2.71f);
		rigid.drag = 0;
		if (!touchingFloor) {
			transform.Translate (Vector2.down * Time.deltaTime * speed);
		}
		transform.Translate (Vector2.right*Time.deltaTime*slideSpeed);
	}

	void SlideRight(){
		anim.Play ("Slide");
		col.size = new Vector2(3.87f, 2.71f);
		rigid.drag = 0;
		if (!touchingFloor) {
			transform.Translate (Vector2.down * Time.deltaTime * speed);
		}
		transform.Translate (Vector2.right*Time.deltaTime*slideSpeed);
	}

	void RunRight(){
		anim.Play ("Run");
		col.size = new Vector2(3.87f, col.size.y);
		rigid.drag = 0;
		transform.Translate (Vector2.right*Time.deltaTime*speed);
	}

	void RunLeft(){
		anim.Play ("Run");
		col.size = new Vector2(3.87f, col.size.y);
		rigid.drag = 0;
		transform.Translate (Vector2.right * Time.deltaTime * speed);
	}
		
		

	void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject.tag =="Ground") {
			airTime = 0f;
			touchingFloor = true;
			jumps = 0;
		} else{
			touchingFloor = false;
		}
	}

}
