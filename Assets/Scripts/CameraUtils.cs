using UnityEngine;
using System.Collections;

public class CameraUtils {

    private GameObject camera;

	// Use this for initialization
	public CameraUtils(GameObject camera) 
    {
        this.camera = camera;
	}

    public void CameraSizeLerp(float start, float end)
    {
        LeanTween.value(camera, start, end, 0.5f).setDelay(0.5f).setOnUpdate((float val) =>
        {
            this.camera.GetComponent<Camera>().orthographicSize = val;
        });
    }

    public void CameraDown(int y)
    {
        Vector3 position = this.camera.GetComponent<Transform>().position;
        LeanTween.value(camera, position, new Vector3(position.x, position.y - y, position.z), 0.5f).setOnUpdate((Vector3 val) =>
        {
            this.camera.GetComponent<Transform>().position = val;
        });
    }
}
