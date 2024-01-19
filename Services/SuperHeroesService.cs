using Microsoft.EntityFrameworkCore;
using SuperHeroApi.Data;
using SuperHeroApi.Dto;
using SuperHeroApi.Entities;

namespace SuperHeroApi.Services
{
    public class SuperHeroesService
    {
        private readonly AppDataContext _dbContext;

        public SuperHeroesService(AppDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        //  FETCH ALL SUPERHEROES
        public async Task<List<SuperHero>> FindAll()
        {
            return await _dbContext.SuperHeroes.ToListAsync();
        }

        // FECTH A SINGLE SUPERHERO BY ID IF EXITES, ELSE RETURN NULL
        public async Task<SuperHero?> FindById(int heroId)
        {
            return await _dbContext.SuperHeroes.FindAsync(heroId);
        }

        // REMOVE A SINGLE SUPERHERO BY ID IF EXISTS, ELSE DO NOTHING
        public async Task<int?> Remove(int id)
        {
            var superHero = await FindById(id);

            if (superHero is null) return null;

            _dbContext.SuperHeroes.Remove(superHero);
            return await _dbContext.SaveChangesAsync();

        }

        // CREATE A NEW SUPERHERO    
        public async Task Create(SuperHeroDTO request)
        {
            var superHero = new SuperHero()
            {
                Name = request.Name,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Place = request.Place,
            };

            _dbContext.SuperHeroes.Add(superHero);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int?> Update(int superHeroId, SuperHeroDTO request)
        {
            var superHero = await FindById(superHeroId);

            if (superHero is null) return null;

            superHero.Name = request.Name;
            superHero.FirstName = request.FirstName;
            superHero.LastName = request.LastName;
            superHero.Place = request.Place;

            _dbContext.SuperHeroes.Attach(superHero);

            return await _dbContext.SaveChangesAsync();
        }
    }
}