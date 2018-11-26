using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TermPaperForm_v1
{
    public class Library
    {
        private List<Book> regularBooks = new List<Book>();
        private List<Book> booksForReadingRoom = new List<Book>();
        private List<ReadingRoomCard> readersInReadingRoom = new List<ReadingRoomCard>();

        private string name;
        private Semaphore semaphore;
        private ILibraryOutput output;
        private object threadlock = new object();
        private const int MAX_BOOKS_PER_READER = 3; 

        public Library() { }

        public Library(string name, ILibraryOutput output)
        {
            this.name = name;
            this.output = output;
            semaphore = new Semaphore(0, 10);
            semaphore.Release(10);
        }

        public List<Book> GiveBooksForHands(Reader reader, String[] wishList)
        {
            lock (threadlock)
            {
                if(regularBooks.Count> 0)
                {
                    Book temp;
                    List<Book> books = new List<Book>();
                    for (int i = 0; i < wishList.Count(); ++i)
                    {
                        temp = regularBooks.Find(delegate (Book b)
                        {
                            return b.Title.Equals(wishList[i]);
                        });
                        if(temp != null)
                        {
                            books.Add(temp);
                            regularBooks.Remove(temp);
                            output.deleteRegularBook(temp);
                        }
                    }
                    return books;
                }
                else
                {
                    return null;
                }
            }
        }

        public ReadingRoomCard GiveBooksForReadingRoom(Reader reader, String[] wishList)
        {
            lock (threadlock)
            {
                if (booksForReadingRoom.Count > 0)
                {
                    Book temp;
                    List<Book> books = new List<Book>();
                    for (int i = 0; i < wishList.Count(); ++i)
                    {
                        temp = booksForReadingRoom.Find(delegate (Book b)
                        {
                            return b.Title.Equals(wishList[i]);
                        });
                        if (temp != null)
                        {
                            books.Add(temp);
                            output.deleteBookForReadingRoom(temp);
                            booksForReadingRoom.Remove(temp);
                        }
                    }
                    if (books.Count > 0)//костыль
                    {
                        ReadingRoomCard card = new ReadingRoomCard(reader, books);
                        //readersInReadingRoom.Add(card);
                        reader.Card = card;
                        output.addToReadingRoom(reader);                    
                        return card;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        public void ReadingRoom(Reader reader, Action<string> callback)
        {
            lock (threadlock)
            {
                semaphore.WaitOne();

                //while (true)//eternal hell unless...
                {
                    if (reader.Card == null)
                    {
                        return;
                    }
                    /*foreach (Book b in reader.Card)
                    {
                        b.Read(reader.PrintToLog);

                        Librarian(reader, b);
                        if (reader.Card == null)
                        {
                            break;
                        }
                    }*/
                    Book temp;
                    while (true)
                    {
                        temp = reader.Card.PopUp();
                        if (temp != null)
                        {
                            temp.Read(reader.PrintToLog);
                            Librarian(reader, temp);
                            if (reader.Card == null)
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }



                    //break;
                }
            }
            semaphore.Release();
        }

        private bool Librarian(Reader reader, Book book)//так у каждого читателя есть карточка ну как есть когда он берет книги для читательного зала она создается
        {
            lock (threadlock)
            {
                reader.Card.getBooks().Remove(book);
                booksForReadingRoom.Add(book);
                output.addBookForReadingRoom(book);
                output.deleteFromReadingRoom(reader);
                if (reader.Card.getBooks().Count == 0)
                {
                    reader.Card = null;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void TakeBook(Book book)// принимает возвращенные книги(те которые были выданы на руки)
        {
            lock (threadlock)
            {
                regularBooks.Add(book);
                output.addRegularBook(book);
            }
        }

        #region direct manupilation with books arrays
        public void AddRegularBook(Book b)
        {
            lock (threadlock)
            {
                regularBooks.Add(b);
            }
        }

        public void AddRegularBook(Book[] b)
        {
            lock (threadlock)
            {
                regularBooks.AddRange(b);
            }
        }

        public void AddBookForReadingRoom(Book b)
        {
            lock (threadlock)
            {
                booksForReadingRoom.Add(b);
            }
        }

        public void AddBookForReadingRoom(Book[] b)
        {
            lock (threadlock)
            {
                booksForReadingRoom.AddRange(b);
            }
        }

        public List<Book> GetListOfRegularBooks()
        {
            return regularBooks;
        }

        public List<Book> GetReadingRoomBooks()
        {
            return booksForReadingRoom;
        }
        #endregion

    }
}
