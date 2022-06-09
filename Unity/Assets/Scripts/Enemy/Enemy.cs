using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy : MonoBehaviour
{
    public int hp;
    public float moveSpeed;

    public Transform enemyTransform;
    public Transform centerPos;
    public Transform frontPos;
    public Transform backPos;
    public Transform downPos;
    public Transform headPos;

    [SerializeField] protected Animator _anim;
    [SerializeField] protected Rigidbody2D enemyRB;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected SpriteRenderer enemySR;
}
