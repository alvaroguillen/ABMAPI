using AbmApi.Api.Response;
using MediatR;

namespace AbmApi.Application.Query
{
    public class GetProductosQuery: IRequest<IEnumerable<ProductoResponse>>
    {
    }
}
