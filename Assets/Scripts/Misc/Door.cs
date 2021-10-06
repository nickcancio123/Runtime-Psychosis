using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float xOffsetOnOpen = 1;
    [SerializeField] private float openDelay = 0.5f;
    public void OpenDoor()
    {
        StartCoroutine(OpenDelay());
    }

    IEnumerator OpenDelay()
    {
        yield return new WaitForSeconds(openDelay);
        
        gameObject.GetComponent<Animator>().SetTrigger("Open");
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.transform.position += Vector3.right * xOffsetOnOpen;
    }
}
