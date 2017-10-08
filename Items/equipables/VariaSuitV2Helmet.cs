﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MetroidMod.Items.equipables
{
    [AutoloadEquip(EquipType.Head)]
    public class VariaSuitV2Helmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Varia Suit V2 Helmet");
            Tooltip.SetDefault("5% increased ranged damage\n" +
            "+15 overheat capacity\n" +
            "Improved night vision");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.rare = 4;
            item.value = 12000;
            item.defense = 10;
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedDamage += 0.05f;
            player.nightVision = true;
            MPlayer mp = player.GetModPlayer<MPlayer>(mod);
            mp.maxOverheat += 15;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "VariaSuitBreastplate");
            recipe.AddIngredient(ItemID.MythrilBar, 10);
            //recipe.AddIngredient(ItemID.SoulofLight, 5);
            //recipe.AddIngredient(ItemID.SoulofNight, 5);
            recipe.AddIngredient(null, "EnergyTank");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "VariaSuitBreastplate");
            recipe.AddIngredient(ItemID.OrichalcumBar, 10);
            //recipe.AddIngredient(ItemID.SoulofLight, 5);
            //recipe.AddIngredient(ItemID.SoulofNight, 5);
            recipe.AddIngredient(null, "EnergyTank");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
