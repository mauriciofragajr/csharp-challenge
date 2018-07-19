using System;
using System.Collections.Generic;
using System.Linq;

namespace CamelCase
{

    class CustomResponse
    {
        // Classe para ajudar no retorno da função auxiliar.. Leia o código abaixo que vai dar pra entender melhor
        public int EndIndex { get; set; }
        public string Word { get; set; }

        public CustomResponse(int endIndex, string word)
        {
            EndIndex = endIndex;
            Word = word;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string[] exitCodes = { "0", "exit", "sair", "s", "bye" };
            Console.WriteLine("Para sair, digite \"0\", \"exit\", \"sair\", \"s\" ou \"bye\"");
            string txt = "";
            while (!exitCodes.Contains(txt))
            {
                Console.WriteLine("Entre com a palavra desejada :)");
                txt = Console.ReadLine();

                if (exitCodes.Contains(txt)) break;

                Console.WriteLine("Resultado:");
                foreach (string word in ConverterCamelCase(txt)) Console.Write(word + ", ");
                Console.WriteLine();
            }
        }

        public static List<string> ConverterCamelCase(string original)
        {
            var list = new List<string>(); // Iniciando a lista que vai armazenar todas as palavras

            int searchFromIndex = 0; // Variável vai guardar o índex raiz da minha proxima procura

            // Vamos rodar até retornar o -1, pois é sinal que não tem mais nada pra percorrer na string original
            while (searchFromIndex != -1)
            {
                // Passando sempre a string original, e o índice que a partir dele vou procurar uma nova string
                CustomResponse response = getNextWord(original, searchFromIndex); // Armazeno na classe auxiliar que fiz para ajudar
                list.Add(response.Word.ToLower()); // Adicionando a palavra encontrada de acordo com a especificação do problema

                searchFromIndex = response.EndIndex;  // Retorna o índice que devo começar a procurar
            }
            return list;
        }

        public static CustomResponse getNextWord(string original, int startIndex)
        {
            int len = original.Length;

            int endIndex = -1;
            int i = startIndex + 1;

            // Essa é a bruxaria
            while (len > i + 1)
            {
                if (char.IsLower(original[i]) && char.IsUpper(original[i + 1])) // nomeIndice - retorna índice do I depois do e
                {
                    endIndex = i + 1;
                    break;
                }
                else if (char.IsUpper(original[i]) && char.IsLower(original[i + 1])) // CPFContrib - retorna índice do C depois do F
                {
                    endIndex = i;
                    break;
                }
                i++;
            }
            if (endIndex == -1)
                return new CustomResponse(endIndex, original.Substring(startIndex));
            else
                // IMPORTANTE DIMINUIR DO START INDEX, DEU DOR DE CABEÇA ISSO AE
                return new CustomResponse(endIndex, original.Substring(startIndex, endIndex - startIndex));
        }
    }
}
