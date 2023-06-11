import {Navigate, Route, Routes} from "react-router-dom";
import AppContainer from "views/sidebar/AppContainer";
import GamesPage from "views/games/GamesPage";
import CharactersPage from "views/characters/CharactersPage";
import GamePage from "views/games/GamePage";
import Member from "views/members/Member";
import CharacterPage from "views/characters/CharacterPage";
import AccountContainer from "views/account/AccountContainer";
import Profile from "views/account/Profile";
import {useStore} from "stores/StoreProvider";

const LoggedRouter = () => {
  const session = useStore().session;

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