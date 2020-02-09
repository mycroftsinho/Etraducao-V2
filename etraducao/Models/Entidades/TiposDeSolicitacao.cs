using System.ComponentModel.DataAnnotations;

namespace etraducao.Models.Entidades
{
    public enum TiposDeSolicitacao
    {
        [Display(Name = "Tradução Técnica")]
        TraducaoTecnica,

        [Display(Name = "Tradução Juramentada")]
        TraducaoJuramentada
    }
}
