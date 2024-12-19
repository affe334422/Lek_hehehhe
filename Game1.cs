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
        

        if(kstate.IsKeyDown(Keys.F)){
            stop = 2;
        }
        if(kstate.IsKeyDown(Keys.R)){
            stop = 1;
        }



        if(countdown != 10 && stop == 1){
            countdown++;
        }
        if(/*countdown == 10 && stop == 1 || */kstate.IsKeyDown(Keys.R) || countdown == 10){
            int Hurmångaloopar = 5;

            for(int i = 1; i < Hurmångaloopar; i++){
                boll.Add(new Rectangle(kordinatx,kordinaty+i,20,20));
                bollhastighet.Add(new Point(bollxvo *= -1,bollyvo += 0)); 
                bollhastighet.Add(new Point(bollxvo += 0,bollyvo*=-1));
            }
            


            bollxvo = 5;
            bollyvo = 5;
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

        
        
        
        
        

        for(int i = 0; i < boll.Count; i++){
            boll[i] = new Rectangle(boll[i].X + bollhastighet[i].X, boll[i].Y + bollhastighet[i].Y, boll[i].Width,boll[i].Height);
        

            if(boll[i].Y <= 0 || boll[i].Y >= ybollgräns){
                bollhastighet[i] = new Point(bollhastighet[i].X, -bollhastighet[i].Y);

            }

            if(boll[i].X <= 0 || boll[i].X >= xbollgräns){
                //boll[i] = new Rectangle(390,230,20,20);
                //bollhastighet[i] = new Point(bollxvo *= -1,bollyvo *= -1);
                bollhastighet[i] = new Point(-bollhastighet[i].X ,bollhastighet[i].Y);

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
