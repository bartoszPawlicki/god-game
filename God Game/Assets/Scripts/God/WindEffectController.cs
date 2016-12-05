using UnityEngine;

public class WindEffectController : MonoBehaviour {
    public ParticleSystem particles;

    public void SetEnabled(bool enabled)
    {
        ParticleSystem.EmissionModule em = particles.emission;
        if (enabled)
            em.rate = 50;
        else
            em.rate = 0;
    }
}
