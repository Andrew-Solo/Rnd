import {redirect, Route, Routes} from "react-router-dom";
import AppContainer from "./sidebar/AppContainer";
import Login from "./account/Login";
import Register from "./account/Register";
import GamesPage from "./games/GamesPage";
import CharactersPage from "./characters/CharactersPage";
import GamePage from "./games/GamePage";
import Member from "./members/Member";
import CharacterPage from "./characters/CharacterPage";
import AccountContainer from "./account/AccountContainer";
import Profile from "./account/Profile";

export default function Router() {
  return (
    <Routes>
      {/*TODO landing on / path*/}
      <Route path="account" element={<AccountContainer/>}>
        <Route index element={<Profile/>}/>
        <Route path="login" element={<Login/>}/>
        <Route path="register" element={<Register/>}/>
      </Route>
      <Route path="/" element={<AppContainer/>}>
        <Route index action={() => redirect("/app")}/>
        <Route path="app">
          <Route path="games">
            <Route index element={<GamesPage/>}/>
            <Route path=":gameName" element={<GamePage/>}/>
          </Route>
          <Route path="members">
            <Route path=":username" element={<Member/>}/>
          </Route>
          <Route path="characters">
            <Route index element={<CharactersPage/>}/>
            <Route path=":characterName" element={<CharacterPage/>}/>
          </Route>
        </Route>
      </Route>
    </Routes>
  );
}