# TokenMod

*A tModLoader mod for Terraria allowing crafting of rare items.*

Six Blessed Apples, but not a single Rod of Discord.
The Golem giving you his fifth Sun Stone without any Picksaw.
Two hours of generating worlds in search of a Pyramid - in vain.
Just one underground painting missing for your collection.
Fishing, no comment needed.

Does that sound familiar ? If yes, you have been visited by the R.N.Goddess and found unworthy. But thanks to this mod, your troubles are over !

TokenMod is a mod that adds special crafting components to the game, dropping randomly within the world. These components allow you to craft (almost) all rare or hard-to-find items in the game from the comfort of your home.

## New items

New items are sorted in three categories that are used for various purposes.

* Location tokens are used to craft items linked to specific biomes and locations. They can be acquired from killing enemies in the appropriate biome.
* Tier Essence is used to craft items of a certain Tier, depending on their acquisition method (Some items are slightly adjusted for balance, such as the Reaver Shark). They can be acquired from killing enemies, the dropped essence depending on defeated bosses. The world is split in 8 tiers, as follow :
  * Tier 1 : Pre-Hardmode, Pre-EoW items. Very early items (Surface forest) are internally Tier 0, making them cheaper to purchase.
  * Tier 2 : Pre-Hardmode, post-EoW items - notably includes Space, Dungeon, Underworld, Underground Jungle items
  * Tier 3 : Hardmode, pre-Mechanical boss items
  * Tier 4 : Post-MB, pre-Plantera items
  * Tier 5 : Post-Plantera, pre-Golem items
  * Tier 6 : Post-Golem, pre-Moon Lord items
  * Tier 7 : Post-Moon Lord items (Not a lot !)

  Tier essence can be downgraded for free.

* Special Tokens and items are acquired differently and have various uses
  * Boss Tokens are acquired from bosses and used to craft Boss items
  * Eternia Stones are exchanged 1:1 from Defender Medals and contain both Boss and Invasion Tokens as well as Tier Essence
  * Fishing Tokens are acquired by fishing or completing quests and are used for crafting special Fish and quest rewards
  * Invasion Tokens are acquired from Invasion-type event (Both progress and time-limited) and used to craft related items
  * NPC Tokens can be acquired by killing critters and other friendlies and are used to craft NPC drops (But you would *never*... right ?)
  * Shadow Locked Tokens can be created for a marginal cost and unlocked with a Shadow Key to create Shadow Chest items
  * Traveling Merchant Tokens are purchased from the Merchant and are used to create Traveling Merchant items or to duplicate them (The latter does not cost Essence or Tokens). Each token is worth 10 silver and used to create 10 silver worth of Traveling Merchant items.
  * Weather Tokens are acquired during special weather and are used to create items from Weather-restricted enemies
  * The Meteor Charm is a craftable item that summons a Meteorite on the world. This does not override standard Meteorite restrictions (e.g. block limit)

## Essence and Token Acquisition

Most sources of Tier Essence will give the same amount of the appropriate Location or Special token. This amount is determined by two things :

- A base value, which is equal to the base coin value of enemies or a fixed value for other sources (e.g. Eternia Stone). Usually, the harder the enemy, the more Essence it can drop
- A value "softcap" which rises with world tier and lowers amount of tokens granted by high-value enemies (Do note that higher value always equates more tokens, the scaling is just fractional instead)

The cost of an item is calculated by a simple formula that multiplies the item rarity (Calculated from the source rarity and likelihood to give the item) with the average amount of Essence that would be given out by a standard enemy of this tier.

In order to avoid cluttering the inventory while exploring, killing enemies only has a low chance of dropping Tokens, however, the amount they drop is proportionally higher to compensate.

## Crafting recipes

All recipes created by the mod are available at any anvil. Most recipes follow one of several schemas :

* Items with Normal recipes are crafted using Tier tokens and up to two special or Location tokens. Most recipes follow this schema. Costs rise with item rarity and tier. Some items require additional components outlining special acquisition rules (For example, creating Dungeon items require a Golden Key)
* Items with Swap recipes can additionally be crafted using another item of the same pool and a discounted price in Tokens and Location. This is mostly relevant for boss and chest/mimic items.
* Items with Traveling Merchant recipes can be duplicated. Duplicating cheap objects (e.g. Dynasty Wood) requires a stack of the original size. Duplicating simulates buying more of an item and therefore only costs Traveling Merchant Tokens.

Currently, recipes include the following (As a general rule, craftable or purchasable items as well as consumables are excluded) :

* Almost all enemy drops with a droprate lower than 10%
* Almost all enemy drops from invasion events
* All enemy drops from rare enemies (Showing up on the Lifeform Analyzer)
* Almost all non-guaranteed Boss items (Excluding Dev items, Expert items and Achievement items, e.g. 0x33's Aviators)
* Most friendly NPC items
* Most primary chest items (e.g. Magic Mirror, Flamelash)
* Most other useful exploration items (e.g. Life Crystal/Fruits, Enchanted Swords, Shadow/Crimson Orb items)
* Most Wireable traps
* Dye components, Strange plants and Rare Dyes
* Golden Critters, Truffle Worms and Tree Nymph Butterflies (Do not ask how it works)
* Jellyfish and weapon/tool fish
* Fishing crate items
* Uncraftable furniture (Includes decorative banners, uncraftable chests, dungeon and underworld decorations)
* Uncraftable statues
* Non-painter paintings
* Angler items
* Traveling and Skeleton Merchant items
* Seasonal items

## Contact Info

Find me on Terraria Community Forums ! https://forums.terraria.org/index.php?members/seud.11063/

Find me on Steam ! https://steamcommunity.com/id/seudazereza

Find me on Discord ! Seud#4723

Find me on Github ! You are here