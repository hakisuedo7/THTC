using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Flandre : Enemy
    {
        Animator animator;
        int DestroyBlocksCount;

        // Start is called before the first frame update
        void Start()
        {
            name = "Flandre Scarlet";

            maxHealth = 6500;
            curHealth = maxHealth;

            maxCD = 22;
            curCD = 0;

            animator = GetComponent<Animator>();

            DestroyBlocksCount = 4;

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
                Debug.Log("擊破二妹");
                Defeat();
                Destroy(this.gameObject);
            }

            if (curHealth <= maxHealth / 3)
            {
                DestroyBlocksCount = 8;
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
                List<Vector2> v2s = getVector2s(DestroyBlocksCount);

                foreach(Vector2 v2 in v2s)
                {
                    Playfield.DestroyBlock(v2);
                    Instantiate(PublicObj.Template.GetAttackEffect(2), v2, Quaternion.identity);
                }
            }
        }

        List<Vector2> getVector2s(int count)
        {
            List<Vector2> tmpV2 = new List<Vector2>();

            int x = Random.Range(0, Playfield.w);
            int y = Random.Range(0, Playfield.h - 8);
            Vector2 tmp = new Vector2(x, y);

            tmpV2.Add(tmp);

            for(int i = 1; i < count; i++)
            {
                do
                {
                    x = Random.Range(0, Playfield.w);
                    y = Random.Range(0, Playfield.h - 8);
                    tmp = new Vector2(x, y);
                } while (isValid(tmpV2, tmp));
                tmpV2.Add(tmp);
            }
            return tmpV2;
        }

        bool isValid(List<Vector2> v2s, Vector2 v2)
        {
            foreach(Vector2 v in v2s)
            {
                if (Vector2.Equals(v, v2))
                    return true;
            }
            return false;
        }

        public override void DamageAni()
        {
            animator.SetTrigger("GetDamage");
        }
    }
}
