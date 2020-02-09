using System.IO;
using System.Linq;
using Google.Protobuf;
using Google.Cloud.Vision.V1;
using Google.Cloud.Storage.V1;
using System.Collections.Generic;
using etraducao.Models.Interfaces;
using etraducao.Models.Configuration;
using Microsoft.Extensions.Configuration;

namespace etraducao.Models.Services
{
    public class LeitorDePdf : ILeitorDePdfService
    {
        private readonly string _projectId;

        public LeitorDePdf(IConfiguration configuration)
        {
            _projectId = configuration["GOOGLE_PROJECT_ID"];
        }

        public int Executar(string nomeDoArquivo, MemoryStream arquivo)
        {
            var bucketName = BucketName.RandomName();

            var outputPrefix = "";
            var gcsSourceURI = $"gs://{bucketName}/{nomeDoArquivo}";

            SortedDictionary<string, SortedSet<string>> _garbage = new SortedDictionary<string, SortedSet<string>>();
            StorageClient _storage = StorageClient.Create();
            _storage.CreateBucket(_projectId, bucketName);

            _storage.UploadObject(bucketName, nomeDoArquivo, "application/pdf", arquivo);

            SortedSet<string> objectNames;
            if (!_garbage.TryGetValue(bucketName, out objectNames))
            {
                objectNames = _garbage[bucketName] = new SortedSet<string>();
            }
            objectNames.Add(nomeDoArquivo);

            var client = ImageAnnotatorClient.Create();

            var asyncRequest = new AsyncAnnotateFileRequest
            {
                InputConfig = new InputConfig
                {
                    GcsSource = new GcsSource
                    {
                        Uri = gcsSourceURI
                    },
                    MimeType = "application/pdf"
                },
                OutputConfig = new OutputConfig
                {
                    // How many pages should be grouped into each json output file.
                    BatchSize = 100,
                    GcsDestination = new GcsDestination
                    {
                        Uri = $"gs://{bucketName}/{outputPrefix}"
                    }
                }
            };

            asyncRequest.Features.Add(new Feature
            {
                Type = Feature.Types.Type.DocumentTextDetection
            });

            List<AsyncAnnotateFileRequest> requests = new List<AsyncAnnotateFileRequest>
            {
                asyncRequest
            };

            var operation = client.AsyncBatchAnnotateFiles(requests);

            operation.PollUntilCompleted();

            var blobList = _storage.ListObjects(bucketName, outputPrefix);
            var output = blobList.Where(x => x.Name.Contains(".json")).First();

            var jsonString = "";
            using (var stream = new MemoryStream())
            {
                _storage.DownloadObject(output, stream);
                jsonString = System.Text.Encoding.UTF8.GetString(stream.ToArray());
            }


            var response = JsonParser.Default.Parse<AnnotateFileResponse>(jsonString);

            int total = 0;
            for (int i = 0; i < response.Responses.Count; i++)
            {

                var pageResponses = response.Responses[i];
                if (pageResponses != null)
                {
                    var annotation = pageResponses.FullTextAnnotation;
                    var conteudo = annotation.Text.Replace("\n", " ");
                    var remocaoDosEspacos = conteudo.Split(' ');

                    foreach (var item in remocaoDosEspacos)
                        total += item.Length;
                }
            }

            RemoverArquivos(bucketName);

            return total;
        }

        public void RemoverArquivos(string bucketName)
        {
            var storageClient = StorageClient.Create();
            var blobList = storageClient.ListObjects(bucketName, "");
            foreach (var outputFile in blobList.Where(x => x.Name.Contains(".json")).Select(x => x.Name))
            {
                storageClient.DeleteObject(bucketName, outputFile);
            }
        }
    }
}
