using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SampleQuestions.Model
{
    public class FormsModel
    {
        [JsonProperty("formularioId")]
        public string FormularioId { get; set; }

        [JsonProperty("tipoFormularioId")]
        public string TipoFormularioId { get; set; }

        [JsonProperty("descricao")]
        public string Descricao { get; set; }

        [JsonProperty("dataCadastro")]
        public DateTime DataCadastro { get; set; }

        [JsonProperty("areasFormulario")]
        public List<AreasFormulario> AreasFormulario { get; set; }
    }

    public class AreasFormulario
    {
        [JsonProperty("formularioId")]
        public string FormularioId { get; set; }

        [JsonProperty("formularioAreaId")]
        public string FormularioAreaId { get; set; }

        [JsonProperty("descricao")]
        public string Descricao { get; set; }

        [JsonProperty("questoes")]
        public List<Questoes> Questoes { get; set; }
    }

    public class Questoes
    {
        [JsonProperty("formularioAreaId")]
        public string FormularioAreaId { get; set; }

        [JsonProperty("tipoResposta")]
        public int TipoResposta { get; set; }

        [JsonProperty("item")]
        public int Item { get; set; }

        [JsonProperty("descricao")]
        public string Descricao { get; set; }

        [JsonProperty("identificador")]
        public string Identificador { get; set; }

        [JsonProperty("expressaoCalculo")]
        public string ExpressaoCalculo { get; set; }

        [JsonProperty("expressaoExibicao")]
        public string ExpressaoExibicao { get; set; }

        [JsonProperty("expressaoCalculoMobile")]
        public string ExpressaoCalculoMobile { get; set; }

        [JsonProperty("listaRespostas")]
        public List<long> ListaRespostas { get; set; }
    }

}
