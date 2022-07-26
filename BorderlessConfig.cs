using System.ComponentModel;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace BorderlessTerraria
{
    [Label("$Mods.BorderlessTerraria.Config.Header")]
    public class BorderlessConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Label("$Mods.BorderlessTerraria.Config.BorderlessFullscreen")]
        [DefaultValue(true)]
        public bool BorderlessFullscreen;

        public override void OnLoaded()
        {
            On.Terraria.Main.BorderedHeight += (orig, height, state) =>
            {
                if (state) return orig(height, state);
                return ModContent.GetInstance<BorderlessConfig>().BorderlessFullscreen ? height : orig(height, state);
            };

            base.OnLoaded();
            Update(BorderlessFullscreen);
        }

        public override void OnChanged()
        {
            base.OnChanged();
            Update(BorderlessFullscreen);
        }

        public static void Update(bool borderless)
        {
            Main.QueueMainThreadAction(() =>
            {
                Main.SetFullScreen(!borderless);
                Main.instance.Window.IsBorderlessEXT = borderless;

                if (borderless)
                {
                    Main.SetDisplayMode(Main.displayWidth[Main.numDisplayModes - 1], Main.displayHeight[Main.numDisplayModes - 1], false);
                    SDL2.SDL.SDL_SetWindowPosition(Main.instance.Window.Handle, 0, 0);
                }
            });
        }
    }
}