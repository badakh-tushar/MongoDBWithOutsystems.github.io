namespace MongoDBPOCCsharp.Models
{
    public class ResponseEntity<TData>
    {
        public TData? Data { get; set; }


        public List<TData>? DataList { get; set; }
        public bool IsSuccess { get; set; }

        public string? Message { get; set; }
    }
}
