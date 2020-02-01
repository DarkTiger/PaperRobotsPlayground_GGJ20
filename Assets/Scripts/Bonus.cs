using UnityEngine;

public class Bonus : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(transform.InverseTransformDirection(transform.up) * 60 * Time.deltaTime);
    }
}
