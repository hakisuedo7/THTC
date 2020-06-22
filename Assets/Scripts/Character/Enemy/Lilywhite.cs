using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Lilywhite : Enemy
    {
        Animator animator;
        // Start is called before the first frame update
        void Start()
        {
            name = "Lilywhite";

            maxHealth = 4000;
            curHealth = maxHealth;

            maxCD = 20;
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
                Debug.Log("擊破春之妖精");
                Defeat();
                Destroy(this.gameObject);
            }

            if(curHealth <= maxHealth / 3)
            {
                maxCD = 15;
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

                int i = Random.Range(0, Playfield.w);
                Playfield.increaseFullRow(i);
            }
        }

        public override void DamageAni()
        {
            animator.SetTrigger("GetDamage");
        }
    }
}
