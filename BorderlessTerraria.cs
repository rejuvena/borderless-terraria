using Terraria.ModLoader;

namespace BorderlessTerraria
{
    public class BorderlessTerraria : Mod
    {
        public override void Unload()
        {
            base.Unload();

            BorderlessConfig.Update(false);
        }
    }
}