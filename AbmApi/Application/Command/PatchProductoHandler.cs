using AbmApi.Api.Response;
using AbmApi.Infraestructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AbmApi.Application.Command
{
    public class PatchProductoHandler : IRequestHandler<PatchProductoCommand, ProductoResponse?>
    {
        private readonly PostgresDbContext _postgresDbContext;
        private readonly ILogger<PatchProductoHandler> _logger;
        public PatchProductoHandler(PostgresDbContext postgresDbContext, ILogger<PatchProductoHandler> logger)
        {
            _postgresDbContext = postgresDbContext;
            _logger = logger;
        }

        public async Task<ProductoResponse?> Handle(PatchProductoCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Activando producto - ID: {ProductoId}", request.Id);
            var entity = await _postgresDbContext.Productos.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (entity == null)
            { 
                _logger.LogWarning("Producto a activar ID {ProductoId} no encontrado.", request.Id);
                return null; 
            }

            entity.Activo = true;
            entity.FechaActualizacion = DateTime.Now;

            await _postgresDbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Producto activado - ID: {ProductoId}", request.Id);
            return new ProductoResponse(
                entity.Id,
                entity.Codigo,
                entity.Nombre,
                entity.Descripcion ?? string.Empty,
                (double)entity.Precio,
                entity.Activo,
                entity.CategoriaId,
                entity.FechaCreacion,
                entity.FechaActualizacion,
                entity.CantidadStock ?? 0
            );
        }
    }
}
