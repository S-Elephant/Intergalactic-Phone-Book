using UnityEngine;

public class AngryCat : MonoBehaviour
{
    #region Constants
    private const float BaseSpeed = 24f;
    private const float SpeedVariance = 6f;

    /// <summary>
    /// This is the position where this cat will run towards.
    /// </summary>
    private static readonly Vector3 TargetDest = Vector3.zero;

    /// <summary>
    /// The distance tolerance to the destination target that counts as having reached the destination.
    /// </summary>
    private const float HitDistanceTolerance = 15f;
    
    private const ESFX DeathSfx = ESFX.Cat_Death;
    #endregion

    public bool IsAlive = true;
    private float Speed;

    private void Start()
    {
        Speed = BaseSpeed + Random.Range(-SpeedVariance, SpeedVariance);
        LookAtPlayer();
    }

    public void UpdateMe()
    {
        transform.position = Vector3.MoveTowards(transform.position, TargetDest, Speed * Time.deltaTime);
        ProcessHitPlayer();
    }

    private void LookAtPlayer()
    {
        transform.LookAt(CatMgr.Instance.AngryCatTerminator.transform);
    }

    private void ProcessHitPlayer()
    {
        if (Vector3.Distance(transform.position, TargetDest) <= HitDistanceTolerance)
        {
            OnHitPlayer();
        }
    }

    private void OnHitPlayer()
    {
        IsAlive = false;
    }

    public void Kill()
    {
        if (!IsAlive) { throw new System.Exception("This cat is already dead. Who and why is it being killed (again)?"); } // Extra safety check.

        GameObject vfxGo = Instantiate<GameObject>(PrefabMgr.Instance.UltraNova, CatMgr.Instance.VfxParent);
        vfxGo.transform.position = transform.position;
        AudioMgr.Instance.PlaySFX_2D(DeathSfx);
        IsAlive = false;
    }
}
