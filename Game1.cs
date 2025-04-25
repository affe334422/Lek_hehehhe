using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lek_hehehhe;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    Texture2D pixel;

    


    // skärmen är x = 800, y = 465

    List<Rectangle> boll = new List<Rectangle>();

    List<Point> bollhastighet = new List<Point>();
    int bollxvo = 5;
    int bollyvo = 5;

    int kordinatx = 390;
    int kordinaty = 235;
    int xbollgräns = 780;
    int ybollgräns = 465;

    int countdown = 1;
    int countdown1 = 1;
    
    int stop = 2;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        
        base.Initialize();

    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        pixel = new Texture2D(GraphicsDevice, 1,1);
        pixel.SetData(new Color[]{Color.Black});
        // pixel = Content.Load<Texture2D>("pixel");
        

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)){
            Exit();
        }if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.O)){
            ResetElapsedTime();
        }

        KeyboardState kstate  = Keyboard.GetState();
        

        if(kstate.IsKeyDown(Keys.S)){
            stop = 2;
        }
        if(kstate.IsKeyDown(Keys.G)){
            stop = 1;
        }



        if(countdown != 100 && stop == 1){
            countdown++;
        }
        if(/*countdown == 10 && stop == 1 || */kstate.IsKeyDown(Keys.G) && countdown == 100){
            int Hurmångaloopar = 5;

            
            boll.Add(new Rectangle(kordinatx,kordinaty,20,20));
            bollhastighet.Add(new Point(bollxvo *= -1,bollyvo += 0)); 
            bollhastighet.Add(new Point(bollxvo += 0,bollyvo*=-1));
            
            


            
            countdown = 1;
        }

        if (kstate.IsKeyDown(Keys.Up) || countdown1 < 5)
        {
            //skärmen
            _graphics.PreferredBackBufferWidth+=2;
            _graphics.PreferredBackBufferHeight+=2;
            //där bollen studsar
            ybollgräns+=2;
            xbollgräns+=2;
            //där de studsar
            kordinatx+=1;
            kordinaty+=1;

            countdown1++;

            _graphics.ApplyChanges();
        }
        if (kstate.IsKeyDown(Keys.Down))
        {
            //skärmen
            _graphics.PreferredBackBufferWidth-=2;
            _graphics.PreferredBackBufferHeight-=2;
            //där bollen studsar
            ybollgräns-=2;
            xbollgräns-=2;
            //där de studsar
            kordinatx-=1;
            kordinaty-=1;

            

            _graphics.ApplyChanges();
        }

        
List<int> bollCooldown = new List<int>(); // Cooldown för varje boll

for (int i = boll.Count - 1; i >= 0; i--) // Iterera baklänges
{
    // Om bollCooldown inte har tillräckligt många element, fyll på
    while (bollCooldown.Count < boll.Count)
    {
        bollCooldown.Add(0); // Nollställ cooldown för nya bollar
    }

    // Minska cooldown
    if (bollCooldown[i] > 0)
    {
        bollCooldown[i]--;
        continue; // Hoppa över denna boll om cooldown är aktiv
    }

    // Flytta bollen
    boll[i] = new Rectangle(boll[i].X + bollhastighet[i].X, boll[i].Y + bollhastighet[i].Y, boll[i].Width, boll[i].Height);

    // Kontrollera om bollen träffar toppen eller botten
    if (boll[i].Y <= 0 || boll[i].Y >= ybollgräns)
    {
        // Spara bollens position, bredd och höjd
        var originalPosition = boll[i].Location;
        var originalWidth = boll[i].Width;
        var originalHeight = boll[i].Height;

        // Ta bort den gamla bollen och dess hastighet
        boll.RemoveAt(i);
        bollhastighet.RemoveAt(i);
        bollCooldown.RemoveAt(i);

        // Skapa två nya bollar på samma plats med olika hastigheter
        var random = new Random();
        for (int j = 0; j < 2; j++)
        {
            // Generera en ny slumpmässig hastighet
            var newSpeed = new Point(
                random.Next(-5, 6), // Slumpmässig X-hastighet
                random.Next(1, 6) * (random.Next(0, 2) == 0 ? -1 : 1) // Positiv eller negativ Y-hastighet
            );

            // Lägg till en ny boll
            boll.Add(new Rectangle(originalPosition.X, originalPosition.Y, originalWidth, originalHeight));
            bollhastighet.Add(newSpeed);

            // Lägg till en cooldown för de nya bollarna
            bollCooldown.Add(30); // T.ex. 30 uppdateringar innan den kan skapa nya bollar
        }
    }
    // Kontrollera om bollen träffar vänster eller höger vägg
    else if (boll[i].X <= 0 || boll[i].X >= xbollgräns)
    {
        bollhastighet[i] = new Point(-bollhastighet[i].X, bollhastighet[i].Y);
    }
}




        // TODO: Add your update logic here

        base.Update(gameTime);
    }


    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Wheat);
        

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        
        foreach(Rectangle bollar in boll){
            _spriteBatch.Draw(pixel,bollar,Color.Black);
        }
        _spriteBatch.End();


        base.Draw(gameTime);
    }
}
