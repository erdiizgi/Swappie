using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    private Player player;
    private World world;
    private CameraUtils camera;
    
    private int accelaratingFactor = 36;
    private bool isFinished; 

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

            player.UpdateVelocity(12);
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

                        this.world.RotateClockWise();
                        this.world.isRotatable = false;
                        this.camera.CameraSizeLerp(16.45f, 15);
                    }
                    break;

                //When the player fits into the Triangle Landscape
                case "TriangleLandscape":

                    if (this.world.isCounterRotatable)
                    {
                        if (this.player.Shape() != 2)
                        {
                            this.Restart();
                        }

                        this.world.RotateCounterClockWise();
                        this.camera.CameraSizeLerp(15, 12);
                        this.camera.CameraDown(4);
                        this.world.isCounterRotatable = false;
                        this.player.canAccelerate = true;
                        this.accelaratingFactor = 12;
                    }
                    break;

                //When the player collided with the accellerator
                case "Accelerator":

                    if (this.player.Shape() == 1 && this.player.canAccelerate == true)
                    {
                        this.player.SetVelocity(this.accelaratingFactor);
                        this.player.canAccelerate = false;
                    }
                    break;

                //When the player collides with target
                case "Target":

                    if (this.player.Shape() == 0)
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
