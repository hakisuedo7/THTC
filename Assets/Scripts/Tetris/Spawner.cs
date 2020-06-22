using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] groups;

    List<GameObject> BlocksList = new List<GameObject>();

    GameObject[] BlocksTips = new GameObject[5];
    GameObject SwitchBlock;
    bool canSwitchBlock;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            BlocksList.Add(spawnBlock());
        }
        InitTips();
        EnableNext();

        canSwitchBlock = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Playfield.increaseFullRow();
        }
    }

    public void EnableNext()
    {
        BlocksList.RemoveAt(0);
        BlocksList.Add(spawnBlock());

        BlocksTips[0].transform.position = transform.position;
        BlocksTips[0].GetComponent<Group>().enabled = true;
        BlocksTips[0].transform.localScale = Vector3.one;
        NextTips();
    }

    public void EnableSwitch()
    {
        canSwitchBlock = true;
    }

    public bool Switch()
    {
        return canSwitchBlock;
    }

    public void SwitchBlocK(GameObject block)
    {
        if (canSwitchBlock)
        {
            if(SwitchBlock == null)
            {
                block.GetComponent<Group>().enabled = false;
                block.transform.position = new Vector2(-5, 20);
                block.transform.localScale = new Vector3(0.8f, 0.8f, 0);
                SwitchBlock = block;

                EnableNext();
            }
            else
            {
                SwitchBlock.transform.position = transform.position;
                SwitchBlock.GetComponent<Group>().enabled = true;
                SwitchBlock.transform.localScale = Vector3.one;
                SwitchBlock.GetComponent<Group>().Init();

                block.GetComponent<Group>().enabled = false;
                block.transform.position = new Vector2(-5, 20);
                block.transform.localScale = new Vector3(0.8f, 0.8f, 0);
                SwitchBlock = block;
            }
            canSwitchBlock = false;
        }
    }

    public GameObject spawnBlock()
    {
        int i = Random.Range(0, groups.Length);
        return groups[i];
    }

    void InitTips()
    {
        foreach(GameObject obj in BlocksTips)
        {
            Destroy(obj);
        }

        for(int index = 0; index < 5;index++)
        {
            GameObject obj = Instantiate(BlocksList[index], new Vector2(13, 20 - index * 2.2f), Quaternion.identity);
            obj.GetComponent<Group>().enabled = false;

            int i = Random.Range(0, 4);
            obj.GetComponentsInChildren<Block>()[i].SetToAttackBlock();

            if (index > 0)
            {
                obj.transform.localScale = new Vector3(0.5f, 0.5f, 0);
            }
            else
            {
                obj.transform.localScale = new Vector3(0.8f, 0.8f, 0);
            }

            BlocksTips[index] = obj;
        }
    }

    void NextTips()
    {
        for(int index = 0; index < 4; index++)
        {
            BlocksTips[index] = BlocksTips[index + 1];
            BlocksTips[index].transform.position = new Vector2(13, 20 - index * 2.2f);
        }
        BlocksTips[0].transform.localScale = new Vector3(0.8f, 0.8f, 0);

        GameObject obj = Instantiate(BlocksList[4], new Vector2(13, 20 - 4 * 2.2f), Quaternion.identity);
        obj.GetComponent<Group>().enabled = false;
        obj.transform.localScale = new Vector3(0.5f, 0.5f, 0);

        int i = Random.Range(0, 4);
        obj.GetComponentsInChildren<Block>()[i].SetToAttackBlock();

        BlocksTips[4] = obj;
    }

    public void ManipulationOfFate(int count)
    {
        for(int i = 1; i <= count; i++)
        {
            int current = BlocksTips.Length - i;
            GameObject tmpObj = groups[(current % 2) * 2 + 4];

            BlocksList[current] = tmpObj;

            Instantiate(PublicObj.Template.GetAttackEffect(1), BlocksTips[current].transform.position, Quaternion.identity);
        }
        InitTips();
    }
}
