using Microsoft.VisualStudio.TestTools.UnitTesting;
using Smile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class SmileBoardTest
    {

        
        [TestMethod]
        public void MyTestBoardAndCardWithAllCardCompletedBoard()
        {
            try
            {
                AppSB.showMsgLog = TypeShowLog.Console;
                var b = new DefinitionBoardSize(3, 3, this.InitCards());
                b.Run();
                Assert.AreEqual<int>(9, b.maxNumberOfInsertedCardOnBoard + 1, "All card is inserted to board."); //  +  2 = 1 -> ( calculate from zero) , 1 -> ( inserted card is calclulate from 0)

                List<int> position = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                for (int p = 0; p < 9; p++)
                {
                    CardDefinition c = b.maximumSolutionCardsOnBoard[0].boardField[p % 3, p / 3];
                    position[(c.Position - 1)] = -1;
                };

                Assert.AreEqual<int>(position.Count(c=>c>-1), 0);

            }
            catch (Exception ex)
            {
                Assert.Fail("Ex: " + ex.ToString());
            }
        }

        private PackCards InitCards()
        {
            // Card X x Y
            PackCards pc = new PackCards();
            pc.AddCardToPack(new CardDefinition() // Card 1x1
            {
                Top = new ImageOnCard() { Smile = TypeSmile.Mount, Color = TypeColorSmile.Red },
                Right = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Blue },
                Bottom = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Yellow },
                Left = new ImageOnCard() { Smile = TypeSmile.Mount, Color = TypeColorSmile.Green },
                Position = 1
            });

            pc.AddCardToPack(new CardDefinition() // Card 2x1
            {
                Top = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Blue },
                Right = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Red },
                Bottom = new ImageOnCard() { Smile = TypeSmile.Mount, Color = TypeColorSmile.Green },
                Left = new ImageOnCard() { Smile = TypeSmile.Mount, Color = TypeColorSmile.Blue },
                Position = 2
            });

            pc.AddCardToPack(new CardDefinition() // Card 3x1
            {
                Top = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Red },
                Right = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Yellow },
                Bottom = new ImageOnCard() { Smile = TypeSmile.Mount, Color = TypeColorSmile.Yellow },
                Left = new ImageOnCard() { Smile = TypeSmile.Mount, Color = TypeColorSmile.Red },
                Position = 3
            });

            pc.AddCardToPack(new CardDefinition() // Card 1x2
            {
                Top = new ImageOnCard() { Smile = TypeSmile.Mount, Color = TypeColorSmile.Yellow },
                Right = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Yellow },
                Bottom = new ImageOnCard() { Smile = TypeSmile.Mount, Color = TypeColorSmile.Yellow },
                Left = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Red },
                Position = 4
            });

            pc.AddCardToPack(new CardDefinition() // Card 2x2
            {
                Top = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Green },
                Right = new ImageOnCard() { Smile = TypeSmile.Mount, Color = TypeColorSmile.Blue },
                Bottom = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Blue },
                Left = new ImageOnCard() { Smile = TypeSmile.Mount, Color = TypeColorSmile.Yellow },
                Position = 5
            });

            pc.AddCardToPack(new CardDefinition() // Card 2x3
            {
                Top = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Yellow },
                Right = new ImageOnCard() { Smile = TypeSmile.Mount, Color = TypeColorSmile.Red },
                Bottom = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Red },
                Left = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Blue },
                Position = 6
            });

            pc.AddCardToPack(new CardDefinition() // Card 3x1
            {
                Top = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Yellow },
                Right = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Green },
                Bottom = new ImageOnCard() { Smile = TypeSmile.Mount, Color = TypeColorSmile.Blue },
                Left = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Blue },
                Position = 7
            });

            pc.AddCardToPack(new CardDefinition() // Card 3x2
            {
                Top = new ImageOnCard() { Smile = TypeSmile.Mount, Color = TypeColorSmile.Blue },
                Right = new ImageOnCard() { Smile = TypeSmile.Mount, Color = TypeColorSmile.Red },
                Bottom = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Yellow },
                Left = new ImageOnCard() { Smile = TypeSmile.Mount, Color = TypeColorSmile.Green },
                Position = 8
            });

            pc.AddCardToPack(new CardDefinition() // Card 3x3
            {
                Top = new ImageOnCard() { Smile = TypeSmile.Mount, Color = TypeColorSmile.Red },
                Right = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Green },
                Bottom = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Yellow },
                Left = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Red },
                Position = 9
            });
            return pc;
        }
    }
}
