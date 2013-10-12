using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameSlamProject
{
	/// <summary>
    /// Needs to be worked on. Should be for 3D objects. 99% sure it doesn't work.
    /// </summary>
    public class ModelObject
    {
        public Model model;
        public Vector3 position = Vector3.Zero;
        public float rotationX = 0.0f;
        public float rotationY = 0.0f;
        public float rotationZ = 0.0f;
        public float zoom = 2500;
        public Matrix gameWorldRotation;

        public void LoadModel(ContentManager Content, string assetName)
        {
            model = Content.Load<Model>(assetName);
        }

        public void UpdateModelRotation()
        {
            gameWorldRotation = Matrix.CreateRotationX(MathHelper.ToRadians(rotationX)) * Matrix.CreateRotationY(MathHelper.ToRadians(rotationY)) * Matrix.CreateRotationZ(MathHelper.ToRadians(rotationZ));
        }

        public void DrawModel(GraphicsDeviceManager graphics, Vector3 camera)
        {
            Matrix[] transforms = new Matrix[model.Bones.Count];
            float aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio;
            model.CopyAbsoluteBoneTransformsTo(transforms);
            Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), aspectRatio, 1.0f, 10000.0f);
            Matrix view = Matrix.CreateLookAt(new Vector3(0.0f, 50.0f, zoom), Vector3.Zero, Vector3.Up);
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = transforms[mesh.ParentBone.Index] *
                        Matrix.CreateRotationY(rotationY)
                        * Matrix.CreateTranslation(position);
                    effect.View = Matrix.CreateLookAt(camera,
                        Vector3.Zero, Vector3.Up);
                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                        MathHelper.ToRadians(45.0f), aspectRatio,
                        1.0f, 10000.0f);
                }
                mesh.Draw();
            }
        }
    }
}
