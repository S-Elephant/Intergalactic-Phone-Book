using UnityEngine;

public class AngryCatTerminator : MonoBehaviour
{
    private Camera Cam;
    private Vector3 CenterScreen;

    private void Start()
    {
        Cam = Camera.main;
        CenterScreen = new Vector3(Screen.width / 2, Screen.height / 2);
    }

    private void Update()
    {
        AttemptToShootCats();
    }

    private void AttemptToShootCats()
    {
        Ray ray = Cam.ScreenPointToRay(CenterScreen);
        //Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.yellow);
        
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider != null)
            {
                AngryCat catScript = hit.collider.gameObject.GetComponent<AngryCat>();
                if (catScript != null && catScript.IsAlive)
                {
                    catScript.Kill();
                }
            }
        }
    }
}
