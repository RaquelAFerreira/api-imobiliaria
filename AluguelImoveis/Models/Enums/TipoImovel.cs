using System.Text.Json.Serialization;

namespace AluguelImoveis.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TipoImovel
    {
        Apartamento = 1,
        Casa = 2,
        Sobrado = 3,
        Kitnet = 4,
        LojaComercial = 5,
        GalpaoIndustrial = 6,
        Terreno = 7,
        Chacara = 8,
        Sitio = 9,
        Flat = 10
    }
}