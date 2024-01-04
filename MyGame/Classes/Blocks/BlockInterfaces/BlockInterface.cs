using MyClass.MyUtils;
using Microsoft.Xna.Framework;
namespace MyClass.MyBlocks.BlocksInterfaces
{
    internal interface ICollidable
    {
    }
    internal interface IMovable
    {
        void Update(float elapsedSec);
        Vector2 GetVelocty();
    }
    //--ADD make it interactable with player*. OR OTHER?
    internal interface IInteractable
    {
        bool Update(float elapsedSec, RectangleF player, Vector2 playerVelocity);
    }
}
