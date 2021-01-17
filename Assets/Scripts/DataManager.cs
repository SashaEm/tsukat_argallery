using easyar;
using System.Linq;
using UnityEngine;
using static DatabaseSystem;

public class DataManager : MonoBehaviour
{
    private const string imageDescPath = @"ImageDescsUpload";
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private ImageTrackerFrameFilter frameFilter;

    

    private void Awake()
    {
        LocalDataSystem.InitPaths();
        Initialize();
    }

    private void Initialize()
    {
        int hasPlayed = PlayerPrefs.GetInt("HasPlayed");

        if (hasPlayed == 0)
        {
            GetDataFromServer();
            PlayerPrefs.SetInt("HasPlayed", 1);
        }
    }

    /// <summary>
    /// Puts all data to image description database (firebase) 
    /// </summary>
    public void PostAllData()
    {
        var imageDescList = Resources.LoadAll<ImageDesc>(imageDescPath).ToList();
        var id = 0;
        foreach (var image in imageDescList)
        {
            var thisObj = new ImageDescSerializable(image.fileName, image.name, image.author, image.description);
            DatabaseSystem.PostImageDesc(thisObj, id.ToString(), () => { Debug.Log("posted"); });
            id++;
        }
    }

    private void GetDataFromServer()
    {
        Debug.Log("GetDataFromServer Called");
        GetImageDescsFromDB(users =>
        {
            foreach (var user in users)
            {
                SaveImage($"{user.Value.fileName}");
                LocalDataSystem.SaveDescription(user.Value, user.Key);
            }
        });
    }

    public void CacheData()
    {
        var imageDescriptionLibrary = LocalDataSystem.LoadAllImageDescs();

        GameObject[] targets = new GameObject[imageDescriptionLibrary.Count];
        for (int i = 0; i < imageDescriptionLibrary.Count; i++)
        {
            targets[i] = Instantiate(targetPrefab);
        }

        LocalDataSystem.LoadNewTargetBehaviour(targets, frameFilter);
    }
}
