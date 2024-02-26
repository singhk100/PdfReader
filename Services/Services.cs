using PdfReader.Interface;
using PdfReader.Model;
using PdfReader.ViewModel;

namespace PdfReader.Services
{
    public class Services: IService
    {
        readonly IJsonConvert _jsonConvert;


        public Services(JsonConverts jsonConvert)
        {
            _jsonConvert = jsonConvert;
        }
        public string Convert()
        {
            string result = _jsonConvert.Convert();
            return result;
        }
    }
}
