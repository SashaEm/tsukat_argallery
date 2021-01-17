using easyar;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public static class LocalDataSystem 
{
    public static string SAVE_DESC_FOLDER = Application.persistentDataPath + "/SavedImageDescs/";
    public static string IMAGE_FOLDER = Application.persistentDataPath + "/Images/";
    public static void InitPaths()
    {
        if (!Directory.Exists(SAVE_DESC_FOLDER))
        {
            Directory.CreateDirectory(SAVE_DESC_FOLDER);
        }

        if(!Directory.Exists(IMAGE_FOLDER))
        {
            Directory.CreateDirectory(IMAGE_FOLDER);
        }
    }

    public static void SaveDescription(ImageDescSerializable user, string saveId)
    {
        string json = JsonUtility.ToJson(user);
        if (!File.Exists(SAVE_DESC_FOLDER + "save_" + saveId + ".txt"))
            File.WriteAllText(SAVE_DESC_FOLDER + "save_" + saveId + ".txt", json);
    }

    public static Dictionary<string, ImageDescSerializable> LoadAllImageDescs()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_DESC_FOLDER);
        FileInfo[] files = directoryInfo.GetFiles("*.txt");
        Dictionary<string, ImageDescSerializable> imageDescsLoaded = new Dictionary<string, ImageDescSerializable>();
        foreach(var file in files)
        {
            var fileContent = File.ReadAllText(file.ToString());
            var desc = JsonUtility.FromJson<ImageDescSerializable>(fileContent);
            imageDescsLoaded.Add(desc.fileName, desc);
        }
        return imageDescsLoaded;
    }

    public static void LoadNewTargetBehaviour(GameObject[] targets, ImageTrackerFrameFilter frameFilter)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(IMAGE_FOLDER);
        FileInfo[] images = directoryInfo.GetFiles("*.jpeg");

        for(int i = 0; i < images.Length; i++)
        {
            var targetControl = targets[i].GetComponent<ImageTargetController>();
            targetControl.Tracker = frameFilter;
            targetControl.ImageFileSource.Path = images[i].FullName;
            targets[i].GetComponent<DescriptionManager>().UpdateText(LoadAllImageDescs()[images[i].Name]);
        }
    }
}
