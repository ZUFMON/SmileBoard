using Smile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smile
{
    public class DefinitionBoardSize
    {
        System.Diagnostics.Stopwatch watch;

        BoardDeskAndPackCard Board;
        List<BoardDeskAndPackCard> GameBoard = new List<BoardDeskAndPackCard>();
        Stack<BoardDeskAndPackCard> BoardProcessingCollection = new Stack<BoardDeskAndPackCard>();
        public List<BoardDeskAndPackCard> maximumSolutionCardsOnBoard { get; private set; }
        public int maxNumberOfInsertedCardOnBoard { get; private set; }  // Inserted card to Board of the variable actualPositionCard is calculated from zero 0
        private int boardSizeX = 0;
        private int boardSizeY = 0;

        List<Task> TaskItems = new List<Task>();

        private void InsertCardOnBoard(int numberCardOnBoardToEnd)
        {
            while (BoardProcessingCollection.Count > 0)
            {
                for (int t = 1; t <  System.Environment.ProcessorCount * 2; t++)
                {
                    if (BoardProcessingCollection.Count == 0) continue;
                    BoardDeskAndPackCard boardAndCards = BoardProcessingCollection.Pop();
                    TaskItems.Add(Task.Factory.StartNew(() =>
                    {
                        foreach (var itm in boardAndCards.GetAllPossibilityBoardAndCardsOnNextPosition())
                        {
                            if (itm.actualPositionCard  > maxNumberOfInsertedCardOnBoard)
                            {
                                maxNumberOfInsertedCardOnBoard = itm.actualPositionCard;
                                maximumSolutionCardsOnBoard.Clear();
                            }
                            if ((itm.actualPositionCard) == maxNumberOfInsertedCardOnBoard)
                            {
                                maximumSolutionCardsOnBoard.Add(itm);
                            }
                            BoardProcessingCollection.Push(itm);
                        }
                    }));
                }
                Task.WaitAll(TaskItems.ToArray());
                TaskItems.Clear();
            }

            if (maxNumberOfInsertedCardOnBoard + 1 == (numberCardOnBoardToEnd)) // +1 -> count with zero number
            {
                this.watch.Stop();
                if (maximumSolutionCardsOnBoard.Any(im => im.CounterSkipPosition == 0))
                {
                    AppSB.Log("Winner - All card is on board. Count of solution: " + maximumSolutionCardsOnBoard.Count(im => im.CounterSkipPosition == 0).ToString() + ". Time processing: " + watch.Elapsed.TotalSeconds.ToString(), true);
                    AppSB.Log("One Board card: " + Environment.NewLine  + maximumSolutionCardsOnBoard[0].ShowBoard());
                }
                else
                {
                    AppSB.Log("Winner with several empty card(s) on Board. Count  Solution: " + maximumSolutionCardsOnBoard.Count(im => im.CounterSkipPosition != 0).ToString() + ". Time processing: " + watch.Elapsed.TotalSeconds.ToString(), true);
                    AppSB.Log("One Board card with empty card: " + Environment.NewLine + maximumSolutionCardsOnBoard[0].ShowBoard());
                }
            }
            else
            {
                this.watch.Stop(); ;
                AppSB.Log("The solution is not possible - Maximum card on board:  " + (maxNumberOfInsertedCardOnBoard + 1).ToString() + " . Calculate solution: " + maximumSolutionCardsOnBoard.Count.ToString() + " Time processing: " + watch.Elapsed.TotalSeconds.ToString(), true);
                AppSB.Log("One Board without complete all cards: " + Environment.NewLine + maximumSolutionCardsOnBoard[0].ShowBoard());

                if (System.Windows.MessageBox.Show("Continue with inserting empty Card on position [" + (maximumSolutionCardsOnBoard[0].actualPositionCard + 1).ToString() + "] on board? ", "Question", System.Windows.MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.Yes)
                {
                    if (maxNumberOfInsertedCardOnBoard + 2 >= numberCardOnBoardToEnd)
                    {
                        AppSB.Log("Is not possible to move the next position." + (maxNumberOfInsertedCardOnBoard + 1).ToString() + " . Calculate solution: " + maximumSolutionCardsOnBoard.Count.ToString() + " Time processing: " + watch.Elapsed.TotalSeconds.ToString(), true);
                    }
                    this.watch.Start();
                    maximumSolutionCardsOnBoard.ForEach(it =>
                    {
                        it.SkipPosition();
                        BoardProcessingCollection.Push(it);
                    });
                    InsertCardOnBoard(numberCardOnBoardToEnd);
                }
                this.watch.Start();
            }
        }

        /// <summary>
        /// Definition Board size 
        /// </summary>
        /// <param name="x">Count cards on coordinate x</param>
        /// <param name="y">Count cards on coordinate y</param>
        /// <param name="PackCard">Definition Card in Package, which will by put on the board.</param>
        public DefinitionBoardSize(int x, int y, PackCards PackCard)
        {
            AppSB.Log("Inicialization board [" + x.ToString() + "," + y.ToString() + "] with PackCards count [" + PackCard.Items.Count + "]");
            this.maximumSolutionCardsOnBoard = new List<BoardDeskAndPackCard>();
            this.maxNumberOfInsertedCardOnBoard = -1;
            Board = new BoardDeskAndPackCard(x, y);
            this.boardSizeX = x;
            this.boardSizeY = y;
            Board.PackCard = PackCard;
        }

        public void Run()
        {
            try
            {
                this.watch = System.Diagnostics.Stopwatch.StartNew();

                AppSB.Log("Inicialization all card on position [0,0]");
                foreach (var itm in Board.GetCardAvaibleToPosition(0, 0))
                {
                    BoardProcessingCollection.Push(itm);
                }
                this.InsertCardOnBoard(this.boardSizeX * this.boardSizeY);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Application Error: " + ex.ToString());
            }
        }
    }
}
