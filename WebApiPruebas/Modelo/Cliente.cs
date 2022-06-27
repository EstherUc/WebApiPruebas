namespace WebApiPruebas.Modelo
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }    

        public List<Factura> Facturas { get; set; } //propiedad de navegación
    }
}
