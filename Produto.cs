using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aula_27_28_29_30
{
    public class Produto : IProduto
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public float Preco { get; set; }
        private const string PATH = "Database/produto.csv";
        
        public Produto(){
            // Cria o arquivo caso não exista e gera a pasta caso não exista.
            if(!File.Exists(PATH)){
                Directory.CreateDirectory("Database");
                File.Create(PATH).Close();
            }
        }

        public void CSValterado(List<string> linhas){
            using (StreamWriter output = new StreamWriter(PATH))
            {
                foreach (string ln in linhas)
                {
                    output.Write(ln + "\n");
                }
            }
        }


        /// Lê as linhas para alteração de produtos
        public void LerLinhas(List<string> linhas, string _termo){
            using (StreamReader arquivo = new StreamReader(PATH))
            {
                string linha;
                while ((linha = arquivo.ReadLine()) != null)
                {
                    linhas.Add(linha);
                }
                linhas.RemoveAll(z => z.Contains(_termo));
            }
        }

        public void LerLinhas(List<string> linhas){
            using (StreamReader arquivo = new StreamReader(PATH))
            {
                string linha;
                while ((linha = arquivo.ReadLine()) != null)
                {
                    linhas.Add(linha);
                }
            }

        }
        public string Separar(string dado){
            return dado.Split('=')[1];
        }


        /// Cadastra um produto
        public void Cadastrar(Produto prod){
            var linha = new string[] {PrepararLinha(prod)};
            File.AppendAllLines(PATH, linha);
        }

        /// Lê o csv
        public List<Produto> Ler(){
            List<Produto> produtos = new List<Produto>();

            string[] linhas = File.ReadAllLines(PATH);
            foreach (var linha in linhas){
                string [] dados = linha.Split(';');

                Produto prod = new Produto();
                prod.Codigo  = Int32.Parse(Separar(dados[0]));
                prod.Nome    = Separar(dados[1]);
                prod.Preco   = float.Parse( Separar(dados[2]));

                produtos.Add(prod);
            }
            produtos = produtos.OrderBy(z => z.Nome).ToList();
            return produtos;
        }

        /// Filtra um produto por nome, codigo ou preço específico
        public List<Produto> Filtrar(string _nome)
        {
            return Ler().FindAll(n => n.Nome == _nome);
        }

        /// Remove um produto da lista
        public void Remover(string _termo)
        {
            List<string> linhas = new List<string>();
            LerLinhas(linhas, _termo);
            CSValterado(linhas);

        }

        /// Altera um produto
        public void Alterar(Produto _produtoAlterado)
        {
            List<string> linhas = new List<string>();
            LerLinhas(linhas);
            linhas.RemoveAll(z => z.Split(";")[0].Contains(_produtoAlterado.Codigo.ToString()));
            linhas.Add(PrepararLinha(_produtoAlterado));
            CSValterado(linhas);

        }
        private string PrepararLinha(Produto p){
            return $"Codigo={p.Codigo};nome={p.Nome};preco={p.Preco}";
        }

    }
}