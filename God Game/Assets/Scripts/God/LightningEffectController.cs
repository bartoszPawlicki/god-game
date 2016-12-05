using System.Collections;
using UnityEngine;

public class LightningEffectController : MonoBehaviour {

    public Texture2D[] lightningSprites;
    private Material material;

    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

	void OnEnable()
    {
        StartCoroutine(RandomizerCoroutine());
    }

    private IEnumerator RandomizerCoroutine()
    {
        while(enabled)
        {
            yield return new WaitForSeconds(.25f);
            transform.rotation = Quaternion.Euler(new Vector3(0, Random.value * 360));
            material.mainTexture = lightningSprites[Mathf.RoundToInt(Random.value * (lightningSprites.Length -1))];
        }
    }
}
