﻿using DiplomskiPokusaj1.Model;
using DiplomskiPokusaj1.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.Repository
{
    public class AuthorRepository : IAuthorRepository
    {

        DBContext databaseContext;

        public AuthorRepository(DBContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<Author> Create(Author author)
        {
            Author newAuthor = new Author
            {
                Id = Guid.NewGuid().ToString(),
                Firstname = author.Firstname,
                Lastname = author.Lastname,
                Biography = author.Biography,
                CreatedAt = DateTime.Now,
                ImageId = author.ImageId,
                Image = author.Image
            };

            var trackedEntity = await databaseContext.Authors.AddAsync(newAuthor);

            await databaseContext.SaveChangesAsync();

            return trackedEntity.Entity;
        }

        public async Task<bool> Delete(string id)
        {
            var entityToDelete = await Get(id);
            if (entityToDelete == null)
            {
                return false;
            }
            entityToDelete.DeletedAt = DateTime.Now;
            databaseContext.Authors.Update(entityToDelete);
            await databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<Author> Get(string id)
        {
            return await databaseContext.Authors
                .Include(author => author.Image)
                .Where(author => author.Id == id && author.DeletedAt == null)
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<Author>> GetAll()
        {   
                return await databaseContext.Authors
                .Include(author => author.Image)
                .Where(author => author.DeletedAt == null)
                .ToListAsync();
            
            
        }

        public async Task<Author> Update(string id, Author author)
        {
            var entityToUpdate = await Get(id);
            if (entityToUpdate == null)
            {
                return null;
            }

            entityToUpdate.Firstname = author.Firstname;
            entityToUpdate.Lastname = author.Lastname;
            entityToUpdate.Biography = author.Biography;
            entityToUpdate.ImageId = author.ImageId;
            entityToUpdate.UpdatedAt = DateTime.Now;

            var trackedEntity = databaseContext.Authors.Update(entityToUpdate);
            await databaseContext.SaveChangesAsync();
            return trackedEntity.Entity;
        }
    }
}
