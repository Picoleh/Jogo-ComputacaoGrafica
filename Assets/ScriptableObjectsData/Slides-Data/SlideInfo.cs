using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SlideInfo", menuName = "Scriptable Objects/SlideInfo")]
public class SlideInfo : ScriptableObject
{
    public Sprite image;
    public Dialogue lines;
}
