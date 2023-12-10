using System.Collections;
using UnityEngine;

public class UnitTarget : MonoBehaviour
{
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    /// <summary>
    /// Reset, active and start cooldown
    /// </summary>
    public void Show()
    {
        Hide();

        gameObject.SetActive(true);
        StopAllCoroutines();

        StartCoroutine(Cooldown_Coroutine());
    }

    /// <summary>
    /// Stop all coroutines and deactive from hierarchy
    /// </summary>
    public void Hide()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
    }

    IEnumerator Cooldown_Coroutine()
    {
        yield return new WaitForSeconds(10);
        gameObject.SetActive(false);
    }
}
