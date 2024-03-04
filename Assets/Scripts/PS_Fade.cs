using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_Fade : MonoBehaviour
{
    public float changeSpeed = 0.5f;

    void Start()
    {
        StartCoroutine(FadeOutAndDestroy());
    }

    IEnumerator FadeOutAndDestroy()
    {
        ParticleSystemRenderer renderer = GetComponentInChildren<ParticleSystemRenderer>();
        Material material = renderer.material;

        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * changeSpeed;
            material.SetFloat("_Alpha", alpha); 
            yield return null;
        }

        material.SetFloat("_Alpha", 1f);
        Destroy(gameObject);
    }
}
