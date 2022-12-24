using Modding;
using System;
using SFCore.Utils;

namespace GrimmchildAnywhere
{
    public class GrimmchildAnywhereMod : Mod
    {
        private static GrimmchildAnywhereMod? _instance;

        internal static GrimmchildAnywhereMod Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException($"An instance of {nameof(GrimmchildAnywhereMod)} was never constructed");
                }
                return _instance;
            }
        }

        public override string GetVersion() => GetType().Assembly.GetName().Version.ToString();

        public GrimmchildAnywhereMod() : base("GrimmchildAnywhere")
        {
            _instance = this;
        }

        public override void Initialize()
        {
            Log("Initializing");

            On.PlayMakerFSM.OnEnable += OnFsmEnable;

            Log("Initialized");
        }
        private void OnFsmEnable(On.PlayMakerFSM.orig_OnEnable orig, PlayMakerFSM self)
        {
            orig(self);

            if (self.gameObject.name == "Charm Effects" && self.FsmName == "Spawn Grimmchild")
            {
                self.ChangeFsmTransition("Dream?", "FINISHED", "Charms Allowed?");
            }
            else if (self.gameObject.name == "Grimm Scene" && self.Fsm.Name == "Initial Scene")
            {
                self.ChangeFsmTransition("Absorb Start", "FINISHED", "Absorb End");
                self.ChangeFsmTransition("Absorb Start 2", "FINISHED", "Absorb End 2");
            }
        }
    }
}
