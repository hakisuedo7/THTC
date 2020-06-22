using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PublicObj
{
    public static class Template
    {
        static Dictionary<string, Sprite> _resourceBFSprite = new Dictionary<string, Sprite>();
        static Dictionary<string, GameObject> _EnemyObj = new Dictionary<string, GameObject>();
        static Dictionary<string, GameObject> _AttackEffectObj = new Dictionary<string, GameObject>();

        public static void LoadAll()
        {
            LoadAllSprites();
            LoadAllEnemies();
            LoadAllAttackEffects();
        }
        public static void LoadAllSprites(string[] files)
        {
            for (int i = 0; i < files.Length; i++)
            {
                var sprites = Resources.LoadAll(files[i], typeof(Sprite));
                for (int j = 0; j < sprites.Length; j++)
                {
                    _resourceBFSprite[sprites[j].name] = (Sprite)sprites[j];
                }
            }
        }
        public static void LoadAllSprites()
        {
            LoadAllSprites(new string[] { "Texture/Battlefields" });
        }
        public static Sprite GetSprite(int index)
        {
            string name = "Battlefields_" + index.ToString();
            return _resourceBFSprite[name];
        }

        public static void LoadAllEnemies(string[] files)
        {
            for (int i = 0; i < files.Length; i++)
            {
                var objs = Resources.LoadAll(files[i], typeof(GameObject));
                for (int j = 0; j < objs.Length; j++)
                {
                    _EnemyObj[objs[j].name] = (GameObject)objs[j];
                }
            }
        }
        public static void LoadAllEnemies()
        {
            LoadAllEnemies(new string[] { "Prefab/Character/Enemy" });
        }
        public static GameObject GetEnemy(string name)
        {
            return _EnemyObj[name];
        }

        public static void LoadAllAttackEffects(string[] files)
        {
            for (int i = 0; i < files.Length; i++)
            {
                var objs = Resources.LoadAll(files[i], typeof(GameObject));
                for (int j = 0; j < objs.Length; j++)
                {
                    _AttackEffectObj[objs[j].name] = (GameObject)objs[j];
                }
            }
        }

        public static void LoadAllAttackEffects()
        {
            LoadAllAttackEffects(new string[] { "Prefab/AttackEffect" });
        }

        public static GameObject GetAttackEffect(int index)
        {
            string name = "AtkEffect" + index.ToString();
            return _AttackEffectObj[name];
        }
    }
}
