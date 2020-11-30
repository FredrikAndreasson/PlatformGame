using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarioPlatformer
{
    public class Camera
    {
        private Matrix transform;
        private Viewport view;

        public Camera(Viewport view)
        {
            this.view = view;
            this.transform = Matrix.CreateTranslation(0, 0, 0);
        }
        
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)transform.Translation.X, (int)transform.Translation.Y, view.Width, view.Height);
            }
        }

        public Matrix Transform 
        {
            get { return transform; }
            set { transform = value; }
        }

        public void CenterOn(Vector2 position)
        {
            transform.Translation = new Vector3(-position.X + view.Width / 2, -position.Y + view.Height / 2, 0.0f);
        }

        public void SetPosition(Vector2 position)
        {
            transform.Translation = new Vector3(position.X, position.Y, 0.0f);
        }
        
    }
}
