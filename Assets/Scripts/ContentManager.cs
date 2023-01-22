using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ContentManager : MonoBehaviour
{
    public TransitionManager transitionManagerRef;
    public SingleContent SingleContentPrefab;

    [SerializeField] Button homeButton;

    [SerializeField] Button leftNavButton;
    [SerializeField] Button rightNavButton;

    public int _id;

    [SerializeField]
    TMP_Text title;

    [SerializeField]
    Vector3[] spawnPoints;

    [SerializeField]
    TMP_Text[] objectName;

    [SerializeField]
    GameObject[] prefabs;

    [SerializeField]
    Dictionary<int, List<SingleContent>> contentObjDict = new Dictionary<int, List<SingleContent>>();

    [SerializeField]
    GameObject[] contentSections;

    Contents data;
    Camera mainCam;

    
    void Start()
    {
        mainCam = Camera.main;
        homeButton.onClick.AddListener(OnHomeButtonClicked);
        leftNavButton.onClick.AddListener(OnLeftButtonClicked);
        rightNavButton.onClick.AddListener(OnRightButtonClicked);
    }

    private void OnEnable()
    {
        leftNavButton.interactable = false;
        currentContent = 0;
        OpenSpecificSection(0);
    }

    void OpenSpecificSection(int _index)
    {
        for(int i = 0; i < contentSections.Length; i++)
        {
            contentSections[i].SetActive(false);
        }

        contentSections[_index].SetActive(true);
        SetNewContent();

        _id = data.allContents[_index].id;
        title.text = data.allContents[_index].title;
    }

    public void SetData(Contents _data)
    {
        data = _data;

        for (int i = 0; i < _data.allContents.Count; i++)
        {
            SetEachContentData(_data.allContents[i], contentSections[i].transform);
        }
    }

    void SetEachContentData(ContentData _data, Transform _parent)
    {
        List<SingleContent> objs = new List<SingleContent>();
        
        for (int i = 0; i < _data.objects.Count; i++)
        {
            objs.Add(InstantiateObject(_data.objects[i], _parent));
        }
        
        contentObjDict.Add(_data.id, objs);
    }

    GameObject gObj;
    SingleContent InstantiateObject(ContentObjects _content, Transform _parent)
    {
        gObj = Instantiate(prefabs[_content.type], _parent);
        gObj.transform.localPosition = spawnPoints[_content.type];
        gObj.SetActive(true);

        SingleContent sc = gObj.GetComponent<SingleContent>();

        sc.SetData(_content);

        return sc;
    }

    void OnHomeButtonClicked()
    {
        transitionManagerRef.PlayTransition();
        UnselectObject();
        ResetLastContent();
        detectedObject = null;
        this.gameObject.SetActive(false);
        LevelManager.Instance.homeManagerRef.gameObject.SetActive(true);
    }

    int currentContent = 0;
    public void OnLeftButtonClicked()
    {
        ResetLastContent();

        transitionManagerRef.PlayTransition();
        currentContent -= 1;
        currentContent = currentContent < 0 ? 0 : currentContent;

        rightNavButton.interactable = true;
        if (currentContent == 0)
        {
            leftNavButton.interactable = false;
        }

        OpenSpecificSection(currentContent);
    }

    public void OnRightButtonClicked()
    {
        ResetLastContent();

        transitionManagerRef.PlayTransition();
        currentContent += 1;
        currentContent = (currentContent > (contentSections.Length - 1)) ? (contentSections.Length - 1) : currentContent;

        leftNavButton.interactable = true;
        if (currentContent == contentSections.Length - 1)
        {
            rightNavButton.interactable = false;
        }
        
        OpenSpecificSection(currentContent);
    }

    private void SetNewContent()
    {
        for (int i = 0; i < contentObjDict[data.allContents[currentContent].id].Count; i++)
        {
            contentObjDict[data.allContents[currentContent].id][i].gameObject.SetActive(true);
        }
    }

    private void ResetLastContent()
    {
        for(int i = 0; i < contentObjDict[data.allContents[currentContent].id].Count; i++)
        {
            contentObjDict[data.allContents[currentContent].id][i].gameObject.SetActive(false);
        }

        for(int i = 0; i < objectName.Length; i++)
        {
            objectName[i].text = string.Empty;
            objectName[i].gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DetectObject();
        }
    }

    SingleContent detectedObject = null;
    void DetectObject()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray.origin, ray.direction * 10, out RaycastHit hit))
        {
            if (hit.collider.gameObject != null)
            {
                if(detectedObject != null)
                {
                    if (detectedObject._id == hit.collider.gameObject.transform.parent.GetComponent<SingleContent>()._id)
                    {
                        //do nothing
                    }
                    else
                    {
                        UnselectObject();
                        detectedObject = hit.collider.gameObject.transform.parent.GetComponent<SingleContent>();
                        detectedObject.PlayAnimation();
                        SetUIContentsForObejct();
                    }
                }
                else
                {
                    detectedObject = hit.collider.gameObject.transform.parent.GetComponent<SingleContent>();
                    detectedObject.PlayAnimation();
                    SetUIContentsForObejct();
                }
            }
        }
    }

    void SetUIContentsForObejct()
    {
        for (int i = 0; i < contentObjDict[data.allContents[currentContent].id].Count; i++)
        {
            if(contentObjDict[data.allContents[currentContent].id][i]._id == detectedObject._id)
            {
                objectName[detectedObject.type].text = detectedObject.title;
                objectName[detectedObject.type].gameObject.SetActive(true);
                break;
            }
        }
    }

    void UnselectObject()
    {
        objectName[detectedObject.type].gameObject.SetActive(false);
        detectedObject.ResetAnimation();
    }
}
