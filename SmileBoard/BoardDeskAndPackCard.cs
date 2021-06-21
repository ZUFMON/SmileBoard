using Smile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smile
{
    [Serializable]
    public class BoardDeskAndPackCard
    {
        [System.Xml.Serialization.XmlArray]
        internal CardDefinition[,] boardField;
        [System.Xml.Serialization.XmlElement]
        public PackCards PackCard = new PackCards();

        [System.Xml.Serialization.XmlElement]
        private static int sizeX;
        [System.Xml.Serialization.XmlElement]
        private static int sizeY;
        [System.Xml.Serialization.XmlElement]
        public int actualPositionCard = 0; // start as 0 x 0
        public int CounterSkipPosition = 0;

        public int ActualX { get { return actualPositionCard % (sizeX + 1); } }
        public int ActualY { get { return actualPositionCard / (sizeY + 1); } }

        public BoardDeskAndPackCard(int x, int y)
        {
            boardField = new CardDefinition[x, y];
            sizeX = boardField.GetUpperBound(0);
            sizeY = boardField.GetUpperBound(1);
        }

        public IEnumerable<BoardDeskAndPackCard> GetAllPossibilityBoardAndCardsOnNextPosition()
        {
            actualPositionCard++;
            if (this.ActualX > sizeX || this.ActualY > sizeY) return new List<BoardDeskAndPackCard>();// { this };  // position on board is on end board
            var ac = this.GetCardAvaibleToPosition(this.ActualX, this.ActualY);
            return ac;
        }

        public void SkipPosition()
        {
            this.CounterSkipPosition++;
        }

        public IEnumerable<BoardDeskAndPackCard> GetCardAvaibleToPosition(int x, int y)
        {
            var resultValue = new List<BoardDeskAndPackCard>();
            var listCardInserted = new List<CardDefinition>();

            for (int ind = 0; ind < this.PackCard.Items.Count; ind++)
            {
                {
                    CardDefinition card = this.PackCard.Items[ind];
                    var rotationCard = (CardDefinition)card.DeepCopy();
                    for (int i = 0; i < CardDefinition.ROTATION_MOVE; i++)
                    {
                        // Rotation card  "all item (Smile and mount) in card is rotation around"
                        rotationCard = i == 0 ? rotationCard : CardDefinition.RotationCardToRight(rotationCard);
                        if (this.IsPossiblePutCardOnBoard(x, y, rotationCard))
                        {
                            if (listCardInserted.Any<CardDefinition>(c => c.IsTheSameCard(rotationCard)) == false) // optimalization (insert evenly card are ignore)
                            {
                                var cloneBoard = (BoardDeskAndPackCard)this.Clone();
                                cloneBoard.boardField[x, y] = rotationCard;
                                cloneBoard.PackCard.RemoveCard(card);
                                listCardInserted.Add(rotationCard);
                                resultValue.Add(cloneBoard);
                            }
                        }
                    }
                }
            };
            return resultValue;
        }

        private bool IsPossiblePutCardOnBoard(int x, int y, CardDefinition card)
        {
            // Card of upper
            if (y < sizeY && (this.boardField[x, y + 1] != null))
                if (card.Compare(this.boardField[x, y + 1], TypePositionImages.Top) == false) return false;
            // Card of Right
            if (x < sizeX && (this.boardField[x + 1, y] != null))
                if (card.Compare(this.boardField[x + 1, y], TypePositionImages.Right) == false) return false;
            // Card of bottom
            if (y > 0 && (this.boardField[x, y - 1] != null))
                if (card.Compare(this.boardField[x, y - 1], TypePositionImages.Bottom) == false) return false;
            // Card of Left
            if (x > 0 && (this.boardField[x - 1, y] != null))
                if (card.Compare(this.boardField[x - 1, y], TypePositionImages.Left) == false) return false;
            return true;
        }

        public bool IsPossibleInsertCardToNextPosition()
        {
            if (this.ActualX == sizeX && this.ActualY == sizeY) return false;
            foreach (var card in this.PackCard.Items)
            {
                try
                {
                    this.actualPositionCard++;
                    if (IsPossiblePutCardOnBoard(this.ActualX, this.ActualY, card) == true)
                    {
                        return true;
                    }
                }
                finally
                {
                    this.actualPositionCard--;
                }

            }
            return false;
        }


        public bool InsertCardToBoard(int x, int y, CardDefinition card)
        {
            if ((x < 0 || x > sizeX) || (y < 0 || y > sizeY)) throw new IndexOutOfRangeException("Insert Card on Board is out the area X. Range X is defined as [X,Y] [" + sizeX + "," + sizeY + "] inserted position is [" + x.ToString() + "," + y.ToString() + "]. Position is calculated from 0!");
            if (boardField[x, y] != null) throw new Exception("The field on the board is ocupied. Position [" + x.ToString() + "," + y.ToString() + "]. Position is calculated from 0!");
            return (IsPossiblePutCardOnBoard(x, y, card));
        }

        public BoardDeskAndPackCard Clone()
        {
            return (BoardDeskAndPackCard)this.DeepCopy();
        }

        public string ShowBoard()
        {
            StringBuilder str = new StringBuilder(777);
            for (int x = 0; x <= this.boardField.GetUpperBound(0); x++)
            {
                for (int y = 0; y <= this.boardField.GetUpperBound(1); y++)
                {
                    str.Append("Coordinate [" + x.ToString() + "," + y.ToString() + "]: ");
                    if (this.boardField[x,y] != null)
                    {
                        str.Append(this.boardField[x, y].ToString() + " ID card: " + this.boardField[x,y].Position.ToString());
                    }
                    else
                    {
                        str.Append(" Card is empty!");
                    }
                    str.AppendLine();
                }
            }
            return str.ToString();
        }
    }
}
