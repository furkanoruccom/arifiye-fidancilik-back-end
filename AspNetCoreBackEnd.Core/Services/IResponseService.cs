using AspNetCoreBackEnd.Core.Response;

namespace AspNetCoreBackEnd.Core.Services
{
    public interface IResponseService
    {
        ResponseAjax HandleSuccess(string message);
        ResponseAjax HandleSuccessData(string message, object data = null);
        ResponseAjax HandleError(string message);
    }
}
