using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TiledCS;
using Penguin_Adventure.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Penguin_Adventure.Enemy;
using System.Collections;

/* Sources:

https://www.youtube.com/watch?v=ceBCDKU_mNw

https://www.youtube.com/watch?v=8u0ndF9H3TU

https://www.youtube.com/watch?v=-U0d41UPjRk

https://www.youtube.com/watch?v=Yi0phs7A7uY

https://www.youtube.com/watch?v=_TlnUM-uhSI&t=325s

https://www.youtube.com/watch?v=03Hq0qbAy8s

https://www.youtube.com/watch?v=ClDf_V5eVMo&t=9s



*/

namespace Penguin_Adventure
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Camera
        private Matrix camera;

        //tiled
        private TiledMap map;
        private Dictionary<int, TiledTileset> tilesets;
        private Texture2D tilesetTexture;

        // used to flip/rotate sprites as necessary later
        [Flags]
        enum Trans
        {
            None = 0,
            Flip_H = 1 << 0,
            Flip_V = 1 << 1,
            Flip_D = 1 << 2,

            Rotate_90 = Flip_D | Flip_H,
            Rotate_180 = Flip_H | Flip_V,
            Rotate_270 = Flip_V | Flip_D,

            Rotate_90AndFlip_H = Flip_H | Flip_V | Flip_D,
        }

        //change card system
        private Dictionary<int, List<Dictionary<string, ArrayList>>> cardsystem = new Dictionary<int, List<Dictionary<string, ArrayList>>>();
        private int currentcard = 0;

        // Movement
        private int speed = 5;

        // Base penguin
        private TiledLayer Tree;
        private Texture2D idle;
        private Texture2D up;
        private Texture2D down;
        private Texture2D left;
        private Texture2D right;

        private Rectangle[] standingAnimation = new Rectangle[8];
        private Rectangle[] upAnimation = new Rectangle[6];
        private Rectangle[] downAnimation = new Rectangle[6];
        private Rectangle[] leftAnimation = new Rectangle[9];
        private Rectangle[] rightAnimation = new Rectangle[9];

        //penguin 1
        private Texture2D idle1;
        private Texture2D up1;
        private Texture2D down1;
        private Texture2D left1;
        private Texture2D right1;

        private Rectangle[] standingAnimation1 = new Rectangle[8];
        private Rectangle[] upAnimation1 = new Rectangle[6];
        private Rectangle[] downAnimation1 = new Rectangle[6];
        private Rectangle[] leftAnimation1 = new Rectangle[9];
        private Rectangle[] rightAnimation1 = new Rectangle[9];

        //penguin 2
        private Texture2D idle2;
        private Texture2D up2;
        private Texture2D down2;
        private Texture2D left2;
        private Texture2D right2;

        private Rectangle[] standingAnimation2 = new Rectangle[8];
        private Rectangle[] upAnimation2 = new Rectangle[6];
        private Rectangle[] downAnimation2 = new Rectangle[6];
        private Rectangle[] leftAnimation2 = new Rectangle[9];
        private Rectangle[] rightAnimation2 = new Rectangle[9];

        //penguin 3
        private Texture2D idle3;
        private Texture2D up3;
        private Texture2D down3;
        private Texture2D left3;
        private Texture2D right3;

        private Rectangle[] standingAnimation3 = new Rectangle[8];
        private Rectangle[] upAnimation3 = new Rectangle[6];
        private Rectangle[] downAnimation3 = new Rectangle[6];
        private Rectangle[] leftAnimation3 = new Rectangle[9];
        private Rectangle[] rightAnimation3 = new Rectangle[9];

        //penguin 4
        private Texture2D idle4;
        private Texture2D up4;
        private Texture2D down4;
        private Texture2D left4;
        private Texture2D right4;

        private Rectangle[] standingAnimation4 = new Rectangle[8];
        private Rectangle[] upAnimation4 = new Rectangle[6];
        private Rectangle[] downAnimation4 = new Rectangle[6];
        private Rectangle[] leftAnimation4 = new Rectangle[9];
        private Rectangle[] rightAnimation4 = new Rectangle[9];

        //penguin 5
        private Texture2D idle5;
        private Texture2D up5;
        private Texture2D down5;
        private Texture2D left5;
        private Texture2D right5;

        private Rectangle[] standingAnimation5 = new Rectangle[8];
        private Rectangle[] upAnimation5 = new Rectangle[6];
        private Rectangle[] downAnimation5 = new Rectangle[6];
        private Rectangle[] leftAnimation5 = new Rectangle[9];
        private Rectangle[] rightAnimation5 = new Rectangle[9];

        private float timer = 0f;
        private int threshold = 50;
        private int currentAnimationIndex = 1;

        private Vector2 location;

        private bool drawattacking = false;
        private bool drawmoving = false;

        private Keys _movementKey = Keys.D; //This keeps track of the key that was pressed last.


        private Point GameBounds = new(1280, 720); // Screen Resolution

        // UI
        private bool startGame;
        private SpriteFont gameTitle;
        private SpriteFont healthText;
        private SpriteFont instructions;
        private List<Component> buttons;
        private Texture2D healthbar;
        private Rectangle healthRec;
        private Texture2D cardContainer;
        private Texture2D _texture;
        private SpriteFont deathText;
        private bool dead;
        private bool win;

        // Weapon system
        private Texture2D stick;
        private Texture2D stickLeft;
        private Texture2D sword;
        private Texture2D swordLeft;
        private Texture2D shuriken;
        private Texture2D shurikenLeft;
        private bool stickAttack;
        private bool swordAttack;
        private bool shurikenAttack;

        // Collision
        private Rectangle playerCollider;
        private Rectangle weaponCollider;
        private bool leftFacing = false;
        private bool rightFacing = false;
        private bool upFacing = true;
        private bool downFacing = false;
        private Vector2 weaponLocation;

        // Damage System
        private int health;
        private Texture2D pixel;

        // Enemy
        // Fox
        private List<Fox> foxes;
        private Texture2D foxIdle;
        private Texture2D foxHit;
        private Texture2D foxDeath;
        private Rectangle[] foxIdleAnimation = new Rectangle[14];
        private Rectangle[] foxHitAnimation = new Rectangle[11];
        private Rectangle[] foxDeathAnimation = new Rectangle[7];
        private int foxIdleFrame;
        private int foxHitFrame;
        private int foxDeathFrame;
        private Fox fox1;
        private Fox fox2;
        private Fox fox3;
        private Fox fox4;
        private Fox fox5;
        private Fox fox6;
        private Fox fox7;
        private Fox fox8;
        private Fox fox9;
        private Fox fox10;
        private Fox fox11;
        private Fox fox12;
        private Fox fox13;
        private Fox fox14;
        private Fox fox15;
        private Fox fox16;
        private Fox fox17;
        private Fox fox18;
        private Fox fox19;
        private Fox fox20;
        private Fox fox21;
        private Fox fox22;
        private Fox fox23;
        private Fox fox24;
        private Fox fox25;
        private Fox fox26;
        private Fox fox27;
        // Wolf
        private List<Wolf> wolves;
        private Texture2D wolfIdle;
        private Texture2D wolfHit;
        private Texture2D wolfDeath;
        private Rectangle[] wolfIdleAnimation = new Rectangle[6];
        private Rectangle[] wolfHitAnimation = new Rectangle[11];
        private Rectangle[] wolfDeathAnimation = new Rectangle[7];
        private int wolfIdleFrame;
        private int wolfHitFrame;
        private int wolfDeathFrame;
        private Wolf wolf1;
        private Wolf wolf2;
        private Wolf wolf3;
        private Wolf wolf4;
        private Wolf wolf5;
        private Wolf wolf6;
        private Wolf wolf7;
        private Wolf wolf8;
        private Wolf wolf9;
        private Wolf wolf10;
        private Wolf wolf11;
        private Wolf wolf12;
        private Wolf wolf13;
        private Wolf wolf14;
        private Wolf wolf15;
        private Wolf wolf16;
        private Wolf wolf17;
        private Wolf wolf18;
        private Wolf wolf19;
        private Wolf wolf20;
        private Wolf wolf21;
        private Wolf wolf22;
        private Wolf wolf23;
        // Bat
        private List<Bat> bats;
        private Texture2D batIdle;
        private Texture2D batHit;
        private Texture2D batDeath;
        private Rectangle[] batIdleAnimation = new Rectangle[6];
        private Rectangle[] batHitAnimation = new Rectangle[11];
        private Rectangle[] batDeathAnimation = new Rectangle[7];
        private int batIdleFrame;
        private int batHitFrame;
        private int batDeathFrame;
        private Bat bat1;
        private Bat bat2;
        private Bat bat3;
        private Bat bat4;
        private Bat bat5;
        private Bat bat6;
        private Bat bat7;
        private Bat bat8;
        private Bat bat9;
        private Bat bat10;
        private Bat bat11;
        private Bat bat12;
        private Bat bat13;
        private Bat bat14;
        private Bat bat15;
        private Bat bat16;
        private Bat bat17;
        private Bat bat18;
        private Bat bat19;
        private Bat bat20;
        private Bat bat21;
        private Bat bat22;
        private Bat bat23;

        //card
        private bool card1;
        private bool card2;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);

            _graphics.PreferredBackBufferWidth = GameBounds.X;
            _graphics.PreferredBackBufferHeight = GameBounds.Y;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            startGame = false;
            dead = false;
            win = false;
            IsMouseVisible = true;
            location = new Vector2(1100, 1100);
            card1 = false;
            card2 = false;
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            _texture = new Texture2D(GraphicsDevice, 1, 1);
            _texture.SetData(new Color[] { Color.WhiteSmoke });

            //tiled
            map = new TiledMap(Content.RootDirectory + "\\mapXL.tmx");
            tilesets = map.GetTiledTilesets(Content.RootDirectory + "\\snow tilset.tsx");
            tilesetTexture = Content.Load<Texture2D>("snow tileset");

            Tree = map.Layers.First(l => l.name == "tree");
            gameTitle = Content.Load<SpriteFont>("Fonts/gameTitle");
            healthText = Content.Load<SpriteFont>("Fonts/healthText");
            instructions = Content.Load<SpriteFont>("Fonts/instructions");
            healthbar = Content.Load<Texture2D>("healthbar");
            cardContainer = Content.Load<Texture2D>("card");

            var startButton = new Button(Content.Load<Texture2D>("Controls/startButton"), Content.Load<SpriteFont>("Fonts/menuButtonFont"))
            {
                position = new Vector2(500, 430),
                text = "Start",
            };

            startButton.Click += StartButton_Click;

            var quitButton = new Button(Content.Load<Texture2D>("Controls/quitButton"), Content.Load<SpriteFont>("Fonts/menuButtonFont"))
            {
                position = new Vector2(700, 430),
                text = "Quit",
            };

            quitButton.Click += QuitButton_Click;

            buttons = new List<Component>()
            { startButton, quitButton };

            // Weapon initialization
            stickAttack = false;
            swordAttack = false;
            shurikenAttack = false;

            // Enemy Animation frame initialization
            foxIdleFrame = 0;
            foxHitFrame = 0; ;
            foxDeathFrame = 0;
            wolfIdleFrame = 0;
            wolfHitFrame = 0;
            wolfDeathFrame = 0;
            batIdleFrame = 0; 
            batHitFrame = 0;
            batDeathFrame = 0;

            // Player
            health = 1170;
        }

        private void QuitButton_Click(object sender, System.EventArgs e)
        {
            Exit();
        }

        private void StartButton_Click(object sender, System.EventArgs e)
        {
            startGame = true;

            List<Dictionary<string, ArrayList>> base_penguin = new List<Dictionary<string, ArrayList>>();
            Dictionary<string, ArrayList> base_penguin_sprites = new Dictionary<string, ArrayList>();
            idle = Content.Load<Texture2D>("sprites/penguin_idle");
            up = Content.Load<Texture2D>("sprites/up");
            down = Content.Load<Texture2D>("sprites/down");
            right = Content.Load<Texture2D>("sprites/right2");
            left = Content.Load<Texture2D>("sprites/left2");

            base_penguin_sprites.Add("idle", new ArrayList() { idle });
            base_penguin_sprites.Add("up", new ArrayList() { up });
            base_penguin_sprites.Add("down", new ArrayList() { down });
            base_penguin_sprites.Add("right", new ArrayList() { right });
            base_penguin_sprites.Add("left", new ArrayList() { left });
            Dictionary<string, ArrayList> base_penguin_animation = new Dictionary<string, ArrayList>();

            for (int i = 0; i < standingAnimation.Length; i++)
            {
                int width = idle.Width / 8;
                int height = idle.Height / 1;
                int row = i / 8;
                int column = i % 8;
                standingAnimation[i] = new Rectangle(width * column, height * row, width, height);
            }
            for (int i = 0; i < upAnimation.Length; i++)
            {
                int width = up.Width / 6;
                int height = up.Height / 1;
                int row = i / 6;
                int column = i % 6;
                upAnimation[i] = new Rectangle(width * column, height * row, width, height);
            }
            for (int i = 0; i < downAnimation.Length; i++)
            {
                int width = down.Width / 6;
                int height = down.Height / 1;
                int row = i / 6;
                int column = i % 6;
                downAnimation[i] = new Rectangle(width * column, height * row, width, height);
            }
            for (int i = 0; i < leftAnimation.Length; i++)
            {
                int width = left.Width / 9;
                int height = left.Height / 1;
                int row = i / 9;
                int column = i % 9;
                leftAnimation[i] = new Rectangle(width * column, height * row, width, height);
            }
            for (int i = 0; i < rightAnimation.Length; i++)
            {
                int width = right.Width / 9;
                int height = right.Height / 1;
                int row = i / 9;
                int column = i % 9;
                rightAnimation[i] = new Rectangle(width * column, height * row, width, height);
            }

            base_penguin_animation.Add("standingAnimation", new ArrayList() { standingAnimation });
            base_penguin_animation.Add("upAnimation", new ArrayList() { upAnimation });
            base_penguin_animation.Add("downAnimation", new ArrayList() { downAnimation });
            base_penguin_animation.Add("leftAnimation", new ArrayList() { leftAnimation });
            base_penguin_animation.Add("rightAnimation", new ArrayList() { rightAnimation });

            base_penguin.Add(base_penguin_sprites);
            base_penguin.Add(base_penguin_animation);

            if (!cardsystem.ContainsKey(0))
                cardsystem.Add(0, base_penguin);

            //Penguin 1
            List<List<object>> penguin1 = new List<List<object>>();
            List<object> penguin1_sprites = new List<object>();
            idle1 = Content.Load<Texture2D>("sprites/penguin_idle");
            up1 = Content.Load<Texture2D>("sprites/up");
            down1 = Content.Load<Texture2D>("sprites/down");
            right1 = Content.Load<Texture2D>("sprites/right2");
            left1 = Content.Load<Texture2D>("sprites/left2");
            penguin1_sprites.Add(idle1);
            penguin1_sprites.Add(up1);
            penguin1_sprites.Add(down1);
            penguin1_sprites.Add(right1);
            penguin1_sprites.Add(left1);


            List<object> penguin1_animation = new List<object>();

            for (int i = 0; i < standingAnimation1.Length; i++)
            {
                int width = idle1.Width / 8;
                int height = idle1.Height / 1;
                int row = i / 8;
                int column = i % 8;
                standingAnimation1[i] = new Rectangle(width * column, height * row, width, height);
            }
            for (int i = 0; i < upAnimation1.Length; i++)
            {
                int width = up1.Width / 6;
                int height = up1.Height / 1;
                int row = i / 6;
                int column = i % 6;
                upAnimation1[i] = new Rectangle(width * column, height * row, width, height);
            }
            for (int i = 0; i < downAnimation1.Length; i++)
            {
                int width = down1.Width / 6;
                int height = down1.Height / 1;
                int row = i / 6;
                int column = i % 6;
                downAnimation1[i] = new Rectangle(width * column, height * row, width, height);
            }
            for (int i = 0; i < leftAnimation1.Length; i++)
            {
                int width = left1.Width / 9;
                int height = left1.Height / 1;
                int row = i / 9;
                int column = i % 9;
                leftAnimation1[i] = new Rectangle(width * column, height * row, width, height);
            }
            for (int i = 0; i < rightAnimation1.Length; i++)
            {
                int width = right1.Width / 9;
                int height = right1.Height / 1;
                int row = i / 9;
                int column = i % 9;
                rightAnimation1[i] = new Rectangle(width * column, height * row, width, height);
            }

            penguin1_animation.Add(standingAnimation1);
            penguin1_animation.Add(upAnimation1);
            penguin1_animation.Add(downAnimation1);
            penguin1_animation.Add(leftAnimation1);
            penguin1_animation.Add(rightAnimation1);

            penguin1.Add(penguin1_sprites);
            penguin1.Add(penguin1_animation);

            //Penguin 2
            idle2 = Content.Load<Texture2D>("sprites/penguin_idle");
            up2 = Content.Load<Texture2D>("sprites/up");
            down2 = Content.Load<Texture2D>("sprites/down");
            right2 = Content.Load<Texture2D>("sprites/right2");
            left2 = Content.Load<Texture2D>("sprites/left2");

            for (int i = 0; i < standingAnimation2.Length; i++)
            {
                int width = idle2.Width / 8;
                int height = idle2.Height / 1;
                int row = i / 8;
                int column = i % 8;
                standingAnimation2[i] = new Rectangle(width * column, height * row, width, height);
            }
            for (int i = 0; i < upAnimation2.Length; i++)
            {
                int width = up2.Width / 6;
                int height = up2.Height / 1;
                int row = i / 6;
                int column = i % 6;
                upAnimation2[i] = new Rectangle(width * column, height * row, width, height);
            }
            for (int i = 0; i < downAnimation2.Length; i++)
            {
                int width = down2.Width / 6;
                int height = down2.Height / 1;
                int row = i / 6;
                int column = i % 6;
                downAnimation2[i] = new Rectangle(width * column, height * row, width, height);
            }
            for (int i = 0; i < leftAnimation2.Length; i++)
            {
                int width = left2.Width / 9;
                int height = left2.Height / 1;
                int row = i / 9;
                int column = i % 9;
                leftAnimation2[i] = new Rectangle(width * column, height * row, width, height);
            }
            for (int i = 0; i < rightAnimation2.Length; i++)
            {
                int width = right2.Width / 9;
                int height = right2.Height / 1;
                int row = i / 9;
                int column = i % 9;
                rightAnimation2[i] = new Rectangle(width * column, height * row, width, height);
            }

            //Penguin 3
            idle3 = Content.Load<Texture2D>("sprites/penguin_idle");
            up3 = Content.Load<Texture2D>("sprites/up");
            down3 = Content.Load<Texture2D>("sprites/down");
            right3 = Content.Load<Texture2D>("sprites/right2");
            left3 = Content.Load<Texture2D>("sprites/left2");

            for (int i = 0; i < standingAnimation3.Length; i++)
            {
                int width = idle3.Width / 8;
                int height = idle3.Height / 1;
                int row = i / 8;
                int column = i % 8;
                standingAnimation3[i] = new Rectangle(width * column, height * row, width, height);
            }
            for (int i = 0; i < upAnimation3.Length; i++)
            {
                int width = up3.Width / 6;
                int height = up3.Height / 1;
                int row = i / 6;
                int column = i % 6;
                upAnimation3[i] = new Rectangle(width * column, height * row, width, height);
            }
            for (int i = 0; i < downAnimation3.Length; i++)
            {
                int width = down3.Width / 6;
                int height = down3.Height / 1;
                int row = i / 6;
                int column = i % 6;
                downAnimation3[i] = new Rectangle(width * column, height * row, width, height);
            }
            for (int i = 0; i < leftAnimation3.Length; i++)
            {
                int width = left3.Width / 9;
                int height = left3.Height / 1;
                int row = i / 9;
                int column = i % 9;
                leftAnimation3[i] = new Rectangle(width * column, height * row, width, height);
            }
            for (int i = 0; i < rightAnimation3.Length; i++)
            {
                int width = right3.Width / 9;
                int height = right3.Height / 1;
                int row = i / 9;
                int column = i % 9;
                rightAnimation3[i] = new Rectangle(width * column, height * row, width, height);
            }

            //Penguin 4
            idle4 = Content.Load<Texture2D>("sprites/penguin_idle");
            up4 = Content.Load<Texture2D>("sprites/up");
            down4 = Content.Load<Texture2D>("sprites/down");
            right4 = Content.Load<Texture2D>("sprites/right2");
            left4 = Content.Load<Texture2D>("sprites/left2");

            for (int i = 0; i < standingAnimation4.Length; i++)
            {
                int width = idle4.Width / 8;
                int height = idle4.Height / 1;
                int row = i / 8;
                int column = i % 8;
                standingAnimation4[i] = new Rectangle(width * column, height * row, width, height);
            }
            for (int i = 0; i < upAnimation4.Length; i++)
            {
                int width = up4.Width / 6;
                int height = up4.Height / 1;
                int row = i / 6;
                int column = i % 6;
                upAnimation4[i] = new Rectangle(width * column, height * row, width, height);
            }
            for (int i = 0; i < downAnimation4.Length; i++)
            {
                int width = down4.Width / 6;
                int height = down4.Height / 1;
                int row = i / 6;
                int column = i % 6;
                downAnimation4[i] = new Rectangle(width * column, height * row, width, height);
            }
            for (int i = 0; i < leftAnimation4.Length; i++)
            {
                int width = left4.Width / 9;
                int height = left4.Height / 1;
                int row = i / 9;
                int column = i % 9;
                leftAnimation4[i] = new Rectangle(width * column, height * row, width, height);
            }
            for (int i = 0; i < rightAnimation4.Length; i++)
            {
                int width = right4.Width / 9;
                int height = right4.Height / 1;
                int row = i / 9;
                int column = i % 9;
                rightAnimation4[i] = new Rectangle(width * column, height * row, width, height);
            }

            //Penguin 5
            idle5 = Content.Load<Texture2D>("sprites/penguin_idle");
            up5 = Content.Load<Texture2D>("sprites/up");
            down5 = Content.Load<Texture2D>("sprites/down");
            right5 = Content.Load<Texture2D>("sprites/right2");
            left5 = Content.Load<Texture2D>("sprites/left2");

            for (int i = 0; i < standingAnimation5.Length; i++)
            {
                int width = idle5.Width / 8;
                int height = idle5.Height / 1;
                int row = i / 8;
                int column = i % 8;
                standingAnimation5[i] = new Rectangle(width * column, height * row, width, height);
            }
            for (int i = 0; i < upAnimation5.Length; i++)
            {
                int width = up5.Width / 6;
                int height = up5.Height / 1;
                int row = i / 6;
                int column = i % 6;
                upAnimation5[i] = new Rectangle(width * column, height * row, width, height);
            }
            for (int i = 0; i < downAnimation5.Length; i++)
            {
                int width = down5.Width / 6;
                int height = down5.Height / 1;
                int row = i / 6;
                int column = i % 6;
                downAnimation5[i] = new Rectangle(width * column, height * row, width, height);
            }
            for (int i = 0; i < leftAnimation5.Length; i++)
            {
                int width = left5.Width / 9;
                int height = left5.Height / 1;
                int row = i / 9;
                int column = i % 9;
                leftAnimation5[i] = new Rectangle(width * column, height * row, width, height);
            }
            for (int i = 0; i < rightAnimation5.Length; i++)
            {
                int width = right5.Width / 9;
                int height = right5.Height / 1;
                int row = i / 9;
                int column = i % 9;
                rightAnimation5[i] = new Rectangle(width * column, height * row, width, height);
            }

            // Weapon System
            stick = Content.Load<Texture2D>("Weapons/Stick01");
            stickLeft = Content.Load<Texture2D>("Weapons/Stick02");
            sword = Content.Load<Texture2D>("Weapons/Sword01");
            swordLeft = Content.Load<Texture2D>("Weapons/Sword02");
            shuriken = Content.Load<Texture2D>("Weapons/Shuriken01");
            shurikenLeft = Content.Load<Texture2D>("Weapons/Shuriken02");

            // Enemy Creation
            // Fox
            for (int i = 0; i < 14; i++)
            {
                foxIdleAnimation[i] = new Rectangle(i * 128, 0, 128, 176);
            }

            fox1 = new Fox();
            foxIdle = Content.Load<Texture2D>("FoxIdle");
            fox1.Initialize(foxIdle, new Vector2(1050, 1450), foxIdleAnimation, foxIdleFrame, new Rectangle(1070, 1500, 85, 65), healthbar, new Rectangle((int)fox1.position.X, (int)fox1.position.Y, fox1.health, 10));
            fox2 = new Fox();
            fox2.Initialize(foxIdle, new Vector2(1150, 2400), foxIdleAnimation, foxIdleFrame, new Rectangle(1170, 2450, 85, 65), healthbar, new Rectangle((int)fox2.position.X, (int)fox2.position.Y, fox2.health, 10));
            fox3 = new Fox();
            fox3.Initialize(foxIdle, new Vector2(1200, 2400), foxIdleAnimation, foxIdleFrame, new Rectangle(1220, 2450, 85, 65), healthbar, new Rectangle((int)fox3.position.X, (int)fox3.position.Y, fox3.health, 10));
            fox4 = new Fox();
            fox4.Initialize(foxIdle, new Vector2(2300, 2000), foxIdleAnimation, foxIdleFrame, new Rectangle(2320, 2050, 85, 65), healthbar, new Rectangle((int)fox4.position.X, (int)fox4.position.Y, fox4.health, 10));
            fox5 = new Fox();
            fox5.Initialize(foxIdle, new Vector2(2300, 2050), foxIdleAnimation, foxIdleFrame, new Rectangle(2320, 2100, 85, 65), healthbar, new Rectangle((int)fox5.position.X, (int)fox5.position.Y, fox5.health, 10));
            fox6 = new Fox();
            fox6.Initialize(foxIdle, new Vector2(2300, 2100), foxIdleAnimation, foxIdleFrame, new Rectangle(2320, 2150, 85, 65), healthbar, new Rectangle((int)fox6.position.X, (int)fox6.position.Y, fox6.health, 10));
            fox7 = new Fox();
            fox7.Initialize(foxIdle, new Vector2(1200, 3500), foxIdleAnimation, foxIdleFrame, new Rectangle(1220, 3550, 85, 65), healthbar, new Rectangle((int)fox7.position.X, (int)fox7.position.Y, fox7.health, 10));
            fox8 = new Fox();
            fox8.Initialize(foxIdle, new Vector2(1250, 3500), foxIdleAnimation, foxIdleFrame, new Rectangle(1270, 3550, 85, 65), healthbar, new Rectangle((int)fox8.position.X, (int)fox8.position.Y, fox8.health, 10));
            fox9 = new Fox();
            fox9.Initialize(foxIdle, new Vector2(4780, 1480), foxIdleAnimation, foxIdleFrame, new Rectangle(4800, 1530, 85, 65), healthbar, new Rectangle((int)fox9.position.X, (int)fox9.position.Y, fox9.health, 10));
            fox10 = new Fox();
            fox10.Initialize(foxIdle, new Vector2(5330, 1770), foxIdleAnimation, foxIdleFrame, new Rectangle(5350, 1820, 85, 65), healthbar, new Rectangle((int)fox10.position.X, (int)fox10.position.Y, fox10.health, 10));
            fox11 = new Fox();
            fox11.Initialize(foxIdle, new Vector2(6070, 1970), foxIdleAnimation, foxIdleFrame, new Rectangle(6090, 2020, 85, 65), healthbar, new Rectangle((int)fox11.position.X, (int)fox11.position.Y, fox11.health, 10));
            fox12 = new Fox();
            fox12.Initialize(foxIdle, new Vector2(3970, 2870), foxIdleAnimation, foxIdleFrame, new Rectangle(3990, 2920, 85, 65), healthbar, new Rectangle((int)fox12.position.X, (int)fox12.position.Y, fox12.health, 10));
            fox13 = new Fox();
            fox13.Initialize(foxIdle, new Vector2(3970, 2970), foxIdleAnimation, foxIdleFrame, new Rectangle(3990, 3020, 85, 65), healthbar, new Rectangle((int)fox13.position.X, (int)fox13.position.Y, fox13.health, 10));
            fox14 = new Fox();
            fox14.Initialize(foxIdle, new Vector2(3970, 3070), foxIdleAnimation, foxIdleFrame, new Rectangle(3990, 3120, 85, 65), healthbar, new Rectangle((int)fox14.position.X, (int)fox14.position.Y, fox14.health, 10));
            fox15 = new Fox();
            fox15.Initialize(foxIdle, new Vector2(4310, 3570), foxIdleAnimation, foxIdleFrame, new Rectangle(4330, 3620, 85, 65), healthbar, new Rectangle((int)fox15.position.X, (int)fox15.position.Y, fox15.health, 10));
            fox16 = new Fox();
            fox16.Initialize(foxIdle, new Vector2(4360, 3570), foxIdleAnimation, foxIdleFrame, new Rectangle(4380, 3620, 85, 65), healthbar, new Rectangle((int)fox16.position.X, (int)fox16.position.Y, fox16.health, 10));
            fox17 = new Fox();
            fox17.Initialize(foxIdle, new Vector2(4410, 3570), foxIdleAnimation, foxIdleFrame, new Rectangle(4430, 3620, 85, 65), healthbar, new Rectangle((int)fox17.position.X, (int)fox17.position.Y, fox17.health, 10));
            fox18 = new Fox();
            fox18.Initialize(foxIdle, new Vector2(4410, 3520), foxIdleAnimation, foxIdleFrame, new Rectangle(4430, 3570, 85, 65), healthbar, new Rectangle((int)fox18.position.X, (int)fox18.position.Y, fox18.health, 10));
            fox19 = new Fox();
            fox19.Initialize(foxIdle, new Vector2(4410, 3470), foxIdleAnimation, foxIdleFrame, new Rectangle(4430, 3520, 85, 65), healthbar, new Rectangle((int)fox19.position.X, (int)fox19.position.Y, fox19.health, 10));
            fox20 = new Fox();
            fox20.Initialize(foxIdle, new Vector2(3730, 3570), foxIdleAnimation, foxIdleFrame, new Rectangle(3750, 3620, 85, 65), healthbar, new Rectangle((int)fox20.position.X, (int)fox20.position.Y, fox20.health, 10));
            fox21 = new Fox();
            fox21.Initialize(foxIdle, new Vector2(5950, 3100), foxIdleAnimation, foxIdleFrame, new Rectangle(5970, 3150, 85, 65), healthbar, new Rectangle((int)fox21.position.X, (int)fox21.position.Y, fox21.health, 10));
            fox22 = new Fox();
            fox22.Initialize(foxIdle, new Vector2(5950, 3150), foxIdleAnimation, foxIdleFrame, new Rectangle(5970, 3200, 85, 65), healthbar, new Rectangle((int)fox22.position.X, (int)fox22.position.Y, fox22.health, 10));
            fox23 = new Fox();
            fox23.Initialize(foxIdle, new Vector2(5950, 3200), foxIdleAnimation, foxIdleFrame, new Rectangle(5970, 3250, 85, 65), healthbar, new Rectangle((int)fox23.position.X, (int)fox23.position.Y, fox23.health, 10));
            fox24 = new Fox();
            fox24.Initialize(foxIdle, new Vector2(5950, 3250), foxIdleAnimation, foxIdleFrame, new Rectangle(5970, 3300, 85, 65), healthbar, new Rectangle((int)fox24.position.X, (int)fox24.position.Y, fox24.health, 10));
            fox25 = new Fox();
            fox25.Initialize(foxIdle, new Vector2(5950, 3300), foxIdleAnimation, foxIdleFrame, new Rectangle(5970, 3350, 85, 65), healthbar, new Rectangle((int)fox25.position.X, (int)fox25.position.Y, fox25.health, 10));
            fox26 = new Fox();
            fox26.Initialize(foxIdle, new Vector2(5950, 3350), foxIdleAnimation, foxIdleFrame, new Rectangle(5970, 3400, 85, 65), healthbar, new Rectangle((int)fox26.position.X, (int)fox26.position.Y, fox26.health, 10));
            fox27 = new Fox();
            fox27.Initialize(foxIdle, new Vector2(5950, 3400), foxIdleAnimation, foxIdleFrame, new Rectangle(5970, 3450, 85, 65), healthbar, new Rectangle((int)fox27.position.X, (int)fox27.position.Y, fox27.health, 10));

            foxes = new List<Fox>()
            { fox1, fox2, fox3, fox4, fox5, fox6, fox7, fox8, fox9, fox10, fox11, fox12, fox13, fox14, fox15, fox16, fox17, fox18, fox19, fox20, fox21, fox22, fox23, fox24, fox25, fox26, fox27 };

            // Wolf
            for (int i = 0; i < 6; i++)
            {
                wolfIdleAnimation[i] = new Rectangle(i * 128, 0, 128, 64);
            }
            wolf1 = new Wolf();
            wolfIdle = Content.Load<Texture2D>("WolfIdle");
            wolf1.Initialize(wolfIdle, new Vector2(2000, 2000), wolfIdleAnimation, wolfIdleFrame, new Rectangle(2020, 2020, 80 , 45), healthbar, new Rectangle((int)wolf1.position.X, (int)wolf1.position.Y, wolf1.health, 10));
            wolf2 = new Wolf();
            wolf2.Initialize(wolfIdle, new Vector2(2100, 2100), wolfIdleAnimation, wolfIdleFrame, new Rectangle(2120, 2120, 80, 45), healthbar, new Rectangle((int)wolf2.position.X, (int)wolf2.position.Y, wolf2.health, 10));
            wolf3 = new Wolf();
            wolf3.Initialize(wolfIdle, new Vector2(2350, 3100), wolfIdleAnimation, wolfIdleFrame, new Rectangle(2370, 3120, 80, 45), healthbar, new Rectangle((int)wolf3.position.X, (int)wolf3.position.Y, wolf3.health, 10));
            wolf4 = new Wolf();
            wolf4.Initialize(wolfIdle, new Vector2(2400, 3100), wolfIdleAnimation, wolfIdleFrame, new Rectangle(2420, 3120, 80, 45), healthbar, new Rectangle((int)wolf4.position.X, (int)wolf4.position.Y, wolf4.health, 10));
            wolf5 = new Wolf();
            wolf5.Initialize(wolfIdle, new Vector2(2430, 1300), wolfIdleAnimation, wolfIdleFrame, new Rectangle(2450, 1320, 80, 45), healthbar, new Rectangle((int)wolf5.position.X, (int)wolf5.position.Y, wolf5.health, 10));
            wolf6 = new Wolf();
            wolf6.Initialize(wolfIdle, new Vector2(2430, 1350), wolfIdleAnimation, wolfIdleFrame, new Rectangle(2450, 1370, 80, 45), healthbar, new Rectangle((int)wolf6.position.X, (int)wolf6.position.Y, wolf6.health, 10));
            wolf7 = new Wolf();
            wolf7.Initialize(wolfIdle, new Vector2(3670, 2020), wolfIdleAnimation, wolfIdleFrame, new Rectangle(3690, 2040, 80, 45), healthbar, new Rectangle((int)wolf7.position.X, (int)wolf7.position.Y, wolf7.health, 10));
            wolf8 = new Wolf();
            wolf8.Initialize(wolfIdle, new Vector2(3670, 2120), wolfIdleAnimation, wolfIdleFrame, new Rectangle(3690, 2140, 80, 45), healthbar, new Rectangle((int)wolf8.position.X, (int)wolf8.position.Y, wolf8.health, 10));
            wolf9 = new Wolf();
            wolf9.Initialize(wolfIdle, new Vector2(4780, 1280), wolfIdleAnimation, wolfIdleFrame, new Rectangle(4800, 1300, 80, 45), healthbar, new Rectangle((int)wolf9.position.X, (int)wolf9.position.Y, wolf9.health, 10));
            wolf10 = new Wolf();
            wolf10.Initialize(wolfIdle, new Vector2(4780, 1380), wolfIdleAnimation, wolfIdleFrame, new Rectangle(4800, 1400, 80, 45), healthbar, new Rectangle((int)wolf10.position.X, (int)wolf10.position.Y, wolf10.health, 10));
            wolf11 = new Wolf();
            wolf11.Initialize(wolfIdle, new Vector2(5330, 1870), wolfIdleAnimation, wolfIdleFrame, new Rectangle(5350, 1890, 80, 45), healthbar, new Rectangle((int)wolf11.position.X, (int)wolf11.position.Y, wolf11.health, 10));
            wolf12 = new Wolf();
            wolf12.Initialize(wolfIdle, new Vector2(5330, 1970), wolfIdleAnimation, wolfIdleFrame, new Rectangle(5350, 1990, 80, 45), healthbar, new Rectangle((int)wolf12.position.X, (int)wolf12.position.Y, wolf12.health, 10));
            wolf13 = new Wolf();
            wolf13.Initialize(wolfIdle, new Vector2(6070, 1870), wolfIdleAnimation, wolfIdleFrame, new Rectangle(6090, 1890, 80, 45), healthbar, new Rectangle((int)wolf13.position.X, (int)wolf13.position.Y, wolf13.health, 10));
            wolf14 = new Wolf();
            wolf14.Initialize(wolfIdle, new Vector2(6070, 1770), wolfIdleAnimation, wolfIdleFrame, new Rectangle(6090, 1790, 80, 45), healthbar, new Rectangle((int)wolf14.position.X, (int)wolf14.position.Y, wolf14.health, 10));
            wolf15 = new Wolf();
            wolf15.Initialize(wolfIdle, new Vector2(3970, 2770), wolfIdleAnimation, wolfIdleFrame, new Rectangle(3990, 2790, 80, 45), healthbar, new Rectangle((int)wolf15.position.X, (int)wolf15.position.Y, wolf15.health, 10));
            wolf16 = new Wolf();
            wolf16.Initialize(wolfIdle, new Vector2(4450, 2310), wolfIdleAnimation, wolfIdleFrame, new Rectangle(4470, 2330, 80, 45), healthbar, new Rectangle((int)wolf16.position.X, (int)wolf16.position.Y, wolf16.health, 10));
            wolf17 = new Wolf();
            wolf17.Initialize(wolfIdle, new Vector2(4500, 2310), wolfIdleAnimation, wolfIdleFrame, new Rectangle(4520, 2330, 80, 45), healthbar, new Rectangle((int)wolf17.position.X, (int)wolf17.position.Y, wolf17.health, 10));
            wolf18 = new Wolf();
            wolf18.Initialize(wolfIdle, new Vector2(4550, 2310), wolfIdleAnimation, wolfIdleFrame, new Rectangle(4570, 2330, 80, 45), healthbar, new Rectangle((int)wolf18.position.X, (int)wolf18.position.Y, wolf18.health, 10));
            wolf19 = new Wolf();
            wolf19.Initialize(wolfIdle, new Vector2(4550, 2360), wolfIdleAnimation, wolfIdleFrame, new Rectangle(4570, 2380, 80, 45), healthbar, new Rectangle((int)wolf19.position.X, (int)wolf19.position.Y, wolf19.health, 10));
            wolf20 = new Wolf();
            wolf20.Initialize(wolfIdle, new Vector2(4550, 2400), wolfIdleAnimation, wolfIdleFrame, new Rectangle(4570, 2420, 80, 45), healthbar, new Rectangle((int)wolf20.position.X, (int)wolf20.position.Y, wolf20.health, 10));
            wolf21 = new Wolf();
            wolf21.Initialize(wolfIdle, new Vector2(3630, 3570), wolfIdleAnimation, wolfIdleFrame, new Rectangle(3650, 3590, 80, 45), healthbar, new Rectangle((int)wolf21.position.X, (int)wolf21.position.Y, wolf21.health, 10));
            wolf22 = new Wolf();
            wolf22.Initialize(wolfIdle, new Vector2(3680, 3570), wolfIdleAnimation, wolfIdleFrame, new Rectangle(3700, 3590, 80, 45), healthbar, new Rectangle((int)wolf22.position.X, (int)wolf22.position.Y, wolf22.health, 10));
            wolf23 = new Wolf();
            wolf23.Initialize(wolfIdle, new Vector2(5900, 3300), wolfIdleAnimation, wolfIdleFrame, new Rectangle(5920, 3500, 80, 45), healthbar, new Rectangle((int)wolf23.position.X, (int)wolf23.position.Y, wolf23.health, 10));

            wolves = new List<Wolf>()
            { wolf1, wolf2, wolf3, wolf4, wolf5, wolf6, wolf7, wolf8, wolf9, wolf10, wolf11, wolf12, wolf13, wolf14, wolf15, wolf16, wolf17, wolf18, wolf19, wolf20, wolf21, wolf22, wolf23 };

            // Bat
            for (int i = 0; i < 6; i++)
            {
                batIdleAnimation[i] = new Rectangle(i * 32, 0, 32, 32);
            }

            bat1 = new Bat();
            batIdle = Content.Load<Texture2D>("BatIdle");
            bat1.Initialize(batIdle, new Vector2(3000, 3000), batIdleAnimation, batIdleFrame, new Rectangle(3000, 3000, 30, 30), healthbar, new Rectangle((int)bat1.position.X, (int)bat1.position.Y, bat1.health, 10));
            bat2 = new Bat();
            bat2.Initialize(batIdle, new Vector2(3050, 3050), batIdleAnimation, batIdleFrame, new Rectangle(3050, 3050, 30, 30), healthbar, new Rectangle((int)bat2.position.X, (int)bat2.position.Y, bat2.health, 10));
            bat3 = new Bat();
            bat3.Initialize(batIdle, new Vector2(2350, 3150), batIdleAnimation, batIdleFrame, new Rectangle(2350, 3150, 30, 30), healthbar, new Rectangle((int)bat3.position.X, (int)bat3.position.Y, bat3.health, 10));
            bat4 = new Bat();
            bat4.Initialize(batIdle, new Vector2(2350, 3200), batIdleAnimation, batIdleFrame, new Rectangle(2350, 3200, 30, 30), healthbar, new Rectangle((int)bat4.position.X, (int)bat4.position.Y, bat4.health, 10));
            bat5 = new Bat();
            bat5.Initialize(batIdle, new Vector2(2530, 1300), batIdleAnimation, batIdleFrame, new Rectangle(2530, 1300, 30, 30), healthbar, new Rectangle((int)bat5.position.X, (int)bat5.position.Y, bat5.health, 10));
            bat6 = new Bat();
            bat6.Initialize(batIdle, new Vector2(2530, 1350), batIdleAnimation, batIdleFrame, new Rectangle(2530, 1350, 30, 30), healthbar, new Rectangle((int)bat6.position.X, (int)bat6.position.Y, bat6.health, 10));
            bat7 = new Bat();
            bat7.Initialize(batIdle, new Vector2(3770, 2020), batIdleAnimation, batIdleFrame, new Rectangle(3770, 2020, 30, 30), healthbar, new Rectangle((int)bat7.position.X, (int)bat7.position.Y, bat7.health, 10));
            bat8 = new Bat();
            bat8.Initialize(batIdle, new Vector2(3870, 2020), batIdleAnimation, batIdleFrame, new Rectangle(3870, 2020, 30, 30), healthbar, new Rectangle((int)bat8.position.X, (int)bat8.position.Y, bat8.health, 10));
            bat9 = new Bat();
            bat9.Initialize(batIdle, new Vector2(4880, 1280), batIdleAnimation, batIdleFrame, new Rectangle(4880, 1280, 30, 30), healthbar, new Rectangle((int)bat9.position.X, (int)bat9.position.Y, bat9.health, 10));
            bat10 = new Bat();
            bat10.Initialize(batIdle, new Vector2(4980, 1280), batIdleAnimation, batIdleFrame, new Rectangle(4980, 1280, 30, 30), healthbar, new Rectangle((int)bat10.position.X, (int)bat10.position.Y, bat10.health, 10));
            bat11 = new Bat();
            bat11.Initialize(batIdle, new Vector2(5230, 1770), batIdleAnimation, batIdleFrame, new Rectangle(5230, 1770, 30, 30), healthbar, new Rectangle((int)bat11.position.X, (int)bat11.position.Y, bat11.health, 10));
            bat12 = new Bat();
            bat12.Initialize(batIdle, new Vector2(5130, 1770), batIdleAnimation, batIdleFrame, new Rectangle(5130, 1770, 30, 30), healthbar, new Rectangle((int)bat12.position.X, (int)bat12.position.Y, bat12.health, 10));
            bat13 = new Bat();
            bat13.Initialize(batIdle, new Vector2(6170, 1970), batIdleAnimation, batIdleFrame, new Rectangle(6170, 1970, 30, 30), healthbar, new Rectangle((int)bat13.position.X, (int)bat13.position.Y, bat13.health, 10));
            bat14 = new Bat();
            bat14.Initialize(batIdle, new Vector2(6270, 1970), batIdleAnimation, batIdleFrame, new Rectangle(6270, 1970, 30, 30), healthbar, new Rectangle((int)bat14.position.X, (int)bat14.position.Y, bat14.health, 10));
            bat15 = new Bat();
            bat15.Initialize(batIdle, new Vector2(3870, 2870), batIdleAnimation, batIdleFrame, new Rectangle(3870, 2870, 30, 30), healthbar, new Rectangle((int)bat15.position.X, (int)bat15.position.Y, bat15.health, 10));
            bat16 = new Bat();
            bat16.Initialize(batIdle, new Vector2(4950, 3160), batIdleAnimation, batIdleFrame, new Rectangle(4950, 3160, 30, 30), healthbar, new Rectangle((int)bat16.position.X, (int)bat16.position.Y, bat16.health, 10));
            bat17 = new Bat();
            bat17.Initialize(batIdle, new Vector2(5000, 3160), batIdleAnimation, batIdleFrame, new Rectangle(5000, 3160, 30, 30), healthbar, new Rectangle((int)bat17.position.X, (int)bat17.position.Y, bat17.health, 10));
            bat18 = new Bat();
            bat18.Initialize(batIdle, new Vector2(5050, 3160), batIdleAnimation, batIdleFrame, new Rectangle(5050, 3160, 30, 30), healthbar, new Rectangle((int)bat18.position.X, (int)bat18.position.Y, bat18.health, 10));
            bat19 = new Bat();
            bat19.Initialize(batIdle, new Vector2(5050, 3110), batIdleAnimation, batIdleFrame, new Rectangle(5050, 3110, 30, 30), healthbar, new Rectangle((int)bat19.position.X, (int)bat19.position.Y, bat19.health, 10));
            bat20 = new Bat();
            bat20.Initialize(batIdle, new Vector2(5050, 3070), batIdleAnimation, batIdleFrame, new Rectangle(5050, 3070, 30, 30), healthbar, new Rectangle((int)bat20.position.X, (int)bat20.position.Y, bat20.health, 10));
            bat21 = new Bat();
            bat21.Initialize(batIdle, new Vector2(3730, 3470), batIdleAnimation, batIdleFrame, new Rectangle(3730, 3470, 30, 30), healthbar, new Rectangle((int)bat21.position.X, (int)bat21.position.Y, bat21.health, 10));
            bat22 = new Bat();
            bat22.Initialize(batIdle, new Vector2(3730, 3520), batIdleAnimation, batIdleFrame, new Rectangle(3730, 3520, 30, 30), healthbar, new Rectangle((int)bat22.position.X, (int)bat22.position.Y, bat22.health, 10));
            bat23 = new Bat();
            bat23.Initialize(batIdle, new Vector2(6000, 3300), batIdleAnimation, batIdleFrame, new Rectangle(6000, 3300, 30, 30), healthbar, new Rectangle((int)bat23.position.X, (int)bat23.position.Y, bat23.health, 10));

            bats = new List<Bat>()
            { bat1, bat2, bat3, bat4, bat5, bat6, bat7, bat8, bat9, bat10, bat11, bat12, bat13, bat14, bat15, bat16, bat17, bat18, bat19, bat20, bat21, bat22, bat23 };

            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White });
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            foreach (var component in buttons)
                component.Update(gameTime);


            if (startGame && !dead && !win)
            {
                //end game  6170-6200 2950-3070
                if(location.Y > 2950 && location.Y < 3070-54)
                {
                    if (location.X > 6130)
                    {
                        win = true;
                    }
                }                

                if (Keyboard.GetState().IsKeyDown(Keys.D1))
                    currentcard = 0;
                if (Keyboard.GetState().IsKeyDown(Keys.D2) && card1)
                    currentcard = 1;
                if (Keyboard.GetState().IsKeyDown(Keys.D3) && card2)
                    currentcard = 2;

                foreach (var fox in foxes) { fox.Update(gameTime); }
                foreach (var wolf in wolves) { wolf.Update(gameTime); }
                foreach (var bat in bats) { bat.Update(gameTime); }

                //collect card
                if (!card1)
                {
                    if(location.X + 64 > 1300 && location.X-64 < 1300 + 64)
                    {
                        if(location.Y + 64 > 2900 && location.Y - 64 < 2900 + 64)
                        {
                            card1 = true;
                        }
                    }
                }
                if (!card2)
                {
                    if (location.X + 64 > 5900 && location.X - 64 < 5900 + 64)
                    {
                        if (location.Y + 64 > 2400 && location.Y - 64 < 2400 + 64)
                        {
                            card2 = true;
                        }
                    }
                }

                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    _movementKey = Keys.W;
                    leftFacing = false;
                    rightFacing = false;
                    upFacing = true;
                    downFacing = false;

                    //collision 
                    bool blocked = false;
                    foreach (var obj in Tree.objects)
                    {
                        var tree = new Rectangle((int)obj.x, (int)obj.y, (int)obj.width, (int)obj.height);

                        if (location.X + 64 >= tree.X && location.X  <= tree.X + tree.Width)
                        {
                            if(location.Y > tree.Y && location.Y <= tree.Y + tree.Height +3)
                            {
                                blocked = true;
                            }
                        }


                    }
                    if (!blocked)
                    {
                        location.Y -= speed;
                        if (currentcard == 0)
                        {
                            if (timer > threshold)
                            {
                                if (currentAnimationIndex >= upAnimation.Length - 1)
                                {
                                    currentAnimationIndex = 0;
                                }
                                else
                                {
                                    currentAnimationIndex += 1;
                                }
                                // Reset the timer.
                                timer = 0;
                            }
                            // If the timer has not reached the threshold, then add the milliseconds that have past since the last Update() to the timer.
                            else
                            {
                                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                            }
                        }
                        if (currentcard == 1)
                        {
                            if (timer > threshold)
                            {
                                if (currentAnimationIndex >= upAnimation1.Length - 1)
                                {
                                    currentAnimationIndex = 0;
                                }
                                else
                                {
                                    currentAnimationIndex += 1;
                                }
                                // Reset the timer.
                                timer = 0;
                            }
                            // If the timer has not reached the threshold, then add the milliseconds that have past since the last Update() to the timer.
                            else
                            {
                                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                            }
                        }
                        if (currentcard == 2)
                        {
                            if (timer > threshold)
                            {
                                if (currentAnimationIndex >= upAnimation2.Length - 1)
                                {
                                    currentAnimationIndex = 0;
                                }
                                else
                                {
                                    currentAnimationIndex += 1;
                                }
                                // Reset the timer.
                                timer = 0;
                            }
                            // If the timer has not reached the threshold, then add the milliseconds that have past since the last Update() to the timer.
                            else
                            {
                                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                            }
                        }
                    }
                }

                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    _movementKey = Keys.S;
                    leftFacing = false;
                    rightFacing = false;
                    upFacing = false;
                    downFacing = true;

                    //collision 
                    bool blocked = false;
                    foreach (var obj in Tree.objects)
                    {
                        var tree = new Rectangle((int)obj.x, (int)obj.y, (int)obj.width, (int)obj.height);

                        if (location.X + 64 >= tree.X && location.X <= tree.X + tree.Width)
                        {
                            if (location.Y  + 64 < tree.Y && location.Y + 64 >= tree.Y - 5)
                            {
                                blocked = true;
                            }
                        }


                    }
                    if (!blocked)
                    {
                        location.Y += speed;
                        if (currentcard == 0)
                        {
                            if (timer > threshold)
                            {
                                if (currentAnimationIndex >= downAnimation.Length - 1)
                                {
                                    currentAnimationIndex = 0;
                                }
                                else
                                {
                                    currentAnimationIndex += 1;
                                }
                                // Reset the timer.
                                timer = 0;
                            }
                            // If the timer has not reached the threshold, then add the milliseconds that have past since the last Update() to the timer.
                            else
                            {
                                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                            }
                        }
                        if (currentcard == 1)
                        {
                            if (timer > threshold)
                            {
                                if (currentAnimationIndex >= downAnimation1.Length - 1)
                                {
                                    currentAnimationIndex = 0;
                                }
                                else
                                {
                                    currentAnimationIndex += 1;
                                }
                                // Reset the timer.
                                timer = 0;
                            }
                            // If the timer has not reached the threshold, then add the milliseconds that have past since the last Update() to the timer.
                            else
                            {
                                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                            }
                        }
                        if (currentcard == 2)
                        {
                            if (timer > threshold)
                            {
                                if (currentAnimationIndex >= downAnimation2.Length - 1)
                                {
                                    currentAnimationIndex = 0;
                                }
                                else
                                {
                                    currentAnimationIndex += 1;
                                }
                                // Reset the timer.
                                timer = 0;
                            }
                            // If the timer has not reached the threshold, then add the milliseconds that have past since the last Update() to the timer.
                            else
                            {
                                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                            }
                        }
                    }
                }

                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    _movementKey = Keys.A;
                    leftFacing = true;
                    rightFacing = false;
                    upFacing = false;
                    downFacing = false;

                    //collision 
                    bool blocked = false;
                    foreach (var obj in Tree.objects)
                    {
                        var tree = new Rectangle((int)obj.x, (int)obj.y, (int)obj.width, (int)obj.height);


                        if (location.Y + 64 >= tree.Y && location.Y <= tree.Y + tree.Height)
                        {
                            if (location.X > tree.X && location.X <= tree.X + tree.Width + 5)
                            {
                                blocked = true;
                            }
                        }

                    }
                    if (!blocked)
                    {
                        location.X -= speed;
                        if (currentcard == 0)
                        {
                            if (timer > threshold)
                            {
                                if (currentAnimationIndex >= leftAnimation.Length - 1)
                                {
                                    currentAnimationIndex = 0;
                                }
                                else
                                {
                                    currentAnimationIndex += 1;
                                }
                                // Reset the timer.
                                timer = 0;
                            }
                            // If the timer has not reached the threshold, then add the milliseconds that have past since the last Update() to the timer.
                            else
                            {
                                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                            }
                        }
                        if (currentcard == 1)
                        {
                            if (timer > threshold)
                            {
                                if (currentAnimationIndex >= leftAnimation1.Length - 1)
                                {
                                    currentAnimationIndex = 0;
                                }
                                else
                                {
                                    currentAnimationIndex += 1;
                                }
                                // Reset the timer.
                                timer = 0;
                            }
                            // If the timer has not reached the threshold, then add the milliseconds that have past since the last Update() to the timer.
                            else
                            {
                                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                            }
                        }
                        if (currentcard == 2)
                        {
                            if (timer > threshold)
                            {
                                if (currentAnimationIndex >= leftAnimation2.Length - 1)
                                {
                                    currentAnimationIndex = 0;
                                }
                                else
                                {
                                    currentAnimationIndex += 1;
                                }
                                // Reset the timer.
                                timer = 0;
                            }
                            // If the timer has not reached the threshold, then add the milliseconds that have past since the last Update() to the timer.
                            else
                            {
                                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                            }
                        }
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    _movementKey = Keys.D;
                    leftFacing = false;
                    rightFacing = true;
                    upFacing = false;
                    downFacing = false;

                    //collision 
                    bool blocked = false;
                    foreach (var obj in Tree.objects)
                    {
                        var tree = new Rectangle((int)obj.x, (int)obj.y, (int)obj.width, (int)obj.height);


                        if (location.Y + 64 >= tree.Y && location.Y <= tree.Y + tree.Height)
                        {
                            if (location.X + 64 < tree.X && location.X + 64 >= tree.X - 5)
                            {
                                blocked = true;
                            }
                        }

                    }
                    if (!blocked)
                    {
                        location.X += speed;
                        if (currentcard == 0)
                        {
                            if (timer > threshold)
                            {
                                if (currentAnimationIndex >= rightAnimation.Length - 1)
                                {
                                    currentAnimationIndex = 0;
                                }
                                else
                                {
                                    currentAnimationIndex += 1;
                                }
                                // Reset the timer.
                                timer = 0;
                            }
                            // If the timer has not reached the threshold, then add the milliseconds that have past since the last Update() to the timer.
                            else
                            {
                                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                            }
                            //original direction.
                        }
                        if (currentcard == 1)
                        {
                            if (timer > threshold)
                            {
                                if (currentAnimationIndex >= rightAnimation1.Length - 1)
                                {
                                    currentAnimationIndex = 0;
                                }
                                else
                                {
                                    currentAnimationIndex += 1;
                                }
                                // Reset the timer.
                                timer = 0;
                            }
                            // If the timer has not reached the threshold, then add the milliseconds that have past since the last Update() to the timer.
                            else
                            {
                                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                            }
                            //original direction.
                        }
                        if (currentcard == 2)
                        {
                            if (timer > threshold)
                            {
                                if (currentAnimationIndex >= rightAnimation2.Length - 1)
                                {
                                    currentAnimationIndex = 0;
                                }
                                else
                                {
                                    currentAnimationIndex += 1;
                                }
                                // Reset the timer.
                                timer = 0;
                            }
                            // If the timer has not reached the threshold, then add the milliseconds that have past since the last Update() to the timer.
                            else
                            {
                                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                            }
                        }
                    }
                }

                // Collision
                playerCollider = new Rectangle((int)location.X + 10, (int)location.Y + 15, 45, 45);

                if (currentcard == 0)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        stickAttack = true;
                        if (leftFacing)
                            weaponCollider = new Rectangle((int)location.X - 40, (int)location.Y, 60, 60);
                        if (rightFacing)
                            weaponCollider = new Rectangle((int)location.X + 45, (int)location.Y, 60, 60);
                        if (upFacing)
                            weaponCollider = new Rectangle((int)location.X + 30, (int)location.Y - 15, 60, 60);
                        if (downFacing)
                            weaponCollider = new Rectangle((int)location.X + 30, (int)location.Y + 15, 60, 60);
                    }
                    else
                    {
                        stickAttack = false;
                        if (leftFacing)
                            weaponCollider = new Rectangle((int)location.X - 25, (int)location.Y, 60, 60);
                        else
                            weaponCollider = new Rectangle((int)location.X + 30, (int)location.Y, 60, 60);
                    }
                }
                if (currentcard == 1)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        swordAttack = true;
                        if (leftFacing)
                            weaponCollider = new Rectangle((int)location.X - 60, (int)location.Y, 60, 60);
                        if (rightFacing)
                            weaponCollider = new Rectangle((int)location.X + 65, (int)location.Y, 60, 60);
                        if (upFacing)
                            weaponCollider = new Rectangle((int)location.X + 30, (int)location.Y - 35, 60, 60);
                        if (downFacing)
                            weaponCollider = new Rectangle((int)location.X + 30, (int)location.Y + 35, 60, 60);
                    }
                    else
                    {
                        swordAttack = false;
                        if (leftFacing)
                            weaponCollider = new Rectangle((int)location.X - 25, (int)location.Y, 60, 60);
                        else
                            weaponCollider = new Rectangle((int)location.X + 30, (int)location.Y, 60, 60);
                    }
                }
                if (currentcard == 2)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        shurikenAttack = true;
                        if (leftFacing)
                            weaponCollider = new Rectangle((int)location.X - 200, (int)location.Y, 60, 60);
                        if (rightFacing)
                            weaponCollider = new Rectangle((int)location.X + 205, (int)location.Y, 60, 60);
                        if (upFacing)
                            weaponCollider = new Rectangle((int)location.X + 30, (int)location.Y - 175, 60, 60);
                        if (downFacing)
                            weaponCollider = new Rectangle((int)location.X + 30, (int)location.Y + 175, 60, 60);
                    }
                    else
                    {
                        shurikenAttack = false;
                        if (leftFacing)
                            weaponCollider = new Rectangle((int)location.X - 25, (int)location.Y, 60, 60);
                        else
                            weaponCollider = new Rectangle((int)location.X + 30, (int)location.Y, 60, 60);
                    }
                }

                for (var i = 0; i < foxes.Count; i++)
                {
                    foxes[i].healthRec = new Rectangle((int)foxes[i].position.X, (int)foxes[i].position.Y, foxes[i].health, 10);
                    if (Math.Abs(location.X - foxes[i].position.X) <= 100 || Math.Abs(location.Y - foxes[i].position.Y) <= 100)
                    {
                        if (foxes[i].position.X > location.X)
                        {
                            foxes[i].position.X -= 2;
                            foxes[i].collider.X -= 2;
                        }
                        if (foxes[i].position.X < location.X)
                        {
                            foxes[i].position.X += 2;
                            foxes[i].collider.X += 2;
                        }
                        if (foxes[i].position.Y > location.Y)
                        {
                            foxes[i].position.Y -= 2;
                            foxes[i].collider.Y -= 2;
                        }
                        if (foxes[i].position.Y < location.Y)
                        {
                            foxes[i].position.Y += 2;
                            foxes[i].collider.Y += 2;
                        }
                    }
                    if (playerCollider.Intersects(foxes[i].collider))
                        health -= foxes[i].damage;
                    if (weaponCollider.Intersects(foxes[i].collider) && Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        if (currentcard == 1)
                            foxes[i].health -= 2;
                        else
                            foxes[i].health--;
                    }
                    for (var j = 0; j < foxes.Count; j++)
                    {
                        if (foxes[i] != foxes[j])
                        {
                            while (foxes[i].collider.Intersects(foxes[j].collider))
                            {
                                if (foxes[i].position.X >= foxes[j].position.X)
                                {
                                    foxes[i].position.X++;
                                    foxes[i].collider.X++;
                                }
                                if (foxes[i].position.X <= foxes[j].position.X)
                                {
                                    foxes[i].position.X--;
                                    foxes[i].collider.X--;
                                }
                                if (foxes[i].position.Y >= foxes[j].position.Y)
                                {
                                    foxes[i].position.Y++;
                                    foxes[i].collider.Y++;
                                }
                                if (foxes[i].position.Y <= foxes[j].position.Y)
                                {
                                    foxes[i].position.Y--;
                                    foxes[i].collider.Y--;
                                }
                            }
                        }
                    }
                }
                if (fox1.health <= 0)
                    foxes.Remove(fox1);
                if (fox2.health <= 0)
                    foxes.Remove(fox2);
                if (fox3.health <= 0)
                    foxes.Remove(fox3);
                if (fox4.health <= 0)
                    foxes.Remove(fox4);
                if (fox5.health <= 0)
                    foxes.Remove(fox5);
                if (fox6.health <= 0)
                    foxes.Remove(fox6);
                if (fox7.health <= 0)
                    foxes.Remove(fox7);
                if (fox8.health <= 0)
                    foxes.Remove(fox8);
                if (fox9.health <= 0)
                    foxes.Remove(fox9);
                if (fox10.health <= 0)
                    foxes.Remove(fox10);
                if (fox11.health <= 0)
                    foxes.Remove(fox11);
                if (fox12.health <= 0)
                    foxes.Remove(fox12);
                if (fox13.health <= 0)
                    foxes.Remove(fox13);
                if (fox14.health <= 0)
                    foxes.Remove(fox14);
                if (fox15.health <= 0)
                    foxes.Remove(fox15);
                if (fox16.health <= 0)
                    foxes.Remove(fox16);
                if (fox17.health <= 0)
                    foxes.Remove(fox17);
                if (fox18.health <= 0)
                    foxes.Remove(fox18);
                if (fox19.health <= 0)
                    foxes.Remove(fox19);
                if (fox20.health <= 0)
                    foxes.Remove(fox20);
                if (fox21.health <= 0)
                    foxes.Remove(fox21);
                if (fox22.health <= 0)
                    foxes.Remove(fox22);
                if (fox23.health <= 0)
                    foxes.Remove(fox23);
                if (fox24.health <= 0)
                    foxes.Remove(fox24);
                if (fox25.health <= 0)
                    foxes.Remove(fox25);
                if (fox26.health <= 0)
                    foxes.Remove(fox26);
                if (fox27.health <= 0)
                    foxes.Remove(fox27);
                
                for (var i = 0; i < wolves.Count; i++)
                {
                    wolves[i].healthRec = new Rectangle((int)wolves[i].position.X, (int)wolves[i].position.Y, wolves[i].health, 10);
                    if (Math.Abs(location.X - wolves[i].position.X) <= 100 || Math.Abs(location.Y - wolves[i].position.Y) <= 100)
                    {
                        if (wolves[i].position.X > location.X)
                        {
                            wolves[i].position.X -= 2;
                            wolves[i].collider.X -= 2;
                        }
                        if (wolves[i].position.X < location.X)
                        {
                            wolves[i].position.X += 2;
                            wolves[i].collider.X += 2;
                        }
                        if (wolves[i].position.Y > location.Y)
                        {
                            wolves[i].position.Y -= 2;
                            wolves[i].collider.Y -= 2;
                        }
                        if (wolves[i].position.Y < location.Y)
                        {
                            wolves[i].position.Y += 2;
                            wolves[i].collider.Y += 2;
                        }
                    }
                    if (playerCollider.Intersects(wolves[i].collider))
                        health -= wolves[i].damage;
                    if (weaponCollider.Intersects(wolves[i].collider) && Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        if (currentcard == 1)
                            wolves[i].health -= 2;
                        else
                            wolves[i].health--;
                    }
                    for (var j = 0; j < wolves.Count; j++)
                    {
                        if (wolves[i] != wolves[j])
                        {
                            while (wolves[i].collider.Intersects(wolves[j].collider))
                            {
                                if (wolves[i].position.X >= wolves[j].position.X)
                                {
                                    wolves[i].position.X++;
                                    wolves[i].collider.X++;
                                }
                                if (wolves[i].position.X <= wolves[j].position.X)
                                {
                                    wolves[i].position.X--;
                                    wolves[i].collider.X--;
                                }
                                if (wolves[i].position.Y >= wolves[j].position.Y)
                                {
                                    wolves[i].position.Y++;
                                    wolves[i].collider.Y++;
                                }
                                if (wolves[i].position.Y <= wolves[j].position.Y)
                                {
                                    wolves[i].position.Y--;
                                    wolves[i].collider.Y--;
                                }
                            }
                        }
                    }
                }
                if (wolf1.health <= 0)
                    wolves.Remove(wolf1);
                if (wolf2.health <= 0)
                    wolves.Remove(wolf2);
                if (wolf3.health <= 0)
                    wolves.Remove(wolf3);
                if (wolf4.health <= 0)
                    wolves.Remove(wolf4);
                if (wolf5.health <= 0)
                    wolves.Remove(wolf5);
                if (wolf6.health <= 0)
                    wolves.Remove(wolf6);
                if (wolf7.health <= 0)
                    wolves.Remove(wolf7);
                if (wolf8.health <= 0)
                    wolves.Remove(wolf8);
                if (wolf9.health <= 0)
                    wolves.Remove(wolf9);
                if (wolf10.health <= 0)
                    wolves.Remove(wolf10);
                if (wolf11.health <= 0)
                    wolves.Remove(wolf11);
                if (wolf12.health <= 0)
                    wolves.Remove(wolf12);
                if (wolf13.health <= 0)
                    wolves.Remove(wolf13);
                if (wolf14.health <= 0)
                    wolves.Remove(wolf14);
                if (wolf15.health <= 0)
                    wolves.Remove(wolf15);
                if (wolf16.health <= 0)
                    wolves.Remove(wolf16);
                if (wolf17.health <= 0)
                    wolves.Remove(wolf17);
                if (wolf18.health <= 0)
                    wolves.Remove(wolf18);
                if (wolf19.health <= 0)
                    wolves.Remove(wolf19);
                if (wolf20.health <= 0)
                    wolves.Remove(wolf20);
                if (wolf21.health <= 0)
                    wolves.Remove(wolf21);
                if (wolf22.health <= 0)
                    wolves.Remove(wolf22);
                if (wolf23.health <= 0)
                    wolves.Remove(wolf23);
                for (var i = 0; i < bats.Count; i++)
                {
                    bats[i].healthRec = new Rectangle((int)bats[i].position.X, (int)bats[i].position.Y, bats[i].health, 10);
                    if (Math.Abs(location.X - bats[i].position.X) <= 100 || Math.Abs(location.Y - bats[i].position.Y) <= 100)
                    {
                        if (bats[i].position.X > location.X)
                        {
                            bats[i].position.X -= 3;
                            bats[i].collider.X -= 3;
                        }
                        if (bats[i].position.X < location.X)
                        {
                            bats[i].position.X += 3;
                            bats[i].collider.X += 3;
                        }
                        if (bats[i].position.Y > location.Y)
                        {
                            bats[i].position.Y -= 3;
                            bats[i].collider.Y -= 3;
                        }
                        if (bats[i].position.Y < location.Y)
                        {
                            bats[i].position.Y += 3;
                            bats[i].collider.Y += 3;
                        }
                    }
                    if (playerCollider.Intersects(bats[i].collider))
                        health -= bats[i].damage;
                    if (weaponCollider.Intersects(bats[i].collider) && Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        if (currentcard == 1)
                            bats[i].health -= 2;
                        else
                            bats[i].health--;
                    }
                    for (var j = 0; j < bats.Count; j++)
                    {
                        if (bats[i] != bats[j])
                        {
                            while (bats[i].collider.Intersects(bats[j].collider))
                            {
                                if (bats[i].position.X >= bats[j].position.X)
                                {
                                    bats[i].position.X++;
                                    bats[i].collider.X++;
                                }
                                if (bats[i].position.X <= bats[j].position.X)
                                {
                                    bats[i].position.X--;
                                    bats[i].collider.X--;
                                }
                                if (bats[i].position.Y >= bats[j].position.Y)
                                {
                                    bats[i].position.Y++;
                                    bats[i].collider.Y++;
                                }
                                if (bats[i].position.Y <= bats[j].position.Y)
                                {
                                    bats[i].position.Y--;
                                    bats[i].collider.Y--;
                                }
                            }
                        }
                    }
                }
                if (bat1.health <= 0)
                    bats.Remove(bat1);
                if (bat2.health <= 0)
                    bats.Remove(bat2);
                if (bat3.health <= 0)
                    bats.Remove(bat3);
                if (bat4.health <= 0)
                    bats.Remove(bat4);
                if (bat5.health <= 0)
                    bats.Remove(bat5);
                if (bat6.health <= 0)
                    bats.Remove(bat6);
                if (bat7.health <= 0)
                    bats.Remove(bat7);
                if (bat8.health <= 0)
                    bats.Remove(bat8);
                if (bat9.health <= 0)
                    bats.Remove(bat9);
                if (bat10.health <= 0)
                    bats.Remove(bat10);
                if (bat11.health <= 0)
                    bats.Remove(bat11);
                if (bat12.health <= 0)
                    bats.Remove(bat12);
                if (bat13.health <= 0)
                    bats.Remove(bat13);
                if (bat14.health <= 0)
                    bats.Remove(bat14);
                if (bat15.health <= 0)
                    bats.Remove(bat15);
                if (bat16.health <= 0)
                    bats.Remove(bat16);
                if (bat17.health <= 0)
                    bats.Remove(bat17);
                if (bat18.health <= 0)
                    bats.Remove(bat18);
                if (bat19.health <= 0)
                    bats.Remove(bat19);
                if (bat20.health <= 0)
                    bats.Remove(bat20);
                if (bat21.health <= 0)
                    bats.Remove(bat21);
                if (bat22.health <= 0)
                    bats.Remove(bat22);
                if (bat23.health <= 0)
                    bats.Remove(bat23);
                healthRec = new Rectangle((int)location.X - 600, (int)location.Y - 300, health, 20);

                if (health <= 0)
                {
                    dead = true;
                }

                camera = Matrix.CreateTranslation(-location.X, -location.Y, 0) * Matrix.CreateTranslation(640, 360, 0);
            }

            if ((Keyboard.GetState().IsKeyDown(Keys.L)))
            {
                Debug.WriteLine("Location: " + location);
            }
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            if (startGame) { _spriteBatch.Begin(transformMatrix: camera); }
            else { _spriteBatch.Begin(); }
            
            //tiled
            var tileLayers = map.Layers.Where(x => x.type == TiledLayerType.TileLayer);
            foreach (var layer in tileLayers)
            {
                for (var y = 0; y < layer.height; y++)
                {
                    for (var x = 0; x < layer.width; x++)
                    {
                        // Assuming the default render order is used which is from right to bottom
                        var index = (y * layer.width) + x;
                        var gid = layer.data[index]; // The tileset tile index
                        var tileX = x * map.TileWidth;
                        var tileY = y * map.TileHeight;

                        // Gid 0 is used to tell there is no tile set
                        if (gid == 0)
                        {
                            continue;
                        }

                        // Helper method to fetch the right TieldMapTileset instance
                        // This is a connection object Tiled uses for linking the correct tileset to the 
                        // gid value using the firstgid property
                        var mapTileset = map.GetTiledMapTileset(gid);

                        // Retrieve the actual tileset based on the firstgid property of the connection object 
                        // we retrieved just now
                        var tileset = tilesets[mapTileset.firstgid];

                        // Use the connection object as well as the tileset to figure out the source rectangle
                        var rect = map.GetSourceRect(mapTileset, tileset, gid);

                        // Create destination and source rectangles
                        var source = new Rectangle(rect.x, rect.y, rect.width, rect.height);
                        var destination = new Rectangle(tileX, tileY, map.TileWidth, map.TileHeight);

                        // You can use the helper methods to get information to handle flips and rotations
                        Trans tileTrans = Trans.None;
                        if (map.IsTileFlippedHorizontal(layer, x, y)) tileTrans |= Trans.Flip_H;
                        if (map.IsTileFlippedVertical(layer, x, y)) tileTrans |= Trans.Flip_V;
                        if (map.IsTileFlippedDiagonal(layer, x, y)) tileTrans |= Trans.Flip_D;

                        SpriteEffects effects = SpriteEffects.None;
                        double rotation = 0f;
                        switch (tileTrans)
                        {
                            case Trans.Flip_H: effects = SpriteEffects.FlipHorizontally; break;
                            case Trans.Flip_V: effects = SpriteEffects.FlipVertically; break;

                            case Trans.Rotate_90:
                                rotation = Math.PI * .5f;
                                destination.X += map.TileWidth;
                                break;

                            case Trans.Rotate_180:
                                rotation = Math.PI;
                                destination.X += map.TileWidth;
                                destination.Y += map.TileHeight;
                                break;

                            case Trans.Rotate_270:
                                rotation = Math.PI * 3 / 2;
                                destination.Y += map.TileHeight;
                                break;

                            case Trans.Rotate_90AndFlip_H:
                                effects = SpriteEffects.FlipHorizontally;
                                rotation = Math.PI * .5f;
                                destination.X += map.TileWidth;
                                break;

                            default:
                                break;
                        }

                        // Render sprite at position tileX, tileY using the rect
                        _spriteBatch.Draw(tilesetTexture, destination, source, Color.White,
                            (float)rotation, Vector2.Zero, effects, 0);
                    }
                }
            }

            

            if (!startGame)
            {
                _spriteBatch.DrawString(gameTitle, "Penguin Adventure", new Vector2(70, 170), Color.White);
                _spriteBatch.DrawString(instructions, "WASD to move, Space to attack", new Vector2(420, 330), Color.Yellow);
                foreach (var component in buttons)
                    component.Draw(gameTime, _spriteBatch);
            }
            else
            {
                if (dead)
                    _spriteBatch.DrawString(gameTitle, "Game Over!", new Vector2(location.X - 450, location.Y), Color.Red);
                else if (win)
                    _spriteBatch.DrawString(gameTitle, "You win!", new Vector2(location.X - 450, location.Y), Color.Yellow);
                else
                {
                    _spriteBatch.DrawString(healthText, "Health: ", new Vector2(location.X - 600, location.Y - 350), Color.Black);
                    _spriteBatch.Draw(healthbar, new Vector2(location.X - 600, location.Y - 300), healthRec, Color.Red);
                    //_spriteBatch.Draw(cardContainer, new Vector2(location.X -5 , location.Y + 200), Color.White);
                    _spriteBatch.Draw(_texture, new Rectangle((int)(location.X - 150), (int)(location.Y + 250), 65, 65), Color.White);
                    _spriteBatch.Draw(down, new Vector2((int)(location.X - 150 - 7), (int)(location.Y + 250 - 7)), downAnimation[1], Color.White, 0, new Vector2(0, 0), (float)0.6, SpriteEffects.None, 1);
                    _spriteBatch.Draw(_texture, new Rectangle((int)(location.X - 150 + 65), (int)(location.Y + 250), 65, 65), Color.White);
                    if (card1)
                    {
                        _spriteBatch.Draw(down, new Vector2((int)(location.X - 150 - 7 + 65), (int)(location.Y + 250 - 7)), downAnimation[1], Color.Red, 0, new Vector2(0, 0), (float)0.6, SpriteEffects.None, 1);
                    }

                    _spriteBatch.Draw(_texture, new Rectangle((int)(location.X - 150 + 65 * 2), (int)(location.Y + 250), 65, 65), Color.White);
                    if (card2)
                    {
                        _spriteBatch.Draw(down, new Vector2((int)(location.X - 150 - 7 + 65 * 2), (int)(location.Y + 250 - 7)), downAnimation[1], Color.Yellow, 0, new Vector2(0, 0), (float)0.6, SpriteEffects.None, 1);
                    }

                    _spriteBatch.DrawString(healthText, "1", new Vector2((int)(location.X - 150), (int)(location.Y + 250)), Color.Black, 0, new Vector2(0, 0), (float)0.5, SpriteEffects.None, 1);
                    _spriteBatch.DrawString(healthText, "2", new Vector2((int)(location.X - 150 + 65), (int)(location.Y + 250)), Color.Black, 0, new Vector2(0, 0), (float)0.5, SpriteEffects.None, 1);
                    _spriteBatch.DrawString(healthText, "3", new Vector2((int)(location.X - 150 + 65 * 2), (int)(location.Y + 250)), Color.Black, 0, new Vector2(0, 0), (float)0.5, SpriteEffects.None, 1);

                    // Collision
                    /*_spriteBatch.Draw(pixel, playerCollider, Color.Blue);
                    _spriteBatch.Draw(pixel, weaponCollider, Color.Purple);*/

                    //draw card on map
                    if (!card1)
                    {
                        _spriteBatch.Draw(down, new Vector2(1300, 2900), downAnimation[1], Color.Red, 0, new Vector2(0, 0), (float)0.6, SpriteEffects.None, 1);
                    }
                    if (!card2)
                    {
                        _spriteBatch.Draw(down, new Vector2(5900, 2400), downAnimation[1], Color.Yellow, 0, new Vector2(0, 0), (float)0.6, SpriteEffects.None, 1);
                    }


                    // Draw Enemies
                    foreach (var fox in foxes)
                    {
                        _spriteBatch.Draw(fox.healthbar, new Vector2(fox.position.X, fox.position.Y), fox.healthRec, Color.Red);
                        fox.Draw(_spriteBatch);
                    }

                    foreach (var wolf in wolves)
                    {
                        _spriteBatch.Draw(wolf.healthbar, new Vector2(wolf.position.X, wolf.position.Y), wolf.healthRec, Color.Red);
                        wolf.Draw(_spriteBatch);
                    }
                    foreach (var bat in bats)
                    {
                        _spriteBatch.Draw(bat.healthbar, new Vector2(bat.position.X, bat.position.Y), bat.healthRec, Color.Red);
                        bat.Draw(_spriteBatch);
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        drawmoving = true;

                        if (currentAnimationIndex >= leftAnimation.Length - 1)
                        {
                            currentAnimationIndex = 0;
                        }
                        if (!drawattacking)
                        {
                            if (currentcard == 0)
                            {
                                _spriteBatch.Draw(left, location, leftAnimation[currentAnimationIndex], Color.White, 0, new Vector2(0, 0), (float)0.5, SpriteEffects.None, 1);
                                if (stickAttack)
                                    _spriteBatch.Draw(stickLeft, new Vector2((int)(location.X - 40), (int)(location.Y)), Color.White);
                                else
                                    _spriteBatch.Draw(stickLeft, new Vector2((int)(location.X - 30), (int)(location.Y)), Color.White);
                            }

                            if (currentcard == 1)
                            {
                                _spriteBatch.Draw(left1, location, leftAnimation1[currentAnimationIndex], Color.Red, 0, new Vector2(0, 0), (float)0.5, SpriteEffects.None, 1);
                                if (swordAttack)
                                    _spriteBatch.Draw(swordLeft, new Vector2((int)(location.X - 60), (int)(location.Y)), Color.White);
                                else
                                    _spriteBatch.Draw(swordLeft, new Vector2((int)(location.X - 30), (int)(location.Y)), Color.White);
                            }
                            if (currentcard == 2)
                            {
                                _spriteBatch.Draw(left2, location, leftAnimation2[currentAnimationIndex], Color.Yellow, 0, new Vector2(0, 0), (float)0.5, SpriteEffects.None, 1);
                                if (shurikenAttack)
                                    _spriteBatch.Draw(shurikenLeft, new Vector2((int)(location.X - 200), (int)(location.Y)), Color.White);
                                else
                                    _spriteBatch.Draw(shurikenLeft, new Vector2((int)(location.X - 30), (int)(location.Y)), Color.White);
                            }
                        }
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        drawmoving = true;

                        if (currentAnimationIndex >= rightAnimation.Length - 1)
                        {
                            currentAnimationIndex = 0;
                        }
                        if (!drawattacking)
                        {
                            if (currentcard == 0)
                            {
                                _spriteBatch.Draw(right, location, rightAnimation[currentAnimationIndex], Color.White, 0, new Vector2(0, 0), (float)0.5, SpriteEffects.None, 1);
                                if (stickAttack)
                                    _spriteBatch.Draw(stick, new Vector2((int)(location.X + 40), (int)(location.Y)), Color.White);
                                else
                                    _spriteBatch.Draw(stick, new Vector2((int)(location.X + 30), (int)(location.Y)), Color.White);
                            }
                            if (currentcard == 1)
                            {
                                _spriteBatch.Draw(right1, location, rightAnimation1[currentAnimationIndex], Color.Red, 0, new Vector2(0, 0), (float)0.5, SpriteEffects.None, 1);
                                if (swordAttack)
                                    _spriteBatch.Draw(sword, new Vector2((int)(location.X + 60), (int)(location.Y)), Color.White);
                                else
                                    _spriteBatch.Draw(sword, new Vector2((int)(location.X + 30), (int)(location.Y)), Color.White);
                            }
                            if (currentcard == 2)
                            {
                                _spriteBatch.Draw(right2, location, rightAnimation2[currentAnimationIndex], Color.Yellow, 0, new Vector2(0, 0), (float)0.5, SpriteEffects.None, 1);
                                if (shurikenAttack)
                                    _spriteBatch.Draw(shuriken, new Vector2((int)(location.X + 200), (int)(location.Y)), Color.White);
                                else
                                    _spriteBatch.Draw(shuriken, new Vector2((int)(location.X + 30), (int)(location.Y)), Color.White);
                            }
                        }
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.W))
                    {
                        drawmoving = true;

                        if (currentAnimationIndex >= upAnimation.Length - 1)
                        {
                            currentAnimationIndex = 0;
                        }
                        if (!drawattacking && !(Keyboard.GetState().IsKeyDown(Keys.W) && Keyboard.GetState().IsKeyDown(Keys.A)) && !(Keyboard.GetState().IsKeyDown(Keys.W) && Keyboard.GetState().IsKeyDown(Keys.D)))
                        {
                            if (currentcard == 0)
                            {
                                _spriteBatch.Draw(up, location, upAnimation[currentAnimationIndex], Color.White, 0, new Vector2(0, 0), (float)0.5, SpriteEffects.None, 1);
                                if (stickAttack)
                                    _spriteBatch.Draw(stick, new Vector2((int)(location.X + 30), (int)(location.Y - 10)), Color.White);
                                else
                                    _spriteBatch.Draw(stick, new Vector2((int)(location.X + 30), (int)location.Y), Color.White);
                            }
                            if (currentcard == 1)
                            {
                                _spriteBatch.Draw(up1, location, upAnimation1[currentAnimationIndex], Color.Red, 0, new Vector2(0, 0), (float)0.5, SpriteEffects.None, 1);
                                if (swordAttack)
                                    _spriteBatch.Draw(sword, new Vector2((int)(location.X + 30), (int)(location.Y - 30)), Color.White);
                                else
                                    _spriteBatch.Draw(sword, new Vector2((int)(location.X + 30), (int)(location.Y)), Color.White);
                            }
                            if (currentcard == 2)
                            {
                                _spriteBatch.Draw(up2, location, upAnimation2[currentAnimationIndex], Color.Yellow, 0, new Vector2(0, 0), (float)0.5, SpriteEffects.None, 1);
                                if (shurikenAttack)
                                    _spriteBatch.Draw(shuriken, new Vector2((int)(location.X + 30), (int)(location.Y - 170)), Color.White);
                                else
                                    _spriteBatch.Draw(shuriken, new Vector2((int)(location.X + 30), (int)(location.Y)), Color.White);
                            }
                        }
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.S))
                    {
                        drawmoving = true;

                        if (currentAnimationIndex >= downAnimation.Length - 1)
                        {
                            currentAnimationIndex = 0;
                        }
                        if (!drawattacking && !(Keyboard.GetState().IsKeyDown(Keys.S) && Keyboard.GetState().IsKeyDown(Keys.A)) && !(Keyboard.GetState().IsKeyDown(Keys.S) && Keyboard.GetState().IsKeyDown(Keys.D)))
                        {
                            if (currentcard == 0)
                            {
                                _spriteBatch.Draw(down, location, downAnimation[currentAnimationIndex], Color.White, 0, new Vector2(0, 0), (float)0.5, SpriteEffects.None, 1);
                                if (stickAttack)
                                    _spriteBatch.Draw(stick, new Vector2((int)(location.X + 30), (int)(location.Y + 10)), Color.White);
                                else
                                    _spriteBatch.Draw(stick, new Vector2((int)(location.X + 30), (int)(location.Y)), Color.White);
                            }
                            if (currentcard == 1)
                            {
                                _spriteBatch.Draw(down1, location, downAnimation1[currentAnimationIndex], Color.Red, 0, new Vector2(0, 0), (float)0.5, SpriteEffects.None, 1);
                                if (swordAttack)
                                    _spriteBatch.Draw(sword, new Vector2((int)(location.X + 30), (int)(location.Y + 30)), Color.White);
                                else
                                    _spriteBatch.Draw(sword, new Vector2((int)(location.X + 30), (int)(location.Y)), Color.White);
                            }
                            if (currentcard == 2)
                            {
                                _spriteBatch.Draw(down2, location, downAnimation2[currentAnimationIndex], Color.Yellow, 0, new Vector2(0, 0), (float)0.5, SpriteEffects.None, 1);
                                if (shurikenAttack)
                                    _spriteBatch.Draw(shuriken, new Vector2((int)(location.X + 30), (int)(location.Y + 170)), Color.White);
                                else
                                    _spriteBatch.Draw(shuriken, new Vector2((int)(location.X + 30), (int)(location.Y)), Color.White);
                            }
                        }
                    }

                    if (Keyboard.GetState().GetPressedKeys() == null || Keyboard.GetState().GetPressedKeys().Length == 0 || !drawmoving)
                    {
                        if (currentcard == 0)
                        {
                            if (_movementKey == Keys.A)
                            {
                                _spriteBatch.Draw(left, location, leftAnimation[currentAnimationIndex], Color.White, 0, new Vector2(0, 0), (float)0.5, SpriteEffects.None, 1);
                                if (stickAttack)
                                    _spriteBatch.Draw(stickLeft, new Vector2((int)(location.X - 40), (int)(location.Y)), Color.White);
                                else
                                    _spriteBatch.Draw(stickLeft, new Vector2((int)(location.X - 30), (int)(location.Y)), Color.White);
                            }
                            if (_movementKey == Keys.D)
                            {
                                _spriteBatch.Draw(right, location, rightAnimation[currentAnimationIndex], Color.White, 0, new Vector2(0, 0), (float)0.5, SpriteEffects.None, 1);
                                if (stickAttack)
                                    _spriteBatch.Draw(stick, new Vector2((int)(location.X + 40), (int)(location.Y)), Color.White);
                                else
                                    _spriteBatch.Draw(stick, new Vector2((int)(location.X + 30), (int)(location.Y)), Color.White);
                            }
                            if (_movementKey == Keys.W)
                            {
                                _spriteBatch.Draw(up, location, upAnimation[currentAnimationIndex], Color.White, 0, new Vector2(0, 0), (float)0.5, SpriteEffects.None, 1);
                                if (stickAttack)
                                    _spriteBatch.Draw(stick, new Vector2((int)(location.X + 30), (int)(location.Y - 10)), Color.White);
                                else
                                    _spriteBatch.Draw(stick, new Vector2((int)(location.X + 30), (int)(location.Y)), Color.White);
                            }
                            if (_movementKey == Keys.S)
                            {
                                _spriteBatch.Draw(down, location, downAnimation[currentAnimationIndex], Color.White, 0, new Vector2(0, 0), (float)0.5, SpriteEffects.None, 1);
                                if (stickAttack)
                                    _spriteBatch.Draw(stick, new Vector2((int)(location.X + 30), (int)(location.Y + 10)), Color.White);
                                else
                                    _spriteBatch.Draw(stick, new Vector2((int)(location.X + 30), (int)(location.Y)), Color.White);
                            }
                        }
                        if (currentcard == 1)
                        {
                            if (_movementKey == Keys.A)
                            {
                                _spriteBatch.Draw(left1, location, leftAnimation1[currentAnimationIndex], Color.Red, 0, new Vector2(0, 0), (float)0.5, SpriteEffects.None, 1);
                                if (swordAttack)
                                    _spriteBatch.Draw(swordLeft, new Vector2((int)(location.X - 60), (int)(location.Y)), Color.White);
                                else
                                    _spriteBatch.Draw(swordLeft, new Vector2((int)(location.X - 30), (int)(location.Y)), Color.White);
                            }
                            if (_movementKey == Keys.D)
                            {
                                _spriteBatch.Draw(right1, location, rightAnimation1[currentAnimationIndex], Color.Red, 0, new Vector2(0, 0), (float)0.5, SpriteEffects.None, 1);
                                if (swordAttack)
                                    _spriteBatch.Draw(sword, new Vector2((int)(location.X + 60), (int)(location.Y)), Color.White);
                                else
                                    _spriteBatch.Draw(sword, new Vector2((int)(location.X + 30), (int)(location.Y)), Color.White);
                            }
                            if (_movementKey == Keys.W)
                            {
                                _spriteBatch.Draw(up1, location, upAnimation1[currentAnimationIndex], Color.Red, 0, new Vector2(0, 0), (float)0.5, SpriteEffects.None, 1);
                                if (swordAttack)
                                    _spriteBatch.Draw(sword, new Vector2((int)(location.X + 30), (int)(location.Y - 30)), Color.White);
                                else
                                    _spriteBatch.Draw(sword, new Vector2((int)(location.X + 30), (int)(location.Y)), Color.White);
                            }
                            if (_movementKey == Keys.S)
                            {
                                _spriteBatch.Draw(down1, location, downAnimation1[currentAnimationIndex], Color.Red, 0, new Vector2(0, 0), (float)0.5, SpriteEffects.None, 1);
                                if (swordAttack)
                                    _spriteBatch.Draw(sword, new Vector2((int)(location.X + 30), (int)(location.Y + 30)), Color.White);
                                else
                                    _spriteBatch.Draw(sword, new Vector2((int)(location.X + 30), (int)(location.Y)), Color.White);
                            }
                        }
                        if (currentcard == 2)
                        {
                            if (_movementKey == Keys.A)
                            {
                                _spriteBatch.Draw(left2, location, leftAnimation2[currentAnimationIndex], Color.Yellow, 0, new Vector2(0, 0), (float)0.5, SpriteEffects.None, 1);
                                if (shurikenAttack)
                                    _spriteBatch.Draw(shurikenLeft, new Vector2((int)(location.X - 200), (int)(location.Y)), Color.White);
                                else
                                    _spriteBatch.Draw(shurikenLeft, new Vector2((int)(location.X - 30), (int)(location.Y)), Color.White);
                            }
                            if (_movementKey == Keys.D)
                            {
                                _spriteBatch.Draw(right2, location, rightAnimation2[currentAnimationIndex], Color.Yellow, 0, new Vector2(0, 0), (float)0.5, SpriteEffects.None, 1);
                                if (shurikenAttack)
                                    _spriteBatch.Draw(shuriken, new Vector2((int)(location.X + 200), (int)(location.Y)), Color.White);
                                else
                                    _spriteBatch.Draw(shuriken, new Vector2((int)(location.X + 30), (int)(location.Y)), Color.White);
                            }
                            if (_movementKey == Keys.W)
                            {
                                _spriteBatch.Draw(up2, location, upAnimation2[currentAnimationIndex], Color.Yellow, 0, new Vector2(0, 0), (float)0.5, SpriteEffects.None, 1);
                                if (shurikenAttack)
                                    _spriteBatch.Draw(shuriken, new Vector2((int)(location.X + 30), (int)(location.Y - 170)), Color.White);
                                else
                                    _spriteBatch.Draw(shuriken, new Vector2((int)(location.X + 30), (int)(location.Y)), Color.White);
                            }
                            if (_movementKey == Keys.S)
                            {
                                _spriteBatch.Draw(down2, location, downAnimation2[currentAnimationIndex], Color.Yellow, 0, new Vector2(0, 0), (float)0.5, SpriteEffects.None, 1);
                                if (shurikenAttack)
                                    _spriteBatch.Draw(shuriken, new Vector2((int)(location.X + 30), (int)(location.Y + 170)), Color.White);
                                else
                                    _spriteBatch.Draw(shuriken, new Vector2((int)(location.X + 30), (int)(location.Y)), Color.White);
                            }
                        }
                    }
                }
                
                drawmoving = false;
                drawattacking = false;
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
