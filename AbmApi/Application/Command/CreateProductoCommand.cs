using AbmApi.Api.Request;
using AbmApi.Api.Response;
using MediatR;

namespace AbmApi.Application.Command
{
    public class CreateProductoCommand : IRequest<ProductoResponse>
    {
        public CreateProductoRequest Request { get; }
        public CreateProductoCommand(CreateProductoRequest request)
        {
            Request = request;
        }
    }
}
