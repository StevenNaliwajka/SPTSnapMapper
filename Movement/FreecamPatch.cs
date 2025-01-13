using EFT;
using HarmonyLib;
using SPT.Reflection.Patching;
using SPTSnapMaper.DesignPatterns;
using System.Reflection;


// Base Freecamera code pulled from 'Freecam' by Terkoiz
// https://hub.sp-tarkov.com/files/file/279-freecam


namespace SPTSnapMaper.Movement
{
     public class FreecamPatch : ModulePatch
     {
          protected override MethodBase GetTargetMethod()
          {
               return AccessTools.Method(typeof(GameWorld), nameof(GameWorld.OnGameStarted));
          }

          [PatchPostfix]
          public static void PatchPostFix()
          {
               var gameWorld = Singleton<GameWorld>.Instance;

               if (gameWorld == null)
                    return;

               // Add FreecamController to GameWorld GameObject
               gameWorld.gameObject.AddComponent<FreecamController>();
          }
     }
}