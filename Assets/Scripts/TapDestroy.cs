using UnityEngine;
using System.Collections;

public class TapDestroy : MonoBehaviour {

    public GameObject activateThis;
    public GameObject activateVirtualController;
    public GameObject destroyThis;
    public GameObject activateText;

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
        this.DestroyThis();
    }

    public void DestroyThis()
    {
        int id = LeanTween.alpha(this.gameObject, 0f, 0.5f).id;

        LTDescr descr = LeanTween.descr(id);
        if (descr != null) // if the tween has already finished it will come back null
            descr.setOnComplete(() => ActivateThese());

        Destroy(this.gameObject, 0.8f);
        Destroy(this.destroyThis);
    }

    public void ActivateThese()
    {
        activateThis.gameObject.SetActive(true);
        activateVirtualController.gameObject.SetActive(true);
        activateText.gameObject.SetActive(true);
    }
}
