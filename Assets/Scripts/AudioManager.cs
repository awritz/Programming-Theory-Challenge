using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip itemPickupSound;
    [SerializeField] private AudioClip itemDropSound;
    [SerializeField] private AudioClip orderCompleteSound;
    [SerializeField] private AudioClip orderIncompleteSound;
    [SerializeField] private AudioClip orderFailedSound;

    private AudioSource _audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayItemPickupSound()
    {
        _audioSource.PlayOneShot(itemPickupSound);
    }

    public void PlayItemDropSound()
    {
        _audioSource.PlayOneShot(itemDropSound);
    }

    public void PlayOrderCompleteSound()
    {
        _audioSource.PlayOneShot(orderCompleteSound);
        
    }

    public void PlayOrderIncompleteSound()
    {
        _audioSource.PlayOneShot(orderIncompleteSound);
        
    }

    public void PlayOrderFailedSound()
    {
        _audioSource.PlayOneShot(orderFailedSound);
        
    }
}
