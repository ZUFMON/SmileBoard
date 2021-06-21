using Smile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smile
{
    [Serializable]
    public class PackCards
    {
        public List<CardDefinition> Items = new List<CardDefinition>();

        public void AddCardToPack(CardDefinition card)
        {
            this.Items.Add(card);
        }

        public void RemoveCard(CardDefinition card)
        {
            this.Items.Remove(card);
        }
    }
}
