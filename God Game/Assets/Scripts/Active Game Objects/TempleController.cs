using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class TempleController : MonoBehaviour
{

    public float StartingHp;
    public GameObject God;
    public GameObject GroundGod;
    public event TempleDestroyedEventHandler OnTempleDestroyed;

    
    private float _hp;
    public float HP
    {
        get { return _hp; }
        set
        {
            if (value != _hp)
            {
                _hp = value;
                Debug.Log(_hp);
                _material.color = new Color(_originColor.r, _originColor.g, _originColor.b, _originColor.a) * HP / StartingHp;
                if (_hp <= 0)
                {
                    _material.color = new Color(1, 0, 0, _originColor.a);
                    _hp = 0;
                    StartCoroutine(TempleDestroyed());
                    

                }
            }
        }
    }

    void Start()
    {
        _hp = StartingHp;
        _godController = God.GetComponent<GodController>();
        _groundGodController = GroundGod.GetComponent<GroundGodController>();

        _material = gameObject.GetComponent<Renderer>().material;

        _originColor = _material.color;
    }

    public void ApplyDamage(float damage)
    {
        HP -= damage;
        
    }

    void Update()
    {
        if (HP <= 0)
        {
            
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
    
    private GodController _godController;
    private GroundGodController _groundGodController;
    private Color _originColor;
    private Material _material;
}
