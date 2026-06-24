using AbmApi.Api.Response;
using AbmApi.Domain.Entity;
using AbmApi.Infraestructure.Context;
using MediatR;

namespace AbmApi.Application.Command
{
    public class CreateProductoHandler : IRequestHandler<CreateProductoCommand, ProductoResponse>
    {
        private readonly PostgresDbContext _postgresDbContext;

        public CreateProductoHandler(PostgresDbContext postgresDbContext)
        {
            _postgresDbContext = postgresDbContext;
        }

        public async Task<ProductoResponse> Handle(CreateProductoCommand request, CancellationToken cancellationToken)
        {
            var entity = new Producto
            {
                Codigo = request.Request.Codigo,
                Nombre = request.Request.Nombre,
                Descripcion = request.Request.Descripcion,
                Precio = (decimal)request.Request.Precio,
                Activo = request.Request.Activo,
                CategoriaId = request.Request.CategoriaId,
                CantidadStock = request.Request.CantidadStock,
                FechaCreacion = DateTime.Now
            };

            _postgresDbContext.Productos.Add(entity);
            await _postgresDbContext.SaveChangesAsync(cancellationToken);

            return new ProductoResponse
                        (
                          Id: entity.Id,
                          Codigo: entity.Codigo,
                          Nombre: entity.Nombre,
                          Descripcion: entity.Descripcion ?? string.Empty,
                          Precio: (double)entity.Precio,
                          Activo: entity.Activo,
                          CategoriaId: entity.CategoriaId,
                          FechaCreacion: entity.FechaCreacion,
                          FechaActualizacion: entity.FechaActualizacion,
                          CantidadStock: entity.CantidadStock ?? 0
                        );
        }
    }
}
