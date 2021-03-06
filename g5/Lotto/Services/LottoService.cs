﻿using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Services.Data;

namespace Services
{
    public class LottoService
    {
        public void CreateNewLottoOrganization(string name)
        {
            Lotto newOrganization = new Lotto(name);
            DataHelper.LottoOrganization = newOrganization;
        }

        public Round Draw()
        {
            int activeRoundNumber = DataHelper.LottoOrganization.Rounds.Select(x => x.RoundNumber).Max();
            Round activeRound = DataHelper.LottoOrganization.Rounds.Single(x => x.RoundNumber == activeRoundNumber);
            
            List<int> winingNumbers = GenerateRandomNumbers();

            activeRound.WiningNumbers = winingNumbers;

            CheckForWinningTickets(activeRound);

            CalculatePrice(activeRound);

            GenerateNewRound(activeRoundNumber);

            return activeRound;
        }

        private List<int> GenerateRandomNumbers()
        {
            List<int> numbers = new List<int>();
            Random rnd = new Random();

            for (int i = 0; i < 7; i++)
            {
                int rndNumber = rnd.Next(1, 38);

                if (numbers.Any(x => x == rndNumber))
                {
                    i--;
                    continue;
                }

                numbers.Add(rndNumber);
            }

            return numbers;
        }

        private void CheckForWinningTickets(Round round)
        {
            foreach (var ticket in round.Tickets)
            {
                #region Step by step check
                //int correctNumber = 0;

                //foreach (int number in ticket.Numbers)
                //{
                //    if (winingNumbers.Contains(number))
                //        correctNumber++;
                //}

                //if (correctNumber == 4)
                //{
                //    ticket.Status = TicketStatus.Win4;
                //    //calculate price
                //}
                //else if(correctNumber == 5)
                //{
                //    ticket.Status = TicketStatus.Win5;
                //    //calculate price
                //}
                //else if (correctNumber == 6)
                //{
                //    ticket.Status = TicketStatus.Win6;
                //    //calculate price
                //}
                //else if (correctNumber == 7)
                //{
                //    ticket.Status = TicketStatus.Win7;
                //    //calculate price
                //}
                //else
                //{
                //    ticket.Status = TicketStatus.Lost;
                //}
                #endregion

                List<int> correctlyGuessNumbers = round.WiningNumbers.Intersect(ticket.Numbers).ToList();

                if (correctlyGuessNumbers.Count < 4)
                {
                    ticket.Status = TicketStatus.Lost;
                }
                else
                {
                    ticket.Status = (TicketStatus)correctlyGuessNumbers.Count;
                }
            }
        }

        private void CalculatePrice(Round round)
        {
            foreach (var ticket in round.Tickets.Where(x =>
                x.Status != TicketStatus.InProgress && x.Status != TicketStatus.Lost))
            {
                ticket.Price = ticket.Payment * DataHelper.PriceTable[ticket.Status];

                User user = DataHelper.Users.FirstOrDefault(x => x.Id == ticket.UserId);

                if (user == null)
                {
                    throw new Exception("User not found.");
                }

                user.Balance += ticket.Price;
            }
        }

        private void GenerateNewRound(int previousRoundNumber)
        {
            DataHelper.LottoOrganization.Rounds.Add(new Round(previousRoundNumber + 1));
        }
    }
}
