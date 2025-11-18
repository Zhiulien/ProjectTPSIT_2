using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjettoTPSIT_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Lista di nominativi (nome e cognome)
            List<string> students = new List<string>
        {
            "Luca Ciaone", "Matteo Parmiciaoli", "Claudio Rossi", "Luigi Macioni", "Gino Belli",
            "Francesca Rossi", "Marco Mazzone", "Alessandro Conte", "Giulia Verdi", "Simone Bianchi"
        };

            // Input dell'utente
            Console.Write("Inserisci una stringa di testo per filtrare i nominativi: ");
            string input = Console.ReadLine().Trim();

            // Filtro dei nominativi tramite espressione regolare
            List<string> filteredStudents = FilterStudents(students, input);

            // Estrazione dei 3 nominativi casuali (o meno se non ce ne sono abbastanza)
            List<string> selectedStudents = SelectRandomStudents(filteredStudents, 3);

            // Mostrare il risultato
            Console.WriteLine("\nI 3 nominativi estratti:");
            foreach (var student in selectedStudents)
            {
                Console.WriteLine(student);
            }
        }
        static List<string> FilterStudents(List<string> students, string input)
        {
            string exactMatchPattern = $@"\b{Regex.Escape(input)}\b";
            string groupMatchPattern = $@"[{Regex.Escape(input)}]+";
            string rangeMatchPattern = $@"[a-oA-O\s]+";

            // Applicazione dell'espressione regolare in ordine di priorità
            List<string> filtered = students.Where(student =>
            {
                return Regex.IsMatch(student, exactMatchPattern, RegexOptions.IgnoreCase) ||
                       Regex.IsMatch(student, groupMatchPattern, RegexOptions.IgnoreCase) ||
                       Regex.IsMatch(student, rangeMatchPattern, RegexOptions.IgnoreCase);
            }).ToList();

            return filtered;
        }
        // Funzione per selezionare casualmente 3 nominativi (o meno)
        static List<string> SelectRandomStudents(List<string> students, int count)
        {
            Random rand = new Random();

            if (students.Count <= count)
            {
                return students;
            }

            // Se ci sono più di 3 nominativi, estrarre 3 casualmente
            List<string> selected = students.OrderBy(x => rand.Next()).Take(count).ToList();
            return selected;
        }
    }
}
