import React from 'react';
import { RockPaperScissors } from "../../Models/RockPaperScissors";

export const Play: React.FC = () => {

    const [name, setName] = React.useState<string | undefined>(undefined);
    const [playerChoice, setPlayerChoice] = React.useState<string | undefined>(undefined);
    const [wins, setWins] = React.useState<number>(0);
    const [losses, setLosses] = React.useState<number>(0);
    const [ties, setTies] = React.useState<number>(0);
    const [totalGames, setTotalGames] = React.useState<number>(0);
    const [score, setScore] = React.useState<number>(0);
    const [nameChange, setNameChange] = React.useState<boolean>(false);

    function createGame(heroChoice: RockPaperScissors): void{
        fetch(process.env.API_BASE_URL + "/game", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                name: name,
                heroChoice: playerChoice,
            }),
        }).then((res) => res.json())
            .then((res) => {
                console.log(res);
            })
            .catch((err) => {
                console.log(err);
            });
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
                        placeholder="Enter value"
                        onChange={(e) => {
                            setNameChange(true);
                            setName(e.target.value)
                            }} />
                    </div>
                </div>
            </div>
            {name &&
            <div className='col-12'>
                <button type="button" className="btn btn-primary" onClick={(e) => createGame(RockPaperScissors.Rock)}>Rock</button>
                <button type="button" className="btn btn-primary" onClick={(e) => createGame(RockPaperScissors.Paper)}>Paper</button>
                <button type="button" className="btn btn-primary" onClick={(e) => createGame(RockPaperScissors.Scissors)}>Scissors</button>
            </div>            }
        </div>
        </>
    );
};

export default Play;