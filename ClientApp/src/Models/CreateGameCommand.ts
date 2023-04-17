import { RockPaperScissors } from "./RockPaperScissors";

export interface CreateGameCommand {
    name: string;
    heroChoice: RockPaperScissors;
}