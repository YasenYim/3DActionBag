using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterController cc;
    protected Animator animator;
    public float Speed;
    public float JumpSpeed;
    public float health = 100;
    public float damage = 100;
    public GameObject DeathParticle;

    Vector3 vel;
    public void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    void Gravity()
    {
        vel.y += Physics.gravity.y * 10f * Time.deltaTime;
    }

    public void Update()
    {
        AttactCheck();
        //手动添加重力，修改vel.y
        Gravity();
        cc.Move(vel * Time.deltaTime);

        //cc.isGrounded只有在调用过cc.Move后才会进行检测
        if (cc.isGrounded)
        {
            // 在地面上时，速度y值应该归0。但是如果写0导致下一帧不能判定落地
            // 写为-1确保切实落在地面上
            vel.y = -1f;
        }
        animator.SetBool("isGround", cc.isGrounded);
    }

    public void Move(float x, float z, bool jump) //只是修改了vel的值，角色移动还是通过cc.movc()实现
    {
        //设置blendtree动画
        animator.SetFloat("Speed", Mathf.Max(Mathf.Abs(x), Mathf.Abs(z)));
        Vector3 input = new Vector3(x, 0, z);
        // 防止斜向移动比正向移动快
        if (input.magnitude > 1)
        {
            input = input.normalized;
        }
        vel.x = x * Speed;
        vel.z = z * Speed;

        if (x != 0 || z != 0)
        {
            var dir = Vector3.right * x + Vector3.forward * z;
            Turn(dir, 10);
        }

        if (jump)
        {
            Jump();
        }
    }

    public void Turn(Vector3 dir, float turnSpeeed)
    {
        //使角色转到向量方向
        Quaternion face = Quaternion.LookRotation(dir);
        //获取当前朝向和目标朝向的中间值，使角色看起来是逐步旋转
        Quaternion slerp = Quaternion.Slerp(transform.rotation, face, turnSpeeed * Time.deltaTime);
        transform.rotation = slerp;
    }

    public void Jump()
    {
        if (cc.isGrounded)
        {
            vel.y = JumpSpeed;
        }
    }
    
    public void AttactCheck()
    {
        var dist = cc.height / 2;
        RaycastHit hit;
        if(Physics.BoxCast(transform.position, new Vector3(0.4f, 0.2f, 0.4f), Vector3.down, out hit,Quaternion.identity, 0.6f))
        {
            if(hit.collider.GetComponent<Character>() && hit.transform != transform)
            {
                hit.collider.GetComponent<Character>().TakeDamage(this, damage);
                // 反弹跳起
                vel.y = JumpSpeed;
            }
        }
    }
    
    public void TakeDamage(Character other,float damage)
    {
        health -= damage;
        if(health <= 0 )
        {
            OnDie();
        }
    }

    public virtual void OnDie()
    {
        var particle = Instantiate(DeathParticle);
        particle.transform.position = transform.position;
        particle.SetActive(true);
        Destroy(gameObject);
    }

}
