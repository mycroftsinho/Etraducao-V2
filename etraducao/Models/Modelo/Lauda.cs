namespace etraducao.Models.Modelo
{
    public class Lauda
    {
        public decimal Valor { get; set; }

        public int Laudas { get; set; }

        public TipoDeDocumento TipoDeDocumento { get; set; }

        public void CalcularValorDaTraducao(int quantidadeDeCaracteres)
        {
            var sobra = quantidadeDeCaracteres % 1000 == 0 ? 0 : 1;
            this.Laudas = (quantidadeDeCaracteres / 1000) + sobra;
        }

        private void CalcularApostilaDeHaia()
        {

        }

        private void CalcularTraducaoTecnica()
        {

        }

        private void CalcularTraducaoJuramentada()
        {

        }
    }
}
