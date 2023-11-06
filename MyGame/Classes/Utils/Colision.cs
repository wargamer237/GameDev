using MyUtils;
using Microsoft.Xna.Framework;

namespace MyUtils
{
    public enum DirectionValue
    {
        Positve = 1,
        Negetive = -1,
        None = 0
    }
    public struct Direction
    {
        public DirectionValue Horzontal = DirectionValue.None;
        public DirectionValue Vertical = DirectionValue.None;
        public Direction()
        {
            Horzontal = DirectionValue.None;
            Vertical = DirectionValue.None;
        }
        public Direction
            (DirectionValue horzontal = DirectionValue.None
            , DirectionValue vertical = DirectionValue.None)
        {
            Horzontal = horzontal;
            Vertical = vertical;
        }
    }

    public struct BoolColision
    {
        public bool Colision;
        public bool Left;
        public bool Top;
        public bool Right;
        public bool Bottom;
    }
    public static class Colision
    {
        //Is the point in a rectangle
        public static bool RectInRect(RectangleF rect1, RectangleF rect2)
        {
            if (PointInRect(new Vector2(rect1.X, rect1.Y), rect2)) return true;
            if (PointInRect(new Vector2(rect1.X + rect1.Width, rect1.Y), rect2)) return true;
            if (PointInRect(new Vector2(rect1.X, rect1.Y + rect1.Height), rect2)) return true;
            if (PointInRect(new Vector2(rect1.X + rect1.Width, rect1.Y + rect1.Height), rect2)) return true;

            return false;
        }
        public static bool PointInRect(Vector2 point, RectangleF border)
        {
            bool checkX = point.X >= border.X && point.X <= border.X + (border.Width);
            bool checkY = point.Y >= border.Y && point.Y <= border.Y + (border.Height);
            if (checkX && checkY) return true;
            return false;
        }
        //COLISION WITH RECTANGLES
        public static bool RectInRectWithSide(RectangleF rect1, RectangleF rect2, ref bool left, ref bool right, ref bool top, ref bool bottom)
        {
            float lenght = 5;//px lenght
            //TOP LEFT
            Vector2 TopLeftLeft = new Vector2((rect1.X) - lenght, (rect1.Y) + lenght);
            Vector2 TopLeftTop = new Vector2((rect1.X) + lenght, (rect1.Y) - lenght);
            //TOP RIGHT
            Vector2 TopRightRight = new Vector2((rect1.X + rect1.Width) + lenght, (rect1.Y) + lenght);
            Vector2 TopRightTop = new Vector2((rect1.X + rect1.Width) - lenght, (rect1.Y) - lenght);
            //BOTTOM RIGHT
            Vector2 BottomRightRight = new Vector2((rect1.X + rect1.Width) + lenght, (rect1.Y + rect1.Height) - lenght);
            Vector2 BottomRightBottom = new Vector2((rect1.X + rect1.Width) - lenght, (rect1.Y + rect1.Height) + lenght);
            //BOTTOM LEFT
            Vector2 BotttomLeftLeft = new Vector2((rect1.X) - lenght, (rect1.Y + rect1.Height) - lenght);
            Vector2 BottomLeftBottom = new Vector2((rect1.X) + lenght, (rect1.Y + rect1.Height) + lenght);

            //LEFT
            if (Colision.PointInRect(TopLeftLeft, rect2)
                || Colision.PointInRect(BotttomLeftLeft, rect2))
                left = true;
            //TOP
            if (Colision.PointInRect(TopLeftTop, rect2)
                || Colision.PointInRect(TopRightTop, rect2))
                top = true;
            //RIGHT
            if (Colision.PointInRect(TopRightRight, rect2)
                || Colision.PointInRect(BottomRightRight, rect2))
                right = true;
            //BOTTOM
            if (Colision.PointInRect(BottomLeftBottom, rect2)
                || Colision.PointInRect(BottomRightBottom, rect2))
                bottom = true;

            if (left || top || right || bottom) 
            return true;
            return false;
        }
        public static bool RectInRectWithSide(RectangleF rect1, RectangleF rect2, ref BoolColision colisions)
        {
            colisions.Colision = RectInRectWithSide(rect1, rect2, ref colisions.Left, ref colisions.Right, ref colisions.Top, ref colisions.Bottom);
            return colisions.Colision;
        }
        public static BoolColision RectInRectWithSide(RectangleF rect1, RectangleF rect2)
        {
            BoolColision colisions = new BoolColision();
            colisions.Colision = RectInRectWithSide(rect1, rect2, ref colisions.Left, ref colisions.Right, ref colisions.Top, ref colisions.Bottom);
            return colisions;
        }
        //WITH VELOCTY
        public static Vector2 FindIntersectionPoint(Vector2 v1, Vector2 v2, Vector2 w1, Vector2 w2)
        {
            float x1 = v1.X;
            float y1 = v1.Y;
            float x2 = v2.X;
            float y2 = v2.Y;
            float x3 = w1.X;
            float y3 = w1.Y;
            float x4 = w2.X;
            float y4 = w2.Y;

            float x = ((x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4)) / ((x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4));
            float y = ((x1 * y2 - y1 * x2) * (y3 - y4) - (y1 - y2) * (x3 * y4 - y3 * x4)) / ((x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4));

            return new Vector2(x, y);
        }

        /* RayInRectDistence
        *   +          +
        *  +|----------|+
        *   |          |
        *   |          |
        *   |          | 
        *  +|----------|+
        *   +          +
        */
        public static bool RayInRectDistence(RectangleF rect1, RectangleF rect2, Vector2 velocty, ref bool left, ref bool right, ref bool top, ref bool bottom)
        {
            float lenght = 5; //margin how mutch the ray have to stick out
            //left top
            Vector2 LeftTop1 = new Vector2(rect1.X, rect1.Y);
            Vector2 LeftTopLeft = new Vector2(LeftTop1.X - lenght, LeftTop1.Y);
            Vector2 TopLeftTop = new Vector2(LeftTop1.X, LeftTop1.Y - lenght);
            //right top
            Vector2 RightTop1 = new Vector2(rect1.X + rect1.Width, rect1.Y);
            Vector2 RightTopRight = new Vector2(RightTop1.X + lenght, RightTop1.Y);
            Vector2 TopRightTop = new Vector2(RightTop1.X, RightTop1.Y - lenght);
            //right bottom
            Vector2 RightBottom1 = new Vector2(rect1.X + rect1.Width, rect1.Y + rect1.Height);
            Vector2 RightBottomRight = new Vector2(RightBottom1.X + lenght, RightBottom1.Y);
            Vector2 BottomRightBottom = new Vector2(RightBottom1.X, RightBottom1.Y + lenght);
            //left bottom
            Vector2 LeftBottom1 = new Vector2(rect1.X, rect1.Y + rect1.Height);
            Vector2 LeftBottomLeft = new Vector2(LeftBottom1.X - lenght, LeftBottom1.Y);
            Vector2 BottomLeftBottom = new Vector2(LeftBottom1.X, LeftBottom1.Y + lenght);

            //SECOND RECT
            Vector2 LeftTop2 = new Vector2(rect2.X, rect2.Y);
            Vector2 RightTop2 = new Vector2(rect2.X + rect2.Width, rect2.Y);
            Vector2 RightBottom2 = new Vector2(rect2.X + rect2.Width, rect2.Y + rect2.Height);
            Vector2 LeftBottom2 = new Vector2(rect2.X, rect2.Y + rect2.Height);

            
            Vector2 lefty = FindIntersectionPoint(LeftTop1, LeftTopLeft, RightTop2, RightBottom2);
            if(lefty == Vector2.Zero)
            {
                lefty = FindIntersectionPoint(LeftBottom1, LeftBottomLeft, RightTop2, RightBottom2);
            }
            Vector2 righty = FindIntersectionPoint(RightTop1, RightTopRight, LeftTop2, LeftBottom2);
            if (righty == Vector2.Zero)
            {
                righty = FindIntersectionPoint(RightBottom1, RightBottomRight, LeftTop2, LeftBottom2);
            }
            Vector2 topy = FindIntersectionPoint(LeftTop1,TopLeftTop, LeftTop2, RightTop2);
            if (topy == Vector2.Zero)
            {
                topy = FindIntersectionPoint(RightTop1, TopRightTop, LeftTop2, RightTop2);
            }
            Vector2 bottomy = FindIntersectionPoint(LeftBottom1, BottomLeftBottom, LeftBottom2, RightBottom2);
            if (bottomy == Vector2.Zero)
            {
                bottomy = FindIntersectionPoint(RightBottom1, BottomRightBottom, LeftBottom2, RightBottom2);
            }

            return false;
        }

        public static bool RectInRectWithSide(RectangleF rect1, RectangleF rect2, Vector2 velocty, ref bool left, ref bool right, ref bool top, ref bool bottom)
        {
            rect1.X += velocty.X;
            rect1.Y += velocty.Y;
            float lenght = 5 + (rect1.Width + rect1.Height)/100;//px lenght//px lenght

            //TOP LEFT
            Vector2 TopLeftLeft = new Vector2((rect1.X) - lenght, (rect1.Y));
            Vector2 TopLeftTop = new Vector2((rect1.X), (rect1.Y) - lenght);
            //TOP RIGHT
            Vector2 TopRightRight = new Vector2((rect1.X + rect1.Width) + lenght, (rect1.Y));
            Vector2 TopRightTop = new Vector2((rect1.X + rect1.Width), (rect1.Y) - lenght);
            //BOTTOM RIGHT
            Vector2 BottomRightRight = new Vector2((rect1.X + rect1.Width) + lenght, (rect1.Y + rect1.Height));
            Vector2 BottomRightBottom = new Vector2((rect1.X + rect1.Width), (rect1.Y + rect1.Height) + lenght);
            //BOTTOM LEFT
            Vector2 BotttomLeftLeft = new Vector2((rect1.X) - lenght, (rect1.Y + rect1.Height));
            Vector2 BottomLeftBottom = new Vector2((rect1.X), (rect1.Y + rect1.Height) + lenght);
            //LEFT
            if (Colision.PointInRect(TopLeftLeft, rect2)
                || Colision.PointInRect(BotttomLeftLeft, rect2))
                left = true;
            //TOP
            if (Colision.PointInRect(TopLeftTop, rect2)
                || Colision.PointInRect(TopRightTop, rect2))
                top = true;
            //RIGHT
            if (Colision.PointInRect(TopRightRight, rect2)
                || Colision.PointInRect(BottomRightRight, rect2))
                right = true;
            //BOTTOM
            if (Colision.PointInRect(BottomLeftBottom, rect2)
                || Colision.PointInRect(BottomRightBottom, rect2))
                bottom = true;

            if (left || top || right || bottom)
                return true;
            return false;
        }
        public static bool RectInRectWithSide(RectangleF rect1, RectangleF rect2, Vector2 velocty, ref BoolColision colisions)
        {
            colisions.Colision = RectInRectWithSide(rect1, rect2, velocty, ref colisions.Left, ref colisions.Right, ref colisions.Top, ref colisions.Bottom);
            return colisions.Colision;
        }
        public static BoolColision RectInRectWithSide(RectangleF rect1, RectangleF rect2, Vector2 velocty)
        {
            BoolColision colisions = new BoolColision();
            colisions.Colision = RectInRectWithSide(rect1, rect2, velocty, ref colisions.Left, ref colisions.Right, ref colisions.Top, ref colisions.Bottom);
            return colisions;
        }
    }
}
