using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float timeWait = 3f;
    [SerializeField] GameObject particleBall, ballPos, ballRot, soundGO;
    private float currTimeWait;

    private void Awake()
    {
        PauseStateManager.Instance.OnPauseStateChanged += OnPauseStateChanged;
    }

    private void OnDestroy()
    {
        PauseStateManager.Instance.OnPauseStateChanged -= OnPauseStateChanged;
    }

    void Start()
    {
        currTimeWait = 0f;
    }
    void Update()
    {
        if (transform.parent != null )
            return;
        currTimeWait += Time.deltaTime;

        if (currTimeWait >= timeWait)
        {
            Instantiate(particleBall, ballPos.transform.position, ballRot.transform.rotation);
            Instantiate(soundGO, ballPos.transform.position, ballRot.transform.rotation);
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (GetComponent<Rigidbody>().velocity != Vector3.zero)
        {
            if (collision.gameObject.layer != 16 || collision.gameObject.layer != 10)//Colisone contra la layer TriggerButtons
            {
                Instantiate(particleBall, ballPos.transform.position, ballRot.transform.rotation);
                Instantiate(soundGO, ballPos.transform.position, ballRot.transform.rotation);
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (GetComponent<Rigidbody>().velocity != Vector3.zero)
        {
            if (other.gameObject.layer == 16 || other.gameObject.layer == 10 || other.gameObject.tag == "Pieces")//Colisone contra la layer TriggerButtons
            {
                Instantiate(particleBall, ballPos.transform.position, ballRot.transform.rotation);
                Instantiate(soundGO, ballPos.transform.position, ballRot.transform.rotation);
                Destroy(gameObject);
            }else if(other.gameObject.layer == 11)
            {
                other.gameObject.GetComponent<Enemies>().ReceiveStun(5);
                Instantiate(particleBall, ballPos.transform.position, ballRot.transform.rotation);
                Instantiate(soundGO, ballPos.transform.position, ballRot.transform.rotation);
                Destroy(gameObject);
            }
        }
    }
    private void OnPauseStateChanged(PauseState newPauseState)
    {
        enabled = newPauseState == PauseState.Gameplay;
    }

}
