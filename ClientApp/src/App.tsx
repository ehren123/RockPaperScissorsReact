import './App.css';
import { Routes, Route } from "react-router-dom";
import Nav from './Components/Nav/Nav';
import Play from './Components/Play/Play';
import Leaderboard from './Components/Leaderboard/Leaderboard';


function App() {
  return (
    <>
      <div className="container-fluid">
        <Nav />
        <Routes>
          <Route path="/" element={<h1>Click play to play or leaderboard to view the leaderboard</h1>} />
          <Route path="/play" element={<Play />} />
          <Route path="/leaderboard" element={<Leaderboard />} />
        </Routes>
      </div>
    </>
  );
}

export default App;
