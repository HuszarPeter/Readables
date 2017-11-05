using System;
using System.Net.Http;
using System.Threading.Tasks;
using Readables.Domain;
using Newtonsoft.Json;
using System.Linq;
using System.Threading;

namespace Readables.Import.Metadata.Moly
{
    public class MolyMetadataScraperService: IMetadataScraperService
    {
        private string molyApiKey = "55f59e22279134469219915552df7d49";

        public ReadableMetadata ScrapeMetadataAsync(Readable readable)
        {
            var searchTask = Task.Factory
                                 .StartNew(() => { return SearchByTitleAsync(readable.Title).Result; })
                                 .ContinueWith((a) => { return GetBookDetails(a.Result.id).Result;});
            Task.WaitAll(searchTask);
            var res = searchTask.Result;
            return new ReadableMetadata
            {
                Description = res.description,
                Subjects = res.tags.Select(t => t.name).ToArray(),
                Title = res.title
            };
        }


        private async Task<MolySearchBook> SearchByTitleAsync(string title) {
            using(var httpClient = new HttpClient()) {
                using (var searchResult = await httpClient.GetAsync($"http://moly.hu/api/books.json?q={title}&key={molyApiKey}"))
                {
					var content = await searchResult.Content.ReadAsStringAsync();
                    var molyResult = JsonConvert.DeserializeObject<MolySearchResult>(content);
                    return molyResult.books.FirstOrDefault();
                }
            }
        }

        private async Task<MolyBookDetails> GetBookDetails(long bookId) {
            using (var httpClient = new HttpClient()) {
                using (var detailsResult = await httpClient.GetAsync($"http://moly.hu/api/book/{bookId}.json?key={molyApiKey}")) {
                    var content = await detailsResult.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<MolyBookDetailsResult>(content).book;
                }
            }
        }

        private struct MolySearchResult
        {
            public MolySearchBook[] books { get; set; }
        }

        private struct MolySearchBook
        {
            public long id { get; set; }
            public string author { get; set; }
            public string title { get; set; }
        }

        private struct MolyBookDetailsResult
        {
            public MolyBookDetails book { get; set; }
        }

        private struct MolyBookDetails
        {
            public long id { get; set; }
            public string title { get; set; }
            public string subtitle { get; set; }
            public string cover { get; set; }
            public string description { get; set; }
            public string url { get; set; }
            public MolyBookDetailsTag[] tags { get; set; }
        }

        private struct MolyBookDetailsTag
        {
            public long id { get; set; }
            public string name { get; set; }
        }
    }
}
