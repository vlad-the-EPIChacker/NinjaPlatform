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
	private bool parachute = false;
	private bool touchingFloor = false;
	private bool mustSlide = false;
	private bool alive = true;
	private float airTime = 0f;
	private int jumps = 0;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rigid = GetComponent<Rigidbody2D> ();
		col = GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!alive) {
			StartCoroutine(Death());
		}
		if (!touchingFloor && alive) {
			airTime += Time.deltaTime;
		} else if(touchingFloor && alive){
			parachute = false ;
		}
		transform.localRotation = new Quaternion();
		col.size = new Vector2(1.93f, 4.58f);
		if (jumps > 0 && alive) {
			touchingFloor = false;
		}

		if (this.anim.GetCurrentAnimatorStateInfo (0).IsName ("Slide") && !mustSlide && alive) {
			col.size = new Vector2(3.87f, 2.71f);
			if (!touchingFloor) {
				transform.Translate (Vector2.down * Time.deltaTime * speed);
			}
		}

		if (parachute && alive) {
			anim.Play ("Fall");
		}

		if (/*airTime > 3f*/Input.GetKey (KeyCode.Space) && !touchingFloor && alive) {
			parachute = true;
			Fall ();
		} else if (Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.D) && touchingFloor && alive && !mustSlide) {
			if (transform.localScale.x < 0) {
				transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
			}
			JumpRight ();
			jumps += 1;
		} else if (Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.A) && touchingFloor && alive && !mustSlide) {
			if (transform.localScale.x > 0) {
				transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
			}
			JumpLeft ();
			jumps += 1;
		} else if (Input.GetKey (KeyCode.W) && touchingFloor && jumps < 2 && alive && !mustSlide) {
			Jump ();
			jumps += 1;
		} else if (Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.A) && alive) {
			if (transform.localScale.x > 0) {
				transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
			}
			SlideLeft ();
		} else if (Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.D) && alive) {
			if (transform.localScale.x < 0) {
				transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
			}
			SlideRight ();

		} else if ((Input.GetKey (KeyCode.S) || mustSlide)&& alive) {
			Slide ();
		}else if (Input.GetKey (KeyCode.D) && alive && !mustSlide) {
			if (transform.localScale.x < 0) {
				transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
			}
			if (!touchingFloor && alive) {
				transform.Translate (Vector2.right * Time.deltaTime * speed);
			} else if(alive){
				RunRight ();
			}
		} else if (Input.GetKey (KeyCode.A) && alive && !mustSlide) {
			
			if (transform.localScale.x > 0) {
				transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

			}
			if (!touchingFloor && alive) {
				transform.Translate (Vector2.right * Time.deltaTime * speed);
			} else if (alive){
				RunLeft ();
			}
		}


	}

	IEnumerator Death(){
		Animation death = GetComponent<Animation> ();
		anim.Play ("Death");
		yield return new WaitForSeconds (0.833f);
		anim.Stop ();
		this.enabled = false;
	}

	void Fall(){
			col.size = new Vector2 (3.87f, col.size.y);
			rigid.drag = fallDrag;
		
	}

	void JumpRight(){
		parachute = false;
		anim.Play ("Jump");
		col.size = new Vector2(3.87f, col.size.y);
		rigid.drag = 0;
		rigid.AddForce (Vector2.up*jumpForce);
		rigid.AddForce (Vector2.right*speed);
	}

	void JumpLeft(){
		parachute = false;
		anim.Play ("Jump");
		col.size = new Vector2(3.87f, col.size.y);
		rigid.drag = 0;
		rigid.AddForce (Vector2.up*jumpForce);
		rigid.AddForce (Vector2.right*speed);
	}

	void Jump(){
		parachute = false;
		anim.Play ("Jump");
		col.size = new Vector2(3.87f, col.size.y);
		rigid.drag = 0;
		rigid.AddForce (Vector2.up*jumpForce);
	}
		
	void SlideLeft(){
		parachute = false;
		anim.Play ("Slide");
		col.size = new Vector2(3.87f, 2.71f);
		rigid.drag = 0;
		if (!touchingFloor) {
			transform.Translate (Vector2.down * Time.deltaTime * speed);
		}
		transform.Translate (Vector2.right*Time.deltaTime*slideSpeed);
	}

	void SlideRight(){
		parachute = false;
		anim.Play ("Slide");
		col.size = new Vector2(3.87f, 2.71f);
		rigid.drag = 0;
		if (!touchingFloor) {
			transform.Translate (Vector2.down * Time.deltaTime * speed);
		}
		transform.Translate (Vector2.right*Time.deltaTime*slideSpeed);
	}

	void Slide(){
		parachute = false;
		anim.Play ("Slide");
		col.size = new Vector2(3.87f, 2.71f);
		rigid.drag = 0;
		if (!touchingFloor) {
			transform.Translate (Vector2.down * Time.deltaTime * speed);
		}
	}

	void RunRight(){
		parachute = false;
		anim.Play ("Run");
		col.size = new Vector2(3.87f, col.size.y);
		rigid.drag = 0;
		transform.Translate (Vector2.right*Time.deltaTime*speed);
	}

	void RunLeft(){
		parachute = false;
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
		if (collision.gameObject.tag == "MustSlide") {
			mustSlide = true;
		}
	}

	void OnCollisionExit2D(Collision2D collision){
		if(collision.gameObject.tag =="Ground") {
			touchingFloor = false;
		}	
		if (collision.gameObject.tag == "MustSlide") {
			mustSlide = false;
		}
	}

	void OnTriggerEnter2D(Collider2D collision){
		if (collision.gameObject.tag == "Acid") {
			print ("Acid");
			rigid.drag = fallDrag * 4;
			alive = false;
		} else if (collision.gameObject.tag == "Spike") {
			print ("Spike");
			alive = false;
		}
	}

}
