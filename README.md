# Football Manager

## Description
The Football Manager is a WPF application with .NET 6.0 and it's object oriented. It is a simulation game where you are the manager and have your own squad. The app provides different operations that you can complete as manager of your team.

## General
The game works with a rating system. The manager has a rating (not in use), the teams have a rating and each player has a rating. On your team, the rating gets determined by the player ratings average. The simulation and the price calculation are all depenedent and based on the rating number.

## Functions
The first and obvious function is to simulate games. A random opponent gets picked with a random rating which you have to play. Depending on the rating the probability of the winning team is determined by random chance, where the team with a higher rating has a higher chance. Next you can look at stats and information about your players and your team (e.g. Goals, Wins, etc.). You can Manage your team meaning substituting players from the bench into the starting eleven and vice versa. You can upgrade your players' ratings, where some players have the attribute 'multiugrade' which is marked by a Ω. There are tiers of upgrades and the players marked with Ω can upgrade up to 10 ratings at once for a price. Other than that theres a shop where you can buy players and packs. There are different packs From Common to Legendary. You have either 5 or 6 players in one pack, but the packs aren't that cheap. You can also sell your players, but for less than buying them.

## Price Calculation
The price of the players is calculated with a power function, so that the price grows exponentially from the lowest rating (58) to the highest (99).  
!()
