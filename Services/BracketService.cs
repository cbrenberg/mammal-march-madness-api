using System;
using System.Collections.Generic;
using System.Linq;
using MMM_Bracket.API.Domain.Models;
using MMM_Bracket.API.Domain.Services;

namespace MMM_Bracket.API.Services
{
    public class BracketService
    {
        public enum Round
        {
            WILD_CARD,
            ONE,
            TWO,
            SWEET_SIXTEEN,
            ELITE_TRAIT,
            FINAL_ROAR,
            SEMIFINAL
        }

        private IParticipantService _participantService;
        private ICategoryService _categoryService;

        public BracketService(IParticipantService participantService, ICategoryService categoryService)
        {
            //TODO: will need battle service to store newly generated battle objects

            _participantService = participantService;
            _categoryService = categoryService;
        }

        public void CreateBattlesForRound(Round round)
        {
            int roundNumberAsInt = (int)round;
            int totalBattlesInRound = GetNumberOfBattlesForRound(round);
            int numberOfCategories = CalculateNumberOfCategoriesRemainingInRound(round);

            int battlesPerCategory = totalBattlesInRound / numberOfCategories;

            List<Battle> battles = new List<Battle>();


            //create new battles for current round, add to list
            for (var i = 0; i < numberOfCategories; i++)
            {
                for (var j = 0; j < battlesPerCategory; j++)
                {
                    var newBattle = new Battle(roundNumberAsInt);

                    battles.Add(newBattle);
                }

            }

            //TODO: get winner participants from previous round, add correctly seeded participants to battles, save to db
        }


        /* The rule is that if the highest-numbered seed is N, where N is a power of 2, 
         * then the seed numbered k plays the seed numbered N - k + 1 in the initial matchup. 
         * (For example, seed k = 4 plays seed 16 - 4 + 1 = 13 in a 16-seed matchup.)
         */

        //TODO: generalize this method, or create helper method to do actual seeding, maybe using index?
        private async void SeedBattles(int year)
        {
            //get all categories for current year
            IEnumerable<Category> currentYearCategories = await _categoryService.GetAllCategoriesByYear(year);

            //for each category:
            foreach(Category category in currentYearCategories)
            {
                //get list of animals
                ICollection<Animal> animalsList = category.Animals;

                int numberOfAnimals = animalsList.Count(); //TODO: add .Where clause to get number remaining in current round

                //for each animal in category:
                foreach(Animal animal in animalsList)
                {
                    //find the correct competitor
                    //TODO: make this a separate private helper method.
                    //TODO: verify this algorithm past round 2
                    Animal competitor = animalsList.FirstOrDefault(c =>
                    {
                        return Animal.
                    });

                    //TODO: create new participant object, add to List?

                    //remove assignees from list
                    animalsList.Remove(competitor);
                    animalsList.Remove(animal);


                }
            }
            //new participants require animalId and battleId
            //TODO: how to get battleId of newly created battles? can entity framework track these? Or can battles/animals be assigned as objects?

            //save participants
        }

        //TODO: method that accepts list of winners and reseeds next round

        private int GetNumberOfBattlesForRound(Round round)
        {
            if (round == Round.SEMIFINAL || round == Round.WILD_CARD)
            {
                return 2;
            }

            return (int)(64 / Math.Pow(2d, (double)round));
        }


        private int CalculateNumberOfCategoriesRemainingInRound(Round round)
        {
            switch (round)
            {
                case Round.WILD_CARD:
                    return 1;
                case Round.SEMIFINAL:
                    return 2;
                default:
                    return 4;
            }
        }
    }
}
