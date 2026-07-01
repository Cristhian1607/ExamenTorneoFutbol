using Supabase;

namespace TorneosFutbolMVC.Services
{
    public static class SupabClient
    {
        private static string _url;
        private static string _key;
        private static Client _client;

        // Se llama desde Program.cs
        public static void Initialize(string url, string key)
        {
            _url = url;
            _key = key;
        }

        public static async Task<Client> GetClient()
        {
            if (_client == null)
            {
                _client = new Client(_url, _key, new SupabaseOptions { AutoConnectRealtime = false });
                await _client.InitializeAsync();
            }
            return _client;
        }
    }
}