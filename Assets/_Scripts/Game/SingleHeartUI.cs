using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleHeartUI : MonoBehaviour
{
    [SerializeField] private Image _heartImage;

    public Image GetHeartImage()
    {
        return _heartImage;
    }
}
