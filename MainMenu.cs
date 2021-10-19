using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


using Microsoft.Xna.Framework.Input;

namespace monogame_assignment
{
    public class MainMenu : GameScreen
    {
        public string Path;
        //Texture2D image;
        public Image Image;
        [XmlIgnore]
        public ContentManager Content;
        public Vector2 Position;
        public override void LoadContent()
        {
            base.LoadContent();
            Image.LoadContent();

           // Path = "play";
           // image = content.Load<Texture2D>(Path);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            Image.UnloadContent();

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Image.Update(gameTime);

            if (InputManager.Instance.KeyPressed(Keys.Enter, Keys.Z))
            {
                ScreenManager.Instance.ChangeScreens("Game1");
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
   
            base.Draw(spriteBatch);
            Image.Draw(spriteBatch);

            //spriteBatch.Draw(image, Vector2.Zero, Color.White);
        }
    }
}

