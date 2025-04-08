using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Azure.Search.Documents.Models;
using System.Security.AccessControl;

namespace ChatCommun
{
    public class AzureAISearchClass
    {


        public static readonly Uri searchServiceName = new Uri("<aquí url de ai search>");
        public static readonly string adminApiKey = "<aquí tu llave de admin de ai search>";
        public static readonly string readApiKey = "<aquí tu llave de lectura de ai search>";
        public static readonly string nombreindice = "<nombre del índice>";




        public void CreateIndex(SearchIndexClient idxcl)
        {
            var _otro = new FieldBuilder();
            var _Def = new SearchIndex(nombreindice)
            {
                Fields = _otro.Build(typeof(hotelModel))
            };
            idxcl.CreateIndex(_Def);
        }

        private SearchIndexClient CreateSearchserviceClient(Azure.AzureKeyCredential credencial)
        {
            SearchIndexClient _searchicl = new SearchIndexClient(
                 searchServiceName, credencial
                );
            return _searchicl;
        }

        public void UploadDocument(SearchIndexClient idxcl)
        {
            List<hotelModel> _h = Tools. RecogeArch();
            var action = new List<IndexDocumentsAction<hotelModel>>();
            foreach (hotelModel _ht in _h)
            {
                var _a = IndexDocumentsAction.Upload(_ht);
                action.Add(_a);
            }
            IndexDocumentsBatch<hotelModel> batch = IndexDocumentsBatch.Create(
                    action.ToArray());
            idxcl.GetSearchClient(nombreindice).IndexDocuments(batch);
        }

        public void SearchDocument(SearchIndexClient idxcl, string busqueda)
        {
            var searchClient = idxcl.GetSearchClient(nombreindice);
            var _respuesta = searchClient.Search<hotelModel>(busqueda).Value;
            foreach (Azure.Search.Documents.Models.SearchResult<hotelModel> _val in _respuesta.GetResults())
            {
                Console.WriteLine($"Calificación : {_val.Score} y resultado : {_val.Document.HotelName}");
            }
        }

        public string SearchDocumentOpen( string busqueda)
        {
            SearchIndexClient idxcl = CreateSearchserviceClient(new Azure.AzureKeyCredential(readApiKey));
            string _elresult = "";
            var searchClient = idxcl.GetSearchClient(nombreindice);
            var _respuesta = searchClient.Search<SearchDocument>(busqueda).Value;
            foreach (var _val in _respuesta.GetResults())
            {
                _elresult += _val.Document.ToString() + "\n";
            }
            return _elresult;
        }


    }
}
