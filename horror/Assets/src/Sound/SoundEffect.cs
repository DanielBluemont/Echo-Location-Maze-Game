using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    private AudioClip m_Clip;
    [SerializeField] private AudioSource _source;

    public AudioClip CLip
    {
        set 
        { 
            m_Clip = value; 
        }
    }

    private void Start()
    {
        _source.PlayOneShot(m_Clip);
        Destroy(gameObject, m_Clip.length);
    }
}
