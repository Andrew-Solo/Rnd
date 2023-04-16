import {Route, Routes} from "react-router-dom";
import Root from "./pages/root/Root";
import {Typography} from "@mui/material";

export default function App () {
  return (
    <Routes>
      <Route path="/" element={<Root/>}>
        <Route index element={<Typography variant="h2">Hi there!</Typography>}/>
      </Route>
    </Routes>
  );
}