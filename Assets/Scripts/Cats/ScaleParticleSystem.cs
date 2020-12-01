using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ScaleParticleSystem : MonoBehaviour
{
    [SerializeField] private Vector3 Scale = Vector3.one;

    private void Start()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ps.transform.localScale = Scale;
        Destroy(this);
    }
}
