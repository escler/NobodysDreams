using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class Explode : MonoBehaviour
{
    [SerializeField] float radiusExplosion, amountTimer, timerForExplode, shakeDuration, shakePotential;
    float actualTime;
    [SerializeField] ClockEnemy clockEnemy;
    [SerializeField] NavMeshAgent nma;
    [SerializeReference] GameObject vfxExplosion;
    AudioSource audioSource;
    

    // Start is called before the first frame update

    void Start()
    {
        clockEnemy = GetComponent<ClockEnemy>();
        nma = clockEnemy.NMA;
        nma.SetDestination(transform.position);
        actualTime = timerForExplode;
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        clockEnemy = GetComponent<ClockEnemy>();
        nma = clockEnemy.NMA;
        nma.SetDestination(transform.position);
        actualTime = timerForExplode;
        clockEnemy.Explosion = true;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        actualTime -= Time.deltaTime;

        if(actualTime <= 0)
        {
            print("explote");
            Explosion();
        }
    }

    void Explosion()
    {
        var explosionArea = Physics.OverlapSphere(transform.position, radiusExplosion);

        foreach (Collider col in explosionArea)
        {
            if(col.gameObject.name == "Char")
            {
                GameObject.Find("TimerController").GetComponent<TimerController>().SubstractTime(amountTimer);
                GameObject.Find("CameraHolder").GetComponent<Shaking>().ShakeExplosion(shakeDuration);
            }
            else if (col.gameObject.tag == "Gelatina")
            {
                col.GetComponent<Gelatina>().DoInDestroy();
            }
            GameObject.Instantiate((UnityEngine.Object)Resources.Load("explode_2"), transform.position, transform.rotation);


        }

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radiusExplosion);
    }
}
