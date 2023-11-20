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
        private void UpdateColision(ref RectangleF rect)
        {

        }
        public void SetVertexs(List<Vertexs> vertexs)
        {

        }
        public List<Vertexs> GetVertexs()
        {
            return new List<Vertexs>();
        }
    }
}
