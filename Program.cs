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
            p1.Preco   = 1549f;

            p1.Cadastrar(p1);
            // p1.Remover("1799");

            Produto alterado = new Produto();
            alterado.Codigo  = 1;
            alterado.Nome    = "Galaxy A80";
            alterado.Preco   = 2499f;

            p1.Alterar(alterado);

            List<Produto> lista = p1.Ler();

            foreach (Produto item in lista){
               System.Console.WriteLine($"R${item.Preco} - {item.Nome}");
            }

        }
    }
}
