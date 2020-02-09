using etraducao.Models.Exception;
using System;

namespace etraducao.Models.Entidades
{
    public class ControleDeValores
    {
        protected ControleDeValores()
        {
        }

        public ControleDeValores(decimal valorDaLauda)
        {
            ValorDaLauda = valorDaLauda;
        }

        public int Id { get; private set; }

        public decimal QuantidadeDeLaudas { get; set; }

        public decimal ValorDaLauda { get; private set; }

        public decimal DeadLineSugerido { get; private set; }

        public decimal DeadLineReal { get; private set; }

        public decimal CoefReal { get; private set; }

        public decimal CoefInterno { get; private set; }

        public decimal CoefSoma { get; private set; }

        public decimal QuantidadeDeDiasParaEntrega { get; private set; }

        public decimal CalcularValorPorLauda()
        {
            return ValorDaLauda + (ValorDaLauda * CoefSoma);
        }

        private void CalcularDeadLineSugerido()
        {
            DeadLineSugerido = 1m + (0.5m * QuantidadeDeLaudas);
        }

        private void CalcularDeadLineReal()
        {
            DeadLineReal = 1m + (0.05m * QuantidadeDeLaudas);
        }

        private void CalcularCoefReal()
        {
            CoefReal = DeadLineSugerido - DeadLineReal;
        }

        private void CalcularCoefInterno()
        {
            CoefInterno = 0.6m / CoefReal;
        }

        private void CalcularCoefDeSoma()
        {
            if (QuantidadeDeDiasParaEntrega == 1)
                throw new DataInvalidaException("Período Indisponível para entrega.");

            int maiorDiaPossivel = decimal.ToInt32(DeadLineSugerido);
            int menorPesoPossivel = 0;

            while (maiorDiaPossivel > QuantidadeDeDiasParaEntrega)
            {
                maiorDiaPossivel--;
                menorPesoPossivel++;
            }
            CoefSoma = CoefInterno * menorPesoPossivel;
        }

        public void DefinirQuantidadeDeLaudas(decimal quantidadeDeLaudas, double quantidadeDeDiasParaEntrega)
        {
            QuantidadeDeLaudas = quantidadeDeLaudas;
            QuantidadeDeDiasParaEntrega = (decimal)quantidadeDeDiasParaEntrega;

            CalcularDeadLineSugerido();
            CalcularDeadLineReal();

            CalcularCoefReal();
            CalcularCoefInterno();
            CalcularCoefDeSoma();
        }

        public decimal ObterDeadLineSugerido(decimal quantidadeDeLaudas)
        {
            QuantidadeDeLaudas = quantidadeDeLaudas;
            CalcularDeadLineSugerido();
            return DeadLineSugerido;
        }
    }
}
