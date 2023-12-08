using System.Collections;
using UnityEngine;

public class UnitTarget : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Unit"))
            StartCoroutine(Cooldown_Coroutine());
    }

    IEnumerator Cooldown_Coroutine()
    {
        yield return new WaitForSeconds(10);
        gameObject.SetActive(false);
    }
}
