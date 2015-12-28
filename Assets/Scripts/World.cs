using UnityEngine;
using System.Collections;

public class World {

    //gamewordl
    GameObject gameWorld;
    Player player;

    public bool isRotatable;
    public bool isCounterRotatable;

	// Use this for initialization
	public World (GameObject world, Player player) 
    {    
        //gameworld
        this.gameWorld = world;
        this.player = player;
        this.isRotatable = true;
        this.isCounterRotatable = true;
	}

    public void RotateClockWise()
    {
        LeanTween.rotate(this.gameWorld, new Vector3(0, 0, -90), 0.5f).setEase(LeanTweenType.easeInOutSine);
     
        this.player.RotateTriangle(90);   
    }

    public void RotateCounterClockWise()
    {
        int id = LeanTween.rotate(this.gameWorld, new Vector3(0, 0, 180), 0.5f).setEase(LeanTweenType.easeInOutSine).id;

        LTDescr descr = LeanTween.descr(id);
        if (descr != null) // if the tween has already finished it will come back null
            descr.setOnComplete(() => this.player.RotateTriangle(0));

        this.player.ChangeDirection();
        this.player.SetVelocity(12);
    }

    public void Rotate(Reaction reaction)
    {
        int id = LeanTween.rotate(this.gameWorld, new Vector3(0, 0, reaction.worldRotation), 0.5f).setEase(LeanTweenType.easeInOutSine).id;

        if (reaction.triangleRotation != -1)
        {
            LTDescr descr = LeanTween.descr(id);
            if (descr != null) // if the tween has already finished it will come back null
                descr.setOnComplete(() => this.player.RotateTriangle(reaction.triangleRotation));
        }

        if (reaction.shouldChangeDirection)
        {
            this.player.ChangeDirection();
        }

        if (reaction.velocity > 0)
        {
            this.player.SetVelocity(12);
        }
    }

}
