using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermPaperForm_v1
{
    public class ReadingRoomCard: IEnumerable
    {
        public int ID { get; set; }
        public Reader Reader { get; set; }

        private List<Book> books;

        public ReadingRoomCard(Reader r, List<Book> books)
        {
            this.ID = r.ID;
            this.Reader = r;
            this.books = books;
        }

        public List<Book> getBooks()
        {
            return books;
        }

        public override string ToString()
        {
            return String.Format("ID:{0};Readers Name:{1}; HasBooks:{2}",ID.ToString(), Reader.Name, books.Count > 0 );
        }

        public Book this[int i]
        {
            get { return (Book)books[i]; }
            set { books[i] = value; }
        }

        public void ClearBooks()
        {
            books.Clear();
        }

        public int Count
        {
            get { return books.Count; }
        }

        public Book PopUp()
        {
            if(books.Count > 0)
            {
                return books[0];
            }
            else
            {
                return null;
            }
        }

        public Book[] ToArray()
        {
            return books.ToArray();
        }

        public IEnumerator GetEnumerator()
        {
            return books.GetEnumerator();
        }
    }
}
