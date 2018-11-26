using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TermPaperForm_v1
{
    public class Reader
    {
        private List<Book> regularBooks = new List<Book>();
        //private List<Book> booksForReadingRoom = new List<Book>();
        private List<string> booksTitles = new List<string>();
        private List<string> booksThatProbablyShouldBeInReadingRoom = new List<string>();
        //private ReadingRoomCard card;

        private bool alive;
        private string name;
        private int id;
        private Library library;
        private Thread life;
        private Object threadLock = new object();
        private int day;
        private const int MAX_AMOUNT_OF_BOOKS_TO_REQUEST = 5;

        private string outputPointer;
        private RichTextBox output;
        private String log;
        private Form1 form;

        public string Name { get { return name; } }
        public int ID { get { return id; }}
        public String Log { get { return log; } }
        public Locations Location { get; set; }
        public Thread Life { get { return life; } }
        public ReadingRoomCard Card { get; set; }


        public Reader() { }

        public Reader(string name, int id, Library localLibrary, Form1 form, string outputP, string[] regularTitles, string[] readingRoomTitles)
        {
            this.name = name;
            this.id = id;
            this.alive = true;
            this.library = localLibrary;
            this.form = form;
            day = 0;// keeping days
            booksTitles.AddRange(regularTitles);
            booksThatProbablyShouldBeInReadingRoom.AddRange(readingRoomTitles);
            ChangeOutput(outputP, form);
            life = new Thread(new ThreadStart(LifeCircle));
            life.Name = this.Name;
            life.Start();
        }

        #region Bunch of code for printing
        private RichTextBox EstablishOutput(Form1 form)
        {      
            Control[] textBox = ((Form1)form).Controls.Find(outputPointer, true);
            return (RichTextBox)textBox[0];
            
        }

        public void ChangeOutput(string str)
        {
            this.outputPointer = str;
            this.output = EstablishOutput(this.form);
        }

        public void ChangeOutput(string str, Form1 f)
        {
            this.outputPointer = str;
           this.output = EstablishOutput(f);
        }

        public void PrintToLog(string str)
        {
            log += str + '\n';
            Print(str);
        }

        public string ConvertArrayToString(string[] str)
        {
            string result = "\n";
            foreach(string s in str)
            {
                result += "* " + s+"\n";
            }
            return result;
        }

        public string ConvertArrayToString<T>(T[] str)
        {
            string result = "";
            foreach (T s in str)
            {
                result += "\n* " + s.ToString();
            }
            return result;
        }

        public void Print(string str)
        {
            if (output != null)
            {
                (form).Invoke((Action)delegate
                {
                    output.Text += str + '\n';
                });
            }
            else
            {
                throw new Exception();
            }
        }

        /*private void PrintState()
        {
            lock (threadLock)
            {
                if (this.regularBooks.Count > 0)
                {
                    Print(String.Format("Books that reader has 'on hands':"));
                    foreach (Book b in regularBooks)
                    {
                        //Console.WriteLine(b.ToString());
                        PrintToLog(b.ToString());
                        Print(b.ToString());
                    }
                }
                if (this.booksForReadingRoom.Count > 0)
                {
                    Print(String.Format("Books that reader will read in 'reading room':"));
                    foreach (Book b in booksForReadingRoom)
                    {
                        Console.WriteLine(b.ToString());//
                        Print(b.ToString());
                        PrintToLog(b.ToString());
                    }
                }
                //Print(String.Format("", Name));
            }
        }*/
        #endregion

        private void LifeCircle()
        {
            //output = EstablishOutput(form);
            Random r = new Random();
            PrintToLog(String.Format("Reader: {0} was born very curious, so since the birthday he is going to library, yeah strange, i know...",this.Name));
            while (alive)
            {
                //Print(String.Format("\n** Day #{0} for reader:{1} ***\n",day.ToString(), Name));
                PrintToLog(String.Format("\n*** Day #{0} for  our precious {1} ***", day.ToString(), Name));

                PrintToLog("***Reader is going to spend some time in reading room.***");
                if (RequestBooksReadingRoom(MakeWishList(booksThatProbablyShouldBeInReadingRoom.ToArray())))
                {
                    PrintToLog("***Reader has gone to reading room,***");
                    library.ReadingRoom(this, PrintToLog);
                    PrintToLog("***Reader has left Reading room.***");
                }
                else
                {
                    PrintToLog("Unfortunately none of books for reading room are currently available");
                }
                if (RequestRegularBooks(MakeWishList(booksTitles.ToArray())))//метод возвращает true если получилось что-то вернуть
                {
                    //Print(String.Format("{0} has gone home to read some books!", Name));
                    PrintToLog("***Reader is going to read his beloved books***");
                    ReadRegularBooks();
                    PrintToLog("***Reader has read all his books and he is going to return them***");
                    ReturnSomeBooks();
                    PrintToLog("***All returned***");
                    //PrintState();
                }
                else
                {
                    PrintToLog("Unfortunately none of requested books are currently available");
                }
                //Print(String.Format("*** Another good day ended for reader:{0} ***", Name));
                //this.regularBooks.Clear();// в конце дня мы все прочитали очищаем наши списки
                //this.booksForReadingRoom.Clear();

                ++day;
                Form1.mre.WaitOne();
            }
        }

        private bool RequestRegularBooks(String[] wishList)
        {
            if(library !=null){
                //Print(String.Format("{1} requested {0} books from library ;", wishList.Count(), Name));
                //PrintToLog(String.Format("{1} requested {0} books from library ;", wishList.Count(), Name));
                List<Book> temp = library.GiveBooksForHands(this, wishList);
                if (temp.Count()>0)
                {
                    regularBooks.AddRange(temp);
                    if(temp.Count == wishList.Length)
                    {
                        PrintToLog(String.Format("Our reader had requested a few books:{0}and library has given them to him.", ConvertArrayToString(wishList)));
                    }
                    else
                    {
                        PrintToLog(String.Format("Our reader had requested a few books:{0}but he was given only those:{1}",
                            ConvertArrayToString(wishList), ConvertArrayToString<Book>(temp.ToArray())));
                    }
                    return true;
                }
                else
                {
                    //Print(String.Format("Library can't give requested books.", Name));
                    PrintToLog(String.Format("Library can't give requested books.", Name));
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool RequestBooksReadingRoom(string[] wishlist)
        {
            if (library != null)
            {
                //Print(String.Format("{1} requested {0} books to read in reading room;", wishlist.Count(), Name));
                Card = library.GiveBooksForReadingRoom(this, wishlist);
                if (Card !=null)
                {
                    if (Card.Count == wishlist.Length)
                    {
                        PrintToLog(String.Format("Our reader had requested a few books:{0}and library has allowed him to read them.", ConvertArrayToString(wishlist)));
                    }
                    else
                    {
                        PrintToLog(String.Format("Alas, our reader had requested a few books:{0}but he was given only those:{1}",
                            ConvertArrayToString(wishlist), ConvertArrayToString<Book>(Card.ToArray())));
                    }
                    return true;
                }
                else
                {
                    //Print(String.Format("Library can't allow to requested books.", Name));
                    PrintToLog(String.Format("Library can't allow to read requested books.", Name));
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void ReadRegularBooks()
        {
            foreach(Book b in regularBooks)
            {
                b.Read(PrintToLog);
                //PrintToLog(String.Format("Reader is currently reading: {0}", b.Title));
            }
        }

        public void ReturnBook(Book b)
        {
            if (library != null)
            {
                library.TakeBook(b);
            }
            
        }

        public void ReturnSomeBooks()
        {
            foreach (Book b in regularBooks)
            {
                ReturnBook(b);
            }
            this.regularBooks.Clear();
        }

        private string[] MakeWishList(string[] books)
        {
            Random r = new Random();
            int amount = r.Next(1, MAX_AMOUNT_OF_BOOKS_TO_REQUEST);
            string[] result = new string[amount];
            books = Shuffle(books);
            for(int i =0; i < amount; ++i)
            {
                result[i] = books[i];
            }
            return result;
        }

        private string[] Shuffle(string[] data)
        {
            Random r = new Random();
            for (int i = (data.Length - 1); i >= 1; i--)
{
                int j = r.Next(i + 1);
                // обменять значения data[j] и data[i]
                var temp = data[j];
                data[j] = data[i];
                data[i] = temp;
            }
            return data;
        } 
    }
}
