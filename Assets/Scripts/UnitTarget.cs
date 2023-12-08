using System.Collections;
using UnityEngine;

public class UnitTarget : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(10);
        gameObject.SetActive(false);
    }
}
