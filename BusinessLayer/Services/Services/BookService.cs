﻿using BusinessLayer.DTOs;
using BusinessLayer.Services.Interface;
using DataAccessLayer.Entities;
using DataAccessLayer.GenericRepository.Interface;

namespace BusinessLayer.Services.Services
{
    public class BookService : IBookService
    {
        private readonly ILogger<BookService> _logger;
        private readonly IGenericRepository<Book> _repository;
        public BookService(ILogger<BookService> logger, IGenericRepository<Book> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<ServiceResponse> AddBookData(Book book)
        {
            try
            {
                var data=await _repository.AddNewData(book);
                if (data==null)
                {
                    _logger.LogWarning("You can not add this Data");
                    return new ServiceResponse(false, "You can not add this data");
                }
                _logger.LogInformation("Data Inserted Successfully");
                return new ServiceResponse(true, "Data Inserted Successfully");
            }
            catch (Exception ex) {
                _logger.LogError($"{ex.Message}");
                return new ServiceResponse(false, ex.Message);
            }
        }

        public async Task<ServiceResponse> DeleteBookData(int? id)
        {
            try
            {
                var data = await _repository.GetDataById(id);
                if (data==null)
                {
                    _logger.LogWarning($"Data not found with Id {id}");
                    return new ServiceResponse(false, $"Data not found with Id {id}");
                }
                _logger.LogInformation($"{id} deleted");
                return new ServiceResponse(true, $"{id} deleted");
            }
            catch (Exception ex) { 
                _logger.LogError($"{ex.Message}", ex);
                return new ServiceResponse (false,ex.Message);
            }
        }

        public async Task<List<Book>> GetAllBookData()
        {
            try
            {
                var data = await _repository.GetAllData();
                if (!data.Any())
                {
                    _logger.LogWarning("Data not found");
                    throw new Exception("Data not found");
                }
                _logger.LogInformation("Got the data");
                return data;
            }
            catch(Exception ex) {
                _logger.LogError($"{ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        public async Task<Book> GetBookById(int? id)
        {
            try {
                var data = await _repository.GetDataById(id);
                if (data==null)
                {
                    _logger.LogWarning($"Data not found with this Id {id}");
                    throw new Exception($"Data not found with this Id {id}");
                }
                _logger.LogInformation($"Data found with this {id}");
                return data;
            } catch(Exception ex) {
                _logger.LogError($"{ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse> UpdateBookData(int? id, Book book)
        {
            try{
                var data = await _repository.GetDataById(id);
                if (data==null)
                {
                    _logger.LogWarning($"Data is not found with this Id {id}");
                    return new ServiceResponse(false, "Data not found");
                }
                data.Title = book.Title;
                data.Author = book.Author;
                data.Genre=book.Genre;
                data.PublicationYear=book.PublicationYear;
                await _repository.UpdateDate(data);
                return new ServiceResponse(true,"Data updated Successfully");
            } catch (Exception ex) {
                _logger.LogError($"{ex.Message}");
                return new ServiceResponse(false, $"{ex.Message}");
            }
        }
    }
}
