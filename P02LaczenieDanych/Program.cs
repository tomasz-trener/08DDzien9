using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02LaczenieDanych
{
    class Program
    {
        static void Main(string[] args)
        {
            ModelBazyDanychDataContext db = new ModelBazyDanychDataContext();

            var wynik1 =
                db.Zawodnik.Select(x => new { Zawodnik =x, Trener = db.Trener.FirstOrDefault(y=>y.id_trenera==x.id_trenera) });

            //foreach (var z in wynik1)
            //{
            //    if(z.Trener != null)
            //        Console.WriteLine(z.Zawodnik.imie + " " + z.Zawodnik.nazwisko + " - " + z.Trener.imie_t + " " + z.Trener.nazwisko_t);
            //    else
            //        Console.WriteLine(z.Zawodnik.imie + " " + z.Zawodnik.nazwisko);
            //}

            foreach (var z in wynik1)
                    Console.WriteLine(
                        z.Zawodnik.imie + " " + z.Zawodnik.nazwisko + // zawodnik
                        (z.Trener == null?"" : " - ") +  // opcjonalnie myślnik
                        z.Trener?.imie_t + " " + z.Trener?.nazwisko_t); // trener tylko gdy nie jest null


            // po dodaniu constraint

            var wynik2 = db.Zawodnik.ToArray();

            foreach (var z in wynik2)
            {
                Console.WriteLine(z.imie + " " + z.nazwisko + " - " + z.Trener?.imie_t + " " + z.Trener?.nazwisko_t);
            }


            Console.ReadKey();
        


        }
    }
}
