using AbmApi.Infraestructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbmApi.Application.Command
{
    public class DeleteProductoHandler : IRequestHandler<DeleteProductoCommand, bool>
    {
        private readonly PostgresDbContext _postgresDbContext;
        private readonly ILogger<DeleteProductoHandler> _logger;
        public DeleteProductoHandler(PostgresDbContext postgresDbContext, ILogger<DeleteProductoHandler> logger)
        {
            _postgresDbContext = postgresDbContext;
            _logger = logger;
        }
        public async Task<bool> Handle(DeleteProductoCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Eliminando producto - ID: {ProductoId}", request.Id);

            var entity = await _postgresDbContext.Productos.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (entity is null)
            {
                _logger.LogWarning("Producto no encontrado para eliminar - ID: {ProductoId}", request.Id);
                return false;
            }

            _postgresDbContext.Productos.Remove(entity);
            await _postgresDbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Producto eliminado - ID: {ProductoId}", request.Id);
            return true;
        }
    }
}
