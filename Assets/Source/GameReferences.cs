using System;
using UnityEngine;


// Static class boys, kekw
public static class GameReferences
{
    public static Settings settings {get; private set;}
    public static Player player {get; private set;}

    // ...
    public static void SetPlayerReference(Player reference) => player = reference;
    public static void SetSettingsReference(Settings reference) => settings = reference;

}
