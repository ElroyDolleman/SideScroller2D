using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SideScroller2D.Code.Graphics
{
    struct SpriteBatchSettings
    {
        public static SpriteBatchSettings Default
        {
            get
            {
                var settings = new SpriteBatchSettings();

                settings.SpriteSortMode = SpriteSortMode.Deferred;
                settings.BlendState = BlendState.AlphaBlend;
                settings.SamplerState = SamplerState.PointClamp;
                settings.DepthStencilState = DepthStencilState.None;
                settings.RasterizerState = RasterizerState.CullCounterClockwise;
                settings.SpriteBatchScale = Matrix.CreateScale(1);

                return settings;
            }
        }

        public SpriteSortMode SpriteSortMode;
        public BlendState BlendState;
        public SamplerState SamplerState;
        public DepthStencilState DepthStencilState;
        public RasterizerState RasterizerState;
        public Matrix SpriteBatchScale;
    }
}
