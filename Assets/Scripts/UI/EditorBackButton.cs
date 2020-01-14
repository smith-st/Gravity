using UnityEngine;

public class EditorBackButton : MonoBehaviour
{
    private void Awake()
    {
#if !UNITY_EDITOR
        Destroy(gameObject);
#endif
    }
}
