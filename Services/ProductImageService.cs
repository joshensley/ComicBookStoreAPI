using ComicBookStoreAPI.Models;
using ComicBookStoreAPI.Repositories;
using ComicBookStoreAPI.Repositories.DTO;
using ComicBookStoreAPI.Utilities;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Services
{
    public class ProductImageService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IProductImageRepository _productImageRepository;

        public ProductImageService(
            IWebHostEnvironment environment, 
            IProductImageRepository productImageRepository)
        {
            _environment = environment;
            _productImageRepository = productImageRepository;
        }

        public async Task<ActionResult<ProductImageDTO>> GetProductImageById(int id)
        {
            return await _productImageRepository.GetById(id, ProductImageDTO.ProductImageSelector);
        }

        public async Task<ActionResult<List<ProductImageDTO>>> GetProductImages(int productId)
        {
            // Get product images by product ID
            var productImages = (await _productImageRepository.GetByProductId(productId, ProductImageDTO.ProductImageSelector)).Value;

            var auth = new FirebaseAuthProvider(new FirebaseConfig(FirebaseKeys.apiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(FirebaseKeys.AuthEmail, FirebaseKeys.AuthPassword);

            var authProductImages = new List<ProductImageDTO>();
            foreach (var item in productImages)
            {
                var task = new FirebaseStorage(
                    FirebaseKeys.Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    })
                    .Child("images")
                    .Child($"{item.ImageTitle}")
                    .GetDownloadUrlAsync().Result;

                item.AuthorizedImageURL = task;
                authProductImages.Add(item);
            }

            return authProductImages;
        }

        public async Task<ActionResult<ProductImageDTO>> PostProductImage(ProductImage productImage)
        {
            FileStream fs;
            var fileUpload = productImage.FileUpload;
            string folderName = "firebaseImages";
            string fileName = Guid.NewGuid() + "_" + fileUpload.FileName;
            string filePath = Path.Combine(_environment.WebRootPath, $"images/{folderName}", fileName);
            string fileImagePath = Path.Combine(_environment.ContentRootPath, $"wwwroot/images/{folderName}/", fileName);

            try
            {
                // Upload file to static image
                using (var fileStream = new FileStream(fileImagePath, FileMode.Create))
                {
                    await fileUpload.CopyToAsync(fileStream);
                }

                // Upload static image to Firebase
                fs = new FileStream(filePath, FileMode.Open);
                var auth = new FirebaseAuthProvider(new FirebaseConfig(FirebaseKeys.apiKey));
                var a = await auth.SignInWithEmailAndPasswordAsync(FirebaseKeys.AuthEmail, FirebaseKeys.AuthPassword);
                var cancellation = new CancellationTokenSource();

                var upload = new FirebaseStorage(
                    FirebaseKeys.Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    })
                    .Child("images")
                    .Child($"{fileName}")
                    .PutAsync(fs, cancellation.Token);

                // Upload to firebase
                await upload;

                productImage.AlternateText = fileUpload.FileName;
                productImage.ImageTitle = fileName;
                productImage.CreatedAt = DateTime.Now;
                productImage.UpdatedAt = DateTime.Now;

                // Add to Database
                await _productImageRepository.Post(productImage);

                // Close file and Delete image in wwwroot
                fs.Close();
                File.Delete(filePath);

                // Get product image dto by id
                var returnProductImage = (await _productImageRepository.GetById(
                    productImage.ID, 
                    ProductImageDTO.ProductImageSelector)).Value;

                var task = new FirebaseStorage(
                    FirebaseKeys.Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    })
                    .Child("images")
                    .Child($"{returnProductImage.ImageTitle}")
                    .GetDownloadUrlAsync().Result;

                returnProductImage.AuthorizedImageURL = task;

                return returnProductImage;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ActionResult<int>> DeleteProductImage(int id, ProductImageDTO productImage)
        {

            // Delete image in firebase
            var auth = new FirebaseAuthProvider(new FirebaseConfig(FirebaseKeys.apiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(FirebaseKeys.AuthEmail, FirebaseKeys.AuthPassword);
            var cancellation = new CancellationTokenSource();

            var delete = new FirebaseStorage(
                FirebaseKeys.Bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                })
                .Child("images")
                .Child($"{productImage.ImageTitle}")
                .DeleteAsync();

            await delete;

            // Delete image in database
            return await _productImageRepository.Delete(id);
        }
    }
}
