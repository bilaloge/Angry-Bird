using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEditor.PackageManager;
using UnityEngine;

[SelectionBase]

public class Monster : MonoBehaviour
{
    [SerializeField] Sprite _deadSprite;
    [SerializeField] ParticleSystem _particleSystem;
    bool _hasDied;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShouldDieOnCollision(collision))
        {
            StartCoroutine(Die());
        }
    }

    bool ShouldDieOnCollision(Collision2D collision)
    {
        if(_hasDied)
            return false;
        Bird bird = collision.gameObject.GetComponent<Bird>();
        if (bird != null)
        {
            return true;
        }
        if (collision.contacts[0].normal.y < -0.5)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    IEnumerator Die()
    {
        _hasDied = true;
        GetComponent<SpriteRenderer>().sprite = _deadSprite;
        _particleSystem.Play();
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
