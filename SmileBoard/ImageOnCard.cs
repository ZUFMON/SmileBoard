using Smile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smile
{
    [Serializable]
    public struct ImageOnCard
    {
        public TypeSmile Smile;
        public TypeColorSmile Color;

        public bool IsConnect(ImageOnCard img)
        {
            return img.Color == this.Color && img.Smile != this.Smile;
        }


        public static bool operator !=(ImageOnCard c1, ImageOnCard c2)
        {
            return !(c1 == c2);
        }

        public static bool operator ==(ImageOnCard c1, ImageOnCard c2)
        {
            return c1.Color == c2.Color && c1.Smile == c2.Smile;
        }

        public override string ToString()
        {
            return Smile.ToString() + " " + Color.ToString();
        }
    }

    public enum TypeColorSmile
    {
        Yellow,
        Blue,
        Green,
        Red
    }

    public enum TypeSmile
    {
        Eye,
        Mount,
    }
}
