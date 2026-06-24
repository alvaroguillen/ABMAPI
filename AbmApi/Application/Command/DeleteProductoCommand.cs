using AbmApi.Api.Response;
using MediatR;

namespace AbmApi.Application.Command
{
    public class DeleteProductoCommand : IRequest<bool>
    {
        public int Id { get; }

        public DeleteProductoCommand(int id)
        {
            Id = id;
        }
    }
}
