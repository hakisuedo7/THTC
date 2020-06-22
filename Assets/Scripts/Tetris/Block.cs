using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    public bool isAttackBlock;

    private void Awake()
    {
        isAttackBlock = false;
        setSprite();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setSprite()
    {
        if (sprite != null)
            sprite.enabled = isAttackBlock;
    }

    public void SetToAttackBlock()
    {
        isAttackBlock = true;
        setSprite();
    }

    public void DestroyBlock()
    {
        Playfield.atkBlock++;
        Destroy(this.gameObject);
    }
}