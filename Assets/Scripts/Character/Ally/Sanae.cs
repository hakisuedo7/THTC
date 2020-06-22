using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ally
{
    public class Sanae : Ally
    {
        Animator animator;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();

            name = "Sanae Kochiya";
            damage = 24;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void Attack(int fullRowCount, int Combo, int atkBlock)
        {
            animator.SetTrigger("Attack");

            Enemy.Enemy[] enemies = FindObjectsOfType<Enemy.Enemy>();
            GameObject AttackEffect = PublicObj.Template.GetAttackEffect(0);
            foreach (Enemy.Enemy enemy in enemies)
            {
                int atkDamage = (damage + atkBlock * Setting.Game.BLOCKDAMAGE) * fullRowCount * (int)Mathf.Pow(1.3f, Combo - 1);
                Instantiate(AttackEffect, enemy.transform.position, Quaternion.identity);
                enemy.GetDamage(atkDamage);
            }
        }

        public override void Defeat()
        {
            animator.SetTrigger("Defeat");
        }
    }
}