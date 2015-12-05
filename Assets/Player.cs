using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private int shapeCount;
    private Shape[] shapes;
    private Rigidbody2D[] rigidBodies;
    
    private Vector3 currentPosition;
    private Vector3 currentVelocity;
    private int currentShape;

    public string collidedTag;
    private bool canAccelerateAtStart;
    public bool canAccelerate;
    public int direction;

	// Use this for initialization
	void Start () {

        this.collidedTag = null;
        this.canAccelerateAtStart = true;
        this.canAccelerate = true;
        this.shapeCount = this.transform.childCount;
        this.direction = 1; //1 shows right according to the first orientation

        this.shapes = new Shape[this.shapeCount];
        this.rigidBodies = new Rigidbody2D[this.shapeCount];

        //Get the shapes which are the children of the player
        for (int i = 0; i < this.shapeCount; i++)
        {
            this.shapes[i] = (this.transform.GetChild(i).gameObject).GetComponent<Shape>();
            this.rigidBodies[i] = this.shapes[i].GetComponent<Rigidbody2D>();

            //Initialize the active shape child index
            if (shapes[i].transform.gameObject.activeInHierarchy == true)
            {
                this.currentShape = i;
            }
        }        
	}

    void SetCollideTags()
    {
        this.collidedTag = this.shapes[this.currentShape].collidedTag;
    }

	// Update is called once per frame
	void Update () {
        //Set all the shapes with the currently active shape's position
        this.currentPosition = shapes[this.currentShape].transform.position;
        this.currentVelocity = this.rigidBodies[this.currentShape].velocity;
        this.SetCollideTags();
	}

    public void Swap()
    {
        for (int i = 0; i < this.shapes.Length; i++)
        {
            shapes[i].gameObject.SetActive(false);
        }

        this.currentShape = (this.currentShape + 1) % 3;

        this.shapes[this.currentShape].gameObject.SetActive(true);
        this.shapes[this.currentShape].transform.position = currentPosition;
        this.rigidBodies[this.currentShape].velocity = new Vector3(this.currentVelocity.x / 2, 0, 0);
    }

    //This function is called in the beginning of the game
    public void UpdateVelocity(float x)
    {
        //If the status is circle give some velocity to it at the beginning of the game
        if (this.canAccelerateAtStart == true && this.currentShape == 1)
        {
            this.canAccelerateAtStart = false;
            this.rigidBodies[this.currentShape].velocity = new Vector2(x, 0);
        }
    }

    //Set the player's velocity to x
    public void SetVelocity(float x)
    {
        this.rigidBodies[this.currentShape].velocity = new Vector2(x * this.direction, 0);
    }

    //Returns the current shape index
    public int Shape()
    {
        return this.currentShape;
    }

    //After rotating the world, triangle is the only shape that has to be rotate
    public void RotateTriangle(float degree)
    {
        this.shapes[2].transform.eulerAngles = new Vector3(0, 0, degree);
    }

    //Changes the direction of the player
    public void ChangeDirection()
    {
        if (this.direction == 1)
        {
            this.direction = -1;
        }
        else
        {
            this.direction = 1;
        }
    }

}
