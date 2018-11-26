 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TermPaperForm_v1
{

    public partial class Form1 : Form, ILibraryOutput
    {
        Library library;
        public static ManualResetEvent mre = new ManualResetEvent(false);
        List<Reader> readers;
        private object threadlock = new object();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)// can design be better?
        {
            readers = new List<Reader>();
            library = new Library("Saint Peter Library", this);

            #region adding books 
            library.AddRegularBook(new Book("Don Quixote", Book_Access.no_restrictions, 2000));
            library.AddRegularBook(new Book("Ulysses", Book_Access.no_restrictions, 1000));
            library.AddRegularBook(new Book("In Search of Lost Time", Book_Access.librabry_only, 1000));
            library.AddRegularBook(new Book("The Great Gatsby", Book_Access.librabry_only, 1000));
            library.AddRegularBook(new Book("Moby Dick", Book_Access.no_restrictions, 2000));
            library.AddRegularBook(new Book("Hamlet", Book_Access.no_restrictions, 1000));
            library.AddRegularBook(new Book("War and Peace", Book_Access.librabry_only, 1000));
            library.AddRegularBook(new Book("The Odyssey", Book_Access.librabry_only, 1000));
            library.AddRegularBook(new Book("One Hundred Years of Solitude", Book_Access.no_restrictions, 1000));
            library.AddRegularBook(new Book("The Divine Comedy", Book_Access.no_restrictions, 1000));
            library.AddRegularBook(new Book("The Brothers Karamazov", Book_Access.no_restrictions, 1000));
            library.AddRegularBook(new Book("Madame Bovary", Book_Access.no_restrictions, 1000));
            library.AddRegularBook(new Book("The Adventures of Huckleberry Finn", Book_Access.librabry_only, 1000));
            library.AddRegularBook(new Book("The Iliad", Book_Access.librabry_only, 1000));
            library.AddRegularBook(new Book("Crime and Punishment", Book_Access.no_restrictions, 1000));
            library.AddRegularBook(new Book("Alice's Adventures in Wonderland", Book_Access.no_restrictions, 1000));
            library.AddRegularBook(new Book("Catch-22", "Joseph Heller"));
            library.AddRegularBook(new Book("Tess of the d’Urbervilles", "Thomas Hardy"));
            library.AddRegularBook(new Book("1984", "George Orwell"));
            library.AddRegularBook(new Book("Great Expectations", "Charles Dickens"));
            library.AddRegularBook(new Book("To Kill a Mockingbird", "Harper Lee "));
            library.AddRegularBook(new Book("Wolf Hall", "Hilary Mantel"));
            library.AddRegularBook(new Book("Frankenstein", "Mary Shelley"));
            library.AddRegularBook(new Book("Lord of the Flies", "William Golding"));

            library.AddBookForReadingRoom(new Book("Jane Eyre", "Charlotte Bronte"));
            library.AddBookForReadingRoom(new Book("Dune", "Frank Herbert"));
            library.AddBookForReadingRoom(new Book("A Clockwork Orange", "Anthony Burgess"));
            library.AddBookForReadingRoom(new Book("Do Androids Dream of Electric Sheep", "Philip K Dick"));
            library.AddBookForReadingRoom(new Book("Heart of Darkness", "Joseph Conrad"));
            library.AddBookForReadingRoom(new Book("Dracula", "Bram Stoker"));
            library.AddBookForReadingRoom(new Book("The Catcher in the Rye", "JD Salinger"));
            library.AddBookForReadingRoom(new Book("The Big Sleep", "Raymond Chandler"));
            library.AddBookForReadingRoom(new Book("Vanity Fair", "William Makepeace Thackeray"));
            library.AddBookForReadingRoom(new Book("Charlie and the Chocolate Factory", "Roald Dahl"));
            library.AddBookForReadingRoom(new Book("100 Years of Solitude", "Gabriel Garcia Marquez"));
            library.AddBookForReadingRoom(new Book("The Trial", "Franz Kafka"));
            library.AddBookForReadingRoom(new Book("Anna Karenina", "Leo Tolstoy"));


            library.AddRegularBook(new Book("Don Quixote", Book_Access.no_restrictions, 2000));
            library.AddRegularBook(new Book("Ulysses", Book_Access.no_restrictions, 1000));
            library.AddRegularBook(new Book("In Search of Lost Time", Book_Access.librabry_only, 1000));
            library.AddRegularBook(new Book("The Great Gatsby", Book_Access.librabry_only, 1000));
            library.AddRegularBook(new Book("Moby Dick", Book_Access.no_restrictions, 2000));
            library.AddRegularBook(new Book("Hamlet", Book_Access.no_restrictions, 1000));
            library.AddRegularBook(new Book("War and Peace", Book_Access.librabry_only, 1000));
            library.AddRegularBook(new Book("The Odyssey", Book_Access.librabry_only, 1000));
            library.AddRegularBook(new Book("One Hundred Years of Solitude", Book_Access.no_restrictions, 1000));
            library.AddRegularBook(new Book("The Divine Comedy", Book_Access.no_restrictions, 1000));
            library.AddRegularBook(new Book("The Brothers Karamazov", Book_Access.no_restrictions, 1000));
            library.AddRegularBook(new Book("Madame Bovary", Book_Access.no_restrictions, 1000));
            library.AddRegularBook(new Book("The Adventures of Huckleberry Finn", Book_Access.librabry_only, 1000));
            library.AddRegularBook(new Book("The Iliad", Book_Access.librabry_only, 1000));
            library.AddRegularBook(new Book("Crime and Punishment", Book_Access.no_restrictions, 1000));
            library.AddRegularBook(new Book("Alice's Adventures in Wonderland", Book_Access.no_restrictions, 1000));
            library.AddRegularBook(new Book("Catch-22", "Joseph Heller"));
            library.AddRegularBook(new Book("Tess of the d’Urbervilles", "Thomas Hardy"));
            library.AddRegularBook(new Book("1984", "George Orwell"));
            library.AddRegularBook(new Book("Great Expectations", "Charles Dickens"));
            library.AddRegularBook(new Book("To Kill a Mockingbird", "Harper Lee "));
            library.AddRegularBook(new Book("Wolf Hall", "Hilary Mantel"));
            library.AddRegularBook(new Book("Frankenstein", "Mary Shelley"));
            library.AddRegularBook(new Book("Lord of the Flies", "William Golding"));

            library.AddBookForReadingRoom(new Book("Jane Eyre", "Charlotte Bronte"));
            library.AddBookForReadingRoom(new Book("Dune", "Frank Herbert"));
            library.AddBookForReadingRoom(new Book("A Clockwork Orange", "Anthony Burgess"));
            library.AddBookForReadingRoom(new Book("Do Androids Dream of Electric Sheep", "Philip K Dick"));
            library.AddBookForReadingRoom(new Book("Heart of Darkness", "Joseph Conrad"));
            library.AddBookForReadingRoom(new Book("Dracula", "Bram Stoker"));
            library.AddBookForReadingRoom(new Book("The Catcher in the Rye", "JD Salinger"));
            library.AddBookForReadingRoom(new Book("The Big Sleep", "Raymond Chandler"));
            library.AddBookForReadingRoom(new Book("Vanity Fair", "William Makepeace Thackeray"));
            library.AddBookForReadingRoom(new Book("Charlie and the Chocolate Factory", "Roald Dahl"));
            library.AddBookForReadingRoom(new Book("100 Years of Solitude", "Gabriel Garcia Marquez"));
            library.AddBookForReadingRoom(new Book("The Trial", "Franz Kafka"));
            library.AddBookForReadingRoom(new Book("Anna Karenina", "Leo Tolstoy"));


            library.AddRegularBook(new Book("Don Quixote", Book_Access.no_restrictions, 2000));
            library.AddRegularBook(new Book("Ulysses", Book_Access.no_restrictions, 1000));
            library.AddRegularBook(new Book("In Search of Lost Time", Book_Access.librabry_only, 1000));
            library.AddRegularBook(new Book("The Great Gatsby", Book_Access.librabry_only, 1000));
            library.AddRegularBook(new Book("Moby Dick", Book_Access.no_restrictions, 2000));
            library.AddRegularBook(new Book("Hamlet", Book_Access.no_restrictions, 1000));
            library.AddRegularBook(new Book("War and Peace", Book_Access.librabry_only, 1000));
            library.AddRegularBook(new Book("The Odyssey", Book_Access.librabry_only, 1000));
            library.AddRegularBook(new Book("One Hundred Years of Solitude", Book_Access.no_restrictions, 1000));
            library.AddRegularBook(new Book("The Divine Comedy", Book_Access.no_restrictions, 1000));
            library.AddRegularBook(new Book("The Brothers Karamazov", Book_Access.no_restrictions, 1000));
            library.AddRegularBook(new Book("Madame Bovary", Book_Access.no_restrictions, 1000));
            library.AddRegularBook(new Book("The Adventures of Huckleberry Finn", Book_Access.librabry_only, 1000));
            library.AddRegularBook(new Book("The Iliad", Book_Access.librabry_only, 1000));
            library.AddRegularBook(new Book("Crime and Punishment", Book_Access.no_restrictions, 1000));
            library.AddRegularBook(new Book("Alice's Adventures in Wonderland", Book_Access.no_restrictions, 1000));
            library.AddRegularBook(new Book("Catch-22", "Joseph Heller"));
            library.AddRegularBook(new Book("Tess of the d’Urbervilles", "Thomas Hardy"));
            library.AddRegularBook(new Book("1984", "George Orwell"));
            library.AddRegularBook(new Book("Great Expectations", "Charles Dickens"));
            library.AddRegularBook(new Book("To Kill a Mockingbird", "Harper Lee "));
            library.AddRegularBook(new Book("Wolf Hall", "Hilary Mantel"));
            library.AddRegularBook(new Book("Frankenstein", "Mary Shelley"));
            library.AddRegularBook(new Book("Lord of the Flies", "William Golding"));

            library.AddBookForReadingRoom(new Book("Jane Eyre", "Charlotte Bronte"));
            library.AddBookForReadingRoom(new Book("Dune", "Frank Herbert"));
            library.AddBookForReadingRoom(new Book("A Clockwork Orange", "Anthony Burgess"));
            library.AddBookForReadingRoom(new Book("Do Androids Dream of Electric Sheep", "Philip K Dick"));
            library.AddBookForReadingRoom(new Book("Heart of Darkness", "Joseph Conrad"));
            library.AddBookForReadingRoom(new Book("Dracula", "Bram Stoker"));
            library.AddBookForReadingRoom(new Book("The Catcher in the Rye", "JD Salinger"));
            library.AddBookForReadingRoom(new Book("The Big Sleep", "Raymond Chandler"));
            library.AddBookForReadingRoom(new Book("Vanity Fair", "William Makepeace Thackeray"));
            library.AddBookForReadingRoom(new Book("Charlie and the Chocolate Factory", "Roald Dahl"));
            library.AddBookForReadingRoom(new Book("100 Years of Solitude", "Gabriel Garcia Marquez"));
            library.AddBookForReadingRoom(new Book("The Trial", "Franz Kafka"));
            library.AddBookForReadingRoom(new Book("Anna Karenina", "Leo Tolstoy"));



            List<string> regularTitles = new List<string>();//Составим список  названий книг 
            foreach (Book b in library.GetListOfRegularBooks())
            {
                regularTitles.Add(b.Title);
            }

            List<string> reedingRoomTitles = new List<string>();
            foreach (Book b in library.GetReadingRoomBooks())
            {
                reedingRoomTitles.Add(b.Title);
            }
            #endregion

            foreach(Book b in library.GetListOfRegularBooks())
            {
                addRegularBook(b);
            }

            foreach(Book b in library.GetReadingRoomBooks())
            {
                addBookForReadingRoom(b);
            }

            for (int i = 1; i <= 6; ++i)
            {
                readers.Add(new Reader("reader"+i, i, library, this, "output"+i,  regularTitles.ToArray(), reedingRoomTitles.ToArray()));
                addReaderToOutput(readers[i-1]);
            }
            mre.Set();// сразу пропукаем проверку, на*** проверку
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Reader r in readers)
            {
                r.Life.Abort();
            }
        }

        public void addReaderToOutput(Reader reader)
        {
            ListViewItem temp = new ListViewItem(reader.ID.ToString());
            temp.SubItems.Add(reader.Name);
            listOfReaders.Items.Add(temp);
        }

        public void deleteReaderFromOutput(int index)
        {
            listOfReaders.Items.RemoveAt(index);
        }

        #region Reading Room Visualisation
        public void addToReadingRoom(Reader reader)
        {
            lock (threadlock)
            {
                ListViewItem temp = new ListViewItem(reader.Name);
                temp.Name = reader.Name;
                temp.SubItems.Add(reader.Card.Count.ToString());
                this.Invoke((Action)delegate
                {
                    readingRoom.Items.Add(temp);
                });
            }
        }

        public void deleteFromReadingRoom(Reader reader)
        {
            this.Invoke((Action)delegate
            {
                ListViewItem[] temp = readingRoom.Items.Find(reader.Name, false);
                if (temp.Length > 0)// костыль
                {
                    Console.WriteLine(readingRoom.ToString());
                    Console.WriteLine(temp.Length);
                    readingRoom.Items.Remove(temp[0]);
                }
            });
        }
        #endregion

        #region book list manipulations
        public void addRegularBook(Book book)
        {
            ListViewItem newItem;
            this.Invoke((Action)delegate
            {
                ListViewItem[] temp = listOfB.Items.Find(book.Title, false);
                Console.WriteLine(temp.ToString());

                if (listOfB.Items.Count == 0 || temp.Length == 0)
                {
                    newItem = new ListViewItem(book.Title);
                    newItem.Name = book.Title;
                    newItem.SubItems.Add(book.Author);
                    newItem.SubItems.Add("1");
                    listOfB.Items.Add(newItem);
                }
                else
                {
                    string devastatiouslyTemporary = temp[0].SubItems[2].Text.ToString();
                    string ultraTemp = (Int32.Parse(devastatiouslyTemporary) + 1).ToString();
                    ListViewItem.ListViewSubItem superTemp = new ListViewItem.ListViewSubItem(temp[0], ultraTemp);
                    temp[0].SubItems[2] = superTemp;
                }
            });
        }

        public void deleteRegularBook(Book book)
        {
            //throw new NotImplementedException();
            //ListViewItem newItem;
            this.Invoke((Action)delegate
            {
                ListViewItem[] temp = listOfB.Items.Find(book.Title, false);
                Console.WriteLine(temp.ToString());

                if (temp.Length != 0)
                {
                    int ultraTemp = Int32.Parse(temp[0].SubItems[2].Text);
                    if (ultraTemp > 1)
                    {
                        string devastatiouslyTemporary = (ultraTemp - 1).ToString();
                        ListViewItem.ListViewSubItem superTemp = new ListViewItem.ListViewSubItem(temp[0], devastatiouslyTemporary);
                        temp[0].SubItems[2] = superTemp;
                    }
                    else if (ultraTemp == 1)
                    {
                        listOfB.Items.Remove(temp[0]);
                    }
                }
            });
        }

        public void addBookForReadingRoom(Book book)
        {
            ListViewItem newItem;
            this.Invoke((Action)delegate
            {
                ListViewItem[] temp = listOfRB.Items.Find(book.Title, false);
                Console.WriteLine(temp.ToString());

                if (listOfB.Items.Count == 0 || temp.Length == 0)
                {
                    newItem = new ListViewItem(book.Title);
                    newItem.Name = book.Title;
                    newItem.SubItems.Add(book.Author);
                    newItem.SubItems.Add("1");
                    listOfRB.Items.Add(newItem);
                }
                else
                {
                    string devastatiouslyTemporary = temp[0].SubItems[2].Text.ToString();
                    string ultraTemp = (Int32.Parse(devastatiouslyTemporary) + 1).ToString();
                    ListViewItem.ListViewSubItem superTemp = new ListViewItem.ListViewSubItem(temp[0], ultraTemp);
                    temp[0].SubItems[2] = superTemp;
                }
            });

            Console.WriteLine(library.GetReadingRoomBooks().Count.ToString());
        }

        public void deleteBookForReadingRoom(Book book)
        {
            //throw new NotImplementedException();
            //ListViewItem newItem;
            this.Invoke((Action)delegate
            {
                ListViewItem[] temp = listOfRB.Items.Find(book.Title, false);
                Console.WriteLine(temp.ToString());

                if (temp.Length != 0)
                {
                    int ultraTemp = Int32.Parse(temp[0].SubItems[2].Text);
                    if (ultraTemp > 1)
                    {
                        string devastatiouslyTemporary = (ultraTemp - 1).ToString();
                        ListViewItem.ListViewSubItem superTemp = new ListViewItem.ListViewSubItem(temp[0], devastatiouslyTemporary);
                        temp[0].SubItems[2] = superTemp;
                        Console.WriteLine(temp.ToString());
                    }
                    else if (ultraTemp == 1)
                    {
                        listOfRB.Items.Remove(temp[0]);
                    }
                }
            });
        }
        #endregion

        #region UI
        private void addUserButton_Click(object sender, EventArgs e)
        {

        }

        private void listOfReaders_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listOfReaders.SelectedIndices.Count > 0)
            {
                ReaderLog form = new ReaderLog(readers[listOfReaders.SelectedIndices[0]]);
                form.ShowDialog();
            }
        }

        private void playPauseButton_Click(object sender, EventArgs e)
        {
            if(((Button)sender).Text == "Pause")
            {
                mre.Reset();
                ((Button)sender).Text = "Play";
            }
            else if(((Button)sender).Text == "Play")
            {
                mre.Set();
                ((Button)sender).Text = "Pause";
            }
        }

        private void changeReaderButton_Click(object sender, EventArgs e)
        {
            ChangeReaderOutput form = new ChangeReaderOutput(readers);
            if(form.ShowDialog() == DialogResult.OK)
            {

                Control[] box = this.Controls.Find(((Button)sender).Tag.ToString(), true);
                RichTextBox output = (RichTextBox)box[0];
                output.Text = form.Result.Log;
                form.Result.ChangeOutput(((Button)sender).Tag.ToString());
            }
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            readers[3].ChangeOutput("output5");
        }
    }
}
