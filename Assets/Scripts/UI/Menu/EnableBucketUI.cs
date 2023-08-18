using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBucketUI : MonoBehaviour
{
    [SerializeField] GameObject ui;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnableUI()
    {
        ui.SetActive(true);
    }

    public void DisableUI()
    {
        ui.SetActive(false);
    }
}
