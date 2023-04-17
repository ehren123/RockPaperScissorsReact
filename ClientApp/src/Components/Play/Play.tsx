import React, { useState } from 'react';
import { RockPaperScissors } from '../../Models/RockPaperScissors';
import { User } from '../../Models/User';
import { GameResult } from '../../Models/GameResult';
import { Config } from '../../Config';
import { CreateGameCommand } from '../../Models/CreateGameCommand';

const Play: React.FC = () => {

    const [name, setName] = useState<string | undefined>(undefined);
    const [nameChange, setNameChange] = useState<boolean>(false);
    const [lastGameText, setLastGameText] = useState<string | undefined>(undefined);

    function nameChanged(name: string): void{
        setName(name);
        setNameChange(true);
    }

    async function createGame(heroChoice: RockPaperScissors): Promise<void> {
        setNameChange(false);

        if(!name){
            throw new Error("Name is required");
        }

        const command: CreateGameCommand = {
            name: name,
            heroChoice: heroChoice
        }

        await fetch(Config.apiBaseUrl + "game", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(command)
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
    }

    function updateLastGameText(user: User): void{
        let text = 'Last Game: '
        if(user?.lastGameResult === GameResult.win){
            text+= 'You won! '
        }else if(user?.lastGameResult === GameResult.loss){
            text+= 'You lost! '
        }else if(user?.lastGameResult === GameResult.tie){
            text+= 'You tied! '
        }

        if(user?.lastGameHeroChoice === RockPaperScissors.Rock){
            text += 'You selected rock, '
        }

        if(user?.lastGameHeroChoice === RockPaperScissors.Paper){
            text += 'You selected paper, '
        }

        if(user?.lastGameHeroChoice === RockPaperScissors.Scissors){
            text += 'You selected scissors, '
        }

        if(user?.lastGameVillainChoice === RockPaperScissors.Rock){
            text += 'villain selected rock.'
        }
        
        if(user?.lastGameVillainChoice === RockPaperScissors.Paper){
            text += 'villain selected paper.'
        }

        if(user?.lastGameVillainChoice === RockPaperScissors.Scissors){
            text += 'villain selected scissors.'
        }

        text += ' Wins: ' + user?.wins + ' Losses: ' + user?.losses + ' Ties: ' + user?.ties + ' Total Games: ' + user?.totalGames + ' Score: ' + user?.score;

        setLastGameText(text);
    }

    return (
        <>
        <div className="row">
            <div className="col-12">
                <div className="form-group">
                    <label htmlFor="nameField">Enter your name</label>
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
                <button type="button" className="btn btn-primary col" onClick={() => createGame(RockPaperScissors.Rock)}>Rock</button>
                <button type="button" className="btn btn-primary col" onClick={() => createGame(RockPaperScissors.Paper)}>Paper</button>
                <button type="button" className="btn btn-primary col" onClick={() => createGame(RockPaperScissors.Scissors)}>Scissors</button>
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
