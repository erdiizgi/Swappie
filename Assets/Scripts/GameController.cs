using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    private Player player;
    private World world;
    private CameraUtils camera;
    
    private int accelaratingFactor = 36;
    private bool isFinished;

    public TouchGesture.GestureSettings GestureSetting;
    private TouchGesture touch;

	// Use this for initialization
	void Start () {
        
        //game has just started
        this.isFinished = false;
       
        //Get the Camera
        this.camera = new CameraUtils(GameObject.FindGameObjectWithTag("MainCamera")); 

        //Get the player
        this.player = (GameObject.FindGameObjectWithTag("Player")).GetComponent<Player>();

        //game
        this.world = new World(GameObject.FindGameObjectWithTag("World"), player);
        /*
        touch = new TouchGesture(this.GestureSetting);
        StartCoroutine(touch.CheckHorizontalSwipes(
            onLeftSwipe: () => { //player.Swap(); 
            },
            onRightSwipe: () => { //player.Swap(); 
            }
            ));
        */
	}
	
	// Update is called once per frame
	void Update () {
       
        this.CheckColliderTagToAct();

        if (this.isFinished != true)
        {
            //Change the shape TODO change it with touch
            if (Input.GetKeyDown(KeyCode.S))
            {
                this.player.Swap();
            }

            player.UpdateVelocity(this.transform.GetComponent<LevelInfo>().startingVelocity);
        }
        else
        {
            this.Restart();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.Restart();
        }
	}

    //Checks the collided tag and apply an action
    void CheckColliderTagToAct()
    {
        if (this.player.collidedTag != null)
        {
            Debug.Log(this.player.collidedTag);
            switch (this.player.collidedTag)
            {
                //When the player fits into the Square Landscape
                case "SquareLandscape":

                    if (this.world.isRotatable)
                    {
                        if (this.player.Shape() != 0)
                        {
                            this.Restart();
                        }

                        this.world.Rotate(this.player.reaction);
                        this.world.isRotatable = false;
                        if (this.player.reaction.cameraSize > 0)
                        {
                            this.camera.CameraSizeLerp(16.45f, this.player.reaction.cameraSize);
                        }
                       
                    }
                    break;

                //When the player fits into the Triangle Landscape
                case "TriangleLandscape":

                    if (this.world.isCounterRotatable)
                    {
                        if (this.player.Shape() != this.player.reaction.acceptedShape)
                        {
                            this.Restart();
                        }

                        this.world.Rotate(this.player.reaction);
                        if (this.player.reaction.cameraSize > 0)
                        {
                            this.camera.CameraSizeLerp(15, this.player.reaction.cameraSize); // 12
                        }

                        if (this.player.reaction.cameraDown > 0)
                        {
                            this.camera.CameraDown(this.player.reaction.cameraDown); // 4
                        }
                        
                        //
                        this.world.isCounterRotatable = false;
                        this.player.canAccelerate = true;
                        this.accelaratingFactor = 12;
                    }
                    break;

                //When the player collided with the accellerator
                case "Accelerator":
                    Debug.Log(player.canAccelerate);
                    if (this.player.Shape() == 1 && this.player.canAccelerate == true)
                    {
                        this.player.SetVelocity(this.player.reaction.velocity);
                        this.player.canAccelerate = false;
                    }
                    break;

                //When the player collides with target
                case "Target":

                    Debug.Log("he");
                    if (this.player.Shape() == this.player.reaction.acceptedShape)
                    {
                        this.isFinished = true;
                        
                    }
                    break;

                //When the player collides with dangereous area that is going to kill it
                case "Danger":

                    this.Restart();
                    break;
            }
        }
    }

    void Restart()
    {
        Application.LoadLevel(0);   
    }

}
