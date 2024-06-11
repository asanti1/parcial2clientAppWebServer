namespace segParAgustinSantinaqueCliente.Dto.Disco;

public class DiscoUpdateDto
{
    public string Nombre { get; set; }
    public string Genero { get; set; }
    public DateTime FechaLanzamiento { get; set; } 
    public string Banda { get; set; }
    public int CantidadVendida { get; set; }
}