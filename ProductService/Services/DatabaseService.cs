using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ProductService;

namespace ProductService.Services;

public class DatabaseService
{
    private readonly IMongoCollection<Product> productCollection;

    public DatabaseService(
        IOptions<DatabaseSettings> productDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            productDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            productDatabaseSettings.Value.DatabaseName);

        productCollection = mongoDatabase.GetCollection<Product>(
            productDatabaseSettings.Value.ProductCollectionName);



    }

    public async Task<List<Product>> GetAsync() =>
        await productCollection.Find(_ => true).ToListAsync();

    public async Task<Product?> GetAsync(string id) =>
        await productCollection.Find(x => x.id == id).FirstOrDefaultAsync();

    public async Task<List<Product>> GetBySentAsync(int sent=0) =>
        await productCollection.Find(x => x.sent > sent).ToListAsync();
   
    public async Task CreateAsync(Product product) =>
        await productCollection.InsertOneAsync(product);

    public async Task UpdateAsync(string id, Product updatedProduct) =>
        await productCollection.ReplaceOneAsync(x => x.id == id, updatedProduct);

    public async Task RemoveAsync(string id) =>
        await productCollection.DeleteOneAsync(x => x.id == id);

    public async Task<List<Product>> GetByNameAsync(string name)
    {
        var filter = new BsonDocument { { "name", new BsonDocument { { "$regex", name }, { "$options", "i" } } } };
       // var filterLower = new BsonDocument { { "name", new BsonDocument { { "$regex", name.ToLower() }, { "$options", "i" } } } };
        var product = await productCollection.Find(filter).ToListAsync();
       // var productLower = await productCollection.Find(filterLower).ToListAsync();
        return product.ToList();
    }

    public async Task DropAll() =>
        await productCollection.DeleteManyAsync(x => x.name != " ");

}