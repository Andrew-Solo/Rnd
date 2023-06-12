import {Box, Typography} from "@mui/material";
import {Done} from "../../../icons";

export default function ActiveGame () {
  return (
    <Box height={170} padding={1} gap={1} ml={-2} display="flex" justifyContent="center" alignItems="center" sx={{background: "rgba(255, 255, 255, 0.2)"}}>
      <Done color="primary" sx={{width: 50, height: 50}}/>
      <Typography variant="h3" color="primary">
        Активна
      </Typography>
    </Box>
  );
}