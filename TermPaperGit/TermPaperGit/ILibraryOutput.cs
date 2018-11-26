using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermPaperForm_v1
{
    public interface ILibraryOutput
    {
        //Методы для обратного взаимодействия с библиотекой
        /*void addReader(Reader reader);
        void deleteReader(Reader reader);
        void addReaderToReadingRoom(Reader reader);
        void deleteReaderFormReadingRoom(Reader reader);
        void addBook(Book book);
        void deleteBook(Book book);
        void addBookForReadingRoom(Book book);
        void deleteBookForReadingRoom(Book book);*/

        //Методы для взаимодействия с формой
        //void addReaderToOutput(Reader reader);
        //void deleteReaderFromOutput(Reader reader);

        void addToReadingRoom(Reader reader);//reading room manipulations
        void deleteFromReadingRoom(Reader reader);

        void addRegularBook(Book book);//book list
        void deleteRegularBook(Book book);

        void addBookForReadingRoom(Book book);
        void deleteBookForReadingRoom(Book book);

    }
}
