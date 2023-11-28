using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VolumeSliderValue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_tmp;

    public void ChangeValue(int v)
    {
        m_tmp.text = v.ToString();
    }
}
