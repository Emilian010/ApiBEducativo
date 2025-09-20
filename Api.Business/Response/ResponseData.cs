using Api.Models.Enums;
using Api.Models.Response;
using Api.Models.Extensors;
using Api.Models.Common;
using Api.Models.User;

namespace Api.Business.Response
{
    public static class ResponseData
    {
        public static ResponseItemDTO<T> ResponseSuccess<T>(T item)
        {
            var response = new ResponseItemDTO<T>();
            if (item != null)
            {
                response.data = item;
            }
            response.Meta = new MetaDTO { Status = StatusType.Success.GetStringValue(), TimeStamp = DateTime.UtcNow.ToString() };

            return response;
        }

        public static ResponseListDTO<T> ResponseSuccess<T>(List<T> listItem)
        {
            var response = new ResponseListDTO<T>();
            if (listItem != null)
            {
                response.data = listItem;
            }
            response.Meta = new MetaDTO { Status = StatusType.Success.GetStringValue(), TimeStamp = DateTime.UtcNow.ToString() };

            return response;
        }

        public static ResponseItemDTO<T> ResponseFailed<T>(IList<ErrorDTO> errorList)
        {
            var response = new ResponseItemDTO<T>();
            if (errorList == null)
            {

                response.Meta = new MetaDTO { Status = StatusType.Failed.GetStringValue() };

            }
            else
            {
                response.Meta = new MetaDTO { Messages = errorList.Select(p => new ErrorDTO { Message = p.Message, Code = p.Message }).ToList(), Status = StatusType.Failed.GetStringValue() };
            }

            return response;

        }

        public static ResponseItemDTO<T> ResponseFailed<T>(ErrorDTO error)
        {
            var response = new ResponseItemDTO<T>();
            if (error == null)
            {

                response.Meta = new MetaDTO { Status = StatusType.Failed.GetStringValue() };

            }
            else
            {
                response.Meta = new MetaDTO { Messages = new List<ErrorDTO> { new ErrorDTO { Message = error.Message, Code = error.Code } }, Status = StatusType.Failed.GetStringValue() };
            }

            return response;

        }

        public static ResponseListDTO<T> ResponseListFailed<T>(ErrorDTO error)
        {
            var response = new ResponseListDTO<T>();
            if (error == null)
            {

                response.Meta = new MetaDTO { Status = StatusType.Failed.GetStringValue() };

            }
            else
            {
                response.Meta = new MetaDTO { Messages = new List<ErrorDTO> { new ErrorDTO { Message = error.Message, Code = error.Code } }, Status = StatusType.Failed.GetStringValue() };
            }

            return response;

        }
    }
}
