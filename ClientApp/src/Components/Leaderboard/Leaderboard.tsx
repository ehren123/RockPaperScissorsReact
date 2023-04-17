import React, { useState, useEffect } from 'react';
import { User } from '../../Models/User';
import { Config } from '../../Config';

const Leaderboard: React.FC = () => {

    const [users, setUsers] = useState<User[]>([]);

    useEffect(() => {
        const fetchData = async () => {
            const response = await fetch(Config.apiBaseUrl + "leaderboard", {
                method: "GET",
                headers: {
                    "Content-Type": "application/json",
                },
            })
            const users: User[] = await response.json() as User[];
            setUsers(users);
        }

        fetchData();
    })

    return (
        <>
            <h1>Leaderboard</h1>
            <div className="row">
                <ul className="list-group">
                    {users.map((user: User, index: number) => (<li className="list-group-item">Rank: {index + 1} Name: {user.name}, Score: {user.score}, Games played: {user.totalGames}</li>))}
                </ul>
            </div>
        </>
    )
}

export default Leaderboard