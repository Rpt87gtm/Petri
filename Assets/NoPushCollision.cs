using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoPushCollision : NetworkBehaviour
{
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CmdHandleCollision(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CmdHandleCollision(collision);
    }

    [ServerCallback]
    private void CmdHandleCollision(Collision2D collision)
    {
        // Получаем Rigidbody2D другого объекта
        Rigidbody2D otherRigidbody = collision.collider.GetComponent<Rigidbody2D>();

        if (otherRigidbody != null)
        {
            // Вычисляем вектор от центра другого объекта к центру текущего объекта
            Vector2 direction = (transform.position - collision.transform.position).normalized;

            // Применяем силу, чтобы оттолкнуть объекты друг от друга
            _rigidbody.velocity = direction * _rigidbody.velocity.magnitude;
            otherRigidbody.velocity = -direction * otherRigidbody.velocity.magnitude;
        }
    }
}
