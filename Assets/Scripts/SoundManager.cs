using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip shootAttack, zombieAttack, ninjaLose, ninjaDead;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        shootAttack = Resources.Load<AudioClip>("sfx_warrior_attack1");
        zombieAttack = Resources.Load<AudioClip>("Z1-V1-Attacking-Free-1");
        ninjaLose = Resources.Load<AudioClip>("stinger_lose");
        ninjaDead = Resources.Load<AudioClip>("foley_warrior_death3");
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip){
        switch(clip){
            case "shoot":
                audioSrc.PlayOneShot(shootAttack);
                break;
            
            case "zombieAttack":
                audioSrc.PlayOneShot(zombieAttack);
                break;
                
            case "ninjaLose":
                audioSrc.PlayOneShot(ninjaLose);
                break;
                
            case "ninjaDead":
                audioSrc.PlayOneShot(ninjaDead);
                break;
        }
    }
}
