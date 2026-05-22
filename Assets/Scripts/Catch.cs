using System;
using UnityEngine;

public class Catch : MonoBehaviour
{
    [SerializeField] private Trorn _trorn1;
    [SerializeField] private Trorn _trorn2;


    [SerializeField] private int amount = 0;

    public void AddCoin(int x)
    {
        amount++;
        if (amount >= 5) Falling();
        Debug.Log(amount);
    }

    public void Falling()
    {
     Rigidbody2D  _rb1 = _trorn1.GetComponent<Rigidbody2D>();
        _rb1.bodyType = RigidbodyType2D.Dynamic;    
        _trorn1.FallingDown();

        Rigidbody2D _rb2 = _trorn2.GetComponent<Rigidbody2D>();
        _rb2.bodyType = RigidbodyType2D.Dynamic;
        _trorn2.FallingDown();
    }

}
