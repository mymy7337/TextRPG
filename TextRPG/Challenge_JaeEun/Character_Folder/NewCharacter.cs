using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Challenge_JaeEun.Character.Job;

namespace TextRPG.Challenge_JaeEun.Character
{
    public class NewCharacter
    {
        public string Name;
        public CharacterJob SelectedJob;

        public NewCharacter(string name, CharacterJob job)
        {
            Name = name;
            SelectedJob = job;
        }
    }

}
