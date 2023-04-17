import { GameResult } from "./GameResult";
import { RockPaperScissors } from "./RockPaperScissors";

export interface User {
    name: string;
    wins: number;
    losses: number;
    ties: number;
    totalGames: number;
    score: number;
    lastGameHeroChoice?: RockPaperScissors;
    lastGameVillainChoice?: RockPaperScissors;
    lastGameResult?: GameResult;
}