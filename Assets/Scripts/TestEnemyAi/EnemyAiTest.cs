using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiTest : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    private IsometricCharacterRenderer isoRenderer;
    private Rigidbody2D rbody;
    private Vector2 currentPos = new Vector2();
    [SerializeField] private Rigidbody2D target;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
    }

    private void FixedUpdate()
    {
        //Vector3 movePosition = transform.position;

        currentPos = rbody.position;
        //isoRenderer.SetDirection()
        //transform.Translate(target.transform.position);
        float x = target.position.x - rbody.transform.position.x;
        float y = target.position.y - rbody.transform.position.y;
        rbody.MovePosition((currentPos + target.position) * Time.fixedDeltaTime * movementSpeed);
    }
}
