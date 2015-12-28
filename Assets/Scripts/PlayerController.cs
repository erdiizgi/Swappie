using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public int currentSpeed;

    private string currentState;
    private Animator animator;
    private int currentDirection;

    //Colliders
    private BoxCollider2D SquareCollider;
    private CircleCollider2D CircleCollider;
    private PolygonCollider2D TriangleCollider;

    //Rigidbody
    private Rigidbody2D rigidBody;

    //Velocit Vector
    Vector3 velocity = new Vector3(1, 0, 0);

	// Use this for initialization
	void Start () {
        this.currentState = "Square"; // Game starts with Square
        this.animator = this.GetComponent<Animator>();
        this.currentDirection = 1; // 1 for RIGHT / -1 for LEFT

        //Grab the colliders
        this.SquareCollider = this.GetComponent<BoxCollider2D>();
        this.CircleCollider = this.GetComponent<CircleCollider2D>();
        this.TriangleCollider = this.GetComponent<PolygonCollider2D>();

        //Grab the Rigidbody
        this.rigidBody = this.GetComponent<Rigidbody2D>();
        this.setRigidbody();
	}
	
	// Update is called once per frame
	void Update () {
        
        this.ReceiveInput();

        switch (this.currentState)
        {
            case "Square":

                this.SquareCollider.enabled = true;
                this.CircleCollider.enabled = false;
                this.TriangleCollider.enabled = false;
                break;

            case "Triangle":

                this.SquareCollider.enabled = false;
                this.CircleCollider.enabled = false;
                this.TriangleCollider.enabled = true;
                break;

            case "Circle":

                this.SquareCollider.enabled = false;
                this.CircleCollider.enabled = true;
                this.TriangleCollider.enabled = false;
               // this.transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(1,0), Time.deltaTime * this.currentSpeed);
                break;
        }
	}

    void LateUpdate()
    {
		if (this.currentState == "Circle" && isOnGround())
        {
            this.rigidBody.velocity = this.currentSpeed * (this.velocity.normalized);
		}
    }

    void ReceiveInput()
    {
        if (Input.GetKeyDown(KeyCode.A)) //Square
        {
            this.PressSquare();
        }

        else if (Input.GetKeyDown(KeyCode.S)) //Circle
        {
            this.PressCircle();
        }

        else if (Input.GetKeyDown(KeyCode.D)) //Triangle
        {
            this.PressTriangle();
        }
    }

    
    public void PressSquare()
    {
        switch (this.currentState)
        {
            case "Circle":

                this.animator.SetInteger("Status", 21);
                break;

            case "Triangle":

                this.animator.SetInteger("Status", 31);
                break;
        }

        this.currentState = "Square";
        this.setRigidbody();
    }

    public void PressCircle()
    {
        switch (this.currentState)
        {
            case "Square":
                
                this.animator.SetInteger("Status", 12);
                break;

            case "Triangle":
               
                this.animator.SetInteger("Status", 32);
                break;
        }

        this.currentState = "Circle";
        this.setRigidbody();
    }

    public void PressTriangle()
    {
        switch (this.currentState)
        {
            case "Square":

                this.animator.SetInteger("Status", 13);
                break;

            case "Circle":

                this.animator.SetInteger("Status", 23);
                break;
        }

        this.currentState = "Triangle";
        this.setRigidbody();
    }

    private void setRigidbody()
    {
        switch (this.currentState)
        {
            case "Square":

                this.rigidBody.gravityScale = 10;
                this.rigidBody.mass = 10;
                break;

            case "Circle":

                this.rigidBody.gravityScale = 5;
                this.rigidBody.mass = 5;
                break;

            case "Triangle":

                this.rigidBody.gravityScale = -1;
                this.rigidBody.mass = 5;
                break;
        }
    }

	private bool isOnGround()
	{
		return Physics2D.Raycast (transform.position, Vector3.down, CircleCollider.bounds.extents.y + 0.1f);
	}
}
