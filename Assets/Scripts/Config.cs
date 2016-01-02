using UnityEngine;
using System.Collections;

public class Config : MonoBehaviour {

    //Congiguration for the shape
    public string acceptedShape;

    //Is it a target point dead point?
    public bool shouldLoadLevel;
    public int levelIndex;

    //Rotation
    public float RotationInDeg;

    //Change Direction after a rotation
    public bool shouldChangeDir;

    //Destroy Something
    public bool isThereDestroyable;
    public GameObject[] destroyables;

    //Movable
    public bool isThereMovable;
    public GameObject movable;
    public Transform movableTarget;

    //Activable
    public bool isThereActivable;
    public GameObject[] activables;

}
