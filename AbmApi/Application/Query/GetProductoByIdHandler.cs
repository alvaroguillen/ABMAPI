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
        private readonly ILogger<GetProductoByIdHandler> _logger;
        public GetProductoByIdHandler(PostgresDbContext postgresDbContext, ILogger<GetProductoByIdHandler> logger)
        {
            _postgresDbContext = postgresDbContext;
            _logger = logger;
        }

        public async Task<ProductoResponse?> Handle(GetProductoByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Buscando producto por ID: {ProductoId}", request.Id);

            var entity = await _postgresDbContext.Productos.AsNoTracking().FirstOrDefaultAsync(p => p.Id ==  request.Id, cancellationToken);

            if (entity is null) { _logger.LogWarning("Producto con ID {ProductoId} no encontrado", request.Id); return null; }

            _logger.LogInformation("Producto con ID {ProductoId} encontrado - Código: {Codigo}", request.Id, entity.Codigo);

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
