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

        MainMenu MainMenu;

        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                {
                    //instance = new ScreenManager();
                    XMLmanager<ScreenManager> xml = new XMLmanager<ScreenManager>();
                    instance = xml.Load("Load/ScreenManager.xml");
                }

                return instance;
            }
        }

        //Below are my functions for screen transitions. For some reason, they do not work. I spent 3 hours staring at it and going through the ppt and lecture but I still am unsure
        //what the issue is. As far as I can tell, it is correct, but obviously something is amiss. If you see what is wrong, PLEASE let me know in a comment on my grade! I really
        //want to know what I have been missing. It is eating at me :[  

        //The only thing I did discover was that if I changed Image.Alpha to greater than 0, it did darken the screen to that alpha when I pressed enter, but it did not fade in or fade out 
        //to the other screen. It abruptly changed the alpha and that's it. Maybe that means FadeEffect doesn't work? But it did work when we cycled the fading. Maybe FadeEffect is not
        //in the effectList? I am not sure.
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
                if (Image.Alpha == 1.0f)
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
                else if (Image.Alpha == 0.0f)
                {
                    Image.IsActive = false;
                    IsTransitioning = false;
                }
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
