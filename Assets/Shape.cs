using UnityEngine;
using System.Collections;

public class Shape : MonoBehaviour
{
    public string collidedTag;

    // Use this for initialization
    void Start()
    {
        this.collidedTag = null;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        this.collidedTag = other.gameObject.tag;    //Possible Tags = "SquareLandscape" , "Accelerator" , "TriangleLandscape" , "Target"  , "Danger"            
    }
}
