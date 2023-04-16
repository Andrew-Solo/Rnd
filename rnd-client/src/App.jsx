import {Route, Routes} from "react-router-dom";
import Root from "./pages/root/Root";
import Login from "./pages/account/Login";
import Register from "./pages/account/Register";
import Games from "./pages/games/Games";
import Characters from "./pages/characters/Characters";
import Game from "./pages/games/Game";
import Member from "./pages/members/Member";
import Character from "./pages/characters/Character";

export default function App () {
  return (
    <div className="app">
      <Routes>
        <Route path="/" element={<Root/>}>
          <Route index/>
          <Route path="account">
            <Route path="login" element={<Login/>}/>
            <Route path="register" element={<Register/>}/>
          </Route>
          <Route path="app">
            <Route path="games" element={<Games/>}>
              <Route path=":gameName" element={<Game/>}/>
            </Route>
            <Route path="members">
              <Route path=":username" element={<Member/>}/>
            </Route>
            <Route path="characters" element={<Characters/>}>
              <Route path=":characterName" element={<Character/>}/>
            </Route>
          </Route>
        </Route>
      </Routes>
    </div>
  );
}