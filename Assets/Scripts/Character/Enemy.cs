using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public abstract class Enemy : MonoBehaviour
    {
        private string name;

        protected int maxHealth;
        protected int curHealth;

        protected float maxCD;
        protected float curCD;

        private float maxHealthBarLength;
        private float healthBarLength;

        private float maxCDBarLength;
        private float CDBarLength;

        private GUIStyle HealthBarStyle = null;
        private GUIStyle CDBarStyle = null;
        private GUIStyle LabelStyle = null;

        GameController gameController;
        StageController stageController;
        Camera cam;
        Vector3 EnemyPos;
        Vector3 HealthBarLeft, HealthBarRight;
        Vector3 CDBarLeft, CDBarRight;

        protected void Init()
        {
            Vector3 Pos = transform.position;
            float h = GetComponent<SpriteRenderer>().size.y / 2 + 1.5f;

            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

            HealthBarLeft = cam.WorldToScreenPoint(Pos + new Vector3(-3f, h, 0));
            HealthBarRight = cam.WorldToScreenPoint(Pos + new Vector3(3f, h, 0));
            maxHealthBarLength = Vector3.Distance(HealthBarRight, HealthBarLeft);

            CDBarLeft = cam.WorldToScreenPoint(Pos + new Vector3(-2.5f, -h, 0));
            CDBarRight = cam.WorldToScreenPoint(Pos + new Vector3(2.5f, -h, 0));
            maxCDBarLength = Vector3.Distance(CDBarRight, CDBarLeft);

            GameObject obj = GameObject.FindGameObjectWithTag("GameController");
            gameController = obj.GetComponent<GameController>();
            stageController = obj.GetComponent<StageController>();

        }

        private void OnGUI()
        {
            InitStyles();
            if (curHealth > 0)
                GUI.Box(new Rect(HealthBarLeft.x, Screen.height - HealthBarLeft.y, healthBarLength, 10f), "", HealthBarStyle);
            if (maxCD > 0 && curCD > 0)
                GUI.Box(new Rect(CDBarLeft.x, Screen.height - CDBarLeft.y, CDBarLength, 5f), "", CDBarStyle);
        }

        void InitStyles()
        {
            if (HealthBarStyle == null)
            {
                HealthBarStyle = new GUIStyle(GUI.skin.box);
                HealthBarStyle.normal.background = MakeTex(1, 1, Color.red);
            }
            if (CDBarStyle == null)
            {
                CDBarStyle = new GUIStyle(GUI.skin.box);
                CDBarStyle.normal.background = MakeTex(1, 1, Color.gray);
            }
            if(LabelStyle == null)
            {
                LabelStyle = new GUIStyle(GUI.skin.label);
                LabelStyle.fontSize = 50;
            }
        }

        private Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; ++i)
            {
                pix[i] = col;
            }
            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }

        protected void AdjustCurHealth()
        {
            healthBarLength = maxHealthBarLength * (curHealth / (float)maxHealth);
        }

        protected void AdjustCurCD()
        {
            CDBarLength = maxCDBarLength * (curCD / maxCD);
        }

        protected void Defeat()
        {
            stageController.EnemyIsDefeat();
        }

        public void GetDamage(int damage)
        {
            curHealth -= damage;
            if (curHealth < 0)
            {
                curHealth = 0;
            }
            DamageAni();
        }
        public abstract void Attack();
        public abstract void DamageAni();
    }
}