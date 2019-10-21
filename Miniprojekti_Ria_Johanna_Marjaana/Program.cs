using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Quizz
{
    class Program
    {
        static List<string> tallennustiedosto = new List<string>();
        static List<Question> Questions = new List<Question>();
        static List<Question> SortedQ = new List<Question>();
        static string playername;
        static int points;
        static void Main(string[] args)
        {
            LoadAllQuestions();
            RandomSortQuestions();
            Start(); // kysyy vastaajan nimen
            AskQuestions(); // kysyy kysymykset
            CalculateResults(); // laskee tulokset + antaa pisteet + testi
        }

        private static void CalculateResults()
        {
            points = 0;
            foreach (var i in SortedQ)
            {
                if (i.PlayerVastaus == i.Vastaus)
                {
                    points++;
                }
            }
            GiveResults();
        }

        private static void GiveResults()
        {
            if (points >= 10)
            {
                string title = @"    .-""""""-.
  .'          '.
 /   O      O   \
:                :
|                |   
: ',          ,' :
 \  '-......-'  /
  '.          .'
    '-......-'";
                Console.WriteLine($"Hurray, oletko {playername} tehnyt muutakin kuin katsonut leffoja?");
                Console.WriteLine(title);
                Console.WriteLine();
                Console.WriteLine($"Tuloksesi on huikeat {points}/10");
            }
            else if (points >= 7 && points <= 9)
            {
                string title = @"    .-""""""-.
   .'          '.
  /   O    -=-   \
 :                :
 |                |  
 : ',          ,' :
  \  '-......-'  /
   '.          .'
     '-......-'";
                Console.WriteLine($"Ihan ok meni, {playername} eksyy välillä leffateatteriin, mutta ei tarpeeksi usein!");
                Console.WriteLine(title);
                Console.WriteLine();
                Console.WriteLine($"Tuloksesi on: {points}/10");

            }
            else if (points >= 4 && points <= 6)
            {
                string title = @"     .-""""""-.
   .'          '.
  /   O      O   \
 :           `    :
 |                |   
 :    .------.    :
  \  '        '  /
   '.          .'
     '-......-' ";
                Console.WriteLine($"Joulupukki tuo sulle {playername} Finnkinon lahjakortteja lahjaksi.");
                Console.WriteLine(title);
                Console.WriteLine();
                Console.WriteLine($"Huonosti meni... tuloksesi on {points}/10");
            }
            else if (points <= 3)
            {
                string title = @"     .-""""""-.
   .'  \    /   '.
  /   O      O   \
 :   ´       `    :
 | ´           `  |   
 :    .------.    :
  \  '        '  /
   '.          .'
     '-......-' ";
                Console.WriteLine($"Vaaau {playername}, kissatkin tietää enemmän leffoista!");
                Console.WriteLine(title);
                Console.WriteLine();
                Console.WriteLine($"Tuloksesi on melkein miinuksella: {points}/10");
            }

            Console.WriteLine();
            Console.WriteLine("Paina [1] nähdäksesi kaikki vastaukset, tai sulje ohjelma painamalla mitä tahansa muuta...");
            var inp = Console.ReadKey();
            Console.Clear();
            if (inp.Key == ConsoleKey.D1)
            {
                foreach (var item in SortedQ)
                {
                    Console.WriteLine(item.Kysymys);
                    Console.WriteLine();
                    Console.WriteLine($"Vastasit: {item.PlayerVastaus}");
                    Console.WriteLine($"Oikea vastaus: {item.Selite}");
                    Console.WriteLine("------------");
                }
                
            }
            else
            {
                // poistuu ohjelmasta
            }
        }

        private static void AskQuestions()
        {
            Console.Clear();
            int counter = 1;
            foreach (var i in SortedQ)
            {
                Console.WriteLine($"Kysymys numero: {counter}");
                Console.WriteLine("--------------------------");
                Console.WriteLine(i.Kysymys);
                Console.WriteLine();
                Console.WriteLine("Valitse vastaus painamalla näppäintä:");
                Console.WriteLine("[1] KYLLÄ [2] EI ");
                Console.WriteLine();
                ConsoleKeyInfo input = Console.ReadKey();

                if (input.Key == ConsoleKey.D1)
                {
                    i.PlayerVastaus = "kyllä";
                }
                else if (input.Key == ConsoleKey.D2)
                {
                    i.PlayerVastaus = "ei";
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Et osunut kumpaankaan pyydettyyn nappiin. Vastauksesi on siis automaattisesti väärin.");
                    Console.ReadKey();
                }
                Console.Clear();
                counter++;
            }
        }

        private static void Start()
        {
            Console.WriteLine("Tervetuloa pelaamaan LeffaQuizz-peliä!");
            Console.WriteLine("Kerro ensin nimesi:");
            playername = Console.ReadLine();
            Console.Clear();
            Console.WriteLine($"Hei {playername}!");
            Console.WriteLine();
            Console.WriteLine("Paina mitä tahansa nappia jatkaaksesi");
            Console.ReadKey();
        }

        private static void LoadAllQuestions()
        {

            try
            {
                using (StreamReader sr = new StreamReader(@"C:\Users\johan\Source\Repos\Leftythefish\Miniprojekti_Quizz\questions.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        tallennustiedosto.Add(line);
                    }
                    sr.Dispose();
                }
                SaveFileAsObjects();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Tiedostoa ei löydy, tarkista tiedoston sijainti.");
                Console.ReadKey();
            }

        }

        private static void SaveFileAsObjects()
        {
            foreach (var rivi in tallennustiedosto)
            {
                string[] osiot;
                osiot = rivi.Split(";");
                string kys, vastaus, selite;

                try
                {
                    if (osiot[0] == null || osiot[0] == "")
                    {
                        kys = "Basic kysymys";
                    }
                    else
                    {
                        kys = osiot[0];
                    }
                    if (osiot[1] == null || osiot[1] == "")
                    {
                        vastaus = "Basic vastaus";
                    }
                    else
                    {
                        vastaus = osiot[1];
                    }
                    if (osiot[2] == null || osiot[2] == "")
                    {
                        selite = "ei selitettä";
                    }
                    else
                    {
                        selite = osiot[2];
                    }

                    Question kysymys = new Question(kys, vastaus, selite); 
                    Questions.Add(kysymys);
                }
                catch (Exception)
                {
                    Console.WriteLine("Ongelma tiedoston lukemisessa, tarkista kysymystiedosto ja uudelleenkäynnistä ohjelma.");
                }
            }
        }

        private static void RandomSortQuestions()
        {
            Random rnd = new Random();
            List<Question> temp = new List<Question>();
            temp = Questions.OrderBy(x => rnd.Next()).ToList();
            SortedQ = temp.Take(10).ToList();

        }
    }
}
