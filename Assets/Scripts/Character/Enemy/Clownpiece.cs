using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy {
    public class Clownpiece : Enemy
    {
        Animator animator;

        // Start is called before the first frame update
        void Start()
        {
            name = "Clownpiece";

            maxHealth = 5000;
            curHealth = maxHealth;

            maxCD = 25;
            curCD = 0;

            animator = GetComponent<Animator>();

            Init();
            Attack();
        }

        // Update is called once per frame
        void Update()
        {
            AdjustCurHealth();

            AdjustCurCD();

            if (curHealth == 0)
            {
                Debug.Log("擊破色情妖精");
                Defeat();
                Destroy(this.gameObject);
            }

            if (curHealth <= maxHealth / 3)
            {
                maxCD = 20;
            }

            curCD += Time.deltaTime;
        }

        public override void Attack()
        {
            StartCoroutine(CoroutineAttack());
        }

        IEnumerator CoroutineAttack()
        {
            while (true)
            {
                yield return new WaitForSeconds(maxCD);
                curCD = 0;
                animator.SetTrigger("Attack");

                int j = Random.Range(0, Playfield.w);
                for (int i = 0; i < 2; i++)
                {
                    Playfield.increaseFullRow(j);
                }
            }
        }

        public override void DamageAni()
        {
            animator.SetTrigger("GetDamage");
        }
    }
}