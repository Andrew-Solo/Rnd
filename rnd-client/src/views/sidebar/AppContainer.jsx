import {Outlet} from "react-router-dom";
import Sidebar from "./Sidebar";
import {Box} from "@mui/material";

export default function AppContainer() {
  return (
    <>
      <Sidebar/>
      <Box width={1}>
        <Outlet />
      </Box>
    </>
  );
}