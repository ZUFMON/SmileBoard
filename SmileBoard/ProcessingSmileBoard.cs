using Smile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smile
{

    public class ProcessingSmileBoard
    {
        DefinitionBoardSize def;

        public ProcessingSmileBoard()
        {
            def = new DefinitionBoardSize(3, 3, InitCards());
            def.Run();
        }

        private PackCards InitCards()
        {
            // Card X x Y
            PackCards pc = new PackCards();
            pc.AddCardToPack(new CardDefinition() // Card 1x1
            {
                Top = new ImageOnCard() { Smile = TypeSmile.Mount, Color = TypeColorSmile.Red },
                Right = new ImageOnCard() { Smile = TypeSmile.Mount, Color = TypeColorSmile.Yellow },
                Bottom = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Red },
                Left = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Green },
                Position = 1

            });

            pc.AddCardToPack(new CardDefinition() // Card 2x1
            {
                Top = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Blue },
                Right = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Yellow },
                Bottom = new ImageOnCard() { Smile = TypeSmile.Mount, Color = TypeColorSmile.Blue },
                Left = new ImageOnCard() { Smile = TypeSmile.Mount, Color = TypeColorSmile.Green },
                Position = 2
            });

            pc.AddCardToPack(new CardDefinition() // Card 3x1
            {
                Top = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Red },
                Right = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Yellow },
                Bottom = new ImageOnCard() { Smile = TypeSmile.Mount, Color = TypeColorSmile.Blue },
                Left = new ImageOnCard() { Smile = TypeSmile.Mount, Color = TypeColorSmile.Yellow },
                Position = 3

            });

            pc.AddCardToPack(new CardDefinition() // Card 1x2
            {
                Top = new ImageOnCard() { Smile = TypeSmile.Mount, Color = TypeColorSmile.Red },
                Right = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Blue },
                //Right = new ImageOnCard() { Smile = TypeSmile.Mount, Color = TypeColorSmile.Blue }, // is correct to finish picture

                Bottom = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Green },
                Left = new ImageOnCard() { Smile = TypeSmile.Mount, Color = TypeColorSmile.Red },
                Position = 4
            });

            pc.AddCardToPack(new CardDefinition() // Card 2x2
            {
                Top = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Blue },
                Right = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Green },
                Bottom = new ImageOnCard() { Smile = TypeSmile.Mount, Color = TypeColorSmile.Red },
                Left = new ImageOnCard() { Smile = TypeSmile.Mount, Color = TypeColorSmile.Yellow },
                Position = 5
            });

            pc.AddCardToPack(new CardDefinition() // Card 2x3
            {
                Top = new ImageOnCard() { Smile = TypeSmile.Mount, Color = TypeColorSmile.Blue },
                Right = new ImageOnCard() { Smile = TypeSmile.Mount, Color = TypeColorSmile.Yellow },
                Bottom = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Red },
                Left = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Green },
                Position = 6
            });

            pc.AddCardToPack(new CardDefinition() // Card 3x1
            {
                Top = new ImageOnCard() { Smile = TypeSmile.Mount, Color = TypeColorSmile.Blue },
                Right = new ImageOnCard() { Smile = TypeSmile.Mount, Color = TypeColorSmile.Green },
                Bottom = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Yellow },
                Left = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Blue },
                Position = 7
            });

            pc.AddCardToPack(new CardDefinition() // Card 3x2
            {
                Top = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Blue },
                Right = new ImageOnCard() { Smile = TypeSmile.Mount, Color = TypeColorSmile.Red },
                Bottom = new ImageOnCard() { Smile = TypeSmile.Mount, Color = TypeColorSmile.Blue },
                Left = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Yellow },
                Position = 8
            });

            pc.AddCardToPack(new CardDefinition() // Card 3x3
            {
                Top = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Yellow },
                Right = new ImageOnCard() { Smile = TypeSmile.Mount, Color = TypeColorSmile.Red },
                Bottom = new ImageOnCard() { Smile = TypeSmile.Mount, Color = TypeColorSmile.Green },
                Left = new ImageOnCard() { Smile = TypeSmile.Eye, Color = TypeColorSmile.Green },
                Position = 9
            });

            return pc;
        }
    }


}
