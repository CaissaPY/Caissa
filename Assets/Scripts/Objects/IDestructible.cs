using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestructible
{
    void TakeDamage(int damageAmount);
    // void PlayTakeDamageSound(AudioClip attackSound);
    
    void Die();
    // void PlayDieSound(AudioClip attackSound);

}
