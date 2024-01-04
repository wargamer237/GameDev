using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyClass.MyUtils
{
#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.Equals(object o)
    public struct SRTVector
    {
        public Vector3 Scale;
        public Vector3 Rotate;
        public Vector3 Translate;
        public SRTVector(Vector3 scale, Vector3 rotate, Vector3 translate)
        {
            Scale = scale;
            Rotate = rotate;
            Translate = translate;
        }
        public SRTVector(SRTVector newVectors)
        {
            Scale = newVectors.Scale;
            Rotate = newVectors.Rotate;
            Translate = newVectors.Translate;
        }
        public SRTVector()
        {
            Scale = new Vector3(1, 1, 1);
            Rotate = new Vector3(0, 0, 0);
            Translate = new Vector3(0, 0, 0);
        }
        public static bool operator ==(SRTVector a, SRTVector b)
        {
            if (a.Scale == b.Scale && a.Rotate == b.Rotate && a.Translate == b.Translate)
            {
                return true;
            }

            return false;
        }
        public static bool operator !=(SRTVector a, SRTVector b)
        {
            return !(a == b);
        }
        public static SRTVector operator +(SRTVector a, SRTVector b)
        {
            a.Scale += b.Scale;
            a.Rotate += b.Rotate;
            a.Translate += b.Translate;

            return a;
        }
        public static SRTVector operator *(SRTVector a, SRTVector b)
        {
            a.Scale *= b.Scale;
            a.Rotate *= b.Rotate;
            a.Translate *= b.Translate;

            return a;
        }
    }
    internal class VectorsListMagment
    {
        List<SRTVector> m_ListVectors;
        public VectorsListMagment()
        {
            m_ListVectors = new List<SRTVector>();
        }
        public void Add(Vector3 scale, Vector3 rotate, Vector3 translate)
        {
            m_ListVectors.Add(new SRTVector(scale, rotate, translate));
        }
        public void Add()
        {
            Add(new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0));
        }
        public void SetScale(Vector3 scale)
        {
            m_ListVectors = new List<SRTVector>(m_ListVectors.Select(v => new SRTVector
            {
                Scale = scale //only change this part
            }).ToList());
        }
        public void SetRotate(Vector3 rotate)
        {
            m_ListVectors = new List<SRTVector>(m_ListVectors.Select(v => new SRTVector
            {
                Rotate = rotate //only change this part
            }).ToList());
        }
        public void SetTranslate(Vector3 translate)
        {
            m_ListVectors = new List<SRTVector>(m_ListVectors.Select(v => new SRTVector
            {
                Translate = translate //only change this part
            }).ToList());
        }
        public bool Exitst()
        {
            if (m_ListVectors.Count == 0)
                return false;
            return true;
        }
        public SRTVector GetLast()
        {
            return GetAll();
        }
        public List<SRTVector> GetList()
        {
            return m_ListVectors;
        }
        public SRTVector GetAll()
        {
            if (!Exitst()) return new SRTVector();
            return m_ListVectors.Last();
        }
        public Vector3 GetScale()
        {
            return GetAll().Scale;
        }
        public Vector3 GetRotate()
        {
            return GetAll().Rotate;
        }
        public Vector3 GetTranslate()
        {
            return GetAll().Translate;
        }
        public void Pop()
        {
            m_ListVectors.RemoveAt(m_ListVectors.Count - 1);
        }
    }
}
