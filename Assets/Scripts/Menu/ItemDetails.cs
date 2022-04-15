using System;

namespace Menu
{
    [Serializable]
    public class ItemDetails
    {
        public string name;
        public int cost;
        public bool unlocked;

        public ItemDetails(string name, int cost, bool unlocked)
        {
            this.name = name;
            this.cost = cost;
            this.unlocked = unlocked;
        }
    }
}