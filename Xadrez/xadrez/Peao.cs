using tabuleiro;

namespace xadrez
{
    class Peao : Peca
    {
        private PartidaDeXadrez _partida;

        public Peao(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor)
        {
            _partida = partida;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool ExisteInimigo(Posicao pos)
        {
            Peca p = Tab.Peca(pos);
            return p != null && p.Cor != Cor;
        }

        private bool Livre(Posicao pos)
        {
            return Tab.Peca(pos) == null;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];
            int direcao = (Cor == Cor.Branca) ? -1 : 1; // Branco sobe (-1), Preto desce (+1)

            Posicao pos = new Posicao(0, 0);

            // Movimento simples
            pos.DefinirValores(Posicao.Linha + direcao, Posicao.Coluna);
            if (Tab.PosicaoValida(pos) && Livre(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // Movimento duplo na primeira jogada
            pos.DefinirValores(Posicao.Linha + 2 * direcao, Posicao.Coluna);
            Posicao casaIntermediaria = new Posicao(Posicao.Linha + direcao, Posicao.Coluna);
            if (QtdMovimentos == 0 && Tab.PosicaoValida(pos) && Livre(pos) && Livre(casaIntermediaria))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // Captura diagonal esquerda
            pos.DefinirValores(Posicao.Linha + direcao, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // Captura diagonal direita
            pos.DefinirValores(Posicao.Linha + direcao, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // En Passant
            int linhaEnPassant = (Cor == Cor.Branca) ? 3 : 4;
            if (Posicao.Linha == linhaEnPassant)
            {
                Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);

                if (Tab.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && Tab.Peca(esquerda) == _partida.VulneravelEnPassant)
                {
                    mat[esquerda.Linha + direcao, esquerda.Coluna] = true;
                }
                if (Tab.PosicaoValida(direita) && ExisteInimigo(direita) && Tab.Peca(direita) == _partida.VulneravelEnPassant)
                {
                    mat[direita.Linha + direcao, direita.Coluna] = true;
                }
            }

            return mat;
        }
    }
}
