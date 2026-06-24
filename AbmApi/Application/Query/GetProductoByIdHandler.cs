using AbmApi.Api.Response;
using AbmApi.Infraestructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace AbmApi.Application.Query
{
    public class GetProductoByIdHandler: IRequestHandler<GetProductoByIdQuery, ProductoResponse?>
    {
        private readonly PostgresDbContext _postgresDbContext;

        public GetProductoByIdHandler(PostgresDbContext postgresDbContext)
        {
            _postgresDbContext = postgresDbContext;
        }

        public async Task<ProductoResponse?> Handle(GetProductoByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _postgresDbContext.Productos.AsNoTracking().FirstOrDefaultAsync(p => p.Id ==  request.Id, cancellationToken);

            if (entity is null) { return null; }

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
