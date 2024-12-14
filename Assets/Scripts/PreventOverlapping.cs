using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventOverlapping : MonoBehaviour
{
    public bool canMoveHorizontally;
    public bool canMoveVertically;
    public bool isOverlapping;

    private void Update()
    {
       
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            if (canMoveHorizontally)
            {
                {
                    if (transform.position.x > other.transform.position.x)
                    {
                        transform.Translate(Vector3.right * 0.1f, Space.World);
                    }
                    else
                    if (transform.position.x < other.transform.position.x)
                    {
                        transform.Translate(Vector3.left * 0.1f, Space.World);
                    }
                    else
                    {
                        Destroy(gameObject);
                    }

                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            isOverlapping = false;
        }
    }
}
