using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public int currentSpeed;

    private string currentState;
    private Animator animator;
    private int currentDirection;
    private bool isOnGround;
    private bool isStucked;

    //Colliders
    private BoxCollider2D SquareCollider;
    private CircleCollider2D CircleCollider;
    private PolygonCollider2D TriangleCollider;

    //Rigidbody
    private Rigidbody2D rigidBody;

    //World
    private GameObject world;

    //Velocit Vector
    Vector3 velocity = new Vector3(1, 0, 0);

	// Use this for initialization
	void Start () {
        this.currentState = "Square"; // Game starts with Square
        this.animator = this.GetComponent<Animator>();
        this.currentDirection = 1; // 1 for RIGHT / -1 for LEFT

        this.world = GameObject.Find("World");
        this.isStucked = false;

        //Grab the colliders
        this.SquareCollider = this.GetComponent<BoxCollider2D>();
        this.CircleCollider = this.GetComponent<CircleCollider2D>();
        this.TriangleCollider = this.GetComponent<PolygonCollider2D>();

        //Grab the Rigidbody
        this.rigidBody = this.GetComponent<Rigidbody2D>();
        this.setRigidbody();
        LeanTween.rotate(this.gameObject, new Vector3(0, 0, 360), 1.5f).setEase(LeanTweenType.linear);
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
                this.RotateCircleAnimation(0);

               // this.transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(1,0), Time.deltaTime * this.currentSpeed);
                break;
        }
	}

    void LateUpdate()
    {
        if (this.currentState == "Circle" && isOnGround)
        {
            this.rigidBody.velocity = this.currentDirection * this.currentSpeed * (this.velocity.normalized);
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
        if (this.isStucked)
        {
            this.UnfixPlayer();
        }

        switch (this.currentState)
        {
            case "Square":
                this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                this.transform.eulerAngles = new Vector3(0, 0, 0);
                this.rigidBody.gravityScale = 10;
                this.rigidBody.mass = 10;
                break;

            case "Circle":
                this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                this.rigidBody.gravityScale = 5;
                this.rigidBody.mass = 5;
                break;

            case "Triangle":
                this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                this.transform.eulerAngles = new Vector3(0, 0, 0);
                this.rigidBody.gravityScale = -0.3f;
                this.rigidBody.mass = 5;
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Target"))
        {
            Config config = other.GetComponent<Config>();

            if (config.acceptedShape == this.currentState)
            {
                other.transform.GetComponent<TargetController>().TargetTrigger(this.gameObject);
                this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

                StartCoroutine(LoadLevel(config.levelIndex, 3f));
            }

            else
            {
                if (config.isTargetText)
                {
                    config.targetText.gameObject.SetActive(true);
                }
            }
        }

        else if (other.CompareTag("Danger"))
        {
            Config config = other.GetComponent<Config>();
            this.LoadLevelviaConf(config);
        }

        else if (other.CompareTag("Spiky Landscape"))
        {
            Config config = other.GetComponent<Config>();

            int id = LeanTween.rotate(this.world, new Vector3(0, 0, config.RotationInDeg), 0.5f).setEase(LeanTweenType.easeInOutSine).id;
            BeforeRotationEvents(config);
            LTDescr descr = LeanTween.descr(id);
            if (descr != null) // if the tween has already finished it will come back null
                descr.setOnComplete(() => AfterRotationEvents(config));

        }

        else if (other.CompareTag("Square Landscape"))
        {
            Config config = other.GetComponent<Config>();

            if (this.currentState == config.acceptedShape)
            {
                int id = LeanTween.rotate(this.world, new Vector3(0, 0, config.RotationInDeg), 0.5f).setEase(LeanTweenType.easeInOutSine).id;
                BeforeRotationEvents(config);
                LTDescr descr = LeanTween.descr(id);
                if (descr != null) // if the tween has already finished it will come back null
                    descr.setOnComplete(() => AfterRotationEvents(config));
            }

            else
            {
                this.LoadLevelviaConf(config);
            }
           
        }

        else if (other.CompareTag("Bounce"))
        {
            this.currentDirection *= -1;
        }
    }

    void LoadLevelviaConf(Config config)
    {
        Application.LoadLevel(config.levelIndex);
    }

    void BeforeRotationEvents(Config config)
    {
        if (config.isThereImmediateDestroy)
        {
            for (int i = 0; i < config.immeddiatetlyDestroyables.Length; i++)
            {
                int id = LeanTween.alpha(config.immeddiatetlyDestroyables[i], 0f, 0.2f).id;
                Destroy(config.immeddiatetlyDestroyables[i], 0.1f);
            }
        }
    }

    void AfterRotationEvents(Config config)
    {
        this.FixPlayer();

        this.isStucked = true;

        if (config.shouldChangeDir)
        {
            this.currentDirection *= -1;
        }

        if (config.isThereDestroyable)
        {
            for (int i = 0; i < config.destroyables.Length; i++)
            {
                int id = LeanTween.alpha(config.destroyables[i], 0f, 0.2f).id;
                Destroy(config.destroyables[i], 0.5f);
            }
        }

        if (config.isThereMovable)
        {
            LeanTween.move(config.movable, config.movableTarget.position, 0.5f);
        }

        if (config.isThereActivable)
        {
            for (int i = 0; i < config.activables.Length; i++)
            {
                int id = LeanTween.alpha(config.activables[i], 1f, 0.2f).id;
                config.activables[i].gameObject.SetActive(true);
            }
        }
    }

    void FixPlayer()
    {
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
    }

    void UnfixPlayer()
    {
        if (this.currentState != "Triangle")
        {
            this.isStucked = false;
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    void OnCollisionStay2D(Collision2D coll) 
    {
        this.isOnGround = coll.transform.CompareTag("Ground");
    }

    void FixedUpdate()
    {
        isOnGround = false;

    }

    void RotateCircleAnimation(int id)
    {
        LTDescr descr = LeanTween.descr(id);
        if (descr != null) // if the tween has already finished it will come back null
            descr.setOnComplete(() => RotateCircleAnimation(this.RotateCircle()));
    }

    int RotateCircle()
    {
        return LeanTween.rotate(this.gameObject, new Vector3(0, 0, 360), 1.5f).setEase(LeanTweenType.linear).id;
    }

    IEnumerator LoadLevel(int n, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Application.LoadLevel(n);
    }
}
