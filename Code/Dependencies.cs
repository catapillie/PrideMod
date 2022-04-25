using System.Linq;

namespace Celeste.Mod.PrideMod {
    internal static class Dependencies {
        public static bool CollabUtils2_Loaded { get; private set; }

        internal static void Everest_OnLoadMod(EverestModuleMetadata meta) {
            if (PrideModModule.Instance.AllDependencies.Contains(meta))
                Hook();
        }

        internal static void Hook() {
            PrideModModule.Log("Hooking into Pride Mod dependencies");

            if (!CollabUtils2_Loaded) {
                if (Everest.Loader.DependencyLoaded(GetDependencyMetadata("CollabUtils2"))) {
                    Reskinning.MiniHearts.Hook_CollabUtils2();
                    CollabUtils2_Loaded = true;
                }
            }
        }

        internal static void Unhook() {
            PrideModModule.Log("Unhooking from Pride Mod dependencies");

            if (CollabUtils2_Loaded) {
                Reskinning.MiniHearts.Unhook_CollabUtils2();
                CollabUtils2_Loaded = false;
            }
        }

        private static EverestModuleMetadata GetDependencyMetadata(string name)
            => PrideModModule.Instance.AllDependencies.First(meta => meta.Name == name);
    }
}
