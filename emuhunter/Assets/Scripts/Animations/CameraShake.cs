using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	private Vector3 originPosition;
	private Quaternion originRotation;
	
	private float shakeDecay;
	private float shakeIntensity;
	
	void OnGUI() {
        if (GUI.Button(new Rect(20, 40, 80, 20), "Shake")) {
            Shake();
        }
    }
	
	void Update() {
		if (shakeIntensity > 0) {
			transform.position = originPosition + Random.insideUnitSphere * shakeIntensity;
			transform.rotation = new Quaternion(
				originRotation.x + Random.Range(-shakeIntensity, shakeIntensity) * .2f,
				originRotation.y + Random.Range(-shakeIntensity, shakeIntensity) * .2f,
				originRotation.z + Random.Range(-shakeIntensity, shakeIntensity) * .2f,
				originRotation.w + Random.Range(-shakeIntensity, shakeIntensity) * .2f);
			shakeIntensity -= shakeDecay;
		}
	}
	
	void Shake(float intensity = 0.2f, float decay = 0.02f) {
		originPosition = transform.position;
		originRotation = transform.rotation;
		shakeIntensity = intensity;
		shakeDecay = decay;
	}
}
