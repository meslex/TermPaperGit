using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace TermPaperForm_v1
{
    public class Book
    {
        private Book_Access type;
        private int timeToRead;
        
        //public bool WasRead { get; set; }
        public string Title { get; set; }
        public string Author { get; set; } 
        public Book_Access Type { get { return type; } }

        #region constructors
        public Book() { }

        public Book(string title, Book_Access type, int time)
        {
            Title = title;
            this.type = type;
            this.timeToRead = time;
            this.Author = "Somebody";
        }

        public Book(string title, int time)
        {
            Title = title;
            this.type = Book_Access.no_restrictions;
            this.Author = "Somebody";
            this.timeToRead = time;
        }

        public Book(string title)
        {
            Title = title;
            this.Author = "Somebody";
            this.type = Book_Access.no_restrictions;
            this.timeToRead = 1000;

        }

        public Book(string title, string author, Book_Access type, int time)
        {
            Title = title;
            this.type = type;
            this.timeToRead = time;
            this.Author = author;
        }

        public Book(string title, string author, int time)
        {
            Title = title;
            this.type = Book_Access.no_restrictions;
            this.timeToRead = time;
            this.Author = author;
        }

        public Book(string title, string author)
        {
            Title = title;
            this.type = Book_Access.no_restrictions;
            this.timeToRead = 1000;
            this.Author = author;

        }
        #endregion

        public void Read()
        {
            Thread.Sleep(timeToRead);
            //WasRead = true;
        }

        public void Read(Action<string> callback)
        {
            callback(String.Format("{0} is currently reading: {1}", Thread.CurrentThread.Name, this.Title));
            Thread.Sleep(timeToRead);
            //WasRead = true;
        }

        public override string ToString()
        {
            return string.Format("Book title: {0}; Author: {1}", Title, Author);
        } 
    }

}
