using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Sounds
{
    public static void MakeSound(Sound sound)
    {
        Collider[] colliders = new Collider[1];
        Physics.OverlapSphereNonAlloc(sound.pos, sound.range, colliders, LayerMask.GetMask("Monster"));
        if (colliders.Length > 0 && colliders[0] != null && colliders[0].TryGetComponent<IHear>(out IHear agent))
        {
            agent.RespondToSound(sound);
        }
    }
}
