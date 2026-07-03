using AbmApi.Api.Response;
using AbmApi.Infraestructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbmApi.Application.Command
{
    public class UpdateProductoHandler : IRequestHandler<UpdateProductoCommand, ProductoResponse?>
    {
        private readonly PostgresDbContext _postgresDbContext;
        private readonly ILogger<UpdateProductoHandler> _logger;
        public UpdateProductoHandler(PostgresDbContext postgresDbContext, ILogger<UpdateProductoHandler> logger)
        {
            _postgresDbContext = postgresDbContext;
            _logger = logger;
        }

        public async Task<ProductoResponse?> Handle(UpdateProductoCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Actualizando producto - ID: {ProductoId}, Código: {Codigo}",request.Id, request.Request.Codigo);

            var entity = await _postgresDbContext.Productos.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if(entity == null) {
                _logger.LogWarning("Producto a actualizar no encontrado - ID: {ProductoId}", request.Id);
                return null;
            }

            entity.Codigo = request.Request.Codigo;
            entity.Nombre = request.Request.Nombre;
            entity.Descripcion = request.Request.Descripcion;
            entity.Precio = (decimal)request.Request.Precio;
            entity.Activo = request.Request.Activo;
            entity.CategoriaId = request.Request.CategoriaId;
            entity.CantidadStock = request.Request.CantidadStock;
            entity.FechaActualizacion = DateTime.Now;

            await _postgresDbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Producto actualizado - ID: {ProductoId}, Código: {Codigo}", request.Id, request.Request.Codigo);

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
