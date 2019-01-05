using Terraria.ID;
using Terraria.ModLoader;

namespace TokenMod
{
    public class TokenRecipe : ModRecipe
    {
        private readonly string condition;

        public TokenRecipe(Mod mod, string condition) : base(mod)
        {
            this.condition = condition;
        }

        public override bool RecipeAvailable()
        {
            if (string.IsNullOrEmpty(condition)) return true;
            return TokenUtils.GetRecipeCondition(condition);
        }
    }
}
