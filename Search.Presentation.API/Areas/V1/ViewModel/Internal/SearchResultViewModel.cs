namespace Search.Presentation.API.Areas.V1.ViewModel.Internal
{
    public class SearchResultViewModel
    {
        public long Id { get; set; }

        public string Type { get; set; }

        public double Score { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Description { get; set; }

        public string Link { get; set; }

    }
}
