using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P03OperacjeCRUD
{
    class Program
    {
        static void Main(string[] args)
        {
            ModelBazyDanychDataContext db = new ModelBazyDanychDataContext();

            // dodawanie

            // Zawodnik nowy = new Zawodnik();
            // nowy.imie = "Jan";
            // nowy.nazwisko = "Kowalski";
            // nowy.kraj = "POL";
            // nowy.data_ur = DateTime.Now;

            // db.Zawodnik.InsertOnSubmit(nowy);
            // //    db.Zawodnik.InsertOnSubmit(nowy2);
            //// db.Zawodnik.InsertAllOnSubmit(tablica..)
            // db.SubmitChanges();

            // // edycja 

            // // krok 1 pobranie zawodnika, którego cchemy edytować
            // // krok 2 zmiana odpowiednich pól
            // // krok 3 submitchanges

            // Zawodnik doEdycji = db.Zawodnik.FirstOrDefault(x => x.id_zawodnika == 29);
            // doEdycji.nazwisko = "Nowak";
            // db.SubmitChanges();


            // usuwanie 
            // pobieramy tego, którego chcemy usunąc i submit..

            Zawodnik doUsuniecia = db.Zawodnik.FirstOrDefault(x => x.id_zawodnika==29);
            db.Zawodnik.DeleteOnSubmit(doUsuniecia);
            db.SubmitChanges();


        }
    }
}
