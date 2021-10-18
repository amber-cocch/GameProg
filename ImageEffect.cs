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
    public class ImageEffect
    {
        protected Image Image;
        public bool IsActive;
        protected float Alpha = 0;

        public ImageEffect()
        {

        }

        public virtual void LoadContent(ref Image Image)
        {
            this.Image = Image;
            this.Alpha = Image.Alpha;
        }

        public virtual void UnloadContent()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }
    }
}
