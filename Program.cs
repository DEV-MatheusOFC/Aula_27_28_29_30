using System;
using System.Collections.Generic;

namespace Aula_27_28_29_30
{
    class Program
    {
        static void Main(string[] args)
        {
            Produto p1 = new Produto();
            p1.Codigo  = 1;
            p1.Nome    = "Galaxy A50";
            p1.Preco   = 1599f;

            p1.Cadastrar(p1);
            p1.Remover("1799");

            List<Produto> lista = p1.Ler();

            foreach (Produto item in lista){
               System.Console.WriteLine($"R${item.Preco} - {item.Nome}");
            }

        }
    }
}
