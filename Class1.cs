using HarmonyLib;

namespace Remove25tAbsorbLimiit
{
    public class Patches
    {
        [HarmonyPatch(typeof(ElementSplitterComponents))]
        [HarmonyPatch("CanFirstAbsorbSecond")]
        public class Patch_Absorb
        {
            public static bool Prefix(
                HandleVector<int>.Handle first,
                HandleVector<int>.Handle second,
                ref bool __result
            )
            {
                if (
                    first == HandleVector<int>.InvalidHandle
                    || second == HandleVector<int>.InvalidHandle
                )
                {
                    __result = false;
                    return false;
                }
                ElementSplitter data = GameComps.ElementSplitters.GetData(first);
                ElementSplitter data2 = GameComps.ElementSplitters.GetData(second);

                __result =
                    data.primaryElement.ElementID == data2.primaryElement.ElementID
                    && data.primaryElement.Units + data2.primaryElement.Units < 100000f
                    && !data.kPrefabID.HasTag(GameTags.MarkedForMove)
                    && !data2.kPrefabID.HasTag(GameTags.MarkedForMove);
                return false;
            }
        }
    }
}
