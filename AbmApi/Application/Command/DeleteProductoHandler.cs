using AbmApi.Infraestructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AbmApi.Application.Command
{
    public class DeleteProductoHandler : IRequestHandler<DeleteProductoCommand, bool>
    {
        private readonly PostgresDbContext _postgresDbContext;
        public DeleteProductoHandler(PostgresDbContext postgresDbContext)
        {
            _postgresDbContext = postgresDbContext;
        }
        public async Task<bool> Handle(DeleteProductoCommand request, CancellationToken cancellationToken)
        {
            var entity = await _postgresDbContext.Productos.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (entity is null) return false;

            _postgresDbContext.Productos.Remove(entity);
            await _postgresDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
