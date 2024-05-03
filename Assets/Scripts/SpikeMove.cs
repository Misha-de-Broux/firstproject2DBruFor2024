using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class SpikeMove : MonoBehaviour
{
    [SerializeField] float speed = 0.5f;
    private SpriteRenderer renderer;
    private Animator animator;
    [SerializeField] int direction = -1;
    float halfSize;
    float height;
    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        renderer.flipX = direction > 0;
        halfSize = GetComponent<Collider2D>().bounds.size.x/1.99f * transform.lossyScale.x;
        height = GetComponent<Collider2D>().bounds.size.y * transform.lossyScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 origin = transform.position + direction * halfSize * transform.right;
        if(Physics2D.Raycast(origin + transform.up * 0.1f * height, direction * transform.right, 0.01f)) {
            Rotate();
        } else if (Physics2D.Raycast(origin + transform.up * 0.9f * height, direction * transform.right, 0.01f)) {
            Rotate();
        }
        else if(!Physics2D.Raycast(origin, - transform.up, 1)) {
            Rotate();
        }
        transform.Translate(direction * transform.right * Time.deltaTime * speed);

        
    }
    void Rotate() {
        direction *= -1;
        renderer.flipX = direction > 0;
    }
}
