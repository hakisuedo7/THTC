using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Remilia : Enemy
    {
        Animator animator;
        Spawner spawner;
        int FateCount;

        // Start is called before the first frame update
        void Start()
        {
            name = "Remilia Scarlet";

            maxHealth = 7000;
            curHealth = maxHealth;

            maxCD = 27;
            curCD = 0;

            animator = GetComponent<Animator>();
            spawner = GameObject.FindObjectOfType<Spawner>();

            FateCount = 2;

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
                Debug.Log("擊破大小姐");
                Defeat();
                Destroy(this.gameObject);
            }

            if (curHealth <= maxHealth / 3)
            {
                FateCount = 3;
            }else if(curHealth <= maxHealth / 5)
            {
                FateCount = 4;
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

                spawner.ManipulationOfFate(FateCount);

                for(int i = 0; i < 3; i++)
                {
                    Playfield.increaseFullRow(i);
                    Playfield.DestroyBlock(new Vector2(Playfield.w - (1 + i), 0));
                }
            }
        }

        public override void DamageAni()
        {
            animator.SetTrigger("GetDamage");
        }
    }
}
