using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectil : MonoBehaviour
{
    public float speed;
    public GameObject slow;
    // Start is called before the first frame update
    void Awake()
    {
        Destroy(gameObject, 10f);
        PauseStateManager.Instance.OnPauseStateChanged += OnPauseStateChanged;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 6 || collision.gameObject.layer == 6)
        {
            GameObject.Instantiate((UnityEngine.Object)Resources.Load("SlowProyectilCanon"), transform.position, collision.transform.rotation);

        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        PauseStateManager.Instance.OnPauseStateChanged -= OnPauseStateChanged;
    }
    private void OnPauseStateChanged(PauseState newPauseState)
    {
        enabled = newPauseState == PauseState.Gameplay;
    }
}
