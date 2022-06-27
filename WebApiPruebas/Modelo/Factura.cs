namespace WebApiPruebas.Modelo
{
    public class Factura
    {
        public int Id { get; set; }
        public string Nom_Empresa { get; set; }
        public int ClienteId { get; set; }

        public Cliente Cliente { get; set; } //propiedad de navegación
    }
}
