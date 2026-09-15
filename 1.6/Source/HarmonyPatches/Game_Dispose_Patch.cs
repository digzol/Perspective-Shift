using System;
using HarmonyLib;
using UnityEngine;
using Verse;

namespace PerspectiveShift
{
    [HarmonyPatch(typeof(Game), nameof(Game.Dispose))]
    public static class Game_Dispose_Patch
    {
        public static void Prefix()
        {
            if (State.Avatar == null) return;
            try
            {
                State.ClearAvatar();
            }
            catch (Exception ex)
            {
                State.Error($"Failed to clear avatar before game dispose: {ex}");
                State.Avatar = null;
                Cursor.visible = true;
            }
        }
    }
}
