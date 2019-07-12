using System;
using System.Collections.Generic;
using MMM_Bracket.API.Domain.Models;
using MMM_Bracket.API.Domain.Repositories;

namespace MMM_Bracket.API.Services
{
    public class BracketService
    {
        public enum Round
        {
            WILD_CARD,
            ONE,
            SWEET_SIXTEEN,
            ELITE_TRAIT,
            FINAL_ROAR,
            SEMIFINAL
        }

        private IParticipantRepository _participantRepository; 

        public BracketService(IParticipantRepository participantRepository)
        {
            //TODO: will need battle repository to store newly generated battle objects

            //TODO: will need participant repository to store correctly seeded participant objects
            _participantRepository = participantRepository;
        }

        public void CreateAllBattlesForRound(Round round)
        {
            int roundNumberAsInt = (int)round;
            int totalBattlesInRound = GetTotalBattlesForRound(round);
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

            //TODO: save new battles to db
        }


        /* The rule is that if the highest-numbered seed is N, where N is a power of 2, 
         * then the seed numbered k plays the seed numbered N - k + 1 in the initial matchup. 
         * (For example, seed k = 4 plays seed 16 - 4 + 1 = 13 in a 16-seed matchup.)
         */
        private void SeedParticipants(int year)
        {
            //TODO:
            //get all animals for current year
            //sort by category and seed
            //for each category of sorted animals:
                //create new participant objects by seed with formula in above comment
                //save participants
        }

        private int GetTotalBattlesForRound(Round round)
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
