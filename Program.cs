using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuMotus
{
        class Program
        {
            static void Main(string[] args)
            {
                Console.WriteLine("Bienvenue dans JeuMotus");

                bool rejouer = true;
                while (rejouer)
                {
                    JouerPartie();
                    Console.WriteLine("Voulez-vous rejouer ? (o / n)");
                    string reponse = Console.ReadLine();
                    rejouer = (reponse.ToLower() == "o");
                }
            }

            static void JouerPartie()
            {
                int nombreTentativesMax = 6;
                int nombreTentatives = nombreTentativesMax;

                List<string> mots = ChargerMots("mots.txt");
                string motCache = ChoisirMot(mots);

                Console.WriteLine($"Le mot caché contient {motCache.Length} caractères et commence par {motCache[0]}");
                Console.WriteLine($"Il vous reste {nombreTentatives} coups à jouer");

                bool motTrouve = false;
                while (nombreTentatives > 0 && !motTrouve)
                {
                    string motPropose = GetMot(motCache.Length);
                    motTrouve = TestMot(motCache, motPropose);
                    nombreTentatives--;

                    if (!motTrouve)
                        Console.WriteLine($"Il vous reste {nombreTentatives} coups à jouer");
                }

                if (motTrouve)
                    Console.WriteLine($"Bravo vous avez gagné, le mot caché était : {motCache}");
                else
                    Console.WriteLine($"Désolé vous avez perdu ! Le mot caché était : {motCache}");
            }

            static void AfficherCouleur(string texte, ConsoleColor couleur)
            {
                Console.ForegroundColor = couleur;
                Console.Write(texte);
                Console.ForegroundColor = ConsoleColor.White;
            }

            static string GetMot(int lenMot)
            {
                string mot;
                do
                {
                    Console.Write($"Entrez votre mot de {lenMot} caractères : ");
                    mot = Console.ReadLine();
                } while (mot.Length != lenMot);

                return mot.ToUpper(); 
            }

            static int GetNombreAleatoire(int min, int max)
            {
                Random random = new Random();
                return random.Next(min, max + 1); 
            }

            static List<string> ChargerMots(string fileName)
            {
                List<string> mots = new List<string>();
                try
                {
                    using (StreamReader sr = new StreamReader(fileName))
                    {
                        string ligne;
                        while ((ligne = sr.ReadLine()) != null)
                        {
                            mots.Add(ligne.ToUpper()); // Convertir en majuscules pour simplifier la comparaison
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Une erreur est survenue lors de la lecture du fichier: {e.Message}");
                }
                return mots;
            }

            static string ChoisirMot(List<string> mots)
            {
                int indexMot = GetNombreAleatoire(0, mots.Count - 1);
                return mots[indexMot];
            }

            static bool TestMot(string motCache, string motSaisi)
            {
                bool motTrouve = true;
                for (int i = 0; i < motCache.Length; i++)
                {
                    if (motCache[i] == motSaisi[i])
                    {
                        AfficherCouleur(motSaisi[i].ToString(), ConsoleColor.Red);
                    }
                    else if (motCache.Contains(motSaisi[i].ToString()))
                    {
                        AfficherCouleur(motSaisi[i].ToString(), ConsoleColor.Yellow);
                        motTrouve = false;
                    }
                    else
                    {
                        Console.Write(motSaisi[i]);
                        motTrouve = false;
                    }
                }
                Console.WriteLine();
                return motTrouve;
            }


        }
 
}
