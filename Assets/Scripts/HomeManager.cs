using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HomeManager : MonoBehaviour
{
    [SerializeField]
    TMP_Text title;

    [SerializeField]
    TMP_Text candidateName;

    [SerializeField]
    Button contentButton;

    [SerializeField]
    Button quitButton;

    public TransitionManager transitionManagerRef;

    void Start()
    {
        contentButton.onClick.AddListener(OnContentButtonClicked);
        quitButton.onClick.AddListener(Application.Quit);
    }

    void OnContentButtonClicked()
    {
        transitionManagerRef.PlayTransition();
        this.gameObject.SetActive(false);
        LevelManager.Instance.contentManagerRef.gameObject.SetActive(true);
    }

    public void SetData()
    {
        title.text = "PLAY WITH PROPS";
        candidateName.text = "-Sirijan";
    }
}
