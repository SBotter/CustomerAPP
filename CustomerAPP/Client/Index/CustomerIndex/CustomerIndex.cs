global using Lucene.Net.Analysis;
global using Lucene.Net.Documents;
global using Lucene.Net.Index;
global using Lucene.Net.QueryParsers.Classic;
global using Lucene.Net.Search;
global using Lucene.Net.Store;
global using Lucene.Net.Util;
global using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents.Extensions;

namespace CustomerAPP.Client.Index.CustomerIndex
{
    #region lucene
    /*
    public class CustomerIndex<T> : ICustomerIndex where T : class
    {
        private const LuceneVersion version = LuceneVersion.LUCENE_48;
        private readonly StandardAnalyzer _analyzer;
        private readonly IndexWriter _indexWriter;
        private readonly IndexSearcher _indexSearcher;
        private readonly Lucene.Net.QueryParsers.Classic.QueryParser _queryParser;

        public CustomerIndex()
        {
            var directory = FSDirectory.Open("Index");
            _analyzer = new StandardAnalyzer(version);
            var indexConfig = new IndexWriterConfig(version, _analyzer);
            _indexWriter = new IndexWriter(directory, indexConfig);
            _indexSearcher = new IndexSearcher(_indexWriter.GetReader(applyAllDeletes: true));
            _queryParser = new QueryParser(version, "content", _analyzer);
        }

        public sealed class LuceneAction : SmartEnum<LuceneAction>
        {
            public static readonly LuceneAction Create = new(nameof(Create), 1);
            public static readonly LuceneAction Update = new(nameof(Update), 2);
            public static readonly LuceneAction Delete = new(nameof(Delete), 3);
            private LuceneAction(string name, int value) : base(name, value)
            {
            }
        }

        public Dictionary<string, List<Document>> GetData(List<string> data)
        {
            var propertyIndex = new Dictionary<string, List<Document>>();
            foreach (var dummy in data.GetType().GetProperties())
            {
                foreach (var property in data.GetType().GetProperties())
                {
                    if (!propertyIndex.ContainsKey(property.Name))
                        propertyIndex.Add(property.Name, new List<Document>());
                    var value = property.GetValue(data, null);
                    if (value is null) continue;
                    var document = new Document
                    { new StringField(property.Name, value.ToString(), Field.Store.YES) };
                    propertyIndex[property.Name].Add(document);
                }
            }
            return propertyIndex;
        }

        public void Index(List<string> data, string options)
        {
            if (data != null)
            {
                var document = GetData(data);
                var docs = document.SelectMany(item
                        => item.Value.Select(doc
                            => new TextField(item.Key, doc.ToString(), Field.Store.YES)))
                    .Select(field => new Document { field })
                    .ToList();
                switch (options)
                {
                    case nameof(LuceneAction.Create):
                        _indexWriter.AddDocuments(docs);
                        break;
                    case nameof(LuceneAction.Update):
                        _indexWriter.UpdateDocuments(new Term("id", "1"), docs);
                        break;
                    case nameof(LuceneAction.Delete):
                        _indexWriter.DeleteDocuments(new Term("id", "1"));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(options), options, null);
                }
                _indexWriter.Flush(triggerMerge: false, applyAllDeletes: false);
            }
        }
        public bool IsExistIndex(T item)
        {
            var parser = new QueryParser(LuceneVersion.LUCENE_48, "id", _analyzer);
            var query = parser.Parse(item.ToString());
            var hits = _indexSearcher.Search(query, 1).ScoreDocs;
            return hits.Length > 0;
        }
        public IEnumerable<Document> Search(string query, int maxResults)
        {
            var fuzzyQuery = new FuzzyQuery(new Term("content", query), 2);
            var queryParser = _queryParser.Parse(query);
            var booleanQuery = new BooleanQuery
        {
            { queryParser, Occur.SHOULD },
            { fuzzyQuery, Occur.SHOULD }
        };
            var hits = _indexSearcher.Search(booleanQuery, maxResults).ScoreDocs;
            foreach (var hit in hits)
                yield return _indexSearcher.Doc(hit.Doc);
        }
        public void ClearAll() => _indexWriter.DeleteAll();
    }
    */
    #endregion

    public class CustomerIndex : ICustomerIndex
    {
        private const LuceneVersion version = LuceneVersion.LUCENE_48;
        private readonly StandardAnalyzer _analyzer;
        private readonly IndexWriter _writer;
        private readonly RAMDirectory _directory;
        //private FSDirectory _directory;


        public CustomerIndex() {

            //string projectPath = System.IO.Directory.GetCurrentDirectory();
            string projectPath = Environment.CurrentDirectory;
            string indexFolderName = "indexFolder";
            string indexPath = Path.Combine(projectPath, indexFolderName);
            string pathTest = AppDomain.CurrentDomain.BaseDirectory;
            indexPath = $"C:/Users/michele/source/repos/CustomerAPP/CustomerAPP/Client/indexFolder";

            _analyzer = new StandardAnalyzer(version);
            //_directory = FSDirectory.Open(indexPath);
            _directory = new RAMDirectory();
            
            var config = new IndexWriterConfig(version, _analyzer);
            try
            {
                if (_writer == null)
                {
                    _writer = new IndexWriter(_directory, config);
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

        }

        public void AddCustomerToIndex(List<Customer> customers)
        {
            /*
            var lockFilePath = Path.Combine(_directory.Directory.FullName, "write.lock");

            if (File.Exists(lockFilePath))
            {
                try
                {
                    File.Delete(lockFilePath);
                }
                catch (Exception ex)
                {
                    var error = "Failed to clear write.lock: " + ex.Message;
                }
            }
            */
            
            
            foreach (var customer in customers)
            {
                var document = new Document();
                document.Add(new TextField("Id", customer.Id.ToString(), Field.Store.YES));
                document.Add(new TextField("FirstName", customer.FirstName, Field.Store.YES));
                document.Add(new TextField("LastName", customer.LastName, Field.Store.YES));
                document.Add(new TextField("Email", customer.Email, Field.Store.YES));
                _writer.AddDocument(document);
            }
            _writer.Commit();
            //_writer.Dispose();
            

        }


        public List<Customer> Search(string searchTerm)
        {
            var searchCustomers = new List<Customer>();
            try
            {
                var directoryReader = DirectoryReader.Open(_directory);
                var indexSearcher = new IndexSearcher(directoryReader);

                string[] fields = { "FirstName", "LastName", "Email" };
                var queryParser = new MultiFieldQueryParser(version, fields, _analyzer);
                var query = queryParser.Parse(searchTerm);

                var hits = indexSearcher.Search(query, 10).ScoreDocs;

            
                foreach (var hit in hits)
                {
                    var document = indexSearcher.Doc(hit.Doc);
                    searchCustomers.Add(new Customer()
                    {
                        FirstName = document.Get("FirstName"),
                        LastName = document.Get("LastName"),
                        Email = document.Get("Email")
                    });
                }

            
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }

            return searchCustomers;
            
        }

    }


}
