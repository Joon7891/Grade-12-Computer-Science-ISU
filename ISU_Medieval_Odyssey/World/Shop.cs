// Author: Joon Song
// File Name: Shop.cs
// Project Name: ISU_Medieval_Odyssey
// Creation Date: 01/01/2019
// Modified Date: 01/17/2019
// Description: Class to hold Shop object

using System.Linq;
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
        private const float MIN_PROFIT_CUT = 0.60f;
        private const float MAX_PROFIT_CUT = 0.95f;
        private readonly float profitCut;
        private const int ROW_SIZE = 9;
        private const int INVENTORY_SIZE = 3 * ROW_SIZE;
        private Button[] transactionButton = new Button[2];
        private ItemSlot[] transactionItemSlot = new ItemSlot[2];
        private ItemSlot[] inventory = new ItemSlot[INVENTORY_SIZE];
        private static SoundEffect errorSoundEffect;
        private static SoundEffect transactionSoundEffect;
        private static Vector2[] priceOfferLocations = new Vector2[2];

        /// <summary>
        /// Static constructor for <see cref="Shop"/> object
        /// </summary>
        static Shop()
        {
            // Importing shop images and audio
            insideShopImage = Main.Content.Load<Texture2D>("Images/Sprites/Buildings/shopInsideImage");
            outsideShopImage = Main.Content.Load<Texture2D>("Images/Sprites/Buildings/shopOutsideImage");
            doorSoundEffect = Main.Content.Load<SoundEffect>("Audio/SoundEffects/doorSoundEffect");
            errorSoundEffect = Main.Content.Load<SoundEffect>("Audio/SoundEffects/errorSoundEffect");
            transactionSoundEffect = Main.Content.Load<SoundEffect>("Audio/SoundEffects/transactionSoundEffect");

            // Setting up price offer text locations
            priceOfferLocations[0] = new Vector2(613, 490);
            priceOfferLocations[1] = new Vector2(235, 490); //80, 32

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
            Item[] shopItems = new Item[SharedData.RNG.Next(2 * ROW_SIZE)];
            profitCut = (float)(SharedData.RNG.NextDouble() * (MAX_PROFIT_CUT - MIN_PROFIT_CUT)) + MIN_PROFIT_CUT;
            for (int i = 0; i < shopItems.Length; ++i)
            {
                shopItems[i] = Item.RandomItem();
                shopItems[i].IsPlayerItem = false;
            }
            for (int i = 0; i < INVENTORY_SIZE; ++i)
            {
                inventory[i] = new ItemSlot((int)(SharedData.SCREEN_HEIGHT / 2 - 5 + (i % ROW_SIZE - 3.5) * 70), 
                    210 + 70 * (i / ROW_SIZE), i < shopItems.Length ? shopItems[i] : null, Color.White);
            }
            transactionItemSlot[0] = new ItemSlot(710, 455, null, Color.Green); // Buy
            transactionItemSlot[1] = new ItemSlot(150, 455, null, Color.Red); // Sell

            // Setting up transaction buttons
            transactionButton[0] = new Button(Main.Content.Load<Texture2D>("Images/Sprites/Buttons/buyButton"), new Rectangle(608, 455, 80, 32), () =>
            {
                // Making tranaction, if possible
                if (transactionItemSlot[0].Item != null && Player.Instance.Gold >= transactionItemSlot[0].Item.Value)
                {
                    transactionItemSlot[0].Item.IsPlayerItem = true;
                    Player.Instance.Gold -= transactionItemSlot[0].Item.Value;
                    Player.Instance.AddToInventory(transactionItemSlot[0].Item);
                    transactionItemSlot[0].Item = null;
                    transactionSoundEffect.CreateInstance().Play();
                }
                else
                {
                    errorSoundEffect.CreateInstance().Play();
                }
            });
            transactionButton[1] = new Button(Main.Content.Load<Texture2D>("Images/Sprites/Buttons/sellButton"), new Rectangle(230, 455, 80, 32), () =>
            {
                // Making transaction if possible, otherwise error
                if (transactionItemSlot[1].Item != null && (inventory.Count(itemSlot => itemSlot.Item == null) + 1 > 0))
                {
                    Player.Instance.Gold += GetOffer(transactionItemSlot[1].Item);
                    transactionItemSlot[1].Item.IsPlayerItem = false;
                    AddToInventory(transactionItemSlot[1].Item);
                    transactionItemSlot[1].Item = null;
                    transactionSoundEffect.CreateInstance().Play();
                }
                else
                {
                    errorSoundEffect.CreateInstance().Play();
                }
            });

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

                if (!player.InTransaction && player.ItemInHand != null && !player.ItemInHand.IsPlayerItem)
                {
                    AddToInventory(player.ItemInHand);
                    player.ItemInHand = null;
                }

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
            
            // Swapping "Sell Item" and "Buy Item" - item if user drops an item in it
            if (MouseHelper.IsRectangleLeftClicked(transactionItemSlot[1].Rectangle))
            {
                if (Player.Instance.ItemInHand == null || Player.Instance.ItemInHand.IsPlayerItem)
                {
                    tempSwapItem = Player.Instance.ItemInHand;
                    Player.Instance.ItemInHand = transactionItemSlot[1].Item;
                    transactionItemSlot[1].Item = tempSwapItem;
                    tempSwapItem = null;
                }
                else
                {
                    errorSoundEffect.CreateInstance().Play();
                }
            }
            else if (MouseHelper.IsRectangleLeftClicked(transactionItemSlot[0].Rectangle))
            {
                if (Player.Instance.ItemInHand == null || !Player.Instance.ItemInHand.IsPlayerItem)
                {
                    tempSwapItem = Player.Instance.ItemInHand;
                    Player.Instance.ItemInHand = transactionItemSlot[0].Item;
                    transactionItemSlot[0].Item = tempSwapItem;
                    tempSwapItem = null;
                }
                else
                {
                    errorSoundEffect.CreateInstance().Play();
                }
            }

            // Picking up the shop's items if user has an empty hand and decides to click on it
            for (byte i = 0; i < inventory.Length; ++i)
            {
                if (MouseHelper.IsRectangleLeftClicked(inventory[i].Rectangle))
                {
                    if (Player.Instance.ItemInHand == null || !Player.Instance.ItemInHand.IsPlayerItem)
                    {
                        tempSwapItem = Player.Instance.ItemInHand;
                        Player.Instance.ItemInHand = inventory[i].Item;
                        inventory[i].Item = tempSwapItem;
                        tempSwapItem = null;
                    }
                    else
                    {
                        errorSoundEffect.CreateInstance().Play();
                    }
                }
            }

            // Updating transaction buttons
            for (byte i = 0; i < transactionButton.Length; ++i)
            {
                transactionButton[i].Update(gameTime);
            }
        }

        /// <summary>
        /// Subprogram to add an <see cref="Item"/> to this <see cref="Shop"/>'s inventory
        /// </summary>
        /// <param name="item">The <see cref="Item"/> to be added</param>
        private void AddToInventory(Item item)
        {
            // Adding item in first available space
            for (byte i = 0; i < inventory.Length; ++i)
            {
                if (inventory[i].Item == null)
                {
                    inventory[i].Item = item;
                    break;
                }
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
            for (byte i = 0; i < transactionItemSlot.Length; ++i)
            {
                transactionItemSlot[i].Draw(spriteBatch);
                transactionButton[i].Draw(spriteBatch);
            }

            // Drawing price offers
            for (byte i = 0; i < priceOfferLocations.Length; ++i)
            {
                if (transactionItemSlot[i].Item != null)
                {
                    spriteBatch.DrawString(SharedData.InformationFonts[2], $"${(i == 0 ? transactionItemSlot[i].Item.Value : GetOffer(transactionItemSlot[i].Item))}", 
                        priceOfferLocations[i], Color.White);
                }
            }
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