using System.Collections;
using UnityEngine;

public class WindEffectController : MonoBehaviour {
    public ParticleSystem particles;
    public ParticleSystem indicatorParticles;
    public WindZone windZone;
    public void SetEnabled(bool enabled)
    {
        ParticleSystem.EmissionModule em = particles.emission;
        if (enabled)
        {
            em.rate = 50;
            StartCoroutine(EnableWind());
        }
        else
        {
            em.rate = 0;
            StartCoroutine(DisableWind());
        }
    }

    public void SetIndicatorEnabled(bool enabled)
    {
        ParticleSystem.EmissionModule em = indicatorParticles.emission;
        if (enabled)
            em.rate = 50;
        else
            em.rate = 0;
    }
    private IEnumerator DisableWind()
    {
        for(int i = 5; i >= 0; i--)
        {
            windZone.windMain = i / 2.0f;
            yield return new WaitForSeconds(.1f);
        }
    }
    private IEnumerator EnableWind()
    {
        for (int i = 0; i <= 6; i++)
        {
            windZone.windMain = i / 2.0f;

            Debug.Log(windZone.windMain);
            yield return new WaitForSeconds(.1f);
        }
    }
}
