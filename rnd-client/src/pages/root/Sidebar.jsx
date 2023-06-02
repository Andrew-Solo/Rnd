import {Box} from "@mui/material";
import Brand from "../../components/Brand";
import Account from "./components/Account";
import CurrentGame from "./components/CurrentGame";
import Navigation from "./components/Navigation";

export default function Sidebar () {
  // TODO  Add border color from Colors
  return (
    <Box width="350px" display="flex" flexDirection="column" justifyContent="space-between" borderRight="1px solid rgba(255, 255, 255, 0.6)">
      <Box display="flex" flexDirection="column">
        <CurrentGame/>
        <Navigation/>
      </Box>
      <Box display="flex" flexDirection="column">
        <Brand/>
        <Account/>
      </Box>
    </Box>
  );
}