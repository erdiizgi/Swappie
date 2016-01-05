using UnityEngine;
using System.Collections;

public class IntroSceneAnimationLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Handheld.PlayFullScreenMovie("swappie-intro-video.mp4", Color.black, FullScreenMovieControlMode.Hidden);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
