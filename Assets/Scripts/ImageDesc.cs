using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ImageProfile")]
public class ImageDesc : ScriptableObject
{
    public string fileName;
    public string name;
    public string author;
    public string description;
}
