// Author: Joon Song
// File Name: Shop.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/01/2019
// Modified Date: 01/17/2019
// Description: Class to hold Shop object

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace ISU_Medieval_Odyssey
{
    public sealed class Shop : IBuilding
    {
        /// <summary>
        /// The graphical <see cref="Rectangle"/> for this <see cref="Shop"/>
        /// </summary>
        public Rectangle Rectangle => insideShopSprite.Rectangle;

        // Shop graphics, images, and important coordinates
        private static Texture2D insideShopImage;
        private static Texture2D outsideShopImage;
        private static SoundEffect doorSoundEffect;
        private static Vector2Int shopLocation = new Vector2Int(4, 3);
        private static Vector2Int exitLocation = new Vector2Int(4, 5);
        private static Vector2Int enterLocation = new Vector2Int(4, 6);
        private static List<Vector2Int> insideObstructionLocs = new List<Vector2Int>();
        private static List<Vector2Int> outsideObstructionLocs = new List<Vector2Int>();

        // Various constants representing dimensions of the Shop
        private const int INSIDE_WIDTH = 7;
        private const int INSIDE_HEIGHT = 2;
        private const int OUTSIDE_WIDTH = 9;
        private const int OUTSIDE_HEIGHT = 6;
        private const int PIXEL_WIDTH = Tile.SPACING * OUTSIDE_WIDTH;
        private const int PIXEL_HEIGHT = Tile.SPACING * OUTSIDE_HEIGHT;

        // Sprites for this Shop's inside and outside images
        private Sprite insideShopSprite;
        private Sprite outsideShopSprite;

        // Shop buying/selling function related data
        private const float MAX_PROFIT_CUT = 0.95f;
        private readonly float profitCut;
        private const int ROW_SIZE = 9;
        private const int INVENTORY_SIZE = 3 * ROW_SIZE;
        private ItemSlot[] inventory = new ItemSlot[INVENTORY_SIZE];
        private ItemSlot sellItemSlot;
        private ItemSlot buyItemSlot;

        /// <summary>
        /// Static constructor for <see cref="Shop"/> object
        /// </summary>
        static Shop()
        {
            // Importing shop images and audio
            insideShopImage = Main.Content.Load<Texture2D>("Images/Sprites/Buildings/shopInsideImage");
            outsideShopImage = Main.Content.Load<Texture2D>("Images/Sprites/Buildings/shopOutsideImage");
            doorSoundEffect = Main.Content.Load<SoundEffect>("Audio/SoundEffects/doorSoundEffect");

            // Setting up inside obstruction tiles
            for (int i = 0; i < INSIDE_WIDTH; ++i)
            {
                insideObstructionLocs.Add(new Vector2Int(1 + i, 2));
            }
            for (int i = 0; i < INSIDE_HEIGHT; ++i)
            {
                insideObstructionLocs.Add(new Vector2Int(0, 3 + i));
                insideObstructionLocs.Add(new Vector2Int(INSIDE_WIDTH + 1, 3 + i));
            }
            for (int i = 0; i < INSIDE_WIDTH / 2; ++i)
            {
                insideObstructionLocs.Add(new Vector2Int(1 + i, 5));
                insideObstructionLocs.Add(new Vector2Int(5 + i, 5));
            }
            insideObstructionLocs.Add(new Vector2Int(4, 6));

            // Setting up outside obstruction tiles
            for (int i = 0; i < INSIDE_WIDTH; ++i)
            {
                outsideObstructionLocs.Add(new Vector2Int(1 + i, 1));
                outsideObstructionLocs.Add(new Vector2Int(1 + i, 5));
            }
            for (int i = 1; i < OUTSIDE_HEIGHT - 1; ++i)
            {
                outsideObstructionLocs.Add(new Vector2Int(1, 1 + i));
                outsideObstructionLocs.Add(new Vector2Int(INSIDE_WIDTH, 1 + i));
            }
        }

        /// <summary>
        /// Constructor for <see cref="Shop"/> object
        /// </summary>
        /// <param name="cornerTile">The position of the tile <see cref="Tile"/> in the top left corner</param>
        public Shop(Vector2Int cornerTile)
        {
            // Setting up shop inventory and buying/selling function
            Item[] shopItems = new Item[SharedData.RNG.Next(INVENTORY_SIZE)];
            profitCut = (float)(SharedData.RNG.NextDouble() * MAX_PROFIT_CUT);
            for (int i = 0; i < shopItems.Length; ++i)
            {
                shopItems[i] = Item.RandomItem();
            }
            for (int i = 0; i < INVENTORY_SIZE; ++i)
            {
                inventory[i] = new ItemSlot((int)(SharedData.SCREEN_HEIGHT / 2 - 5 + (i % ROW_SIZE - 3.5) * 70), 
                    210 + 70 * (i / ROW_SIZE), i < shopItems.Length ? shopItems[i] : null, Color.White);
            }
            buyItemSlot = new ItemSlot(710, 455, null, Color.Green);
            sellItemSlot = new ItemSlot(150, 455, null, Color.Red);

            //toSellItemSlot = new Item((int)(SharedData.SCREEN_HEIGHT / 2 - 5 + (j - 3.5) * 70))


            // Setting up inside and outside shop images
            insideShopSprite = new Sprite(insideShopImage, new Rectangle(cornerTile.X * Tile.SPACING, cornerTile.Y * Tile.SPACING, PIXEL_WIDTH, PIXEL_HEIGHT));
            outsideShopSprite = new Sprite(outsideShopImage, new Rectangle(cornerTile.X * Tile.SPACING, cornerTile.Y * Tile.SPACING, PIXEL_WIDTH, PIXEL_HEIGHT));

            // Setting up appropriate obstructions and functions for certain locations
            for (int i = 0; i < insideObstructionLocs.Count; ++i)
            {
                World.Instance.GetTileAt(cornerTile + insideObstructionLocs[i]).InsideObstructState = true;
            }
            for (int i = 0; i < outsideObstructionLocs.Count; ++i)
            {
                World.Instance.GetTileAt(cornerTile + outsideObstructionLocs[i]).OutsideObstructState = true;
            }
            World.Instance.GetTileAt(cornerTile + exitLocation).OnInteractProcedure = new Interaction(Direction.Down, (player) =>
            {
                World.Instance.IsInside = false;
                World.Instance.CurrentBuilding = null;
                player.Y += Tile.SPACING;
              //  doorSoundEffect.CreateInstance().Play();
            });
            World.Instance.GetTileAt(cornerTile + enterLocation).OnInteractProcedure = new Interaction(Direction.Up, (player) =>
            {               
                World.Instance.IsInside = true;
                World.Instance.CurrentBuilding = this;
                player.Y -= Tile.SPACING;
                //doorSoundEffect.CreateInstance().Play(); // ??
            });
            World.Instance.GetTileAt(cornerTile + shopLocation).OnInteractProcedure = new Interaction(Direction.Up, (player) =>
            {
                player.InTransaction = !player.InTransaction;
                player.IsInventoryOpen = true;
            });
        }

        /// <summary>
        /// Subprogram to get this <see cref="Shop"/>'s offer for a given <see cref="Item"/>
        /// </summary>
        /// <param name="item">The <see cref="Item"/> to get an offer from</param>
        /// <returns>The offer for the <see cref="Item"/></returns>
        public int GetOffer(Item item) => (int)(item.Value * profitCut);

        /// <summary>
        /// Update subprogram for this <see cref="Shop"/>
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public void Update(GameTime gameTime)
        {
            // Item to help with "swapping" items
            Item tempSwapItem = null;
            
            // Swapping "Sell Item"-item use drops an item in it
            if (MouseHelper.IsRectangleLeftClicked(sellItemSlot.Rectangle) && (Player.Instance.ItemInHand == null || 
                (Player.Instance.ItemInHand != null && Player.Instance.ItemInHand.IsPlayerItem)))
            {
                tempSwapItem = Player.Instance.ItemInHand;
                Player.Instance.ItemInHand = sellItemSlot.Item;
                sellItemSlot.Item = tempSwapItem;
                tempSwapItem = null;
            }
        }

        /// <summary>
        /// Subprogram to draw the inside of the <see cref="Shop"/>
        /// </summary>
        /// <param name="spriteBatch">Spritebatch to draw sprites</param>
        public void DrawInside(SpriteBatch spriteBatch)
        {
            // Drawing the inside of the shop
            insideShopSprite.Draw(spriteBatch);
        }

        /// <summary>
        /// Subprogram to draw this <see cref="Shop"/>'s inventory
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw sprites</param>
        public void DrawInventory(SpriteBatch spriteBatch)
        {
            // Drawing inventory items for shop
            for (byte i = 0; i < inventory.Length; ++i)
            {
                inventory[i].Draw(spriteBatch);
            }
            buyItemSlot.Draw(spriteBatch);
            sellItemSlot.Draw(spriteBatch);
        }

        /// <summary>
        /// Subprogram to draw the outside of the <see cref="Shop"/>
        /// </summary>
        /// <param name="spriteBatch">Spritebatch to draw sprites</param>
        public void DrawOutside(SpriteBatch spriteBatch)
        {
            // Drawing the outside of the shop
            outsideShopSprite.Draw(spriteBatch);
        }
    }
}