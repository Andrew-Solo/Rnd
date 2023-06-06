import {Route, Routes} from "react-router-dom";
import Root from "./pages/root/Root";
import Login from "./pages/account/Login";
import Register from "./pages/account/Register";
import Games from "./pages/games/Games";
import Characters from "./pages/characters/Characters";
import Game from "./pages/games/Game";
import Member from "./pages/members/Member";
import Character from "./pages/characters/Character";
import {Box, CssBaseline, ThemeProvider} from "@mui/material";
import {Theme} from "./theme";
import AccountRoot from "./pages/account/AccountRoot";
import Profile from "./pages/account/Profile";

export default function App () {
  return (
    <ThemeProvider theme={Theme}>
      <CssBaseline/>
      <Box className="app">
        <Routes>
          <Route index element={<Register/>}/>
          <Route path="account" element={<AccountRoot/>}>
            <Route index element={<Profile/>}/>
            <Route path="login" element={<Login/>}/>
            <Route path="register" element={<Register/>}/>
          </Route>
          <Route path="/" element={<Root/>}>
            <Route index/>
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
      </Box>
    </ThemeProvider>
  );
}