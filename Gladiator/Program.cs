using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
/*
1.	Aufgabe 
Es geht bei dieser Aufgabe darum, eine vereinfachte Version des Kampfsystems der Gladiatoren spiele nachzubauen und es in einer Konsolenanwendung zu testen.
1.	Definiere eine Klasse mit dem Namen Gladiatoren und gebe es entsprechende Attribute für Name, GladaitorenRasse, Level, Lebenspunkte (wie könnte man dies am besten Speichern?) und Art (z.B. Feuer oder Wasser).
2.	Definiere eine Klasse Angriff mit Attributen für den Namen und eine BasisSchadensmenge.
3.	Füge der Gladaitoren Klasse noch ein Attribut hinzu, um Angriffe zu speichern.
4.	Erstelle eine Funktion in der Klasse Angriff, welches Anhand von zwei Gladiatoren (angreifende und zu angreifende) die Anzahl der Schadenspunkte bestimmt. Benutzt hier vorerst nur die Level der beiden Gladiatoren und sucht euch ein einfaches Berechnungsmodell aus.
5.	Erstelle eine Methode in der Klasse Gladiatoren, welches dem Gladiatoren eine bestimmte Menge an Schaden hinzufügt.
6.	Erstelle eine Methode in der Klasse Gladiatoren mit den Namen GreifeAn, welches als Parameter noch einen Angriff und das andere Gladiatoren nimmt.
7.	Erstelle ein Prädikat (eine Funktion mit einem boolschen Rückgabewert) in der Klasse Gladiatoren, mit dem man anhand der Lebenspunkt überprüfen kann, ob das Gladiatoren noch am Leben ist.
8.	Die Klasse Gladiatoren soll noch eine Funktion erhalten, in dem man eine Zusammenfassung der wichtigsten Informationen als String erhält.
9.	Erstellt in eurer Hauptfunktion zwei Instanzen der Klasse Gladiatoren und weisst den Attributen entsprechende Werte zu. Lass dann das eine Gladiatoren so lange das andere Angreifen, bis es besiegt wurde. Schreibe dabei nach jedem Angriff die wichtigsten Informationen über beide Gladiatoren in die Konsole. Zähle dabei die Anzahl der Angriffe mit.
    
    Zusatzaufgaben
	    - Erweitere bzw. ändere die Klasse Angriff, damit sie nun auch die Art (Wasser, Feuer, Neutral, ...) des Angriffes speichert und bei der einen oder anderen Angriffskombination die Schadenspunkte entsprechend anpasst (z.B. ein Wasserangriff auf einem Feuergladiator).
*/


namespace OOP_Exp_01_Gladiator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Erstellt die Gladiatoren
            Gladiator gladiator1 = new Gladiator(1,"Maximus", "Mensch", 7, 100, "Feuer", new List<string>(), false);
            Gladiator gladiator2 = new Gladiator(2,"Aquaman", "Fisch", 5, 100, "Wasser", new List<string>(), false);
            Gladiator gladiator3 = new Gladiator(3,"Achilles", "Halbgott", 4, 100, "Neutral", new List<string>(), false);

            // Erstellt die Angriffe
            Angriff wasserangriff1 = new Angriff("Wasserangriff 1","wasser", 8);
            Angriff feuerangriff1 = new Angriff("Feuerangriff 1", "feuer", 10);
            Angriff neutralerAngriff = new Angriff("Neutralangriff 1", "neutral", 6);

            Gladiator angreifer = gladiator1;
            Gladiator gegner = gladiator2;
            Angriff angriff = neutralerAngriff;
            string input;
            string attacke;
            bool isDown = false;

            Gladiator.PrintGladiatorDetails();

            while (!isDown)
            {
                Console.Write("Wählen Sie den Angreifer ('1', '2', '3'): ");
                input = Console.ReadLine();
                if (input == "1") { angreifer = gladiator1; }
                if (input == "2") { angreifer = gladiator2; }
                if (input == "3") { angreifer = gladiator3; }
                if (input.ToString() == "exit") { break; }


                Console.Write("Wählen Sie den Gegner ('1', '2', '3'): ");
                input = Console.ReadLine();
                if (input == "1") { 
                    if (angreifer == gladiator1) {
                        Console.WriteLine($"{gladiator1.Name} kann nicht gleichzeitig 'Angreifer' und 'Gegner' sein!\n Gegner wird auf {gladiator2.Name} gesetzt.");
                        gegner = gladiator2;
                    }
                    gegner = gladiator1;
                }
                if (input == "2")
                {
                    if (angreifer == gladiator2)
                    {
                        Console.WriteLine($"{gladiator2.Name} kann nicht gleichzeitig 'Angreifer' und 'Gegner' sein!\n Gegner wird auf {gladiator3.Name} gesetzt.");
                        gegner = gladiator2;
                    }
                    gegner = gladiator1;
                }
                if (input == "3")
                {
                    if (angreifer == gladiator3)
                    {
                        Console.WriteLine($"{gladiator3.Name} kann nicht gleichzeitig 'Angreifer' und 'Gegner' sein!\n Gegner wird auf {gladiator1.Name} gesetzt.");
                        gegner = gladiator1;
                    }
                    gegner = gladiator3;
                }
                if (input.ToString().ToLower() == "exit")
                {
                    break;
                }

                Console.Write("Wählen Sie die Angriffsmethode ('Wasser', 'Feuer', 'Neutral'): ");
                attacke = Console.ReadLine().ToLower();
                if (attacke.ToString() == "wasser") { angriff = wasserangriff1; }
                if (attacke.ToString() == "feuer") { angriff = feuerangriff1; }
                if (attacke.ToString() == "neutral") { angriff = neutralerAngriff; }
                if (attacke.ToString() == "exit") { break; }

                if (!angreifer.IsDown)
                {
                    angreifer.GreifeAn(angriff, gegner);
                    Console.WriteLine("\n{0}({1}) --> {2}", angreifer.Name, angriff.Name, gegner.Name);
                    Gladiator.PrintGladiatorDetails();
                }
                if (gegner.IsDown == true) { Console.WriteLine("Das Spiel ist beendet\n"); break; };
            }
        } 
    }

    class Gladiator
    {
        public override string ToString()
        {
            return this.Name + " --> " + Lebenspunkte[this];
        }

        private static readonly Dictionary<Gladiator, int> Lebenspunkte = new Dictionary<Gladiator, int>();

        public int Id { get; set; }
        public string Name { get; set; }
        public string GladiatorenRasse { get; private set; }
        public int Level { get; private set; }
        public string Art { get; private set; }
        public List<string> Attacken { get; set; }
        public int Leben { get; private set; }
        public bool IsDown { get; private set; }

        public Gladiator(int id, string name, string gladiatorRasse, int level, int leben, string art, List<string> attacken, bool isDown  )
        {
            Id = id;
            Name = name;
            GladiatorenRasse = gladiatorRasse;
            Level = level;
            Leben = leben;
            Art = art;
            Attacken = new List<string>(attacken);
            IsDown = isDown;

            // Fügt den Gladiator zum Dictionary hinzu, um sich selbst referenzieren zu können
            if (!Lebenspunkte.ContainsKey(this)){
                Lebenspunkte.Add(this, Leben); 
            }
        }

        public void GetSchaden(Gladiator gegner, Dictionary<Gladiator, int> lebenspunkte, int schadenspunkte)
        {
            lebenspunkte[gegner] -= schadenspunkte;
            if (lebenspunkte[gegner] < 0)
            {
                lebenspunkte[gegner] = 0;
            }
        }

        public void GreifeAn(Angriff angriff, Gladiator gegner)
        {
            int schadenspunkte = angriff.GetSchadenspunkte(this, gegner);
            
            if (angriff.Kategorie == "wasser") { if (gegner.Art == "Feuer") { schadenspunkte *= 2; } }
            if (angriff.Kategorie == "feuer") { if (gegner.Art == "Wasser") { schadenspunkte *= 2; } }
            if (angriff.Kategorie == "feuer") { if (gegner.Art == "Neutral") { schadenspunkte *= 3; } }

            GetSchaden(gegner, Lebenspunkte, schadenspunkte);
            Attacken.Add(angriff.Name);
            gegner.TargetDown();
        }

        public void TargetDown()
        { 
            if (Lebenspunkte[this] < 0) { IsDown = true; }
            if (Lebenspunkte[this] == 0) { IsDown = true; }   
        }

        public static void PrintGladiatorDetails()
        {
            string name = "Name";
            string rasse = "Rasse";
            string art = "Art";
            string level = "Level";
            string leben = "Leben";

            string tableHead = $"\n| {name, -10} | {rasse,-10} | {art,-7} | {leben,5} | {level,5} |";

            for (int i = 0; i < (tableHead.Length -1); i++)
            {
                Console.Write("-");
            }

            Console.WriteLine(tableHead);

            foreach (Gladiator gladiator in Lebenspunkte.Keys) {
                string line = $"| {gladiator.Name, -10} | {gladiator.GladiatorenRasse, -10} | {gladiator.Art,-7} | {Lebenspunkte[gladiator],5} | {gladiator.Level,5} |";
                Console.WriteLine(line);
            }

            for (int i = 0; i < (tableHead.Length - 1); i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("\n\n");
        }
    }

    class Angriff
    {
        public string Name { get; set; }
        public string Kategorie { get; set; }
        public int BasisSchaden { get; set; }

        public Angriff(string name, string kategorie, int basisSchaden) {
            Name = name;
            Kategorie = kategorie;
            BasisSchaden = basisSchaden;    
        }

        public int GetSchadenspunkte(Gladiator gladiator1,Gladiator gladiator2)
        {
            Gladiator angreifer = gladiator1;
            Gladiator gegner = gladiator2;

            int levelUnterschied = Math.Abs(angreifer.Level - gegner.Level);

            return levelUnterschied * BasisSchaden;
        } 
    }
}

