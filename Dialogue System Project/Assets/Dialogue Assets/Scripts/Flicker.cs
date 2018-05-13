using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to make a light flicker randomly
[RequireComponent(typeof(Light))]
public class Flicker : MonoBehaviour {

    public bool on;
    public float maxIncrease = 1f;
    public float maxDecrease = 1f;
    public float rate = 0.1f;       // Time between flickers
    public float strength = 300f;

    private Light lightSource;
    private float defaultIntensity;

    private float flickerTimer;


	// Use this for initialization
	void Start () {
        lightSource = gameObject.GetComponent<Light>();
        defaultIntensity = lightSource.intensity;
        flickerTimer = rate;
	}

    // Update is called once per frame
    void Update()
    {
        if (on)
        {
            flickerTimer -= Time.deltaTime;
            if (flickerTimer <= 0) {
                lightSource.intensity = Mathf.Lerp(lightSource.intensity, Random.Range(defaultIntensity - maxDecrease, defaultIntensity + maxIncrease), strength * Time.deltaTime);

                flickerTimer = rate;
            }
        } else
        {
            lightSource.intensity = defaultIntensity;
        }
    }
}
