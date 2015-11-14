using System;

namespace FWBancoImagens.Common
{
    public class Enumeradores
    {
        public const string Horizontal = "H";
        public const string Vertical = "V";
        public const string Quadrado = "Q";
        public const string Panoramico = "P";

        public enum TipoImagem 
        { 
            Colorida    = 0,
            PretoBranco = 1/*,
            Ilustracao  = 2*/
        };

        public enum OrientacaoImagem
        {
            Horizontal = 0,
            Vertical = 1/*,
            Panoramica = 2*/
        };

        public enum PositivoNegativo
        {
            Nao = 0,
            Sim = 1,
            
        };


    }
}
