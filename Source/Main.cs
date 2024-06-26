﻿using HarmonyLib;
using Verse;
using RimWorld;

namespace SquadAttackEvenWithSameWeapon
{
    [StaticConstructorOnStartup]
    public static class Main
    {
        static Main()
        {
            Harmony instance = new Harmony("doug.squadattackevenwithsameweapon");
            instance.PatchAll();
        }
    }

    /*[HarmonyPatch(typeof(PawnAttackGizmoUtility), "AtLeastTwoSelectedPlayerPawnsHaveDifferentWeapons")]
    public static class PawnAttackGizmoUtility_AtLeastTwoSelectedPlayerPawnsHaveDifferentWeapons_Patch
    {
	    [HarmonyPrefix]
	    public static bool Prefix(ref bool __result)
	    {
		    __result = Find.Selector.NumSelected >= 2;
		    return false;
	    }
    }*/

    [HarmonyPatch(typeof(PawnAttackGizmoUtility), "ShouldUseSquadAttackGizmo")]
    public static class PawnAttackGizmoUtility_ShouldUseSquadAttackGizmo_Patch
    {
	    [HarmonyPostfix]
	    public static void ShouldUseSquadAttackGizmo(ref bool __result)
	    {
		    if (__result)
		    {
			    return;
		    }

		    __result = (bool) Traverse.Create(typeof(PawnAttackGizmoUtility)).Method("AtLeastOneSelectedPlayerPawnHasRangedWeapon").GetValue();
	    }
    }
}