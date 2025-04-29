using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Story", menuName = "ScriptableObjects/Story", order = 1)]
public class StoryConfig : ScriptableObject
{
    public Sprite _image;
    public string _title;
    public string _description;
}
