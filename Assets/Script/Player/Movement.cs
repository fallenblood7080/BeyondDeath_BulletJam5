using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletJam.Player
{
    using Core.Input;

    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour
    {
        private Rigidbody2D body;

        private Vector2 dir;

        [SerializeField] private float moveSpeed;

        [SerializeField] private Animator animator;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            InputManager.CreateInstance();
        }

        private void Update()
        {
            dir = InputManager.Instance.MoveInputs;
            animator.SetBool("IsMove", dir.sqrMagnitude > 0.1f);
        }

        private void FixedUpdate()
        {
            body.MovePosition(body.position + moveSpeed * Time.fixedDeltaTime * dir);
        }
    }
}