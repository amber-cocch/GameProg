using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;



namespace monogame_assignment
{

    public class ScreenManager
    {
        GameScreen currentScreen, newScreen;

        public Image Image;

        [XmlIgnore]
        public GraphicsDevice GraphicsDevice;
        [XmlIgnore]
        public SpriteBatch SpriteBatch;
        [XmlIgnore]
        public bool IsTransitioning { get; private set; }

        [XmlIgnore]
        private static ScreenManager instance;
        [XmlIgnore]
        public ContentManager Content { private set; get; }
        [XmlIgnore]
        public Vector2 Dimensions { private set; get; }

        XMLmanager<GameScreen> XmlGameScreenManager;

       

        public static ScreenManager Instance
        {
            get
            {
                if(instance == null)
                {
                    //instance = new ScreenManager();
                    XMLmanager<ScreenManager> xml = new XMLmanager<ScreenManager>();
                    instance = xml.Load("Load/ScreenManager.xml");
                }

                return instance;
            }
        }

        public ScreenManager()
        {
            Dimensions = new Vector2(1920, 1080);

            currentScreen = new SplashScreen(); //polymorphism

            XmlGameScreenManager = new XMLmanager<GameScreen>();
            XmlGameScreenManager.type = currentScreen.GetType();
            currentScreen = XmlGameScreenManager.Load("Load/SplashScreen.xml");
        }

        public void ChangeScreens(string screenName)
        {
            newScreen = (GameScreen)Activator.CreateInstance(Type.GetType("monogame_assignment." + screenName));
            Image.IsActive = true;
            Image.FadeEffect.Increase = true;
            Image.Alpha = 0.0f;
            IsTransitioning = true;
        }

        public void Transition(GameTime gameTime)
        {
            if (IsTransitioning)
            {
                Image.Update(gameTime);
                if(Image.Alpha == 1.0f)
                {
                    currentScreen.UnloadContent();
                    currentScreen = newScreen;
                    XmlGameScreenManager.type = currentScreen.type;
                    if (File.Exists(currentScreen.XmlPath))
                    {
                        currentScreen = XmlGameScreenManager.Load(currentScreen.XmlPath);
                    }
                    currentScreen.LoadContent();
                }
                else if(Image.Alpha == 0.0f)
                {
                    Image.IsActive = false;
                    IsTransitioning = false;
                }
            }
        }


        public void LoadContent(ContentManager Content)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            currentScreen.LoadContent();
            Image.LoadContent();
        }

        public void UnloadContent()
        {
            currentScreen.UnloadContent();
            Image.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            currentScreen.Update(gameTime);
            Transition(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
            if (IsTransitioning)
            {
                Image.Draw(spriteBatch);
            }
        }
    }

    
}
