using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [Header("Reload Time")]
    public float reloadMin;
    public float reloadMax;
    public float reload;

    [Header("Wait Time")]
    public float waitTimeMin;
    public float waitTimeMax;
    public float waitTime;
    
    [Header("Spikes")]
    public GameObject spikes;

    private Collider collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        StartCoroutine(LaunchSpikes());
    }

    IEnumerator LaunchSpikes()
    {
        reload = Mathf.Clamp(reload, reloadMin, reloadMax);
        waitTime = Mathf.Clamp(waitTime, waitTimeMin, waitTimeMax);
        yield return new WaitForSeconds(reload);
        spikes.SetActive(true);
        collider.enabled = true;
        yield return new WaitForSeconds(waitTime);
        spikes.SetActive(false);
        collider.enabled = false;
        StartCoroutine(LaunchSpikes());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Die");
        }
    }
}
