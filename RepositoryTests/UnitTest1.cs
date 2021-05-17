using Microsoft.Data.SqlClient;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using VZM.Data;
using VZM.Entities;

namespace RepositoryTests
{
    [TestFixture]
    public class Tests
    {
        public SqlConnection sqlConnonnection = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = TestDb; Integrated Security = True");
        private Product _product = new();
        private List<User> _users = new();
        private ProductRepository _productRepository;

        [SetUp]
        public void Setup()
        {
            _productRepository = new(sqlConnonnection);

            _product.Title = "Title1";
            _product.MetaTitle = "MetaTitle1";
            _product.Price = 100;
            _product.Description = "Description1";
            _product.DescriptionShort = "DescriptionShort1";
            _product.SellerId = Guid.Parse("2E94E9A5-21A4-4466-BCB2-33C9E1559E46");
            
        }

        [Test]
        public void Test1()
        {
            var actualProduct = _productRepository.GetProductsByName("Title1").FirstOrDefault();
            Assert.AreEqual(_product.Title, actualProduct.Title);
            Assert.AreEqual(_product.Price, actualProduct.Price);
            Assert.AreEqual(_product.MetaTitle, actualProduct.MetaTitle);
            Assert.AreEqual(_product.Description, actualProduct.Description);
            Assert.AreEqual(_product.DescriptionShort, actualProduct.DescriptionShort);
        }

        [Test]
        public void Test2()
        {
            var actualProduct = _product;
            actualProduct.Description = "DescriptionChanged";
            actualProduct.Title = "Title4";
            actualProduct.CreatedAt = DateTime.Now;
            actualProduct.SellerId = Guid.Parse("2E94E9A5-21A4-4466-BCB2-33C9E1559E46");
            actualProduct.ImageUrl = "f";

            _productRepository.SaveProduct(actualProduct);
            actualProduct = _productRepository.GetProductsByName("Title4").FirstOrDefault();
            _product.ProductId = new Guid(actualProduct.ProductId.ToString());

            Assert.AreEqual(_product.Title, actualProduct.Title);
            Assert.AreEqual(_product.Price, actualProduct.Price);
            Assert.AreEqual(_product.MetaTitle, actualProduct.MetaTitle);
            Assert.AreEqual(_product.Description, actualProduct.Description);
            Assert.AreEqual(_product.DescriptionShort, actualProduct.DescriptionShort);
        }

        [Test]
        public void Test3()
        {
            _productRepository.DeleteProduct(_product.ProductId);

            var actualProduct = _productRepository.GetProductsByName("Title4").FirstOrDefault();
            Assert.AreEqual(null, actualProduct);
        }
    }
}