using System;
using System.Collections.Generic;
using System.Text;

namespace StackOverflowPostApp
{
    internal class StackOverflowPost
    {
        public StackOverflowPost(string title, string description)
        {
            Title = title;
            Description = description;
            DateCreated = DateTime.Now;
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime DateCreated { get; private set; }
        public int Vote { get; private set; }

        public void UpVote()
        {
            Vote++;
        }
        public void DownVote()
        {
            Vote--;
        }
    }
}
