﻿using System;

namespace tabuleiro
{
    class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] _pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            _pecas = new Peca[linhas, colunas];
        }

        public Peca Peca(int linha, int coluna)
        {
            return _pecas[linha, coluna];
        }

        public Peca Peca(Posicao pos)
        {
            return _pecas[pos.Linha, pos.Coluna];
        }

        public bool ExistePeca (Posicao pos)
        {
            ValidarPosicao(pos);
            return _pecas[pos.Linha, pos.Coluna] != null;
        }

        public void ColocarPeca(Peca p, Posicao pos)
        {
            if (ExistePeca(pos))
            {
                throw new TabuleiroException("Já existe uma peça nessa posição");
            }

            _pecas[pos.Linha, pos.Coluna] = p;
            p.Posicao = pos;
        }

        public Peca RetirarPeca(Posicao pos)
        {
            if (_pecas[pos.Linha, pos.Coluna] == null)
            {
                return null;
            }
            else
            {
                Peca aux = _pecas[pos.Linha, pos.Coluna];
                aux.Posicao = null;
                _pecas[pos.Linha, pos.Coluna] = null;
                return aux;
            }
        }

        public bool PosicaoValida(Posicao pos)
        {
            if (pos.Linha < 0 || pos.Linha >= Linhas || pos.Linha < 0 || pos.Linha > Colunas)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void ValidarPosicao(Posicao pos)
        {
            if (!PosicaoValida(pos))
            {
                throw new TabuleiroException("Posição inválida!");
            }
        }
    }
}
