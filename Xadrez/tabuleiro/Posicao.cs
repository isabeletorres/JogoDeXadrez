using System;

namespace tabuleiro
{
    class Posicao
    {
        public int Linha { get; set; }
        public int Coluna { get; set; }

        public Posicao(int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;
        }

        public void DefinirValores(int linhas, int colunas)
        {
            Linha = linhas;
            Coluna = colunas;
        }
        public override string ToString()
        {
            return Linha
                + ", "
                + Coluna;
        }
    }
}
