using AbmApi.Api.Response;
using MediatR;

namespace AbmApi.Application.Query
{
    public record GetProductoByIdQuery(int Id) : IRequest<ProductoResponse?>;
}
