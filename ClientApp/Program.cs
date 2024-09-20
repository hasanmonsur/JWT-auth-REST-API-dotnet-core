// See https://aka.ms/new-console-template for more information
using System.Net.Http.Json;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text;
using ClientApp;
using Newtonsoft.Json;



        HttpClient _httpClient = new HttpClient();

        var token = "";
        // The login API endpoint
        var urllogin = "https://localhost:44336/api/Auth/login";

        // Login credentials
        var loginModel = new
        {
            Username = "user",
            Password = "password"
        };

        var json = JsonConvert.SerializeObject(loginModel);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Post request to get the JWT token
        var response = await _httpClient.PostAsync(urllogin, content);

        if (response.IsSuccessStatusCode)
        {
            var responseString = await response.Content.ReadAsStringAsync();

            // Assuming the token is in the response body like { "token": "your-jwt-token" }
            dynamic tokenData = JsonConvert.DeserializeObject(responseString);
          token=tokenData.token;

        }

        _httpClient = new HttpClient();
        string apiUrl = "https://localhost:44336/api/Products";

        // Add the JWT token in the Authorization header
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        // Get all products
        var products = await _httpClient.GetFromJsonAsync<Product[]>(apiUrl);
        foreach (var product in products)
        {
            Console.WriteLine($"{product.Id}: {product.Name} - ${product.Price}");
        }

        // Add a new product
        var newProduct = new Product { Name = "New Product", Price = 19.99M };
        response = await _httpClient.PostAsJsonAsync(apiUrl, newProduct);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Product added successfully.");
        }

        // Get a product by id
        var productById = await _httpClient.GetFromJsonAsync<Product>($"{apiUrl}/1");
        Console.WriteLine($"{productById.Id}: {productById.Name} - ${productById.Price}");

        // Update a product
        var updateProduct = new Product { Id = 1, Name = "Updated Product", Price = 24.99M };
        await _httpClient.PutAsJsonAsync($"{apiUrl}/1", updateProduct);

        // Delete a product
        await _httpClient.DeleteAsync($"{apiUrl}/2");
 

