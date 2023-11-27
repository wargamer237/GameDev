using MyUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Classes.interfaces
{
    internal interface ICheckColisions
    {
       void UpdateColision(ref RectangleF rect);
        void SetVertexs(List<Vertexs> vertexs);
        public List<Vertexs> GetVertexs();
    }
}
