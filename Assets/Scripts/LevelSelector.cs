using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSelector : MonoBehaviour {

    public Sprite[] sprites;

    public int slot1Index;
    public int slot2Index;
    public int slot3Index;

    Image slot1;
    Image slot2;
    Image slot3;

	// Use this for initialization
	void Start () 
    {
        this.slot1Index = 0;
        this.slot2Index = 1;
        this.slot3Index = 2;

        slot1 = GameObject.Find("Slot1").transform.GetChild(0).GetComponent<Image>();
        slot2 = GameObject.Find("Slot2").transform.GetChild(0).GetComponent<Image>();
        slot3 = GameObject.Find("Slot3").transform.GetChild(0).GetComponent<Image>();
	}

    public void Next()
    {
        this.slot1Index = (this.slot1Index + 1) % this.sprites.Length;
        this.slot2Index = (this.slot2Index + 1) % this.sprites.Length;
        this.slot3Index = (this.slot3Index + 1) % this.sprites.Length;

        slot1.sprite = this.sprites[this.slot1Index];
        slot2.sprite = this.sprites[this.slot2Index];
        slot3.sprite = this.sprites[this.slot3Index];
    }

    public void Previous()
    {
        this.slot1Index = (this.slot1Index + (this.sprites.Length - 1)) % this.sprites.Length;
        this.slot2Index = (this.slot2Index + (this.sprites.Length - 1)) % this.sprites.Length;
        this.slot3Index = (this.slot3Index + (this.sprites.Length - 1)) % this.sprites.Length;

        slot1.sprite = this.sprites[this.slot1Index];
        slot2.sprite = this.sprites[this.slot2Index];
        slot3.sprite = this.sprites[this.slot3Index];
    }

    public void LoadSlot1()
    {
        Application.LoadLevel(this.slot1Index + 1);
    }

    public void LoadSlot2()
    {
        Application.LoadLevel(this.slot2Index + 1);
    }

    public void LoadSlot3()
    {
        Application.LoadLevel(this.slot3Index + 1);
    }
}
