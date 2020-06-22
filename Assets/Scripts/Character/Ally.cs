using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ally
{
    public abstract class Ally : MonoBehaviour
    {
        protected string name;
        protected int damage;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public abstract void Attack(int fullRowCount, int Combo, int atkBlock);
        public abstract void Defeat();
    }
}

