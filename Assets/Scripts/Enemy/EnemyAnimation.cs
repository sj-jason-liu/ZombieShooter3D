using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void ChasingTrigger()
    {
        _anim.SetTrigger("Chasing");
    }

    public void StartAttacking(bool canAttack)
    {
        _anim.SetBool("CanAttack", canAttack);
    }

    public void Attacking()
    {
        EnemyAI enemy = gameObject.GetComponentInParent<EnemyAI>();
        if(enemy != null)
            enemy.Attacking();
    }
}
