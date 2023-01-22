using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public TransitionManager transitionManagerRef;
    public ContentManager contentManagerRef;
    public HomeManager homeManagerRef;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance != null)
        {
            Instance = null;
        }
    }

    public void SetData(Contents _data)
    {
        homeManagerRef.SetData();
        contentManagerRef.SetData(_data);
    }
}
