using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class TempleController : MonoBehaviour
{

    public float _startingHp;
    public GameObject God;
    public GameObject GroundGod;
    public event TempleDestroyedEventHandler OnTempleDestroyed;

    void Start()
    {
        HP = _startingHp;
        _godController = God.GetComponent<GodController>();
        _groundGodController = GroundGod.GetComponent<GroundGodController>();

        _material = gameObject.GetComponent<Renderer>().material;

        _originColor = _material.color;
    }

    public void ApplyDamage(float damage)
    {
        HP -= damage;
        _material.color = new Color(_originColor.r, _originColor.g, _originColor.b, _originColor.a) * HP / _startingHp;
    }

    void Update()
    {
        if (HP <= 0)
        {
            StartCoroutine(TempleDestroyed());
            _material.color = new Color(1, 0, 0, _originColor.a);
        }
    }

    IEnumerator TempleDestroyed()
    {
        _godController.silenceGodSkills(true);
        yield return new WaitForSeconds(3);

        if (OnTempleDestroyed != null)
            OnTempleDestroyed.Invoke(this, gameObject.transform);
        gameObject.SetActive(false);
    }

    private float HP;
    private GodController _godController;
    private GroundGodController _groundGodController;
    private Color _originColor;
    private Material _material;
}
