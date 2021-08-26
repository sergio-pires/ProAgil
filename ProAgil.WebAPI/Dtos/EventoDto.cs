using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProAgil.WebAPI.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        public string Local { get; set; }

        [Required(ErrorMessage = "{0} é obrigatória")]
        public string DataEvento { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "{0} deve ter entre 3 e 100 carateres")]
        public string Tema { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [Range(1,120000, ErrorMessage = "{0} deve ser entre 1 e 120000")]
        public int QtdPessoas { get; set; }

        public string ImagemURL {get; set;}
        public string Telefone { get; set; }
        
        [Required(ErrorMessage = "{0} é obrigatório")]
        [EmailAddress(ErrorMessage = "{0} é inválido")]
        public string Email { get; set; }

        public List<LoteDto> Lotes { get; set; }
        public List<RedeSocialDto> RedesSociais { get; set; }
        public List<OradorDto> Oradores { get; set; }
    }
}