import {Box, Stack} from "@mui/material";
import {Outlet} from "react-router-dom";
import Brand from "../ui/Brand";

export default function AccountContainer () {
  return (
    <Box height={1} width={1} display="flex" justifyContent="center">
      <Stack width={350} mt={25}>
        <Box display="flex" justifyContent="center" padding={4}>
          <Brand/>
        </Box>
        <Box display="flex" justifyContent="center" padding={2}>
          <Outlet/>
        </Box>
      </Stack>
    </Box>
  );
}