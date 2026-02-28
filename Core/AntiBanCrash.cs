using HarmonyLib;
using PlayFab;
using PlayFab.Internal;
using System;
using System.Collections.Generic;

/*
 * This mod is NOT for cheating, its built for protection.
 * There are NO cheats or hacks in the mod.
 * 
 * Enjoy :3 -nyph
*/

namespace GorillaProt.Core
{
    [HarmonyPatch(typeof(PlayFabUnityHttp), nameof(PlayFabUnityHttp.MakeApiCall))]
    public class AntiBanCrash
    {
        public static bool enabled;

        private static bool Prefix(object reqContainerObj)
        {
            if (!enabled || reqContainerObj == null)
                return true;

            CallRequestContainer callRequestContainer = (CallRequestContainer)reqContainerObj;

            if (callRequestContainer.ErrorCallback != null)
            {
                Action<PlayFabError> errorCallback = callRequestContainer.ErrorCallback;
                void overrideError(PlayFabError error)
                {
                    if (error.ErrorMessage.ToLower().Contains("ban") || error.ErrorMessage.ToLower().Contains("banned") || error.ErrorMessage.ToLower().Contains("suspended") || error.ErrorMessage.ToLower().Contains("suspension"))
                    {
                        if (error.ErrorMessage.ToLower().Contains("this ip has been banned"))
                            UnityEngine.Debug.LogError("GorillaProt > Your IP address is currently banned.");
                        else
                            UnityEngine.Debug.LogError("GorillaProt > Your account is currently banned.");
                        PlayFabError fakeError = new PlayFabError
                        {
                            Error = PlayFabErrorCode.UnknownError,
                            ErrorMessage = "An unknown error occurred.",
                            ErrorDetails = new Dictionary<string, List<string>>()
                        };
                        errorCallback?.Invoke(fakeError);
                        return;
                    }
                    errorCallback?.Invoke(error);
                }

                callRequestContainer.ErrorCallback = overrideError;
            }

            return true;
        }
    }
}
