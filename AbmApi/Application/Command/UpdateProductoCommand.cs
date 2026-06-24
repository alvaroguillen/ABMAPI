using AbmApi.Api.Request;
using AbmApi.Api.Response;
using MediatR;

namespace AbmApi.Application.Command
{
    public class UpdateProductoCommand : IRequest<ProductoResponse?>
    {
        public int Id { get; }

        public UpdateProductoRequest Request { get; }

        public UpdateProductoCommand(int id, UpdateProductoRequest request)
        {
            Id = id;
            Request = request;
        }
    }
}
