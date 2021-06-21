using Smile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smile
{
    [Serializable]
    public class CardDefinition : ICloneable
    {
        public const int ROTATION_MOVE = 4;
        public ImageOnCard Top { get; set; }
        public ImageOnCard Right { get; set; }
        public ImageOnCard Bottom { get; set; }
        public ImageOnCard Left { get; set; }

        [System.Xml.Serialization.XmlElement]
        public int Position { get; set; }

        public CardDefinition() { }
        public CardDefinition(ImageOnCard Top, ImageOnCard Right, ImageOnCard Bottom, ImageOnCard Left, int Position)
        {
            this.Top = Top;
            this.Left = Left;
            this.Right = Right;
            this.Bottom = Bottom;
            this.Position = Position;
        }

        public override string ToString()
        {
            return "T:" + Top.ToString() + " R:" + Right.ToString() + " B:" + Bottom.ToString() + " L:" + Left.ToString();
        }

        /// <summary>
        /// Rotation actual card to Right
        /// </summary>
        /// <param name="card"></param>
        public static CardDefinition RotationCardToRight(CardDefinition c)
        {
            ImageOnCard temp = c.Top;
            c.Top = c.Right;
            c.Right = c.Bottom;
            c.Bottom = c.Left;
            c.Left = temp;
            return c;
        }


        /// <summary>
        /// Compare Actual Card with Card of the location
        /// </summary>
        /// <param name="CompareCard">Comapred Card</param>
        /// <param name="PositionCard">Compare Actual Card on Position</param>
        /// <returns>Return True if the card is connected</returns>
        public bool Compare(CardDefinition CompareCard, TypePositionImages PositionCard)
        {
            // Card of upper
            // upper  if (y < sizeY && (this.boardField[x, y + 1] != null)) isPosible = card.Compare(this.boardField[x, y + 1], TypePositionImages.Top) ? isPosible : false;
            switch (PositionCard)
            {
                case TypePositionImages.Top:
                    return this.Top.IsConnect(CompareCard.Bottom);
                case TypePositionImages.Right:
                    return this.Right.IsConnect(CompareCard.Left);
                case TypePositionImages.Bottom:
                    return this.Bottom.IsConnect(CompareCard.Top);
                case TypePositionImages.Left:
                    return this.Left.IsConnect(CompareCard.Right);
                default:
                    throw new Exception("The Compare is not defined! Please check the compare Card Procedure. Value [" + PositionCard.ToString() + "]");
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is CardDefinition)
            {
                var c = (CardDefinition)obj;
                return this.Top == c.Top && this.Right == c.Right && this.Bottom == c.Bottom && this.Left == c.Left;
            }
            return false;
        }

        internal bool IsTheSameCard(CardDefinition RotationCard)
        {
            if (this != RotationCard) return false;

            var rtCard = (CardDefinition)RotationCard.Clone();
            for (int i = 0; i < CardDefinition.ROTATION_MOVE - 1; i++)
            {
                rtCard = CardDefinition.RotationCardToRight(rtCard);
                if (rtCard != this) return false;
            }
            return true;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public enum TypePositionImages
    {
        Top,
        Left,
        Bottom,
        Right
    }
}
