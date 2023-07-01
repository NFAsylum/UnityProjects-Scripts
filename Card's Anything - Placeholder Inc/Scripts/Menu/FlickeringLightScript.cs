using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLightScript : MonoBehaviour
{
    [SerializeField]
    float minDelay, maxDelay, disabledMinDelay, disabledMaxDelay;
    float originalIntensity;

    bool isFlicking;

    void Start()
    {
        if (disabledMinDelay == 0)
        {
            disabledMinDelay = minDelay;
            disabledMaxDelay = maxDelay;
        }

        originalIntensity = GetComponent<Light>().intensity;

        StartCoroutine(Flick());
    }

    IEnumerator Flick()
    {
        while (true)
        {
            if (!isFlicking)
            {
                isFlicking = true;

                yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

                GetComponent<Light>().intensity = 0;

                yield return new WaitForSeconds(Random.Range(disabledMinDelay * 0.15f, disabledMaxDelay * 0.15f));

                GetComponent<Light>().intensity = 0.8f * originalIntensity;

                yield return new WaitForSeconds(Random.Range(minDelay * 0.1f, maxDelay * 0.1f));

                GetComponent<Light>().intensity = 0.3f * originalIntensity;

                yield return new WaitForSeconds(Random.Range(disabledMinDelay * 0.15f, disabledMaxDelay * 0.15f));

                GetComponent<Light>().intensity = 0.8f * originalIntensity;

                yield return new WaitForSeconds(Random.Range(minDelay * 0.2f, maxDelay * 0.2f));

                GetComponent<Light>().intensity = 0;

                yield return new WaitForSeconds(Random.Range(disabledMinDelay * 0.35f, disabledMaxDelay * 0.35f));

                GetComponent<Light>().intensity = originalIntensity;

                yield return new WaitForSeconds(Random.Range(minDelay * 2, maxDelay * 2));

                isFlicking = false;
            }
        }
    }
}
