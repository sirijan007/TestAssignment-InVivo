using UnityEngine;

public class SingleContent : MonoBehaviour
{
    public int _id;
    public string title;
    public int type;

    [SerializeField] Animator animator;

    [SerializeField] Material selectionMat;
    [SerializeField] Material deselectionMat;

    [SerializeField] MeshRenderer meshRenderer;

    int selectID;
    int deselectID;

    void Start()
    {
        meshRenderer.sharedMaterial = deselectionMat;
        selectID = Animator.StringToHash("Select");
        deselectID = Animator.StringToHash("Deselect");
    }

    public void SetData(ContentObjects _data)
    {
        _id = _data.id;
        title = _data.title;
        type = _data.type;
    }

    public void PlayAnimation()
    {
        animator.SetTrigger(selectID);
        meshRenderer.sharedMaterial = selectionMat;
    }

    public void ResetAnimation()
    {
        meshRenderer.sharedMaterial = deselectionMat;
        animator.SetTrigger(deselectID);
    }
}
