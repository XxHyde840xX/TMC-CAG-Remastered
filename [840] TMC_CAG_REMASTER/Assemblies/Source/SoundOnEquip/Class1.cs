using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.Sound;

namespace SoundOnEquip
{
    public class SoundExtension : DefModExtension
    {
        public SoundDef equipSound;
    }
	[StaticConstructorOnStartup]
	internal static class HarmonyContainer
	{
		public static Harmony harmony;
		static HarmonyContainer()
		{
			harmony = new Harmony("SoundOnEquip.Mod");
			harmony.PatchAll();
		}
	}

    [HarmonyPatch(typeof(Pawn_ApparelTracker), "Wear")]
    public static class ApparelTracker_Wear
    {
        public static void Postfix(Apparel newApparel, Pawn_ApparelTracker __instance)
        {
            var extension = newApparel.def.GetModExtension<SoundExtension>();
            if (extension != null && extension.equipSound != null)
            {
                extension.equipSound.PlayOneShot(new TargetInfo(__instance.pawn.Position, __instance.pawn.Map));
            }
        }
    }
}
