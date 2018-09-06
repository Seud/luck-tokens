using Terraria.ModLoader;

namespace TokenMod
{
    public class TokenMod : Mod
    {
        public TokenMod()
        {
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadBackgrounds = true,
                AutoloadSounds = true
            };
        }

        public override void AddRecipes()
        {
            TokenRecipes recipes = new TokenRecipes(this);
            recipes.AddRecipes();
        }
    }
}
