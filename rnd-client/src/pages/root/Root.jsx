import {Box} from "@mui/material";
import {Outlet} from "react-router-dom";
import Header from "./Header";
import Sidebar from "./Sidebar";

export default function Root() {
  return (
    <>
      <Sidebar/>
      <Box width={1} padding={4} gap={4} display="flex" flexDirection="column">
        <Header/>
        <Box component="main">
          <Outlet />
        </Box>
      </Box>
    </>
  );
}