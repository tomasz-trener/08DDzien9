using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01ORMWstep
{
    class Program
    {
        static void Main(string[] args)
        {
            ModelBazyDanychDataContext db = new ModelBazyDanychDataContext();
            Zawodnik[] zawodnicy= db.Zawodnik.ToArray();



            // znajdz tylko polakow 
            Zawodnik[] wynik1 = db.Zawodnik.Where(dowolny => dowolny.kraj == "POL" || dowolny.kraj=="GER").ToArray();

            Zawodnik[] wynik2 = db.Zawodnik.Where(x => x.kraj == "POL" && x.wzrost>180).ToArray();

            // pobranie zawodnika o konretnym ID
            Zawodnik wynik3 = db.Zawodnik.Where(x => x.id_zawodnika == 3).ToArray()[0];
            Zawodnik wynik4 = db.Zawodnik.Where(x => x.id_zawodnika == 3).FirstOrDefault(); // gdy nieznadziemy to wynik=null
            Zawodnik wynik5 = db.Zawodnik.Where(x => x.id_zawodnika == 3).First(); // gdy nie znajdziemy zawodnika otrzymamy błąd
            Zawodnik wynik6 = db.Zawodnik.FirstOrDefault(x => x.id_zawodnika == 3); // najprostszy sposób 

            // 1) znajdz zawodników urodzonych w miesiacach parzystych 

            Zawodnik[] wynik7 = db.Zawodnik.Where(x => x.data_ur.Value.Month % 2 == 0).ToArray();

            // 2) Wypisz zawodników, krózy mają czteroliterowe imie lub nazwisko 

            Zawodnik[] wynik8 = db.Zawodnik.Where(x => x.imie.Length == 4 || x.nazwisko.Length == 4).ToArray();

            // 3) wypisz zawodników, którrych imie i nazwisko zaczyna się na tę samą litere 

            Zawodnik[] wynik9 = db.Zawodnik.Where(x => x.imie[0] == x.nazwisko[0]).ToArray();

            // 4) wypisz zawodników, którzy nie mają trenera 

            Zawodnik[] wynik10 = db.Zawodnik.Where(x => x.id_trenera==null).ToArray();


            // sortowanie 

            Zawodnik[] wynik11 =db.Zawodnik.OrderBy(x => x.kraj).ToArray();
            
            Zawodnik[] wynik12 = db.Zawodnik.OrderByDescending(x => x.kraj).ToArray();

            Zawodnik[] wynik13 = db.Zawodnik.OrderBy(x => x.kraj).ThenByDescending(x=>x.wzrost).ToArray();


            // 5)  wypisz zawodników posrotowanych po BMI malejąco
            // bmi =  waga[kg] / wzrost[m]^2

            //zadanie dodatkowe:  wypisz zawodników, wraz z informacją o BMI

            Zawodnik[] wynik14 = 
                db.Zawodnik.OrderByDescending(x => x.waga / Math.Pow((double)x.wzrost / 100.0, 2)).ToArray();


            string[] wynik15 = db.Zawodnik.Select(x => x.imie).ToArray();

            string[] wynik16 = db.Zawodnik.Select(x => x.imie + " " + x.nazwisko).ToArray();


            //sposób 1: tworzymy osobną klasę, która będzie przechowywac wynik


            ZawodnikMini[] wynik17=
                db.Zawodnik.Select(x => new ZawodnikMini() { Imie = x.imie, Nazwisko = x.nazwisko }).ToArray();

            var wynik18 = db.Zawodnik.Select(x => new { MojeImie = x.imie, MojeNazwsko = x.nazwisko }).ToArray();

            //foreach (var z in wynik18)
            //    Console.WriteLine(z.MojeImie + " " + z.MojeNazwsko);


            // stwórz tablice napisów zawierającą imie nazwisko (kraj)

            string[] wynik19= 
                db.Zawodnik
                .Select(x => x.imie + " " + x.nazwisko + "(" + x.kraj + ")")
                .OrderBy(x=>x).ToArray();


            string[] wynik20 =
                db.Zawodnik
                .OrderBy(x => x.nazwisko)
                .Select(x => x.imie + " " + x.nazwisko + "(" + x.kraj + ")").ToArray();



            // podaj średni wzrost zawodników

            double sredniWzrost = db.Zawodnik.Select(x => (double)x.wzrost).Average();
            Zawodnik[] wynik21 = db.Zawodnik.Where(x => x.wzrost > sredniWzrost).ToArray();


            // podzapytania 
            Zawodnik[] wynik22 =  db.Zawodnik.Where(x => x.wzrost > db.Zawodnik.Select(y => (double)y.wzrost).Average()).ToArray();

            double wynik22a= db.Zawodnik.Average(x => (double)x.wzrost);

            // grupowanie 

            var wynik23=
                db.Zawodnik
                .GroupBy(x => x.kraj)
                .Select(z => new { Grupa = z.Key, Nazwa = z.Average(y => y.wzrost) }).ToArray();


            db.Zawodnik
                .Where(x => x.kraj == "pol")
                .OrderBy(y => y.kraj).ToArray();



            string[] napisy = { "ala", "ma", "kota" };
            string[] wynik24 = napisy.Where(x => x.Length > 2).ToArray();

            // LINQ - składania rozszerzajca język c# 
            // LINQtoSQL - mechanizm mapowania ORM 

            // 1) wypisz dla każdego kraju imie i nazwisko najwyższego zawodnika z tego kraju 
          

            //db.Zawodnik
            //    .GroupBy(x => x.kraj)
            //    .Select(y => y.Max()).ToArray();

            // wersja 1 
            //var wynik25 =
            //    db.Zawodnik
            //        .GroupBy(x => x.kraj).ToArray()
            //        .Select(y => new { Nazwa = y.Select(z => z.imie + " " + z.nazwisko), Wzrost = y.Max(z => z.wzrost) }).ToArray();

            //foreach (var z in wynik25)
            //    Console.WriteLine(string.Join("*", z.Nazwa) + " " + z.Wzrost);

            // wersja 2

            //var wynik25 =
            //   db.Zawodnik
            //       .GroupBy(x => x.kraj).ToArray()
            //       .Select(y => new { Element = y, Wzrost = y.Max(z => z.wzrost) }).ToArray();

            //foreach (var z in wynik25)
            //{
            //    Console.WriteLine(
            //    string.Join("*",
            //        z.Element.Where(x => x.wzrost == z.Wzrost).ToArray().Select(x => x.imie + " " + x.nazwisko + " " +x.wzrost))
            //    );
            //}

            // wersja 3

            //var wynik25 =
            //   db.Zawodnik
            //       .GroupBy(x => x.kraj).ToArray()
            //       .Select(y => new GrupaElementow { Element = y, Wzrost = y.Max(z => z.wzrost) }).ToArray();

            //foreach (GrupaElementow z in wynik25)
            //{
            //    Console.WriteLine(
            //    string.Join("*",
            //        z.Element.Where(x => x.wzrost == z.Wzrost).ToArray().Select(x => x.imie + " " + x.nazwisko + " " + x.wzrost))
            //    );
            //}

            // wersja 4

            var wynik26 =
              db.Zawodnik
                  .GroupBy(x => x.kraj).ToArray()
                  .Select(y => new GrupaElementow2 { Element = y.Select(w=>w).ToArray(), Wzrost = y.Max(z => (double) z.wzrost) }).ToArray();

            foreach (GrupaElementow2 z in wynik26)
            {
                Console.WriteLine(
                string.Join("*",
                    z.Element.Where(x => x.wzrost == z.Wzrost).ToArray().Select(x => x.imie + " " + x.nazwisko + " " + x.wzrost))
                );
            }



            //foreach (Zawodnik z in wynik14)
            //{
            //    Console.WriteLine(z.imie + " " + z.nazwisko + " " + z.waga / Math.Pow((double)z.wzrost / 100.0, 2));
            //}


            // 2) wypisz grupy zawodników , urodzonych w tym samym miesiącu 

            var grupy =
                db.Zawodnik
                    .GroupBy(x => x.data_ur.Value.Month)
                    .Select(x => new { NumerMiesica = x.Key, Zawodnicy = x.Select(y => y) }).ToArray();


            foreach (var g in grupy)
                Console.WriteLine(g.NumerMiesica + " " + string.Join("*", g.Zawodnicy.Select(x=>x.nazwisko)));


            // nie polecam takiej składni 
            //Zawodnik[] zawodnicy2 =
            //    (from x in db.Zawodnik
            //     where x.kraj == "pol"
            //     select x).ToArray();



            Console.ReadKey();
        }
    }
}
