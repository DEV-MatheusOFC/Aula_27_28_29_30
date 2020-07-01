using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aula_27_28_29_30
{
    public class Produto
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public float Preco { get; set; }

        private const string PATH = "Database/produto.csv";

        public string Separar(string dado){
            return dado.Split('=')[1];
        }
        public Produto(){
            // Cria o arquivo caso não exista
            if(!File.Exists(PATH)){
                Directory.CreateDirectory("Database");
                File.Create(PATH).Close();
            }
        }

        public void Cadastrar(Produto prod){
            var linha = new string[] {PrepararLinha(prod)};
            File.AppendAllLines(PATH, linha);
        }

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

        public List<Produto> Filtrar(string _nome){
            return Ler().FindAll(n => n.Nome == _nome);
        }
        public void Remover(string _termo){

            List<string> linhas = new List<string>();
            
            using(StreamReader arquivo = new StreamReader(PATH)){

                string linha;

                while((linha = arquivo.ReadLine()) != null ){
                    linhas.Add(linha);
                }

                linhas.RemoveAll(z => z.Contains(_termo));
            }

            using(StreamWriter output = new StreamWriter(PATH)){

                output.Write(String.Join(Environment.NewLine, linhas.ToArray())); 

            }
        }
        private string PrepararLinha(Produto p){
            return $"\nCodigo={p.Codigo};nome={p.Nome};preco={p.Preco}";
        }
    }
}