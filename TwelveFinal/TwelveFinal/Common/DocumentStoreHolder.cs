//using Microsoft.Extensions.Options;
//using Raven.Client.Documents;

//namespace TwelveFinal
//{

//    public interface IDocumentStoreHolder
//    {
//        IDocumentStore Store { get; }
//    }
//    public class DocumentStoreHolder : IDocumentStoreHolder
//    {

//        public DocumentStoreHolder(IOptions<AppSettings> appSettings)
//        {
//            var settings = appSettings.Value;

//            Store = new DocumentStore
//            {
//                Urls = settings.Urls,
//                Database = settings.DBLog
//            }.Initialize();
//        }

//        public IDocumentStore Store { get; }
//    }
//}
