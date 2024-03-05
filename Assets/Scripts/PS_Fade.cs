using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_Fade : MonoBehaviour
{
    public float changeSpeed = 0.5f;
    public float endAlpha = 1f;

    void Start()
    {
        StartCoroutine(FadeOutAndDestroy());
    }

    IEnumerator FadeOutAndDestroy()
    {
        ParticleSystemRenderer renderer = GetComponentInChildren<ParticleSystemRenderer>();
        Material material = renderer.material;

        float alpha = 0.01f;
        while (alpha < endAlpha)
        {
            alpha += Time.deltaTime * changeSpeed;
            material.SetFloat("_Alpha", alpha); 
            yield return null;
        }

        material.SetFloat("_Alpha", 1f);
        Destroy(gameObject);
    }
}
