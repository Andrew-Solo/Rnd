import {Box} from "@mui/material";
import {Outlet} from "react-router-dom";
import Header from "./Header";
import Sidebar from "./Sidebar";

export default function Root() {
  return (
    <>
      <Sidebar/>
      <Box width="100%" padding="32px" display="flex" gap="32px" flexDirection="column">
        <Header/>
        <Box component="main">
          <Outlet />
        </Box>
      </Box>
    </>
  );
}