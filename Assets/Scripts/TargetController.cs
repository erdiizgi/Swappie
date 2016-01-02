using UnityEngine;
using System.Collections;

public class TargetController : MonoBehaviour {

    Transform Magnet;
    bool canBeTriggered;
    void Start()
    {
        this.Magnet = this.transform.GetChild(0);
        this.canBeTriggered = true;
    }

    public void TargetTrigger(GameObject player)
    {
        if (this.canBeTriggered)
        {
            this.canBeTriggered = false;
            this.RunMagnet(player);
        }
    }

    private void RunMagnet(GameObject player)
    {
        LeanTween.move(player, this.Magnet.position, 0.5f).setEase(LeanTweenType.easeInOutBack);
    }

}
