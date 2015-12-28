using UnityEngine;
using System;
using System.Collections;

public class TapController : MonoBehaviour {

    public GameObject player;


    protected virtual void OnEnable()
    {
        // Hook into the OnFingerTap event
        Lean.LeanTouch.OnFingerTap += OnFingerTap;
    }

    protected virtual void OnDisable()
    {
        // Unhook into the OnFingerTap event
        Lean.LeanTouch.OnFingerTap -= OnFingerTap;
    }

    public void OnFingerTap(Lean.LeanFinger finger)
    {
        // Does the prefab exist?
        if (player != null)
        {
            // Make sure the finger isn't over any GUI elements
            player.GetComponent<Player>().Swap();

        }
    }

}
