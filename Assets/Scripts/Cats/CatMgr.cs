using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Todo: Refactor: Object pooling and etc, etc, I had to skip here because of time limitations.
/// </summary>
public class CatMgr : SingletonNonPersistent<CatMgr>
{
    #region Properties
    /// <summary>
    /// The player.
    /// </summary>
    [HideInInspector] public GameObject AngryCatTerminator;
    
    /// <summary>
    /// All (alive) cats.
    /// </summary>
    private readonly List<AngryCat> AllCats = new List<AngryCat>();

    private float TimeLeftUntilNextSpawn = 0f;
    
    private Transform CatsParent;
    public Transform VfxParent { get; private set; }

    private const int SlowUpdateInterval = 6;
    private int CurrentInterval = SlowUpdateInterval;

    [Header("Spawning")]
    /// <summary>
    /// If there are less cats than this number, then the next time a cat is spawn it will keep spawning cats until it reaches this number of cats in total.
    /// This number should be less or equal than <c>MaxCats</c>.
    /// </summary>
    [SerializeField]
    [Range(0, 30)]
    private int MinCats = 4;

    [SerializeField]
    [Range(0, 30)]
    private int MaxCats = 30;

    [SerializeField]
    [Range(1, 60)]
    private float SpawnIntervalInSeconds = 1.8f;

    /// <summary>
    /// This value should be smaller than the MaxSpawnRange.
    /// </summary>
    [SerializeField]
    [Range(25f, 150f)]
    private float MinSpawnRange = 50f;

    /// <summary>
    /// This value should be bigger than the MinSpawnRange.
    /// </summary>
    [SerializeField]
    [Range(250f, 500f)]
    private float MaxSpawnRange = 300f;

    #endregion

    protected override void Awake()
    {
        base.Awake();
        CatsParent = new GameObject("Cats").transform;
        VfxParent = new GameObject("Vfx").transform;
        VfxParent.transform.position = Vector3.zero;
        AngryCatTerminator = Instantiate<GameObject>(PrefabMgr.Instance.CatTerminator);
    }

    private void Update()
    {
        UpdateAndProcessAllCats();

        // A slower update for performance.
        CurrentInterval--;
        if (CurrentInterval <= 0)
        {
            CurrentInterval = SlowUpdateInterval;
            SlowUpdate();
        }

        // Do we have to spawn a cat?
        if (AllCats.Count < MaxCats)
        {
            TimeLeftUntilNextSpawn -= Time.deltaTime;
            if (TimeLeftUntilNextSpawn <= 0f)
            {
                TimeLeftUntilNextSpawn = SpawnIntervalInSeconds;
                SpawnCats();
            }
        }
    }

    private void SlowUpdate()
    {
        for (int i = 0; i < AllCats.Count; i++)
        {
            if (!AllCats[i].IsAlive)
            {
                Destroy(AllCats[i].gameObject);
                AllCats.RemoveAt(i);
            }
        }
    }

    private void UpdateAndProcessAllCats()
    {
        for (int i = 0; i < AllCats.Count; i++) { AllCats[i].UpdateMe(); }
    }

    public void SpawnCats()
    {
        do
        {
            GameObject cat = Instantiate<GameObject>(PrefabMgr.Instance.AngryCat);
            cat.transform.SetParent(CatsParent.transform);
            cat.transform.position = GetRandomSpawnPos(-MaxSpawnRange, MaxSpawnRange);
            cat.name = "Cat_" + cat.transform.position.ToString();
            AllCats.Add(cat.GetComponent<AngryCat>());
        } while (AllCats.Count < MinCats);
    }

    private Vector3 GetRandomSpawnPos(float min, float max)
    {
        Vector3 result;
        do
        {
            result = new Vector3(Random.Range(min, max), Random.Range(0f, max), Random.Range(min, max));
        } while (Vector3.Distance(AngryCatTerminator.transform.position, result) <= MinSpawnRange);

        return result;
    }

    public void Exit()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(Constants.SceneNames.Main);
    }
}
