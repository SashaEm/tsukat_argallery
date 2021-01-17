using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ImageDescSerializable {
    public string fileName;
    public string name;
    public string author;
    public string description;

    public ImageDescSerializable(string fileName, string name, string author, string description)
    {
        this.fileName = fileName;
        this.name = name;
        this.author = author;
        this.description = description;
    }
}
