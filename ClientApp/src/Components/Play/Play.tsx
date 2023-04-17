import React from 'react';
import { RockPaperScissors } from '../../Models/RockPaperScissors';
import { User } from '../../Models/User';
import { GameResult } from '../../Models/GameResult';
import { Config } from '../../config';

export const Play: React.FC = () => {

    const [name, setName] = React.useState<string | undefined>(undefined);
    const [playerChoice, setPlayerChoice] = React.useState<string | undefined>(undefined);
    // const [wins, setWins] = React.useState<number>(0);
    // const [losses, setLosses] = React.useState<number>(0);
    // const [ties, setTies] = React.useState<number>(0);
    // const [totalGames, setTotalGames] = React.useState<number>(0);
    // const [score, setScore] = React.useState<number>(0);
    const [nameChange, setNameChange] = React.useState<boolean>(false);
    // const [lastVillainChoice, setLastVillainChoice] = React.useState<RockPaperScissors | undefined>(undefined);
    // const [lastHeroChoice, setLastHeroChoice] = React.useState<RockPaperScissors | undefined>(undefined);
    // const [lastResult, setLastResult] = React.useState<GameResult | undefined>(undefined);

    const[lastGameText, setLastGameText] = React.useState<string | undefined>(undefined);

    function nameChanged(name: string): void{
        setName(name);
        setNameChange(true);
    }

    async function createGame(heroChoice: RockPaperScissors): Promise<void> {
        setNameChange(false);

        await fetch(Config.apiBaseUrl + "game", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                name: name,
                heroChoice: heroChoice,
            }),
        });

        await getUser();
    }

    async function getUser(): Promise<void> {
        const response = await fetch(Config.apiBaseUrl + "game/" + name, {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
            },
        })

        const user: User = await response.json() as User;
        updateLastGameText(user);

        // setWins(user.wins);
        // setLosses(user.losses);
        // setTies(user.ties);
        // setTotalGames(user.totalGames);
        // setScore(user.score);
        // setLastHeroChoice(user.lastGameHeroChoice);
        // setLastVillainChoice(user.lastGameVillainChoice);
        // setLastResult(user.lastGameResult);
    }

    function updateLastGameText(user: User){

        let text = 'Last Game: '
        if(user?.lastGameResult === GameResult.win){
            text+= 'You won! '
        }else if(user?.lastGameResult === GameResult.loss){
            text+= 'You lost! '
        }else if(user?.lastGameResult === GameResult.tie){
            text+= 'You tied! '
        }

        if(user?.lastGameHeroChoice === RockPaperScissors.Rock){
            text += 'You selected Rock, '
        }

        if(user?.lastGameHeroChoice === RockPaperScissors.Paper){
            text += 'You selected Paper, '
        }

        if(user?.lastGameHeroChoice === RockPaperScissors.Scissors){
            text += 'You selected Scissors, '
        }

        if(user?.lastGameVillainChoice === RockPaperScissors.Rock){
            text += 'the villain selected Rock.'
        }
        
        if(user?.lastGameVillainChoice === RockPaperScissors.Paper){
            text += 'the villain selected Paper.'
        }

        if(user?.lastGameVillainChoice === RockPaperScissors.Scissors){
            text += 'the villain selected Paper.'
        }

        text += ' Wins: ' + user?.wins + ' Losses: ' + user?.losses + ' Ties: ' + user?.ties + ' Total Games: ' + user?.totalGames + ' Score: ' + user?.score;

        setLastGameText(text);
    }

    return (
        <>
        <div className="row">
            <div className="col-12">
                <div className="form-group">
                    <label htmlFor="nameField">Label for Input Field</label>
                    <div className="input-group has-validation">
                    <input 
                        type="text" 
                        className="form-control" 
                        id="nameField" 
                        placeholder="Enter your name"
                        onChange={(e) => {
                            nameChanged(e.target.value);
                            }} />
                    </div>
                </div>
            </div>
        </div>
        {name &&
            <div className="row">
                <button type="button" className="btn btn-primary col" onClick={(e) => createGame(RockPaperScissors.Rock)}>Rock</button>
                <button type="button" className="btn btn-primary col" onClick={(e) => createGame(RockPaperScissors.Paper)}>Paper</button>
                <button type="button" className="btn btn-primary col" onClick={(e) => createGame(RockPaperScissors.Scissors)}>Scissors</button>
            </div>}
        <br />
        {name && !nameChange && 
            <div className="row">
                <p>{lastGameText}</p>
            </div>}
        </>
    );
};

export default Play;