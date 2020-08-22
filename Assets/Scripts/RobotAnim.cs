using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAnim : MonoBehaviour
{
    enum State
    {
        Idle = 1,
        Run,
        Jump
    }

    enum Direction
    {
        None = 1,
        Up,
        Down,
        Left,
        Right
    }

    public Animator animator;
    public SpriteRenderer spriteRender;
    private State state;

    void Start()
    {
        Idle();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (y > 0)
        {
            Run(Direction.Up, Vector3.up);
        }
        else if (y < 0)
        {
            Run(Direction.Down, Vector3.down);
        }

        if (x > 0)
        {
            Run(Direction.Right, Vector3.right);
        }
        else if (x < 0)
        {
            Run(Direction.Left, Vector3.left);
        }

        if (state == State.Run)
        {
            if (Mathf.Approximately(x, 0) && Mathf.Approximately(y, 0))
            {
                Idle();
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            Idle();
        }
    }

    private void Idle()
    {
        SetState(State.Idle);
    }

    private void Jump()
    {
        SetState(State.Jump);
    }

    private void Run(Direction direction, Vector3 normal)
    {
        SetState(State.Run);
        spriteRender.flipX = direction == Direction.Left;
        spriteRender.transform.position += normal * Time.deltaTime * 3;
    }

    private void SetState(State idle)
    {
        state = idle;
        animator.SetBool("run", state == State.Run);
        animator.SetBool("jump", state == State.Jump);
        animator.SetBool("idle", state == State.Idle);
    }
}
