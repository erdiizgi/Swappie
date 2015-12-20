using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {

    public void LoadScene(int n, GameObject levelInformation)
    {
        switch(n){
            case 1:
                Application.LoadLevel(levelInformation.GetComponent<LevelSelector>().slot1Index);
                break;
            case 2:
                Application.LoadLevel(levelInformation.GetComponent<LevelSelector>().slot2Index);
                break;
            case 3:
                Application.LoadLevel(levelInformation.GetComponent<LevelSelector>().slot3Index);
                break;
        }
    }
}
