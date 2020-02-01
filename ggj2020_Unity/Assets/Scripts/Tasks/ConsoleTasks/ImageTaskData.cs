using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewImageTask", menuName = "Image Task")]
public class ImageTaskSO : ScriptableObject
{
    public string startText;
    public string winText;
    public string failText;

    [TextArea(20, 100)]
    public string image;
}
