import {Box} from "@mui/material";
import GameBanner from "./banners/GameBanner";
import Navigation from "./navigation/Navigation";
import AccountBanner from "./banners/AccountBanner";
import Brand from "../ui/Brand";

export default function Sidebar () {
  // TODO  Add border color from Colors
  return (
    <Box width={350} display="flex" flexDirection="column" justifyContent="space-between" borderRight="1px solid rgba(255, 255, 255, 0.6)">
      <Box display="flex" flexDirection="column">
        <GameBanner/>
        <Navigation/>
      </Box>
      <Box display="flex" flexDirection="column">
        <Brand/>
        <AccountBanner/>
      </Box>
    </Box>
  );
}