using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBird : Bird
{
    [SerializeField]
    public float _boostForce = 100;
    public bool _hasBoost = false;

    public void Boost()
    {
        if (State == BirdState.thrown && !_hasBoost)
        {
            Rigidbody.AddForce(Rigidbody.velocity * _boostForce);
            _hasBoost = true;
        }
    }

    public override void OnTap()
    {
        Boost();
    }
}
