using System;
using System.Collections.Generic;

namespace Digimon.Digimon.Scripts.Applications.Static
{
    public sealed class PictorialBookEntity
    {
        public readonly Dictionary<Enums.MonsterName, bool> Book;

        public PictorialBookEntity()
        {
            Book = new Dictionary<Enums.MonsterName, bool>();
            foreach (Enums.MonsterName name in Enum.GetValues(typeof(Enums.MonsterName)))
            {
                Book.Add(name, false);
            }

            Book[Enums.MonsterName.Baby] = true;
        }

        public void Evolution(Enums.MonsterName name)
        {
            Book[name] = true;
        }
    }
}