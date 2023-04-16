import {Typography} from "@mui/material";
import {Outlet} from "react-router-dom";

export default function Root() {
  return (
    <div>
      <Typography variant="h1">
        Hello, World!
      </Typography>
      <div>
        <Outlet />
      </div>
    </div>
  );
}