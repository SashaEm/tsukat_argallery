using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionManager : MonoBehaviour
{
    [SerializeField] private Text _name;
    [SerializeField] private Text _author;
    [SerializeField] private Text _description;

    public void UpdateText(ImageDescSerializable imageDescSerializable)
    {
        _name.text = imageDescSerializable.name;
        _author.text = imageDescSerializable.author;
        _description.text = imageDescSerializable.description;
    }
}
