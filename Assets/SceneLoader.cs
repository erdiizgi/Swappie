using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour {

    public void LoadScene(int n)
    {
        Application.LoadLevel(n);
    }
}
