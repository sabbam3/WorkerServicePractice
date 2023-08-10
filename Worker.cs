namespace WorkerServicePractice
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private HttpClient _httpClient;
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _httpClient = new HttpClient(); 
            return base.StartAsync(cancellationToken);
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _httpClient.Dispose();
            return base.StopAsync(cancellationToken);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = await _httpClient.GetAsync("https://google.com");
                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation("This website is up, StatusCode {StatusCode}", result.StatusCode);
                }
                else _logger.LogInformation("This website is down, StatusCode{StatusCode}", result.StatusCode);
                await Task.Delay(2*1000, stoppingToken);
            }
        }
    }
}