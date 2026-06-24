using AbmApi.Api.Response;
using AbmApi.Infraestructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbmApi.Application.Query
{
    public class GetProductosHandler : IRequestHandler<GetProductosQuery, IEnumerable<ProductoResponse>> 
    {
        private readonly PostgresDbContext _postgresDbContext;

        public GetProductosHandler(PostgresDbContext postgresDbContext)
        {
            _postgresDbContext = postgresDbContext;
        }

        public async Task<IEnumerable<ProductoResponse>> Handle(GetProductosQuery request, CancellationToken cancellationToken)
        {
            var entities = await _postgresDbContext.Productos.AsNoTracking().ToListAsync(cancellationToken);

            return entities.Select(entity => new ProductoResponse(
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
                ));
        }
    }
}
