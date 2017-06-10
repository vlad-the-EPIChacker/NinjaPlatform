using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	[SerializeField] private GameObject player;
	public float smoothTime = -1f;
	private Vector2 velocity = Vector2.zero;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector2 targetPosition = player.transform.position;
		transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
		transform.position = new Vector3 (transform.position.x, transform.position.y, -10f); 	
	
	}
		
}
