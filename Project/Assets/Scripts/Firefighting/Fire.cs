using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField, Range(0f,1f)] private float currentIntensity=1.0f;
    [SerializeField] private ParticleSystem[] fireParticleSystems= new ParticleSystem[0];
    private float[] startIntensities = new float[0];
    float timeLastWatered = 0;
    [SerializeField] private float regenDelay = 2.5f;
    [SerializeField] private float regenRate = .1f;
    private bool isLit = true;

    private void ChangeIntensity()
    {
        for (int i = 0; i < fireParticleSystems.Length; i++)
        {
            var emission = fireParticleSystems[i].emission;
            emission.rateOverTime = currentIntensity * startIntensities[i];
        }

    }

    public bool TryExtinguish(float amount)
    {
        /*timeLastWatered = Time.time;
         currentIntensity -= amount;
         if (currentIntensity <= 0)
         {
             isLit = false;
             return true;
         }

         return false; //fire is still lit*/

        currentIntensity -= amount;
        ChangeIntensity();
        return currentIntensity<=0;
    }

    // Start is called before the first frame update
    void Start()
    {
        startIntensities = new float[fireParticleSystems.Length];
        for (int i = 0; i < fireParticleSystems.Length; i++)
        {
            startIntensities[i] = fireParticleSystems[i].emission.rateOverTime.constant;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (isLit && currentIntensity < 1.0f && Time.time - timeLastWatered >= regenDelay)
        {
            currentIntensity += regenRate * Time.deltaTime;
            ChangeIntensity();
        }
    }
}
