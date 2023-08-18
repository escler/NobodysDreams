using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviours : MonoBehaviour
{
    [SerializeField]Material bossMaterial;
    Color colorBoss, initialBossColor;
    float alpha;
    bool showMonster, moving;
    [SerializeReference] Transform nextPos;
    // Start is called before the first frame update
    void Start()
    {
        initialBossColor = bossMaterial.color;
        initialBossColor.a = 0;
        bossMaterial.color = initialBossColor;
        print(bossMaterial.color);
        colorBoss = Color.Lerp(initialBossColor, new Color(1, 1, 1, 1), .3f * Time.deltaTime);
    }

    public bool ShowMonster
    {
        get { return showMonster; }
        set { showMonster = value; }
    }

    // Update is called once per frame
    void Update()
    {
        if (showMonster)
        {
            bossMaterial.color = new Color(bossMaterial.color.r, bossMaterial.color.g, bossMaterial.color.b, Mathf.Lerp(bossMaterial.color.a, 1.0f, .5f * Time.deltaTime));
        }
        else
        {
            bossMaterial.color = new Color(bossMaterial.color.r, bossMaterial.color.g, bossMaterial.color.b, Mathf.Lerp(bossMaterial.color.a, 0.0f, .5f * Time.deltaTime));
        }

        if (moving)
        {
            gameObject.transform.position = Vector3.Lerp(transform.position, nextPos.position, 5f * Time.deltaTime);

            float distance = Vector3.Distance(transform.position, nextPos.position);

            if(distance < 0.4f)
            {
                moving = false;
            }
        }
    }

    public void MoveBoss(Transform pos)
    {
        nextPos = pos;
        moving = true;
    }
}
