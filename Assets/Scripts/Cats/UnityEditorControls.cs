using UnityEngine;

public class UnityEditorControls : MonoBehaviour
{
#if UNITY_EDITOR
    
    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            CatMgr.Instance.Exit();
        }
    }
#endif
}
