using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Challenge_JaeEun.Character.Job_Folder;


namespace TextRPG.Challenge_JaeEun.Character
{
    public class NewCharacter
    {
        public string Name;
        public Job SelectedJob;

        public NewCharacter(string name, Job job)
        {
            Name = name;
            SelectedJob = job;
        }
    }

}
