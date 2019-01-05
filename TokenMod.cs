using Terraria;
using Terraria.ID;
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

        public override void AddRecipeGroups()
        {
            RecipeGroup group = new RecipeGroup(() => "Any Strange Plant", new int[]
            {
                ItemID.StrangePlant1,
                ItemID.StrangePlant2,
                ItemID.StrangePlant3,
                ItemID.StrangePlant4
            });
            RecipeGroup.RegisterGroup("AnyStrangePlant", group);
        }
    }
}
