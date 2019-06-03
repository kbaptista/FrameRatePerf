using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate Vector3 SpawnLocation ();

public enum SpawnMethod
{
    Up,
    Down,
    Left,
    Right,
    Front,
    Back,
    Random
}

