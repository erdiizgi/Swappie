using UnityEngine;
using System.Collections;

public class SwipeControlMenu : MonoBehaviour {

    public GameObject swipeControlObject;
    public TouchGesture.GestureSettings GestureSetting;
    private TouchGesture touch;

    void Start()
    {
       /* touch = new TouchGesture(this.GestureSetting);
        StartCoroutine(touch.CheckHorizontalSwipes(
            onLeftSwipe: () => { swipeControlObject.GetComponent<LevelSelector>().Next(); },
            onRightSwipe: () => { swipeControlObject.GetComponent<LevelSelector>().Previous(); }
            ));*/
    }
}
