using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Contents
{
    public List<ContentData> allContents;

    public Contents()
    {
        allContents = new List<ContentData>();
    }
}

[System.Serializable]
public class ContentData
{
    public int id;
    public string title;
    public List<ContentObjects> objects;

    public ContentData()
    {
        objects = new List<ContentObjects>();
    }
}


[System.Serializable]
public class ContentObjects
{
    public int id;
    public string title;
    public int type;
}

public class ContentDataReader : MonoBehaviour
{
    private Contents allContentsData;

    void Start()
    {
        var data = Resources.Load<TextAsset>("DataFiles/contentsData");
        string _data = data.text;
        allContentsData = JsonUtility.FromJson<Contents>(_data);

        LevelManager.Instance.SetData(allContentsData);
    }
}
