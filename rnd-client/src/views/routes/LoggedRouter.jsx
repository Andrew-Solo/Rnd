import {Navigate, Route, Routes} from "react-router-dom";
import AccountContainer from "../account/AccountContainer";
import Profile from "../account/Profile";
import AppContainer from "../sidebar/AppContainer";
import GamesPage from "../games/GamesPage";
import GamePage from "../games/GamePage";
import Member from "../members/Member";
import CharactersPage from "../characters/CharactersPage";
import CharacterPage from "../characters/CharacterPage";

const LoggedRouter = () => {
  return (
    <Routes>
      {/*TODO landing on / path*/}
      <Route index element={<Navigate to="/app/games"/>}/>
      <Route path="account" element={<AccountContainer/>}>
        <Route index element={<Profile/>}/>
        <Route path="signout"/>
      </Route>
      <Route path="app" element={<AppContainer/>}>
        <Route index element={<Navigate to="/app/games"/>}/>
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
    </Routes>
  );
};

export default LoggedRouter