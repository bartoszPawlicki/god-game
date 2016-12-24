using System.Collections;
using UnityEngine;

namespace Assets.Scripts.God
{
    public class GroundLightningEffectController : MonoBehaviour
    {
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
            while (enabled)
            {
                yield return new WaitForSeconds(.25f);
                transform.localEulerAngles = new Vector3(Random.value * 360, 90, 90);
                material.mainTexture = lightningSprites[Mathf.RoundToInt(Random.value * (lightningSprites.Length - 1))];
            }
        }
    }
}
