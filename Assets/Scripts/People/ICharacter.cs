using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    void Jump();
    // void PlayAttackSound(AudioClip attackSound);

    void Attack();
    // void PlayAttackSound(AudioClip attackSound);

    void TakeDamage(int damageAmount);
    // void PlayTakeDamageSound(AudioClip attackSound);
    
    void Die();
    // void PlayDieSound(AudioClip attackSound);
    
}
