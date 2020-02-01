using UnityEngine;

public class Bonus : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(transform.InverseTransformDirection(transform.up) * 50 * Time.deltaTime);
    }
}
