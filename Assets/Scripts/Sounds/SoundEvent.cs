using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEvent : ScriptableObject
{
    public enum Kind
    {
        Random,
    }

    public Kind kind;
}
