using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;

public class PlayerCollider : MonoBehaviour {

	// PUBLIC INSTANCE VARIABLES - Exposed on the Inspector
	public GameObject impact;
	public GameObject explosion;
	public GameController gameController;
	
	// PRIVATE INSTANCE VARIABLES 
	private GameObject[] _impacts;
	private int _currentImpact = 0;
	private int _maxImpacts = 5; 
	private Transform _transform;
	
	//private bool _shooting = false;
	
	// Use this for initialization
	void Start () {
		// reference to the gameObject's transform component
		this._transform = gameObject.GetComponent<Transform> ();
		
		// Object pool for impacts
		this._impacts = new GameObject[this._maxImpacts];
		for (int impactCount = 0; impactCount < this._maxImpacts; impactCount++) {
			this._impacts[impactCount] = (GameObject) Instantiate(this.impact);
		}
		
	}
	
	// Update is called once per frame

		
	
	
	// Physics effects
	void FixedUpdate() {
			RaycastHit hit;
			if(Physics.Raycast(this._transform.position, this._transform.forward, out hit, 50f)) {
				if(hit.transform.CompareTag("Coin")) {
					Destroy(hit.transform.gameObject);
					gameController.AddScore(10); // add 10 points
				}
			if(hit.transform.CompareTag("Mine")) {
				Destroy(hit.transform.gameObject);
				gameController.SubtractLives(20); // subtracts 20 life points
			}
			if(hit.transform.CompareTag("Health")) {
				Destroy(hit.transform.gameObject);
				gameController.AddLives(20); // adds 20 life points
			}

				
				//move impact particle system to location of ray hit
				this._impacts[this._currentImpact].transform.position = hit.point;
				// play the particle effect (impact)
				this._impacts[this._currentImpact].GetComponent<ParticleSystem>().Play();
				
				
				// ensure that you don't go out of bounds of the object pool
				if(++this._currentImpact >= this._maxImpacts) {
					this._currentImpact = 0;
				}
				
			}
		}
	}

