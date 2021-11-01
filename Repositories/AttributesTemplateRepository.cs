﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopApp.Interfaces;
using ShopApp.Models;
using ShopApp.Data;

namespace ShopApp.Repositories
{
    public class AttributesTemplateRepository : IAttributesTemplateRepository
    {
        private readonly AppDbContext _db;
        private readonly ILogger _logger;

        public AttributesTemplateRepository(AppDbContext appDbContext, ILogger<AttributesTemplateRepository> logger)
        {
            this._db = appDbContext;
            this._logger = logger;
        }
        public async Task<bool> CreateAsync(AttributesTemplate attributesTemplate)
        {
            try
            {
                attributesTemplate.Name = await Task.Run(() => PrepareAttributesTemplateForSaving(attributesTemplate.Name));

                _db.AttributesTemplates.Add(attributesTemplate);
                await _db.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "CreateAsync method error");

                return false;
            }
        }
        public async Task<List<AttributesTemplate>> GetAllAttributesTemplatesAsync()
        {
            try
            {
                return await _db.AttributesTemplates.AsNoTracking().ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "GetAllAttributesTemplates method error");

                List<AttributesTemplate> attributesTemplates = new List<AttributesTemplate>();

                return attributesTemplates;
            }
        }
        public async Task<List<AttributesTemplate>> GetAttributesTemplatesByCategoryIdAsync(int categoryId)
        {
            try
            {
                return await _db.AttributesTemplates.Where(x => x.CategoryId == categoryId).AsNoTracking().ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "GetAttributesTemplatesByCategoryIdAsync method error");

                List<AttributesTemplate> attributesTemplates = new List<AttributesTemplate>();

                return attributesTemplates;
            }
        }

        public async Task<AttributesTemplate> GetAttributesTemplateByIdAsync(int id)
        {
            try
            {
                return await _db.AttributesTemplates.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "GetAttributesTemplateByIdAsync method error");

                return null;
            }
        }
        public async Task<bool> EditAsync(AttributesTemplate attributesTemplate)
        {
            try
            {
                attributesTemplate.Name = await Task.Run(() => PrepareAttributesTemplateForSaving(attributesTemplate.Name));

                _db.AttributesTemplates.Update(attributesTemplate);
                await _db.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "EditAsync method error");

                return false;
            }

        }

        public async Task<bool> DeleteAsync(int Id)
        {
            try
            {
                var attributesTemplate = await _db.AttributesTemplates.FirstOrDefaultAsync(x => x.Id == Id);

                _db.AttributesTemplates.Remove(attributesTemplate);
                await _db.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "DeleteAsync method error");

                return false;
            }
        }

        private static string PrepareAttributesTemplateForSaving(string templateName)
        {
            //Create an array of strings by spaces and remove extra spaces
            string[] stringArray = templateName.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string correctedTemplateName = "";
            char space = ' ';
            //Capitalize each word
            foreach (var item in stringArray)
            {
                if (item.Length > 1)
                {
                    correctedTemplateName += char.ToUpper(item[0]) + item.Substring(1) + " ";
                }
                else
                {
                    correctedTemplateName += item.ToUpper() + " ";
                }
            }
            return correctedTemplateName.TrimEnd(space);
        }

    }
}
