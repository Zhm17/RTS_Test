using System.Collections;
using UnityEngine;

public class UnitTarget : MonoBehaviour
{
    public delegate void SpawnAction(UnitTarget target, bool active);
    public static event SpawnAction OnSpawn;

    [SerializeField]
    public float LifeTimeSeconds = 10f;

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    /// <summary>
    /// Reset, active and start cooldown
    /// </summary>
    public void Show(Vector3 newpos)
    {
        Hide();

        gameObject.SetActive(true);
        PlaySound();

        transform.position = new Vector3(newpos.x,
                                            transform.position.y,
                                            newpos.z);

        if (OnSpawn!=null) 
            OnSpawn(this, true);

        StartCoroutine(Cooldown_Coroutine());
    }

    /// <summary>
    /// Stop all coroutines and deactive from hierarchy
    /// </summary>
    public void Hide()
    {
        StopAllCoroutines();

        if (OnSpawn != null) 
            OnSpawn(this, false);

        gameObject.SetActive(false);
    }

    IEnumerator Cooldown_Coroutine()
    {
        yield return new WaitForSeconds(LifeTimeSeconds);
        Hide();
    }
    private void PlaySound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource) audioSource.Play();
    }

}
