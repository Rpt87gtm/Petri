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
        // �������� Rigidbody2D ������� �������
        Rigidbody2D otherRigidbody = collision.collider.GetComponent<Rigidbody2D>();

        if (otherRigidbody != null)
        {
            // ��������� ������ �� ������ ������� ������� � ������ �������� �������
            Vector2 direction = (transform.position - collision.transform.position).normalized;

            // ��������� ����, ����� ���������� ������� ���� �� �����
            _rigidbody.velocity = direction * _rigidbody.velocity.magnitude;
            otherRigidbody.velocity = -direction * otherRigidbody.velocity.magnitude;
        }
    }
}
