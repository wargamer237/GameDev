using MyUtils;
using Microsoft.Xna.Framework;
namespace Blocks
{
    internal interface ICollidable
    {
    }
    internal interface IMovable
    {
        bool Update(float elapsedSec);
    }
    //--ADD make it interactable with player*. OR OTHER?
    internal interface IInteractable
    {
        bool Update(float elapsedSec, RectangleF player, Vector2 playerVelocity);
    }
}
