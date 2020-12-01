using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class AutoDestroyVfx : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, GetComponent<ParticleSystem>().main.duration);
    }
}
