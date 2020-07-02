using System.Collections.Generic;
namespace Aula_27_28_29_30
{
    public interface IProduto
    {
        void Cadastrar(Produto prod);
        List<Produto> Ler();
        void Alterar(Produto _produtoAlterado);
        void Remover(string _termo);
    }
}